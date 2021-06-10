using UnityEngine;
using UnityEngine.Rendering;

namespace PaperTank.Game.Entity
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteShadow : MonoBehaviour
    {
        private void Awake()
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.receiveShadows = false;
            spriteRenderer.shadowCastingMode = ShadowCastingMode.TwoSided;
        }
    }
}
