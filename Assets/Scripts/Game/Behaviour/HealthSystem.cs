
using PaperTank.Game.Behaviour.Tank;

using UnityEngine;

namespace PaperTank.Game.Behaviour
{
    [RequireComponent(typeof(TankBehaviour))]
    public class HealthSystem : MonoBehaviour
    {
        private TankBehaviour _behaviour;

        private void Awake()
        {
            _behaviour = GetComponent<TankBehaviour>();
        }

        private void Update()
        {
            if (StageManager.isGameEnd || _behaviour.health != 0) return;

            Debug.Log("Destroied");
            Destroy(gameObject);

            if (gameObject.CompareTag("Player"))
            {
                StageManager.GameOver();
            }
        }
    }
}
