#nullable enable

using UnityEngine;
using UnityEngine.Events;

namespace GTC
{
    public record Launch(float Force, Vector2 Direction);

    public class Launchable : MonoBehaviour
    {
        public UnityEvent launched = new UnityEvent();
        
        [SerializeField] private float torque;

        private new Rigidbody2D rigidbody = null!;

        public void Launch(Launch launch)
        {
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
            rigidbody.velocity = launch.Direction * launch.Force;
            rigidbody.angularVelocity = torque;

            launched.Invoke();
        }

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }
    }
}