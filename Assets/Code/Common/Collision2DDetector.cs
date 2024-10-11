#nullable enable

using System;
using UnityEngine;

namespace GTC
{
    public class Collision2DDetector : MonoBehaviour
    {
        public event Action<Collision2D>? CollisionHappened;

        private void OnCollisionEnter2D(Collision2D collision2D)
        {
            CollisionHappened?.Invoke(collision2D);
        }
    }
}