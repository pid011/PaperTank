using UnityEngine;

namespace PaperTank
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class TankBehaviour : MonoBehaviour
    {
        [SerializeField] private Turret _turret;

        public SpriteRenderer TankSprite => _tankSprite;
        public Turret Turret => _turret;

        private SpriteRenderer _tankSprite;

        protected virtual void Awake()
        {
            _tankSprite = GetComponent<SpriteRenderer>();
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
