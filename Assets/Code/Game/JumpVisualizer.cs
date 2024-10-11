using UnityEngine;

namespace GTC.Game
{
    public class JumpVisualizer : MonoBehaviour
    {
        [SerializeField] private float forceToScaleMultiplier;

        private SpriteRenderer spriteRenderer;
        private JumpController jumpController;

        private void OnJumpChanged(Launch launch)
        {
            spriteRenderer.enabled = true;
            transform.right = launch.Direction;
            transform.localScale =
                Vector3.one * (launch.Force * forceToScaleMultiplier);
        }

        private void OnJumpCommitted(Launch _)
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