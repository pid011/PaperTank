using PaperTank.Util;
using UnityEngine.SceneManagement;

namespace PaperTank.Game
{
    public sealed class StageManager : Singleton<StageManagerComponent>
    {
        public static Scene currentScene => instance.currentScene;
        public static bool isStage => instance.isGameEnd;
        public static bool isGameEnd => instance.isGameEnd;

        public static void GameOver()
        {
            if (!instance.isStage || instance.isGameEnd) return;

            instance.CreateStageEndPanel("Game Over");
            instance.isGameEnd = true;
        }

        public static void StageClear()
        {
            if (!instance.isStage || instance.isGameEnd) return;

            instance.CreateStageEndPanel("Stage Clear!");
            instance.isGameEnd = true;
        }
    }
}
