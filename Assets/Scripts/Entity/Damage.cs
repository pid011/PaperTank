using DG.Tweening;
using UnityEngine;

namespace PaperTank
{
    [RequireComponent(typeof(TextMesh))]
    public class Damage : MonoBehaviour
    {
        private const float Duration = 1f;

        public int DamageNumber { get; set; }

        private TextMesh _damageText;

        private void Awake()
        {
            _damageText = GetComponent<TextMesh>();
        }

        private void Start()
        {
            _damageText.text = DamageNumber.ToString();

            transform.DOMove(transform.position + Vector3.up * 2f, Duration).SetEase(Ease.OutCirc);
            GetComponent<MeshRenderer>().material.DOFade(0f, Duration).SetEase(Ease.InCirc);
            Destroy(gameObject, Duration);
        }
    }
}
