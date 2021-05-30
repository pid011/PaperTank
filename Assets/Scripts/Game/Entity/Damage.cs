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
            transform
                .DOMove(transform.position + Vector3.up * 2f, Duration)
                .SetEase(Ease.OutCirc);

            GetComponent<MeshRenderer>().material
                .DOFade(0f, Duration)
                .SetEase(Ease.InCirc);

            StartCoroutine(DestoryAfterDuration());
        }

        private IEnumerator DestoryAfterDuration()
        {
            yield return new WaitForSeconds(Duration);
            yield return new WaitForEndOfFrame();
            Destroy(gameObject);
        }
    }
}
