#nullable enable

using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using static GTC.AsyncUtils;

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

        private async Task MoveCurtain(RectTransform curtain, float targetX,
            CancellationToken ct)
        {
            var startX = curtain.anchoredPosition.x;
            await foreach (var t in SampleRangeAsync(0, 1, moveTimeSeconds, ct))
            {
                var x = Mathf.SmoothStep(startX, targetX, t);
                SetCurtain(curtain, x);
            }
        }

        public async Task Close()
        {
            if (isClosed) return;
            isClosed = true;
            await Task.WhenAll(
                MoveCurtain(leftCurtain, closedX, destroyCancellationToken),
                MoveCurtain(rightCurtain, -closedX, destroyCancellationToken)
            );
        }

        public async Task Open()
        {
            if (!isClosed) return;
            isClosed = false;
            await Task.WhenAll(
                MoveCurtain(leftCurtain, -openedX, destroyCancellationToken),
                MoveCurtain(rightCurtain, openedX, destroyCancellationToken)
            );
        }

        private void Awake()
        {
            if (Instance != null) Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
            Instance = this;

            isClosed = !startOpen;
            SetCurtain(leftCurtain, startOpen ? -openedX : closedX);
            SetCurtain(rightCurtain, startOpen ? openedX : -closedX);
        }
    }
}