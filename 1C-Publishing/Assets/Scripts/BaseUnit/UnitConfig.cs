using UnityEngine;

namespace BaseUnit
{
    [CreateAssetMenu(fileName = "UnitConfig", menuName = "UnitConfig", order = 0)]
    public class UnitConfig : ScriptableObject
    {
        [field: SerializeField] public float SpeedMovement { get; private set; }
        [field: SerializeField] public int Health { get; private set; }
    }
}