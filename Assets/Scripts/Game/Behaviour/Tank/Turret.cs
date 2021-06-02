using UnityEngine;

namespace PaperTank.Game.Behaviour.Tank
{
    [RequireComponent(typeof(TurretAim), typeof(TurretFire))]
    public class Turret : MonoBehaviour
    {
        public TurretAim rotator { get; private set; }
        public TurretFire weaponSystem { get; private set; }

        private void Awake()
        {
            rotator = GetComponent<TurretAim>();
            weaponSystem = GetComponent<TurretFire>();
        }
    }
}
