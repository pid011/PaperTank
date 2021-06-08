using PaperTank.Game.Entity;
using UnityEngine;

namespace PaperTank.Game.Behaviour.Tank
{
    [RequireComponent(typeof(TurretAim))]
    public class TurretFire : MonoBehaviour
    {
        [SerializeField] private GameObject _shell;
        [SerializeField] private float _shellSpeed = 10f;
        [SerializeField] private int _cooldown = 2;

        public int cooldown => _cooldown;

        private TurretAim _turret;

        private void Awake()
        {
            _turret = GetComponent<TurretAim>();
        }

        /// <summary>
        /// 포탄 발사
        /// </summary>
        /// <param name="tagToDoNotCollide">콜라이더에 부딪히면 안되는 태그 (Player, Enemy, ...)</param>
        /// <returns>발사된 포탄 오브젝트</returns>
        public Shell Fire(string tagToDoNotCollide, Shell.MovementType movement = Shell.MovementType.Straight, Vector3 targetPos = default)
        {
            var initPos = transform.position + (Vector3.up * 0.2f);
            var rotation = Quaternion.Euler(_turret.transform.eulerAngles);

            var shell = Instantiate(_shell, initPos, rotation).GetComponent<Shell>();
            shell.shellMovement = movement;
            shell.speed = _shellSpeed;
            shell.endPoint = targetPos;
            shell.tagToDoNotCollide = tagToDoNotCollide;

            return shell;
        }
    }
}
