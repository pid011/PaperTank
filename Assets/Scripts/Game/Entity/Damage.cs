using System.Collections;

using DG.Tweening;

using UnityEngine;

namespace PaperTank.Game.Entity
{
    [RequireComponent(typeof(TextMesh))]
    public class Damage : MonoBehaviour
    {
        private const float Duration = 1f;

        public int damageNumber { get; set; }

        private void Start()
        {
            GetComponent<TextMesh>().text = damageNumber.ToString();
            transform
                .DOMove(transform.position + (Vector3.up * 2f), Duration)
                .SetEase(Ease.OutCirc);

            GetComponent<MeshRenderer>().material
                .DOFade(0f, Duration)
                .SetEase(Ease.InCirc);

            StartCoroutine(DestroyAfterDuration());
        }

        private IEnumerator DestroyAfterDuration()
        {
            yield return new WaitForSeconds(Duration);
            yield return new WaitForEndOfFrame();
            Destroy(gameObject);
        }
    }
}
