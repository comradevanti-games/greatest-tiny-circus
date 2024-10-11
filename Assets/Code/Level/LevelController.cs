#nullable enable

using System;
using System.Threading;
using System.Threading.Tasks;
using GTC.Transition;
using UnityEngine;
using static GTC.Level.LevelGen;

namespace GTC.Level
{
    public class LevelController : MonoBehaviour
    {
        public event Action<Level>? LevelLoaded;

        [Header("Level gen settings")] [SerializeField]
        private float xExtent;

        [SerializeField] private float minY;
        [SerializeField] private float maxY;

        private Level? loadedLevel;

        private async Task LoadLevel(CancellationToken ct)
        {
            var levelData = GenerateLevel(new Vector2(-xExtent, minY),
                new Vector2(xExtent, maxY));

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