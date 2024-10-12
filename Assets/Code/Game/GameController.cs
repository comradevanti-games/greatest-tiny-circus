using GTC.Flea;
using GTC.Level;
using GTC.Transition;
using UnityEngine;

namespace GTC.Game
{
    public class GameController : MonoBehaviour
    {
        public void GoBackToMenu()
        {
            _ = ScreenTransition.TransitionToScene("Menu");
        }

        private void Start()
        {
            Singletons.Get<LevelController>()
                .TryResetLevel();
        }

        private void Awake()
        {
            Singletons.Get<LevelController>().LevelLoaded += level =>
            {
                level.Flea.GetComponent<FleaController>().fleaStateChanged
                    .AddListener(fleaState =>
                    {
                        if (fleaState is not (FleaState.Failed
                            or FleaState.Success)) return;
                        Singletons.Get<LevelController>().TryResetLevel();
                    });
            };
        }
    }
}