#nullable enable

using GTC.Level;
using UnityEngine;

namespace GTC.Game
{
    public class GameInput : MonoBehaviour
    {
        private GameInputActions inputActions = null!;


        private void Start()
        {
            inputActions.Enable();
        }

        private void Awake()
        {
            inputActions = new GameInputActions();
            PlayerController? playerController = null;

            inputActions.Game.PrimaryAction.performed +=
                _ => playerController?.TryStartPrimaryAction();

            inputActions.Game.PrimaryAction.canceled +=
                _ => playerController?.TryCompletePrimaryAction();

            inputActions.Game.Reset.performed += _ =>
                Singletons.Get<LevelController>().TryResetLevel();

            Singletons.Get<LevelController>().LevelLoaded += level =>
                playerController =
                    level.Flea.GetComponent<PlayerController>();
        }
    }
}