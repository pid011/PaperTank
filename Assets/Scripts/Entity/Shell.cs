using System.Collections;
using UnityEngine;

namespace PaperTank
{
    public class Shell : MonoBehaviour
    {
        public enum MovementType
        {
            Straight, Parabola
        }

        [SerializeField] private GameObject _explosion;
        [SerializeField] private float _minDistance = 10f;

        public MovementType ShellMovement { get; set; }
        public float Speed { get; set; }
        public Vector3 EndPoint { get; set; }
        public float ParabolaHeight { get; set; } = 4f;

        private Rigidbody _rigidbody;
        private Vector3 _startPos;
        private Vector3 _previousPos;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _startPos = transform.position;
            _rigidbody.useGravity = false;

            switch (ShellMovement)
            {
                case MovementType.Straight:
                    StartCoroutine(StraightMove());
                    break;
                case MovementType.Parabola:
                    StartCoroutine(Movement.ParabolaMove(transform, transform.position, EndPoint, ParabolaHeight));
                    break;
            }

            _previousPos = transform.position;
        }

        private void FixedUpdate()
        {
            // 이전 위치와 현재 위치 차
            var diff = transform.position - _previousPos;
            // 현재 위치를 기준으로 이전 위치의 반대 벡터를 구함
            var look = transform.position + diff;
            // 구한 벡터로 바라보기
            transform.LookAt(look);

            // 이전 위치 갱신
            _previousPos = transform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player")) return;
            Explosion();
        }

        private IEnumerator StraightMove()
        {
            var waitForFixedUpdate = new WaitForFixedUpdate();
            _rigidbody.AddRelativeForce(Vector3.forward * Speed, ForceMode.Impulse);

            while (Vector3.Distance(_startPos, transform.position) <= _minDistance)
            {
                yield return waitForFixedUpdate;
            }
            Explosion();
        }

        private void Explosion()
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
