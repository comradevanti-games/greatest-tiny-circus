#nullable enable

using UnityEngine;
using UnityEngine.Events;

namespace GTC.Flea
{
    [RequireComponent(typeof(Collision2DDetector))]
    public class TargetHitDetector : MonoBehaviour
    {
        public UnityEvent hitTarget = new UnityEvent();

        [SerializeField] private float downSensitivity;
        [SerializeField] private float minForce;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Target")) return;

            if (collision.relativeVelocity.magnitude < minForce) return;

            var hitDirection = -collision.relativeVelocity.normalized;
            var isDown = Vector2.Dot(Vector2.down, hitDirection) >
                         downSensitivity;

            if (!isDown) return;
            hitTarget.Invoke();
        }
    }
}