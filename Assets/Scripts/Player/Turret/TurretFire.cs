using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PaperTank
{
    [RequireComponent(typeof(TurretRotator))]
    public class TurretFire : MonoBehaviour
    {
        [SerializeField] private GameObject _shell;
        [SerializeField] private LineRenderer _trajectory;
        [SerializeField] private float _shellSpeed = 10f;
        [SerializeField] private float _fireCool = 1f;
        [SerializeField] private float _parabolaHeight = 4f;

        public Shell.MovementType ShellMovement
        {
            get
            {
                return _shellMovement;
            }
            set
            {
                _shellMovement = value;

                switch (value)
                {
                    case Shell.MovementType.Straight:
                        _continueFireWhilePressed = true;
                        break;
                    case Shell.MovementType.Parabola:
                        _continueFireWhilePressed = false;
                        break;
                }
            }
        }

        private Shell.MovementType _shellMovement;
        private TurretRotator _turret;
        private bool _isCooldown;
        private bool _firePressed;
        private bool _continueFireWhilePressed;
        private bool _drawTrajectory;

        private void Awake()
        {
            _turret = GetComponent<TurretRotator>();
        }

        private void Start()
        {
            ShellMovement = Shell.MovementType.Parabola;
        }

        private void Update()
        {
            // do not draw trajectory when cooldown
            if (_isCooldown) return;

            if (_firePressed)
            {
                var initPos = transform.position + Vector3.up * 0.25f;
                var rotation = Quaternion.Euler(_turret.TurretRotate);

                var shell = Instantiate(_shell, initPos, rotation).GetComponent<Shell>();
                shell.ShellMovement = ShellMovement;
                shell.Speed = _shellSpeed;
                shell.EndPoint = _turret.TargetPoint;
                shell.ParabolaHeight = _parabolaHeight;

                StartCoroutine(Cooldown());

                if (!_continueFireWhilePressed) _firePressed = false;
            }

            if (_drawTrajectory)
            {
                var points = Movement.SimulateParabolaMove(transform.position, _turret.TargetPoint, 4f);
                _trajectory.positionCount = points.Count;

                for (int i = 0; i < points.Count; i++)
                {
                    _trajectory.SetPosition(i, points[i]);
                }
            }
            else
            {
                _trajectory.positionCount = 0;
            }
        }

        public void OnFireShell(InputAction.CallbackContext context)
        {
            switch (ShellMovement)
            {
                case Shell.MovementType.Straight:
                    OnFireShellByStraight(context);
                    break;
                case Shell.MovementType.Parabola:
                    OnFireShellByParabola(context);
                    break;
            }
        }

        private void OnFireShellByStraight(InputAction.CallbackContext context)
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

        private void OnFireShellByParabola(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    _firePressed = false;
                    _drawTrajectory = true;
                    break;
                case InputActionPhase.Canceled:
                    _firePressed = true;
                    _drawTrajectory = false;
                    break;
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
