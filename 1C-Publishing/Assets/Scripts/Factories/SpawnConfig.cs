using UnityEngine;

namespace Factories
{
    [CreateAssetMenu(fileName = "SpawnConfig", menuName = "SpawnConfig", order = 0)]
    public class SpawnConfig : ScriptableObject
    {
        [field: SerializeField] public int MinEnemyCount { get; private set; } = 8;
        [field: SerializeField] public int MaxEnemyCount { get; private set; } = 16;
        [field: SerializeField] public int MinCooldownSpawn { get; private set; } = 1;
        [field: SerializeField] public int MaxCooldownSpawn { get; private set; } = 3;
        [field: SerializeField] public int MinMovementSpeed { get; private set; } = 1;
        [field: SerializeField] public int MaxMovementSpeed { get; private set; } = 4;
    }
}