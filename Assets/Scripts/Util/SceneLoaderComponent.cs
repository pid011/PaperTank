using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PaperTank.Util
{
    public sealed class SceneLoaderComponent : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _sceneLoaderCanvasGroup;
        [SerializeField] private Slider _progressBar;

        private string _loadSceneName;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void LoadScene(string sceneName)
        {
            Time.timeScale = 1f;

            gameObject.SetActive(true);
            SceneManager.sceneLoaded += LoadSceneEnd;
            _loadSceneName = sceneName;
            StartCoroutine(Load(sceneName));
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
                    if (_progressBar.value < op.progress) continue;

                    timer = 0f;
                }
                else
                {
                    _progressBar.value = Mathf.Lerp(_progressBar.value, 1f, timer);
                    if (_progressBar.value < 1.0f) continue;

                    op.allowSceneActivation = true;
                    yield break;
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
}
