#nullable enable

using UnityEngine;
using UnityEngine.InputSystem;

namespace GTC.Game
{
    public class GameInputHandler : MonoBehaviour, ILevelLifecycleAware
    {
        private PlayerController? playerController;


        public void OnBuilt(Level.Level level)
        {
            playerController = level.Flea.GetComponent<PlayerController>();
        }

        public void OnPrimaryAction(InputAction.CallbackContext ctx)
        {
            if (playerController == null) return;

            if (ctx.performed)
            {
                playerController.TryStartPrimaryAction();
            }
            else if (ctx.canceled)
            {
                playerController.TryCompletePrimaryAction();
            }
        }
    }
}