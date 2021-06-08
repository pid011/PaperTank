using DG.Tweening;
using PaperTank.Game.Behaviour.Tank;
using UnityEngine;

namespace PaperTank.Game.Entity
{
    public class ShellExplosion : MonoBehaviour
    {
        [SerializeField] private float _radius = 1f;

        private SphereCollider _sphereCollider;
        private ParticleSystem _particle;

        private int _count;

        private void Start()
        {
            _sphereCollider = GetComponent<SphereCollider>();
            _particle = GetComponent<ParticleSystem>();

            _sphereCollider.radius = _radius;
            _count = 0;
            Camera.main.DOShakeRotation(0.1f, strength: 0.2f);
        }

        private void Update()
        {
            if (!_particle.isPlaying) Destroy(gameObject);
        }

        private void FixedUpdate()
        {
            if (++_count >= 3) _sphereCollider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            TankBehaviour behaviour;
            if ((behaviour = other.GetComponent<TankBehaviour>()) != null)
            {
                behaviour.GiveDamage(15);
            }
        }
    }
}
