using System.Collections;
using UnityEngine;

namespace PaperTank
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class TankBehaviour : MonoBehaviour
    {
        [SerializeField] private Turret _turret;
        [SerializeField] private GameObject _damagePrefab;
        [SerializeField] private Vector3 _damageInstantiateOffset;
        [SerializeField] private int _maxHealth = 100;

        public SpriteRenderer TankSprite => _tankSprite;
        public Turret Turret => _turret;
        public int Health { get => _health; private set => _health = Mathf.Clamp(value, 0, _maxHealth); }

        private int _health;
        private SpriteRenderer _tankSprite;
        private bool _canPlayHitAnimation = true;

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
            if (Time.timeScale != 0) FlipSprite();
        }

        public void GiveDamage(int damage)
        {
            Health -= damage;
            var angle = new Vector3(0f, Camera.main.transform.eulerAngles.y, 0f);
            var damageObject = Instantiate(_damagePrefab, transform.position + _damageInstantiateOffset, Quaternion.Euler(angle));
            damageObject.GetComponent<Damage>().DamageNumber = damage;

            if (_canPlayHitAnimation) StartCoroutine(HitAnimation());
        }

        private IEnumerator HitAnimation()
        {
            _canPlayHitAnimation = false;
            TankSprite.material.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            TankSprite.material.color = Color.white;
            _canPlayHitAnimation = true;
        }

        private void FlipSprite()
        {
            // flip by turret rotate
            _tankSprite.flipX = 180 <= Turret.Rotator.Angle.y && Turret.Rotator.Angle.y < 360;
        }
    }
}
