using UnityEngine;

namespace GTC.Game
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float minForce;
        [SerializeField] private float maxForce;
        [SerializeField] private float torque;

        private float? force;


        public void TryStartPrimaryAction()
        {
            PlayerPhysics.LaunchPlayer(gameObject, Vector2.one, maxForce,
                torque);
        }

        public void TryCompletePrimaryAction()
        {
        }
    }
}