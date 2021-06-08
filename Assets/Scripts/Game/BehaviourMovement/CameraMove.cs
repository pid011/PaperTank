using UnityEngine;

namespace PaperTank.Game.BehaviourMovement
{
    [ExecuteInEditMode]
    public class CameraMove : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offset;

        private void Start()
        {
            transform.LookAt(_target.transform);
        }

        private void Update()
        {
            if (_target == null) return;

            var move = _target.position + _offset;
            transform.position = move;
        }
    }
}
