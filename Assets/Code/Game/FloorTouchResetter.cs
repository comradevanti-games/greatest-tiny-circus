#nullable enable

using GTC.Level;
using UnityEngine;

namespace GTC.Game
{
    public class FloorTouchResetter : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D _)
        {
            Singletons.Get<LevelController>().TryResetLevel();
        }
    }
}