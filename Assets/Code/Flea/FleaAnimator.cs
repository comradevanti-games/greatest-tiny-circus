using System;
using UnityEngine;

namespace GTC.Flea
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class FleaAnimator : MonoBehaviour
    {
        [SerializeField] private Sprite standingSprite = null!;
        [SerializeField] private Sprite flyingSprite = null!;
        [SerializeField] private Sprite onFloorSprite = null!;

        private new Rigidbody2D rigidbody = null!;
        private SpriteRenderer spriteRenderer = null!;


        private void SetSpriteFor(FleaState state)
        {
            spriteRenderer.sprite = state switch
            {
                FleaState.PreparingForJump => standingSprite,
                FleaState.Flying => flyingSprite,
                FleaState.OnFloor => onFloorSprite,
                _ => throw new ArgumentOutOfRangeException()
            };

            if (state is FleaState.OnFloor)
            {
                rigidbody.bodyType = RigidbodyType2D.Static;
                transform.right = Vector3.right;
            }
        }

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            rigidbody = GetComponent<Rigidbody2D>();

            GetComponent<FleaStateKeeper>().fleaStateChanged
                .AddListener(SetSpriteFor);
        }
    }
}