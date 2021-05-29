using DG.Tweening;
using UnityEngine;

namespace PaperTank
{
    [RequireComponent(typeof(CanvasGroup))]
    public class PanelInteraction : MonoBehaviour
    {
        [SerializeField] private float _moveDistance;

        private CanvasGroup _canvasGroup;
        private Vector3 _initPos;
        private Vector3 _animatedPos;
        private RectTransform _transform;
        private bool _isOpen;

        private void Awake()
        {
            _transform = GetComponent<RectTransform>();

            _animatedPos = transform.position;
            _initPos = transform.position + Vector3.down * _moveDistance;
            _transform.position = _initPos;

            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0f;
            _canvasGroup.blocksRaycasts = false;
        }

        public void OpenPanel()
        {
            if (_isOpen) return;

            DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 1f, 0.3f);

            _transform.DOMove(_animatedPos, 0.3f);

            _canvasGroup.blocksRaycasts = true;

            _isOpen = true;
        }

        public void ClosePanel()
        {
            if (!_isOpen) return;

            DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 0f, 0.3f);

            _transform.DOMove(_initPos, 0.3f);

            _canvasGroup.blocksRaycasts = false;

            _isOpen = false;
        }
    }
}
