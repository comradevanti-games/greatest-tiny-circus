using GTC.Level;
using UnityEngine;

namespace GTC.Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Collision2DDetector floorCollisionDetector;

        private void Start()
        {
            Singletons.Get<LevelController>()
                .TryResetLevel();
        }

        private void OnPlayerHitFloor()
        {
            Singletons.Get<LevelController>().TryResetLevel();
        }

        private void Awake()
        {
            floorCollisionDetector.CollisionHappened += _ =>
                OnPlayerHitFloor();
        }
    }
}