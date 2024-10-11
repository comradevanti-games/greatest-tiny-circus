#nullable enable

using UnityEngine;
using UnityEngine.Events;

namespace GTC.Flea
{
    [RequireComponent(typeof(Launchable))]
    [RequireComponent(typeof(FloorHitDetector))]
    public class FleaStateKeeper : MonoBehaviour
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
                CurrentState = new FleaState.Flying());

            GetComponent<FloorHitDetector>().floorHit.AddListener(() =>
                CurrentState = new FleaState.OnFloor());
        }
    }
}