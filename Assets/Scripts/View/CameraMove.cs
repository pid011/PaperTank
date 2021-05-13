using UnityEngine;

namespace PaperTank
{
    public class CameraMove : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 2.0f;
        [SerializeField] private Transform _target;
        [SerializeField] private int _height;
        [SerializeField] private Vector2 _offset;

        private void Update()
        {
            var move = _target.position + Vector3.up * _height + new Vector3(_offset.x, 0, _offset.y);
            transform.position = Vector3.Lerp(transform.position, move, _moveSpeed * Time.deltaTime);
        }
    }
}
