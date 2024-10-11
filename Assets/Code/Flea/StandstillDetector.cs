#nullable enable

using System;
using UnityEngine;
using UnityEngine.Events;

namespace GTC.Flea
{
    public class StandstillDetector : MonoBehaviour
    {
        public UnityEvent standstillStarted = new UnityEvent();
        
        [SerializeField] private float thresholdSpeed;
        [SerializeField] private float standstillTimeSeconds;

        private new Rigidbody2D rigidbody = null!;
        private DateTime lastMoveTime = DateTime.MinValue;
        private bool eventSent;

        private void Update()
        {
            var now = DateTime.Now;

            if (rigidbody.velocity.magnitude > thresholdSpeed)
            {
                lastMoveTime = now;
                eventSent = false;
                return;
            }

            if (eventSent) return;

            var timeSinceMove = now - lastMoveTime;
            if (timeSinceMove.TotalSeconds < standstillTimeSeconds) return;

            standstillStarted.Invoke();
            eventSent = true;
        }

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }
    }
}