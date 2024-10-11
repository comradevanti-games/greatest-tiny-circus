using GTC.Flea;
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

        private void Awake()
        {
            Singletons.Get<LevelController>().LevelLoaded += level =>
            {
                level.Flea.GetComponent<FleaStateKeeper>().fleaStateChanged
                    .AddListener(fleaState =>
                    {
                        if (fleaState is not (FleaState.OnFloor
                            or FleaState.OnTarget)) return;
                        Singletons.Get<LevelController>().TryResetLevel();
                    });
            };
        }
    }
}