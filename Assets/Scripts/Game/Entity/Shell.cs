using System.Collections;
using PaperTank.Game.BehaviourMovement;
using UnityEngine;

namespace PaperTank.Game.Entity
{
    public class Shell : MonoBehaviour
    {
        public enum MovementType
        {
            Straight, Parabola
        }

        [SerializeField] private GameObject _explosion;

        public MovementType shellMovement { get; set; }
        public float speed { get; set; }
        public Vector3 endPoint { get; set; }
        public float parabolaHeight { get; set; } = 5f;
        public string tagToDoNotCollide { get; set; }

        private Rigidbody _rigidbody;
        private Vector3 _previousPos;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _rigidbody.useGravity = false;

            switch (shellMovement)
            {
                case MovementType.Straight:
                    StartCoroutine(StraightMove());
                    break;
                case MovementType.Parabola:
                    StartCoroutine(Movement.ParabolaMove(transform, transform.position, endPoint, parabolaHeight));
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
            // 90 degree
            transform.eulerAngles += new Vector3(90, 0, 0);

            // 이전 위치 갱신
            _previousPos = transform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            // do not explode when collide tag of TagToDoNotCollide
            if (!string.IsNullOrWhiteSpace(tagToDoNotCollide) && other.CompareTag(tagToDoNotCollide))
            {
                return;
            }

            // do not explode when collide with projectile
            if (other.CompareTag("Projectile"))
            {
                return;
            }

            Explosion();
        }

        private IEnumerator StraightMove()
        {
            _rigidbody.AddRelativeForce(Vector3.forward * speed, ForceMode.Impulse);
            yield return new WaitForSeconds(1f);
            Explosion();
        }

        private void Explosion()
        {
            Instantiate(_explosion, _previousPos, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
