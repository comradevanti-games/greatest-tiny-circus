#nullable enable
using System;
using UnityEngine;

namespace GTC.Game
{
    public class PlayerLandDetector : MonoBehaviour
    {
        public event Action? PlayerEnteredLandingZone;
        public event Action? PlayerLanded;

        private bool isInLandingZone;

        public void OnTriggerEnter2D(Collider2D other)
        {
            // TODO: Check if other is player

            isInLandingZone = true;
            PlayerEnteredLandingZone?.Invoke();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // TODO: Check if other is player

            // Only allow landing from the top
            if (!isInLandingZone) return;

            PlayerLanded?.Invoke();
        }
    }
}