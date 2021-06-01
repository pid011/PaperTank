using UnityEngine;

namespace PaperTank.Game.Behaviour.Tank
{
    public class TurretAim : MonoBehaviour
    {
        public Vector3 Angle => transform.localEulerAngles;
        public Vector3 TargetPoint
        {
            get => _targetPoint;
            set
            {
                _targetPoint = value;

                var self = transform.position;

                float dy = value.z - self.z;
                float dx = value.x - self.x;

                float rotateDeg = Mathf.Atan2(dy, -dx) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, rotateDeg - 90f, 0f);
            }
        }

        private Vector3 _targetPoint;
    }
}
