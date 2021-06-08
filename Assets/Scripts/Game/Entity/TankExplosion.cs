using UnityEngine;

namespace PaperTank.Game.Entity
{
    public class TankExplosion : MonoBehaviour
    {
        private ParticleSystem _particle;

        private void Start()
        {
            _particle = GetComponent<ParticleSystem>();
        }

        private void Update()
        {
            if (!_particle.isPlaying) Destroy(gameObject);
        }
    }
}
