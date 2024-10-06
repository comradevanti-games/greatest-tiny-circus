using GTC.Level;
using UnityEngine;

namespace GTC.Game
{
    public class GameController : MonoBehaviour
    {
        private void Start()
        {
            Singletons.Get<LevelController>()
                .TryResetLevel();
        }
    }
}