using System.Linq;
using System.Threading;
using GTC.Level;
using UnityEngine;

namespace GTC.Game
{
    public class GameController : MonoBehaviour
    {
        private async void LoadLevel(CancellationToken ct)
        {
            var lifecycleAware = Singletons.All().OfType<ILevelLifecycleAware>()
                .ToHashSet();

            // TODO: Dynamically load level
            var levelData =
                new LevelData(new Vector2(-2, 3), new Vector2(3, 1));

            var level =
                await Singletons.Get<LevelBuilder>().Build(levelData, ct);

            ct.ThrowIfCancellationRequested();
            foreach (var it in lifecycleAware)
                it.OnBuilt(level);
        }

        private void Awake()
        {
            LoadLevel(destroyCancellationToken);
        }
    }
}