using Factory;
using PlayerLogic;
using UnityEngine;
using VContainer;

namespace Factories
{
    public class PlayerFactory : GenericFactory<Player>
    {
        [SerializeField] private Player _prefab;

        private SpawnContext _spawnContext;

        [Inject]
        public void Construct(SpawnContext spawnContext)
        {
            _spawnContext = spawnContext;
        }

        private void Start()
        {
            Spawn();
        }

        private void Spawn()
        {
            Create(_prefab, _spawnContext.PlayerSpawnPoint.position, Quaternion.identity);
        }
    }
}