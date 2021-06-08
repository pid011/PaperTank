using PaperTank.Game.Player;
using UnityEngine;
using UnityEngine.UI;

namespace PaperTank.Game.UI
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Slider _hpSlider;

        private PlayerController _player;

        public void Init(int max, PlayerController player)
        {
            _hpSlider.maxValue = max;
            _hpSlider.value = player.health;
            _player = player;
        }

        private void Update()
        {
            _hpSlider.value = _player.health;
        }
    }
}
