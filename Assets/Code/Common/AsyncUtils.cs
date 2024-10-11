using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace GTC
{
    public static class AsyncUtils
    {
        public static async IAsyncEnumerable<float> SampleRangeAsync(
            float from, float to,
            float timeSeconds, [EnumeratorCancellation] CancellationToken ct)
        {
            Debug.Assert(from < to, "from < to");
            var t = from;
            var lastTime = Time.time;
            yield return t;
            await Task.Yield();

            while (t < to)
            {
                var currentTime = Time.time;
                var deltaTime = currentTime - lastTime;
                lastTime = currentTime;

                ct.ThrowIfCancellationRequested();
                t = Mathf.MoveTowards(t, 1, deltaTime / timeSeconds);
                yield return t;
                await Task.Yield();
            }
        }
    }
}