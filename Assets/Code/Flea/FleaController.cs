#nullable enable

using UnityEngine;
using UnityEngine.Events;

namespace GTC.Flea
{
    [RequireComponent(typeof(Launchable))]
    [RequireComponent(typeof(FloorHitDetector))]
    [RequireComponent(typeof(TargetHitDetector))]
    public class FleaController : MonoBehaviour
    {
        public UnityEvent<FleaState> fleaStateChanged =
            new UnityEvent<FleaState>();

        private FleaState currentState = new FleaState.PreparingForJump();


        public FleaState CurrentState
        {
            get => currentState;
            set
            {
                if (value == currentState) return;

                currentState = value;
                fleaStateChanged.Invoke(currentState);
            }
        }

        private void Start()
        {
            fleaStateChanged.Invoke(currentState);
        }

        private void Awake()
        {
            GetComponent<Launchable>().launched.AddListener(() =>
            {
                GetComponent<StandstillDetector>().enabled = true;
                CurrentState = new FleaState.Flying();
            });

            GetComponent<FloorHitDetector>().floorHit.AddListener(() =>
                CurrentState = new FleaState.Failed());

            GetComponent<TargetHitDetector>().hitTarget.AddListener(() =>
                CurrentState = new FleaState.Success());

            GetComponent<StandstillDetector>().standstillStarted.AddListener(
                () => CurrentState = new FleaState.Failed());
        }
    }
}