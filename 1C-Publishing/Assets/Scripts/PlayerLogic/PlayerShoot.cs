using EnemyLogic;
using UnityEngine;

namespace PlayerLogic
{
    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D _detectionZone;
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private PlayerAnimator _playerAnimator;
        [SerializeField] private LayerMask _enemyLayer;
        
        private RaycastHit2D[] _results = new RaycastHit2D[3];
        private int _damage;
        private float _reloadTime;
        private float _currentReload;

        private void Awake()
        {
            InitializeConfigValue();
        }

        private void FixedUpdate()
        {
            CheckEnemiesInRange();
        }

        private void InitializeConfigValue()
        {
            _damage = _playerConfig.Damage;
            _reloadTime = _playerConfig.ReloadTime;
            _detectionZone.size = new Vector2(_playerConfig.SizeXAreaShoot, _playerConfig.SizeYAreaShoot);
        }

        private void CheckEnemiesInRange()
        {
            if (_currentReload > 0)
            {
                _currentReload -= Time.deltaTime;
                return;
            }

            var size = Physics2D.BoxCastNonAlloc(_detectionZone.bounds.center, _detectionZone.bounds.size,
                0, Vector2.zero, _results, _detectionZone.bounds.extents.magnitude, _enemyLayer);

            float minDistance = Mathf.Infinity;
            Enemy nearestEnemy = null;

            for (int i = 0; i < size; i++)
            {
                if (_results[i].collider.TryGetComponent(out Enemy enemy))
                {
                    float distance = Vector2.Distance(_detectionZone.bounds.center, _results[i].point);
                    if (distance < minDistance && enemy.IsALive)
                    {
                        minDistance = distance;
                        nearestEnemy = enemy;
                    }
                }
            }

            if (nearestEnemy != null)
            {
                Fire(nearestEnemy);
            }
        }
        
        private void Fire(Enemy target)
        {
            _playerAnimator.Shoot();
            target.TakeDamage(_damage);
            _currentReload = _reloadTime;
        }
    }
}