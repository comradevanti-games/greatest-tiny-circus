using System;
using UnityEngine;

namespace GTC.Game
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class FleaAnimator : MonoBehaviour
    {
        public enum MovementState
        {
            Standing,
            Flying
        }

        [SerializeField] private Sprite standingSprite = null!;
        [SerializeField] private Sprite flyingSprite = null!;

        private SpriteRenderer spriteRenderer = null!;
        private MovementState currentMovementSate;


        private void UpdateSprite()
        {
            spriteRenderer.sprite = currentMovementSate switch
            {
                MovementState.Standing => standingSprite,
                MovementState.Flying => flyingSprite,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private MovementState CurrentMovementSate
        {
            set
            {
                currentMovementSate = value;
                UpdateSprite();
            }
        }

        public void OnLaunched()
        {
            CurrentMovementSate = MovementState.Flying;
        }

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}