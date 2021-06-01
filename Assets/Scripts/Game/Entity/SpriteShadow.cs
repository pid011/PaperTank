using UnityEngine;
using UnityEngine.Rendering;

namespace PaperTank.Game.Entity
{
    [RequireComponent(typeof(SpriteRenderer)), ExecuteInEditMode]
    public class SpriteShadow : MonoBehaviour
    {
        private void Awake()
        {
            var renderer = GetComponent<SpriteRenderer>();
            renderer.receiveShadows = false;
            renderer.shadowCastingMode = ShadowCastingMode.TwoSided;
        }
    }
}
