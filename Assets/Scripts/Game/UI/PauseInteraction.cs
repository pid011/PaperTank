using PaperTank.UI;
using PaperTank.Util;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PaperTank.Game.UI
{
    public class PauseInteraction : PanelInteraction
    {
        public void OnPause(InputAction.CallbackContext context)
        {
            if (StageManager.isGameEnd) return;
            if (!context.started) return;

            if (!isOpen)
            {
                OpenPanel();
            }
            else
            {
                ClosePanel();
            }
        }

        public void ResumeStage()
        {
            ClosePanel();
        }

        public void RestartStage()
        {
            SceneLoader.LoadScene(StageManager.currentScene.name);
        }

        public void GoToMainMenu()
        {
            SceneLoader.LoadScene("Main Menu");
        }

        protected override void OnPanelOpened()
        {
            Time.timeScale = 0f;
        }

        protected override void OnPanelClosed()
        {
            Time.timeScale = 1f;
        }
    }
}
