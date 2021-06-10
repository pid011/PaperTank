using System.Collections;
using PaperTank.Game.Behaviour.Tank;
using PaperTank.Game.Entity;
using UnityEngine;

namespace PaperTank.Game.Enemy.Tank
{
    [RequireComponent(typeof(FindPlayer))]
    public class FireToTarget : StateBehaviour
    {
        [SerializeField] private Turret _turret;
        [SerializeField] private Shell.MovementType _movement;

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
            if (target is null) yield break;

            var targetPos = target.position;

            _turret.rotator.targetPoint = targetPos;
            _turret.weaponSystem.Fire("Enemy", _movement, targetPos + Vector3.down);

            var randomCool = Random.Range(-0.2f, 1f);
            yield return new WaitForSeconds(_turret.weaponSystem.cooldown + randomCool);
        }

        protected override IEnumerator OnExit()
        {
            yield break;
        }
    }
}
