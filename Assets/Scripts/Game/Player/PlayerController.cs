using System.Collections;

using PaperTank.Game.Behaviour.Tank;

using UnityEngine;
using UnityEngine.InputSystem;

namespace PaperTank.Game.Player
{
    public class PlayerController : TankBehaviour
    {
        [SerializeField] private float _moveSpeed;

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
            yield return new WaitForSeconds(Turret.WeaponSystem.Cooldown);
            _isCooldown = false;
        }
    }
}
