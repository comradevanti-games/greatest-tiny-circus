using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace GTC.Transition
{
    public static class ScreenTransition
    {
        public static Task Close()
        {
            // TODO: Implement
            return Task.CompletedTask;
        }

        public static Task Open()
        {
            // TODO: Implement
            return Task.CompletedTask;
        }

        public static async Task DoWithTransition(Action action)
        {
            await Close();
            action();
            await Open();
        }

        public static Task TransitionToScene(string sceneName)
        {
            return DoWithTransition(() => SceneManager.LoadScene(sceneName));
        }
    }
}