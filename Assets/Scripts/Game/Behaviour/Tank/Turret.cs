using UnityEngine;

namespace PaperTank
{
    [RequireComponent(typeof(TurretAim), typeof(TurretFire))]
    public class Turret : MonoBehaviour
    {
        public TurretAim Rotator { get; private set; }
        public TurretFire WeaponSystem { get; private set; }

        private void Awake()
        {
            Rotator = GetComponent<TurretAim>();
            WeaponSystem = GetComponent<TurretFire>();
        }
    }
}
