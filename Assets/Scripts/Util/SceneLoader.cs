namespace PaperTank.Util
{
    public sealed class SceneLoader : Singleton<SceneLoaderComponent>
    {
        public static void LoadScene(string sceneName)
        {
            if (instance == null) return;
            instance.LoadScene(sceneName);
        }
    }
}
