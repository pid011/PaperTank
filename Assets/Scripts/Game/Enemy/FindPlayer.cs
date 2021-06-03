using System.Collections;
using PaperTank.Game.Enemy.Tank;
using UnityEngine;

namespace PaperTank.Game.Enemy
{
    [RequireComponent(typeof(Collider))]
    public class FindPlayer : StateBehaviour
    {
        [SerializeField] private float _findRadius = 10f;

        public Transform target { get; private set; }

        protected override IEnumerator OnEnter()
        {
            yield break;
        }

        protected override IEnumerator OnExecute()
        {
            var wait = new WaitForFixedUpdate();

            while (true)
            {
                var colliders = Physics.OverlapSphere(transform.position, _findRadius, 1 << 10); // Behaviour layer

                foreach (var collideObject in colliders)
                {
                    if (!collideObject.CompareTag("Player")) continue;

                    target = collideObject.transform;
                    nextState = typeof(FireToTarget);
                    yield break;
                }

                yield return wait;
            }
        }

        protected override IEnumerator OnExit()
        {
            yield break;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _findRadius);
        }
    }
}
