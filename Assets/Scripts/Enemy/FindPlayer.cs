using System.Collections;
using UnityEngine;

namespace PaperTank
{
    [RequireComponent(typeof(Collider))]
    public class FindPlayer : StateBehaviour
    {
        [SerializeField] private float _findRadius = 10f;

        public Transform Target { get; private set; }

        protected override IEnumerator OnEnter()
        {
            Debug.Log("FindPlayer: OnEnter");
            yield break;
        }

        protected override IEnumerator OnExecute()
        {
            Debug.Log("FindPlayer: OnExecute");
            var wait = new WaitForFixedUpdate();

            while (true)
            {
                var colliders = Physics.OverlapSphere(transform.position, _findRadius, 1 << 10); // Behaviour layer

                foreach (var collider in colliders)
                {
                    if (collider.CompareTag("Player"))
                    {
                        Target = collider.transform;
                        NextState = typeof(FireToTarget);
                        yield break;
                    }
                }

                yield return wait;
            }
        }

        protected override IEnumerator OnExit()
        {
            Debug.Log("FindPlayer: OnExit");
            yield break;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _findRadius);
        }
    }
}
