using System;
using UnityEngine;

namespace GTC.Flea
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class FleaAnimator : MonoBehaviour
    {
        [SerializeField] private Sprite standingSprite = null!;
        [SerializeField] private Sprite flyingSprite = null!;
        [SerializeField] private Sprite failedSprite = null!;
        [SerializeField] private Sprite successSprite = null!;

        private new Rigidbody2D rigidbody = null!;
        private SpriteRenderer spriteRenderer = null!;


        private void SetSpriteFor(FleaState state)
        {
            spriteRenderer.sprite = state switch
            {
                FleaState.PreparingForJump => standingSprite,
                FleaState.Flying => flyingSprite,
                FleaState.Failed => failedSprite,
                FleaState.Success => successSprite,
                _ => throw new ArgumentOutOfRangeException()
            };

            if (state is not (FleaState.Failed or FleaState.Success)) return;
            
            rigidbody.bodyType = RigidbodyType2D.Static;
            transform.right = Vector3.right;
        }

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            rigidbody = GetComponent<Rigidbody2D>();

            GetComponent<FleaController>().fleaStateChanged
                .AddListener(SetSpriteFor);
        }
    }
}