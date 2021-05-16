using UnityEngine;
using UnityEngine.InputSystem;

namespace PaperTank
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private SpriteRenderer _playerSprite;

        private Rigidbody _rigidbody;
        private float _horizontal;
        private float _vertical;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (_horizontal < 0) _playerSprite.flipX = true;
            if (_horizontal > 0) _playerSprite.flipX = false;
        }

        private void FixedUpdate()
        {
            var horizontalMove = _horizontal * _moveSpeed;
            var verticalMove = _vertical * _moveSpeed;

            _rigidbody.velocity = new Vector3(horizontalMove, _rigidbody.velocity.y, verticalMove);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            var input = context.ReadValue<Vector2>();

            _horizontal = input.x;
            _vertical = input.y;
        }
    }
}
