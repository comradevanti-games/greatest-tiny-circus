#nullable enable

using GTC.Level;
using GTC.Transition;
using UnityEngine;

namespace GTC.Game
{
    public class GameInput : MonoBehaviour
    {
        private GameInputActions inputActions = null!;


        private void Awake()
        {
            inputActions = new GameInputActions();
            var jumpController = Singletons.Get<JumpController>();

            inputActions.Game.PrimaryAction.performed +=
                _ => jumpController.TryProgressJump();

            inputActions.Game.PrimaryAction.canceled +=
                _ => jumpController.TryProgressJump();

            inputActions.Game.Reset.performed += _ =>
                Singletons.Get<LevelController>().TryResetLevel();

            inputActions.Game.Exit.performed += _ =>
                Singletons.Get<GameController>().GoBackToMenu();

            ScreenTransition.OnTransitionStarted +=
                () => inputActions.Disable();
            ScreenTransition.OnTransitionCompleted +=
                () => inputActions.Enable();
        }
    }
}