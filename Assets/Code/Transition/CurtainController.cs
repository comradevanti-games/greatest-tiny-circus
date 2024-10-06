#nullable enable

using System.Threading.Tasks;
using UnityEngine;

namespace GTC.Transition
{
    internal class CurtainController : MonoBehaviour
    {
        public static CurtainController? Instance { get; private set; }


        [SerializeField] private float moveTimeSeconds;
        [SerializeField] private float closedX;
        [SerializeField] private float openedX;
        [SerializeField] private bool startOpen;
        [SerializeField] private RectTransform leftCurtain = null!;
        [SerializeField] private RectTransform rightCurtain = null!;

        private bool isClosed;


        private void SetCurtain(RectTransform curtain, float x)
        {
            curtain.anchoredPosition = new Vector2(x, 0);
        }

        private async Task MoveCurtain(RectTransform curtain, float targetX)
        {
            var startX = curtain.anchoredPosition.x;
            var t = 0f;

            while (t < 1)
            {
                await Task.Yield();
                destroyCancellationToken.ThrowIfCancellationRequested();
                t = Mathf.MoveTowards(t, 1, Time.deltaTime / moveTimeSeconds);
                var x = Mathf.SmoothStep(startX, targetX, t);
                SetCurtain(curtain, x);
            }
        }

        public async Task Close()
        {
            if (!isClosed) return;
            isClosed = true;
            await Task.WhenAll(
                MoveCurtain(leftCurtain, closedX),
                MoveCurtain(rightCurtain, -closedX)
            );
        }

        public async Task Open()
        {
            if (isClosed) return;
            isClosed = false;
            await Task.WhenAll(
                MoveCurtain(leftCurtain, -openedX),
                MoveCurtain(rightCurtain, openedX)
            );
        }

        private void Awake()
        {
            if (Instance != null) Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
            Instance = this;

            SetCurtain(leftCurtain, startOpen ? -openedX : closedX);
            SetCurtain(rightCurtain, startOpen ? openedX : -closedX);
        }
    }
}