using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EnemyLogic;
using Factory;
using UI;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace Factories
{
    public class EnemyFactory : GenericFactory<Enemy>
    {
        [SerializeField] private Enemy _prefab;
        [SerializeField] private SpawnConfig _spawnConfig;
        
        private List<Enemy> _enemies = new List<Enemy>();
        
        private SpawnContext _spawnContext;
        private GameStatistic _gameStatistic;
        private WinLosePopup _winLosePopup;
        private int _targetSpawnCount;

        [Inject]
        public void Construct(SpawnContext spawnContext, GameStatistic gameStatistic, WinLosePopup winLosePopup)
        {
            _spawnContext = spawnContext;
            _gameStatistic = gameStatistic;
            _winLosePopup = winLosePopup;
        }

        private IEnumerator Start()
        {
            _targetSpawnCount = Random.Range(_spawnConfig.MinEnemyCount, _spawnConfig.MaxEnemyCount);
            _gameStatistic.SetEnemyScore(_targetSpawnCount);
            
            while (_targetSpawnCount > 0 && !_gameStatistic.IsGameOver)
            {
                int currentCooldownSpawn = Random.Range(_spawnConfig.MinCooldownSpawn, _spawnConfig.MaxCooldownSpawn);
                
                yield return new WaitForSeconds(currentCooldownSpawn);

                int countNotActiveEnemy = _enemies.Count(enemy => !enemy.gameObject.activeSelf);

                if (countNotActiveEnemy > 0)
                {
                    EnableEnemy();
                }
                else
                {
                    Spawn();
                }
            }

            if (_winLosePopup.IsLose)
            {
                DisableEnemy();
            }
            
            yield return new WaitUntil(() => _enemies.Count(enemy => enemy.gameObject.activeSelf) <= 0 && !_winLosePopup.IsLose);
                
            DisableEnemy();

            _winLosePopup.OpenWin();
        }

        private void EnableEnemy()
        {
            ChangeScore();
            
            Enemy enemy = _enemies.First(enemy => !enemy.gameObject.activeSelf);
            
            enemy.Reset();
            
            Vector3 spawnPosition = _spawnContext.EnemySpawnPoints[Random.Range(0, _spawnContext.EnemySpawnPoints.Count)].position;
            enemy.transform.position = spawnPosition;

            var speedMovement = Random.Range(_spawnConfig.MinMovementSpeed, _spawnConfig.MaxMovementSpeed);
            enemy.SetSpeedMovement(speedMovement);
            
            enemy.gameObject.SetActive(true);
        }

        private void Spawn()
        {
            ChangeScore();
            
            Vector3 spawnPosition = _spawnContext.EnemySpawnPoints[Random.Range(0, _spawnContext.EnemySpawnPoints.Count)].position;
            
            Enemy enemy = Create(_prefab, spawnPosition, Quaternion.identity);
            
            var speedMovement = Random.Range(_spawnConfig.MinMovementSpeed, _spawnConfig.MaxMovementSpeed);
            enemy.SetSpeedMovement(speedMovement);
            
            _enemies.Add(enemy);
        }

        private void ChangeScore()
        {
            _targetSpawnCount--;
            _gameStatistic.SetEnemyScore(_targetSpawnCount);
        }

        private void DisableEnemy()
        {
            foreach (Enemy enemy in _enemies)
            {
                enemy.gameObject.SetActive(false);
            }
        }
    }
}