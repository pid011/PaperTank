using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PaperTank
{
    public class PlayerController : TankBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _fireCool = 2f;
        [SerializeField] private BoxCollider _wall;

        private Vector3 _previousPosition;

        // rotate
        private Vector2 _mousePosition;

        // move
        private float _horizontal;
        private float _vertical;

        // fire
        private bool _isCooldown;
        private bool _firePressed;

        protected override void Update()
        {
            base.Update();

            _previousPosition = transform.position;
            transform.Translate(_moveSpeed * Time.deltaTime * new Vector3(_horizontal, 0f, _vertical));

            //RotateSprite();
            RotateTurret();
            DoFire();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            var input = context.ReadValue<Vector2>();

            _horizontal = input.x;
            _vertical = input.y;
        }

        public void OnRotateTurret(InputAction.CallbackContext context)
        {
            _mousePosition = context.ReadValue<Vector2>();
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    _firePressed = true;
                    break;
                case InputActionPhase.Canceled:
                    _firePressed = false;
                    break;
            }
        }

        //private void RotateSprite()
        //{
        //    var spriteAngle = TankSprite.transform.eulerAngles;
        //    var cameraAngle = Camera.main.transform.eulerAngles;

        //    var angle = new Vector3(cameraAngle.x, spriteAngle.y, spriteAngle.z);
        //    TankSprite.transform.eulerAngles = angle;
        //}

        private void RotateTurret()
        {
            Vector3 mousePos = _mousePosition;
            mousePos.z = Camera.main.farClipPlane;

            Ray rayFromCam = Camera.main.ScreenPointToRay(mousePos);
            if (Physics.Raycast(rayFromCam, out RaycastHit hit, maxDistance: 1000f))
            {
                Turret.Rotator.TargetPoint = hit.point;

                Debug.DrawLine(Camera.main.transform.position, Turret.Rotator.TargetPoint, Color.red);
            }
        }

        private void DoFire()
        {
            // do not draw trajectory when cooldown
            if (_isCooldown) return;

            if (_firePressed)
            {
                Turret.WeaponSystem.Fire("Player");
                StartCoroutine(Cooldown());
            }
        }

        private IEnumerator Cooldown()
        {
            _isCooldown = true;
            yield return new WaitForSeconds(_fireCool);
            _isCooldown = false;
        }
    }
}
