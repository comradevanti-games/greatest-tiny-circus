using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace GTC.Level
{
    public class LevelBuilder : MonoBehaviour
    {
        [SerializeField] private GameObject fleaPrefab;

        public async Task Build(LevelData levelData, CancellationToken ct)
        {
            var flea = Instantiate(fleaPrefab, levelData.FleaPosition,
                Quaternion.identity);
        }
    }
}