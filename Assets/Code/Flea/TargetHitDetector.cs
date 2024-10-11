#nullable enable

using UnityEngine;
using UnityEngine.Events;

namespace GTC.Flea
{
    [RequireComponent(typeof(Collision2DDetector))]
    public class TargetHitDetector : MonoBehaviour
    {
        public UnityEvent hitTarget = new UnityEvent();

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Target")) return;

            var hitDirection = -collision.relativeVelocity.normalized;
            var isDown = Vector2.Dot(Vector2.down, hitDirection) > 0.5f;

            if (!isDown) return;
            hitTarget.Invoke();
        }
    }
}