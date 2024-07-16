using System.Collections.Generic;
using UnityEngine;

namespace Factories
{
    public class SpawnContext : MonoBehaviour
    {
        [field: SerializeField] public List<Transform> EnemySpawnPoints { get; private set; } = new();
        [field: SerializeField] public Transform PlayerSpawnPoint { get; private set; }
        [field: SerializeField] public Transform FinishPoint { get; private set; }
    }
}