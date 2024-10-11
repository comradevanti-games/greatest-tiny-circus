#nullable enable

using System;
using GTC.Level;
using UnityEngine;

namespace GTC.Game
{
    public class JumpController : MonoBehaviour
    {
        private abstract record JumpState
        {
            public record PreJump : JumpState;

            public record ChooseAngle(DateTime StartTime, Vector2 Direction)
                : JumpState;

            public record PreForceChoose(Vector2 Direction) : JumpState;

            public record ChooseForce(
                DateTime StartTime,
                Vector2 Direction,
                float Force) : JumpState;

            public record PostJump : JumpState;
        }


        public event Action<Launch>? JumpChanged;
        public event Action<Launch>? JumpCommitted;

        [SerializeField] private float minAngle;
        [SerializeField] private float maxAngle;
        [SerializeField] private float minForce;
        [SerializeField] private float maxForce;
        [SerializeField] private float torque;

        private JumpState currentState = new JumpState.PostJump();


        private JumpState StartAngleChoose()
        {
            var initialDirection = AngleUtils.VectorFromAngle(minAngle);
            JumpChanged?.Invoke(new Launch(maxForce, initialDirection));
            return new JumpState.ChooseAngle(DateTime.Now,
                initialDirection);
        }

        private JumpState CompleteAngleChoose(Vector2 direction)
        {
            return new JumpState.PreForceChoose(direction);
        }

        private JumpState StartForceChoose(Vector2 direction)
        {
            JumpChanged?.Invoke(new Launch(maxForce, direction));
            return new JumpState.ChooseForce(DateTime.Now, direction, maxForce);
        }

        private JumpState DoJump(Vector2 direction, float force)
        {
            JumpCommitted?.Invoke(new Launch(force, direction));

            FindObjectOfType<Launchable>()
                ?.Launch(new Launch(force, direction));

            return new JumpState.PostJump();
        }

        private JumpState ProgressChooseForce(JumpState.ChooseForce state)
        {
            var delta = (float)(DateTime.Now - state.StartTime).TotalSeconds;
            var t = Mathf.PingPong(delta, 1);
            var newForce = Mathf.Lerp(minForce, maxForce, t);

            JumpChanged?.Invoke(new Launch(newForce, state.Direction));

            return state with { Force = newForce };
        }

        private JumpState ProgressChooseAngle(JumpState.ChooseAngle state)
        {
            var delta = (float)(DateTime.Now - state.StartTime).TotalSeconds;
            var t = Mathf.PingPong(delta, 1);
            var angle = Mathf.Lerp(minAngle, maxAngle, t);
            var direction = AngleUtils.VectorFromAngle(angle);

            JumpChanged?.Invoke(new Launch(maxForce, direction));

            return state with { Direction = direction };
        }

        private void Update()
        {
            currentState = currentState switch
            {
                JumpState.ChooseForce state => ProgressChooseForce(state),
                JumpState.ChooseAngle state => ProgressChooseAngle(state),
                _ => currentState
            };
        }

        private void PrepareForJump()
        {
            currentState = new JumpState.PreJump();
        }

        public void TryProgressJump()
        {
            currentState = currentState switch
            {
                JumpState.PreJump => StartAngleChoose(),
                JumpState.ChooseAngle state =>
                    CompleteAngleChoose(state.Direction),
                JumpState.PreForceChoose state => StartForceChoose(
                    state.Direction),
                JumpState.ChooseForce state => DoJump(state.Direction,
                    state.Force),
                _ => currentState
            };
        }

        private void Awake()
        {
            Singletons.Get<LevelController>().LevelLoaded +=
                _ => PrepareForJump();
        }
    }
}