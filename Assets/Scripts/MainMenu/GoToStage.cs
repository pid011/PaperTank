using PaperTank.Util;
using UnityEngine;

namespace PaperTank.MainMenu
{
    public class GoToStage : MonoBehaviour
    {
        [SerializeField] private string _sceneName;

        public void OpenStage()
        {
            SceneLoader.LoadScene(_sceneName);
        }
    }
}
