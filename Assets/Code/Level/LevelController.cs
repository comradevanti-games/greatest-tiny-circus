#nullable enable

using System;
using System.Threading;
using System.Threading.Tasks;
using GTC.Transition;
using UnityEngine;

namespace GTC.Level
{
    public class LevelController : MonoBehaviour
    {
        public event Action<Level>? LevelLoaded;

        private Level? loadedLevel;

        private async Task LoadLevel(CancellationToken ct)
        {
            // TODO: Dynamically load level
            var levelData =
                new LevelData(new Vector2(-2, 3), new Vector2(3, 2));

            loadedLevel =
                await Singletons.Get<LevelBuilder>().Build(levelData, ct);

            LevelLoaded?.Invoke(loadedLevel);
        }

        public async void TryResetLevel()
        {
            await ScreenTransition.DoWithTransition(async () =>
            {
                if (loadedLevel != null)
                    LevelActions.UnbuildLevel(loadedLevel);
                await LoadLevel(destroyCancellationToken);
            });
        }
    }
}