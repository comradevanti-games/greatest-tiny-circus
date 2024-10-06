#nullable enable

using System;
using UnityEngine;

namespace GTC.Game
{
    public class GameInput : MonoBehaviour
    {
        public event Action? PrimaryActionStarted;
        public event Action? PrimaryActionCompleted;
        public event Action? Reset;

        private GameInputActions inputActions = null!;


        private void Start()
        {
            inputActions.Enable();
        }

        private void Awake()
        {
            inputActions = new GameInputActions();

            inputActions.Game.PrimaryAction.performed +=
                _ => PrimaryActionStarted?.Invoke();
            inputActions.Game.PrimaryAction.canceled +=
                _ => PrimaryActionCompleted?.Invoke();
            inputActions.Game.Reset.performed += _ => Reset?.Invoke();
        }
    }
}