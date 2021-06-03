using System.Collections;
using PaperTank.Game.Behaviour.Tank;
using UnityEngine;

namespace PaperTank.Game.Enemy.Tank
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
            yield break;
        }

        protected override IEnumerator OnExecute()
        {
            var target = _findPlayer.target;
            if (target == null) yield break;

            _turret.rotator.targetPoint = target.position;
            _turret.weaponSystem.Fire("Enemy");
            yield return new WaitForSeconds(_turret.weaponSystem.cooldown);
        }

        protected override IEnumerator OnExit()
        {
            yield break;
        }
    }
}
