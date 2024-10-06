using UnityEngine;

namespace GTC
{
    public static class AngleUtils
    {
        public static Vector2 VectorFromAngle(float angle)
        {
            var x = Mathf.Sin(angle * Mathf.Deg2Rad);
            var y = Mathf.Cos(angle * Mathf.Deg2Rad);
            return new Vector2(x, y);
        }
    }
}