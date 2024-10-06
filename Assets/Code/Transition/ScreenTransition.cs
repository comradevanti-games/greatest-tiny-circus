using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace GTC.Transition
{
    public static class ScreenTransition
    {
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
            await Close();
            await action();
            await Open();
        }

        public static Task TransitionToScene(string sceneName)
        {
            return DoWithTransition(async () =>
            {
                var op = SceneManager.LoadSceneAsync(sceneName);

                while (!op!.isDone)
                    await Task.Yield();
            });
        }
    }
}