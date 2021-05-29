using UnityEngine;
using UnityEngine.InputSystem;

namespace PaperTank
{
    public class MainMenuInteraction : MonoBehaviour
    {
        [SerializeField] private PanelInteraction _stagePanel;

        private PanelInteraction _currentPanel;

        public void OpenStagePanel()
        {
            _currentPanel = _stagePanel;
            _currentPanel.OpenPanel();
        }

        public void CloseCurrentPanel(InputAction.CallbackContext _)
        {
            CloseCurrentPanel();
        }

        public void CloseCurrentPanel()
        {
            if (_currentPanel == null) return;

            _currentPanel.ClosePanel();
            _currentPanel = null;
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
