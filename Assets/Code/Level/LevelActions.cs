using UnityEngine;

namespace GTC.Level
{
    public static class LevelActions
    {
        public static void UnbuildLevel(Level level)
        {
            Object.Destroy(level.Root);
        }
    }
}