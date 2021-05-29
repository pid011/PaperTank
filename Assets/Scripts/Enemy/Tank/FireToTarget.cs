using System.Collections;
using UnityEngine;

namespace PaperTank
{
    [RequireComponent(typeof(FindPlayer))]
    public class FireToTarget : StateBehaviour
    {
        [SerializeField] private Turret _turret;

        private FindPlayer _findPlayer;

        private void Awake()
        {
            _findPlayer = GetComponent<FindPlayer>();
        }

        protected override IEnumerator OnEnter()
        {
            // Debug.Log("FireToTarget: OnEnter");
            yield break;
        }

        protected override IEnumerator OnExecute()
        {
            // Debug.Log("FireToTarget: OnExecute");

            var target = _findPlayer.Target;
            if (target == null) yield break;

            _turret.Rotator.TargetPoint = target.position;
            _turret.WeaponSystem.Fire("Enemy");
            yield return new WaitForSeconds(_turret.WeaponSystem.Cooldown);
        }

        protected override IEnumerator OnExit()
        {
            // Debug.Log("FireToTarget: OnExit");
            yield break;
        }
    }
}
