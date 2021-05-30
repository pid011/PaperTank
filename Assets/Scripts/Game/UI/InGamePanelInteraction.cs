using UnityEngine;

namespace PaperTank
{
    public class InGamePanelInteraction : PanelInteraction
    {
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
