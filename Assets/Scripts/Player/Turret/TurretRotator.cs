using UnityEngine;
using UnityEngine.InputSystem;

namespace PaperTank
{
    public class TurretRotator : MonoBehaviour
    {
        public Vector3 TurretRotate => transform.eulerAngles;
        public Vector3 TargetPoint => _targetPoint;

        private Vector2 _mousePosition;
        private Vector3 _targetPoint;

        private void Update()
        {
            RotateTurret();
        }

        public void OnRotateTurret(InputAction.CallbackContext context)
        {
            _mousePosition = context.ReadValue<Vector2>();
        }

        private void RotateTurret()
        {
            Vector3 objPos = transform.position;
            Vector3 mousePos = _mousePosition;
            mousePos.z = Vector3.Distance(objPos, Camera.main.transform.position);

            Ray rayFromCam = Camera.main.ScreenPointToRay(mousePos);
            if (Physics.Raycast(rayFromCam, out RaycastHit hit, maxDistance: 100f, layerMask: 1 << 8)) // Raycast with floor
            {
                _targetPoint = hit.point;
                Debug.DrawLine(Camera.main.transform.position, _targetPoint, Color.red);

                float dy = _targetPoint.z - objPos.z;
                float dx = _targetPoint.x - objPos.x;

                float rotateDeg = Mathf.Atan2(dy, -dx) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, rotateDeg - 90f, 0f);
            }
        }
    }
}
