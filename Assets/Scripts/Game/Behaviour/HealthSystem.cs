using PaperTank.Game.Behaviour.Tank;
using UnityEngine;

namespace PaperTank.Game.Behaviour
{
    [RequireComponent(typeof(TankBehaviour))]
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField] private GameObject _explosionPrefab;
        private TankBehaviour _behaviour;

        private void Awake()
        {
            _behaviour = GetComponent<TankBehaviour>();
        }

        private void Update()
        {
            if (StageManager.isGameEnd || _behaviour.health != 0) return;

            Debug.Log("Destroyed");
            Destroy(gameObject);
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            if (gameObject.CompareTag("Player")) StageManager.GameOver();
        }
    }
}
