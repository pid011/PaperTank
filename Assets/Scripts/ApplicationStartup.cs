using DG.Tweening;
using UnityEngine;

namespace PaperTank
{
    public class ApplicationStartup : MonoBehaviour
    {
        private void Awake()
        {
            DOTween.Init();
            DOTween.defaultTimeScaleIndependent = false; // use Time.timeScale
        }
    }
}
