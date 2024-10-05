using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GTC.Game
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerController playerController;
        private CancellationTokenSource primaryActionSource;

        public void OnPrimaryAction(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                playerController.TryStartPrimaryAction();
            }
            else if (ctx.canceled)
            {
                playerController.TryCompletePrimaryAction();
            }
        }

        private void Awake()
        {
            playerController = GetComponent<PlayerController>();
        }
    }
}