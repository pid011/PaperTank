using UnityEngine;

namespace PaperTank
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
        }

        private void Update()
        {
            if (!_particle.isPlaying) Destroy(gameObject);
        }

        private void FixedUpdate()
        {
            if (++_count >= 1) _sphereCollider.enabled = false;
        }
    }
}
