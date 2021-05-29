using UnityEngine;

namespace PaperTank
{
    public class StageManager : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
