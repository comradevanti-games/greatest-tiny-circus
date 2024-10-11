using System;
using UnityEngine;

namespace GTC.Flea
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class FleaAnimator : MonoBehaviour
    {
        [SerializeField] private Sprite standingSprite = null!;
        [SerializeField] private Sprite flyingSprite = null!;

        private SpriteRenderer spriteRenderer = null!;


        private void SetSpriteFor(FleaState state)
        {
            spriteRenderer.sprite = state switch
            {
                FleaState.PreparingForJump => standingSprite,
                FleaState.Flying => flyingSprite,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();

            GetComponent<FleaStateKeeper>().fleaStateChanged
                .AddListener(SetSpriteFor);
        }
    }
}