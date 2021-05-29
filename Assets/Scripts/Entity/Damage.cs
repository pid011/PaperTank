using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace PaperTank
{
    [RequireComponent(typeof(TextMesh))]
    public class Damage : MonoBehaviour
    {
        private const float Duration = 1f;

        public int DamageNumber { get; set; }

        private void Start()
        {
            GetComponent<TextMesh>().text = DamageNumber.ToString();

            StartCoroutine(Tweening());
        }

        private IEnumerator Tweening()
        {
            int count = 0;
            int max = 2;

            Tween moveTween = transform
                .DOMove(transform.position + Vector3.up * 2f, Duration)
                .SetEase(Ease.OutCirc)
                .OnKill(() => count++);

            Tween fadeTween = GetComponent<MeshRenderer>().material
                .DOFade(0f, Duration)
                .SetEase(Ease.InCirc)
                .OnKill(() => count++);

            yield return new WaitUntil(() => count == max);

            Destroy(gameObject);
        }
    }
}
