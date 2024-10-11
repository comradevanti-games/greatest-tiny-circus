using UnityEngine;

namespace GTC.Level
{
    internal static class LevelGen
    {
        public static LevelData GenerateLevel(Vector2 minPos, Vector2 maxPos)
        {
            var fleaPos = new Vector2(
                Random.Range(minPos.x, Mathf.Lerp(minPos.x, maxPos.x, 0.45f)),
                Random.Range(minPos.y, maxPos.y));
            var targetPos = new Vector2(
                Random.Range(Mathf.Lerp(minPos.x, maxPos.x, 0.65f), maxPos.x),
                Random.Range(minPos.y, maxPos.y));

            return new LevelData(fleaPos, targetPos);
        }
    }
}