#nullable enable
using System;
using UnityEngine;

namespace GTC.Game
{
    public class PlayerLandDetector : MonoBehaviour
    {
        public event Action? PlayerEnteredLandingZone;
        public event Action? PlayerLanded;

        public void OnTriggerEnter2D(Collider2D other)
        {
            // TODO: Check if other is player

            PlayerEnteredLandingZone?.Invoke();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // TODO: Check if other is player

            // Make sure collision happened at the top
            for (var i = 0; i < collision.contactCount; i++)
            {
                var contact = collision.GetContact(i);
                if (contact.point.y >= transform.position.y)
                    return;
            }

            PlayerLanded?.Invoke();
        }
    }
}