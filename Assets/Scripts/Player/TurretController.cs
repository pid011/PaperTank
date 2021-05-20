using UnityEngine;
using UnityEngine.InputSystem;

namespace PaperTank
{
    public class TurretController : MonoBehaviour
    {
        public Vector3 TurretRotate => transform.eulerAngles;

        private Vector2 _mousePosition;

        private void Update()
        {
            Vector3 objPos = transform.position;
            Vector3 mousePos = _mousePosition;
            mousePos.z = Vector3.Distance(objPos, Camera.main.transform.position);

            //Vector3 target = Camera.main.ScreenToWorldPoint(mousePos);
            //Debug.DrawLine(Camera.main.transform.position, target, Color.red);

            Ray rayFromCam = Camera.main.ScreenPointToRay(mousePos);

            if (Physics.Raycast(rayFromCam, out RaycastHit hit))
            {
                Vector3 targetPos = hit.point;
                Debug.DrawLine(Camera.main.transform.position, targetPos, Color.red);

                float dy = targetPos.z - objPos.z;
                float dx = targetPos.x - objPos.x;

                float rotateDeg = Mathf.Atan2(dy, -dx) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, rotateDeg - 90f, 0f);
            }
        }

        public void OnRotateTurret(InputAction.CallbackContext context)
        {
            _mousePosition = context.ReadValue<Vector2>();
            // Debug.Log(_mousePosition);
        }
    }
}
