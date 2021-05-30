using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public sealed class SceneLoader : Singleton<SceneLoader>
{
    [SerializeField] private CanvasGroup _sceneLoaderCanvasGroup;
    [SerializeField] private Slider _progressBar;

    private string _loadSceneName;

    public static void LoadScene(string sceneName)
    {
        if (Instance == null) return;
        Time.timeScale = 1f;

        Instance.gameObject.SetActive(true);
        SceneManager.sceneLoaded += Instance.LoadSceneEnd;
        Instance._loadSceneName = sceneName;
        Instance.StartCoroutine(Instance.Load(sceneName));
    }

    private IEnumerator Load(string sceneName)
    {
        DOTween.KillAll(true);
        DOTween.Clear();

        yield return new WaitForEndOfFrame();

        _progressBar.value = 0f;

        yield return StartCoroutine(Fade(true));

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);

        op.allowSceneActivation = false;

        float timer = 0.0f;

        while (!op.isDone)
        {
            yield return null;
            timer += Time.unscaledDeltaTime;

            if (op.progress < 0.9f)
            {
                _progressBar.value = Mathf.Lerp(_progressBar.value, op.progress, timer);
                if (_progressBar.value >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                _progressBar.value = Mathf.Lerp(_progressBar.value, 1f, timer);

                if (_progressBar.value == 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }

    private void LoadSceneEnd(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == _loadSceneName)
        {
            StartCoroutine(Fade(false));
            SceneManager.sceneLoaded -= LoadSceneEnd;
        }
    }

    private IEnumerator Fade(bool isFadeIn)
    {
        float timer = 0f;
        while (timer <= 1f)
        {
            yield return null;
            timer += Time.unscaledTime * 2f;
            _sceneLoaderCanvasGroup.alpha = Mathf.Lerp(isFadeIn ? 0 : 1, isFadeIn ? 1 : 0, timer);
        }

        if (!isFadeIn) gameObject.SetActive(false);
    }
}
