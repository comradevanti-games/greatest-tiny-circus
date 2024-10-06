#nullable enable
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace GTC.Game
{
    public class PlayerController : MonoBehaviour
    {
        private enum JumpPhase
        {
            PreAngleChoose,
            AngleChoose,
            PreForceChoose,
            ForceChoose,
        }

        [SerializeField] private float minAngle;
        [SerializeField] private float maxAngle;
        [SerializeField] private float minForce;
        [SerializeField] private float maxForce;
        [SerializeField] private float torque;

        private float? angle;
        private float? force;
        private CancellationTokenSource? activeTaskCancellationSource;

        private JumpPhase CurrentJumpPhase =>
            (angle, force, activeTaskCancellationSource) switch
            {
                (null, _, null) => JumpPhase.PreAngleChoose,
                (null, _, not null) => JumpPhase.AngleChoose,
                (not null, null, null) => JumpPhase.PreForceChoose,
                (not null, null, not null) => JumpPhase.ForceChoose,
                _ => throw new InvalidOperationException(
                    $"Bad state (angle: {angle != null}, force: {force != null}, task: {activeTaskCancellationSource != null}).")
            };

        private async void ChargeForce(CancellationToken ct)
        {
            var startTime = Time.time;
            while (true)
            {
                await Task.Yield();
                var t = Mathf.PingPong((Time.time - startTime) * 2, 1);
                Debug.Log(t);
                if (!ct.IsCancellationRequested) continue;

                force = Mathf.Lerp(minForce, maxForce, t);
                var direction = AngleUtils.VectorFromAngle(angle!.Value);
                PlayerPhysics.LaunchPlayer(gameObject, direction, force!.Value,
                    torque);
                break;
            }
        }

        private async void ChooseAngle(CancellationToken ct)
        {
            var startTime = Time.time;
            while (true)
            {
                await Task.Yield();
                var t = Mathf.PingPong((Time.time - startTime) * 2, 1);
                Debug.Log(t);
                if (!ct.IsCancellationRequested) continue;

                angle = Mathf.Lerp(minAngle, maxAngle, t);
                break;
            }
        }

        private void StartChargeForce()
        {
            force = null;
            activeTaskCancellationSource = new CancellationTokenSource();

            ChargeForce(CancellationTokenSource.CreateLinkedTokenSource(
                destroyCancellationToken,
                activeTaskCancellationSource.Token).Token);
        }

        private void StartChooseAngle()
        {
            angle = null;
            activeTaskCancellationSource = new CancellationTokenSource();

            ChooseAngle(CancellationTokenSource.CreateLinkedTokenSource(
                destroyCancellationToken,
                activeTaskCancellationSource.Token).Token);
        }

        private void CompleteCurrentTask()
        {
            activeTaskCancellationSource?.Cancel();
            activeTaskCancellationSource = null;
        }

        private void TryStartPrimaryAction()
        {
            switch (CurrentJumpPhase)
            {
                case JumpPhase.PreForceChoose:
                    StartChargeForce();
                    break;
                case JumpPhase.PreAngleChoose:
                    StartChooseAngle();
                    break;
            }
        }

        private void TryCompletePrimaryAction()
        {
            switch (CurrentJumpPhase)
            {
                case JumpPhase.ForceChoose:
                case JumpPhase.AngleChoose:
                    CompleteCurrentTask();
                    break;
            }
        }

        private void Awake()
        {
            var gameInput = Singletons.Get<GameInput>();
            gameInput.PrimaryActionStarted += TryStartPrimaryAction;
            gameInput.PrimaryActionCompleted += TryCompletePrimaryAction;
        }
    }
}