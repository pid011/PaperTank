using UnityEngine.InputSystem;

namespace PaperTank
{
    public class PauseInteraction : InGamePanelInteraction
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
    }
}
