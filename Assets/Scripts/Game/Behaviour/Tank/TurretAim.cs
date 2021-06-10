using UnityEngine;

namespace PaperTank.Game.Behaviour.Tank
{
    public class TurretAim : MonoBehaviour
    {
        public Vector3 angle => transform.localEulerAngles;

        public Vector3 targetPoint
        {
            get => _targetPoint;
            set
            {
                _targetPoint = value;

                var self = transform.position;

                var dy = value.z - self.z;
                var dx = value.x - self.x;

                var rotateDeg = Mathf.Atan2(dy, -dx) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, rotateDeg - 90f, 0f);
            }
        }

        private Vector3 _targetPoint;
    }
}
