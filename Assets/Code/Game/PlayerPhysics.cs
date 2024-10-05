using UnityEngine;

namespace GTC.Game
{
    public static class PlayerPhysics
    {
        public static void LaunchPlayer(GameObject player, Vector2 direction,
            float force, float torque)
        {
            var rigidbody = player.GetComponent<Rigidbody2D>();
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
            rigidbody.velocity = direction * force;
            rigidbody.angularVelocity = torque;
        }
    }
}