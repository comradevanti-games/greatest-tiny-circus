using GTC.Transition;
using UnityEngine;

namespace GTC.Menu
{
    public class MenuController : MonoBehaviour
    {
        public void StartGame()
        {
            ScreenTransition.TransitionToScene("Game");
        }

        private void Start()
        {
            ScreenTransition.Open();
        }
    }
}