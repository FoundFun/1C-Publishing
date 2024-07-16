using BaseUnit;
using UnityEngine;

namespace PlayerLogic
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "PlayerConfig", order = 0)]
    public class PlayerConfig : UnitConfig
    {
        [field: SerializeField] public float ReloadTime { get; private set; } = 0.2f;
        [field: SerializeField] public float SizeXAreaShoot { get; private set; } = 2;
        [field: SerializeField] public float SizeYAreaShoot { get; private set; } = 7.5f;
        [field: SerializeField] public int Damage { get; private set; } = 1;
    }
}