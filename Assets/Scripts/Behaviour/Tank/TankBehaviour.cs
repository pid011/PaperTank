using UnityEngine;

namespace PaperTank
{
    public class TankBehaviour : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _tankSprite;
        [SerializeField] private Turret _turret;

        public SpriteRenderer TankSprite => _tankSprite;
        public Turret Turret => _turret;

        protected virtual void Awake()
        {
        }

        protected virtual void Start()
        {
            Debug.Assert(_turret, $"{gameObject.name}'s Turret is empty");
        }

        protected virtual void Update()
        {
            FlipSprite();
        }

        private void FlipSprite()
        {
            // flip by turret rotate
            _tankSprite.flipX = 180 <= Turret.Rotator.Angle.y && Turret.Rotator.Angle.y < 360;
        }
    }
}
