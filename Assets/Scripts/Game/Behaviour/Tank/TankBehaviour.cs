using System.Collections;
using DG.Tweening;
using PaperTank.Game.Entity;
using UnityEngine;

namespace PaperTank.Game.Behaviour.Tank
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class TankBehaviour : MonoBehaviour
    {
        [SerializeField] private Turret _turret;
        [SerializeField] private GameObject _damagePrefab;
        [SerializeField] private Vector3 _damageInstantiateOffset;
        [SerializeField] private int _maxHealth = 100;

        protected SpriteRenderer tankSprite { get; set; }
        protected Turret turret => _turret;
        public int health { get => _health; private set => _health = Mathf.Clamp(value, 0, _maxHealth); }

        private int _health;
        private bool _canPlayHitAnimation = true;

        protected virtual void Awake()
        {
            tankSprite = GetComponent<SpriteRenderer>();
            _health = _maxHealth;
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
            health -= damage;
            var angle = new Vector3(0f, Camera.main.transform.eulerAngles.y, 0f);
            var damageObject = Instantiate(
                _damagePrefab,
                transform.position + _damageInstantiateOffset,
                Quaternion.Euler(angle));
            damageObject.GetComponent<Damage>().damageNumber = damage;

            if (_canPlayHitAnimation)
            {
                StartCoroutine(HitAnimation());
                Camera.main.DOShakeRotation(0.5f, strength: 0.5f);
            }
        }

        private IEnumerator HitAnimation()
        {
            _canPlayHitAnimation = false;
            var material = tankSprite.material;

            material.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            material.color = Color.white;
            _canPlayHitAnimation = true;
        }

        private void FlipSprite()
        {
            // flip by turret rotate
            tankSprite.flipX = 180 <= turret.rotator.angle.y && turret.rotator.angle.y < 360;
        }
    }
}
