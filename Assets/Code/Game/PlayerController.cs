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
            PreForceChoose,
            ForceChoose
        }


        [SerializeField] private float minForce;
        [SerializeField] private float maxForce;
        [SerializeField] private float torque;

        private float? force;
        private CancellationTokenSource? activeTaskCancellationSource;

        private JumpPhase CurrentJumpPhase =>
            (force, activeTaskCancellationSource) switch
            {
                (null, null) => JumpPhase.PreForceChoose,
                (null, not null) => JumpPhase.ForceChoose,
                _ => throw new InvalidOperationException("Bad state.")
            };


        private async void ChargeForce(CancellationToken ct)
        {
            var t = 0f;
            while (true)
            {
                await Task.Yield();
                t = Mathf.Repeat(t + Time.deltaTime * 4, 1);
                Debug.Log(t);
                if (!ct.IsCancellationRequested) continue;

                force = Mathf.Lerp(minForce, maxForce, t);
                PlayerPhysics.LaunchPlayer(gameObject,
                    new Vector2(0.5f, 1).normalized,
                    force!.Value,
                    torque);
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

        private void CompleteForceCharge()
        {
            activeTaskCancellationSource?.Cancel();
            activeTaskCancellationSource = null;
        }

        public void TryStartPrimaryAction()
        {
            switch (CurrentJumpPhase)
            {
                case JumpPhase.PreForceChoose:
                    StartChargeForce();
                    break;
            }
        }

        public void TryCompletePrimaryAction()
        {
            switch (CurrentJumpPhase)
            {
                case JumpPhase.ForceChoose:
                    CompleteForceCharge();
                    break;
            }
        }
    }
}