using UnityEngine;
using UnityEngine.InputSystem;

namespace PaperTank
{
    public class PauseInteraction : PanelInteraction
    {
        public void OnPause(InputAction.CallbackContext context)
        {
            if (!context.started) return;

            if (!IsOpen)
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
            SceneLoader.LoadScene(StageManager.CurrentScene.name);
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
