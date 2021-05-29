using UnityEngine;

namespace PaperTank
{
    [RequireComponent(typeof(TurretAim))]
    public class TurretFire : MonoBehaviour
    {
        [SerializeField] private GameObject _shell;
        [SerializeField] private float _shellSpeed = 10f;
        [SerializeField] private int _cooldown = 2;

        public int Cooldown => _cooldown;

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
        public Shell Fire(string tagToDoNotCollide)
        {
            var initPos = transform.position + Vector3.up * 0.2f;
            var rotation = Quaternion.Euler(_turret.transform.eulerAngles);

            var shell = Instantiate(_shell, initPos, rotation).GetComponent<Shell>();
            shell.ShellMovement = Shell.MovementType.Straight;
            shell.Speed = _shellSpeed;
            shell.TagToDoNotCollide = tagToDoNotCollide;

            return shell;
        }
    }
}
