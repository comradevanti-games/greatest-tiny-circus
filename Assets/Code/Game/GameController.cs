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
            var levelData = new LevelData(new Vector2(1, 2));

            await Singletons.Get<LevelBuilder>().Build(levelData, ct);
        }

        private void Awake()
        {
            LoadLevel(destroyCancellationToken);
        }
    }
}