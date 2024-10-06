using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace GTC.Level
{
    public class LevelBuilder : MonoBehaviour
    {
        [SerializeField] private GameObject fleaPrefab;
        [SerializeField] private GameObject targetPrefab;

        public async Task<Level> Build(LevelData levelData,
            CancellationToken ct)
        {
            var flea = Instantiate(fleaPrefab, levelData.FleaPosition,
                Quaternion.identity);

            await Task.Yield();
            ct.ThrowIfCancellationRequested();
            var jumpTarget = Instantiate(targetPrefab, levelData.TargetPosition,
                Quaternion.identity);

            return new Level(flea, jumpTarget);
        }
    }
}