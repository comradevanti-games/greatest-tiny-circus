using UnityEngine;
using UnityEngine.InputSystem;

namespace GTC.Game
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerController playerController;

        public void OnPrimaryAction(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                playerController.OnPrimaryActionStart();
            }
            else if (ctx.canceled)
            {
                playerController.OnPrimaryActionEnd();
            }
        }

        private void Awake()
        {
            playerController = GetComponent<PlayerController>();
        }
    }
}