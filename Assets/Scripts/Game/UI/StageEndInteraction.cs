using PaperTank.UI;
using PaperTank.Util;
using UnityEngine;
using UnityEngine.UI;

namespace PaperTank.Game.UI
{
    public class StageEndInteraction : PanelInteraction
    {
        [SerializeField] private Text _title;

        public void GoToNextStage()
        {
            // StageManager.GoToNextStage()
        }

        public void RestartStage()
        {
            SceneLoader.LoadScene(StageManager.currentScene.name);
        }

        public void GoToMainMenu()
        {
            SceneLoader.LoadScene("Main Menu");
        }

        public void SetTitle(string title)
        {
            _title.text = title;
        }
    }
}
