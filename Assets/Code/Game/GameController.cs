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
                new LevelData(new Vector2(-2, 3), new Vector2(3, 1));

            var level =
                await Singletons.Get<LevelBuilder>().Build(levelData, ct);
        }

        public void ResetLevel()
        {
        }

        private void Start()
        {
            LoadLevel(destroyCancellationToken);
        }

        private void Awake()
        {
            var gameInput = Singletons.Get<GameInput>();
            gameInput.Reset += ResetLevel;
        }
    }
}