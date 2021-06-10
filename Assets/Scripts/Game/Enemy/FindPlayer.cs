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
            while (true)
            {
                var colliders = new Collider[10];
                var size = Physics.OverlapSphereNonAlloc(transform.position, _findRadius, colliders, 1 << 10);

                foreach (var collideObject in colliders)
                {
                    if (collideObject is null || !collideObject.CompareTag("Player"))
                    {
                        continue;
                    }

                    target = collideObject.transform;
                    nextState = typeof(FireToTarget);
                    yield break;
                }

                yield return null;
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
