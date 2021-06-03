using DG.Tweening;
using UnityEngine;

namespace PaperTank.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class PanelInteraction : MonoBehaviour
    {
        private const float MoveDistance = 5f;
        private const float AnimationDuration = 0.1f;

        public bool isOpen { get; private set; }

        private CanvasGroup _canvasGroup;
        private Vector3 _initPos;
        private Vector3 _animatedPos;
        private RectTransform _transform;

        private void Awake()
        {
            _transform = GetComponent<RectTransform>();

            _animatedPos = transform.position;
            _initPos = _animatedPos + (Vector3.down * MoveDistance);
            _transform.position = _initPos;

            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0f;
            _canvasGroup.blocksRaycasts = false;
        }

        public void OpenPanel()
        {
            if (isOpen) return;

            DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 1f, AnimationDuration - 0.01f);
            _transform
                .DOMove(_animatedPos, AnimationDuration)
                .OnComplete(() =>
                {
                    _canvasGroup.blocksRaycasts = true;
                    isOpen = true;
                    OnPanelOpened();
                });
        }

        public void ClosePanel()
        {
            if (!isOpen) return;

            // timeScale이 0일 경우에는 Tween이 움직이지 않으므로
            // OnPanelClosed를 먼저 호출하고 이후에 DOTween 실행
            _canvasGroup.blocksRaycasts = false;
            isOpen = false;
            OnPanelClosed();

            DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 0f, AnimationDuration - 0.01f);
            _transform.DOMove(_initPos, AnimationDuration);
        }

        protected virtual void OnPanelOpened()
        {
        }

        protected virtual void OnPanelClosed()
        {
        }
    }
}
