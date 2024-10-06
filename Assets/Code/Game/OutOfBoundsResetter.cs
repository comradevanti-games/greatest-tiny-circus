#nullable enable

using GTC.Level;
using UnityEngine;

namespace GTC.Game
{
    public class OutOfBoundsResetter : MonoBehaviour
    {
        [SerializeField] private float outOfBoundsY;

        private Transform? playerTransform;

        private void Update()
        {
            if (!playerTransform) return;

            if (!(playerTransform!.position.y < outOfBoundsY)) return;
            
            Singletons.Get<LevelController>().TryResetLevel();
            playerTransform = null;
        }

        private void Awake()
        {
            Singletons.Get<LevelController>().LevelLoaded += level =>
            {
                playerTransform = level.Flea.transform;
            };
        }
    }
}