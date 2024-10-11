#nullable enable

using UnityEngine;
using UnityEngine.Events;

namespace GTC.Flea
{
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
        }
    }

    public abstract record FleaState
    {
        public record PreparingForJump : FleaState;

        public record Flying : FleaState;
    }
}