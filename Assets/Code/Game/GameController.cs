using System.Threading;
using GTC.Level;
using UnityEngine;

namespace GTC.Game
{
    public class GameController : MonoBehaviour
    {
        private async void LoadLevel(CancellationToken ct)
        {
            // TODO: Dynamically load level
            var levelData =
                new LevelData(new Vector2(0.5f, 3), new Vector2(10, 1));

            await Singletons.Get<LevelBuilder>().Build(levelData, ct);
        }

        private void Awake()
        {
            LoadLevel(destroyCancellationToken);
        }
    }
}