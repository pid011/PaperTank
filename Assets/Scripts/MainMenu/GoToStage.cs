using UnityEngine;

namespace PaperTank
{
    public class GoToStage : MonoBehaviour
    {
        [SerializeField] private string _sceneName;

        public void OpenStage()
        {
            SceneLoader.Instance.LoadScene(_sceneName);
        }
    }
}
