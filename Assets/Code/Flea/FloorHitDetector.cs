using UnityEngine;
using UnityEngine.Events;

namespace GTC.Flea
{
    [RequireComponent(typeof(Collision2DDetector))]
    public class FloorHitDetector : MonoBehaviour
    {
        public UnityEvent floorHit = new UnityEvent();

        private void Awake()
        {
            GetComponent<Collision2DDetector>().CollisionHappened +=
                collision =>
                {
                    if (!collision.gameObject.CompareTag("Floor")) return;
                    floorHit.Invoke();
                };
        }
    }
}