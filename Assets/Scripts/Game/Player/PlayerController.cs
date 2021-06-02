using System.Collections;

using PaperTank.Game.Behaviour.Tank;

using UnityEngine;
using UnityEngine.InputSystem;

namespace PaperTank.Game.Player
{
    public class PlayerController : TankBehaviour
    {
        [SerializeField] private float _moveSpeed;
        private bool _firePressed;

        // move
        private float _horizontal;

        // fire
        private bool _isCooldown;

        // rotate
        private Vector2 _mousePosition;
        private float _vertical;

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

            var rayFromCam = Camera.main.ScreenPointToRay(mousePos);
            if (Physics.Raycast(rayFromCam, out var hit, 1000f))
            {
                turret.Rotator.targetPoint = hit.point;

                Debug.DrawLine(Camera.main.transform.position, turret.Rotator.targetPoint, Color.red);
            }
        }

        private void DoFire()
        {
            // do not draw trajectory when cooldown
            if (_isCooldown) return;

            if (_firePressed)
            {
                turret.WeaponSystem.Fire("Player");
                StartCoroutine(Cooldown());
            }
        }

        private IEnumerator Cooldown()
        {
            _isCooldown = true;
            yield return new WaitForSeconds(turret.WeaponSystem.cooldown);
            _isCooldown = false;
        }
    }
}
