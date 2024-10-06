using UnityEngine;

namespace GTC.Game
{
    public class JumpVisualizer : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private JumpController jumpController;

        private void OnJumpChanged(Jump jump)
        {
            spriteRenderer.enabled = true;
            transform.right = jump.Direction;
            transform.localScale = Vector3.one * jump.Force / 5f;
        }

        private void OnJumpCommitted(Jump _)
        {
            spriteRenderer.enabled = false;
        }

        private void Awake()
        {
            jumpController = Singletons.Get<JumpController>();
            spriteRenderer = GetComponent<SpriteRenderer>();

            jumpController.JumpCommitted += OnJumpCommitted;
            jumpController.JumpChanged += OnJumpChanged;
        }

        private void OnDestroy()
        {
            jumpController.JumpCommitted -= OnJumpCommitted;
            jumpController.JumpChanged -= OnJumpChanged;
        }
    }
}