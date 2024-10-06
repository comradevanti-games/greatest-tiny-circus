#nullable enable

using UnityEngine;

namespace GTC.Game
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float torque;

        private FleaAnimator animator = null!;

        private void DoJump(Jump jump)
        {
            PlayerPhysics.LaunchPlayer(gameObject, jump.Direction, jump.Force,
                torque);
            animator.CurrentMovementSate =
                FleaAnimator.MovementState.Flying;
        }

        private void Awake()
        {
            animator = GetComponent<FleaAnimator>();

            Singletons.Get<JumpController>().JumpCommitted += DoJump;
        }

        private void OnDestroy()
        {
            Singletons.Get<JumpController>().JumpCommitted -= DoJump;
        }
    }
}