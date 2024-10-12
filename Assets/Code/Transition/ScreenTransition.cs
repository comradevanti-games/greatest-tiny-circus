#nullable enable
using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace GTC.Transition
{
    public static class ScreenTransition
    {
        public static event Action? OnTransitionStarted;
        public static event Action? OnTransitionCompleted;


        public static Task Close()
        {
            return CurtainController.Instance?.Close() ?? Task.CompletedTask;
        }

        public static Task Open()
        {
            return CurtainController.Instance?.Open() ?? Task.CompletedTask;
        }

        public static async Task DoWithTransition(Func<Task> action)
        {
            OnTransitionStarted?.Invoke();
            await Close();
            await action();
            await Open();
            OnTransitionCompleted?.Invoke();
        }

        public static async Task TransitionToScene(string sceneName)
        {
            await Close();
            SceneManager.LoadScene(sceneName);
        }
    }
}