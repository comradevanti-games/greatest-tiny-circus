using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace GTC.Level
{
    public class LevelBuilder : MonoBehaviour
    {
        [SerializeField] private GameObject fleaPrefab;
        [SerializeField] private GameObject targetPrefab;

        public async Task Build(LevelData levelData, CancellationToken ct)
        {
            _ = Instantiate(fleaPrefab, levelData.FleaPosition,
                Quaternion.identity);

            ct.ThrowIfCancellationRequested();
            _ = Instantiate(targetPrefab, levelData.TargetPosition,
                Quaternion.identity);
        }
    }
}