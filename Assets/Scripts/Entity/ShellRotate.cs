using UnityEngine;

namespace PaperTank
{
    public class ShellRotate : MonoBehaviour
    {
        [SerializeField] private float _rotateSpeed = 20f;
        private void FixedUpdate()
        {
            transform.Rotate(0f, -_rotateSpeed, 0f);
        }
    }
}
