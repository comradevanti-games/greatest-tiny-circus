#nullable enable

using System;
using System.Threading;
using UnityEngine;

namespace GTC.Level
{
    public class LevelController : MonoBehaviour
    {
        public event Action<Level>? LevelLoaded;

        private Level? loadedLevel;

        private async void LoadLevel(CancellationToken ct)
        {
            // TODO: Dynamically load level
            var levelData =
                new LevelData(new Vector2(-2, 3), new Vector2(3, 1));

            loadedLevel =
                await Singletons.Get<LevelBuilder>().Build(levelData, ct);

            LevelLoaded?.Invoke(loadedLevel);
        }

        public void TryResetLevel()
        {
            if (loadedLevel != null)
                LevelActions.UnbuildLevel(loadedLevel);
            LoadLevel(destroyCancellationToken);
        }
    }
}