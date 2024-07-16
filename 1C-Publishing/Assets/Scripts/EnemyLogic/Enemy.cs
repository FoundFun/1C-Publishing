using BaseUnit;
using States.Enemy;
using UnityEngine;

namespace EnemyLogic
{
    public class Enemy : Unit
    {
        [SerializeField] private ParticleSystem _hitParticleSystem;
        [SerializeField] private EnemyAnimator _enemyAnimator;

        public override void PreDie()
        {
            _enemyAnimator.PlayExplosion();
            _enemyAnimator.PlayDie();
        }

        protected override void OnInitializeStates()
        {
            MovingState = new EnemyMovingState(this);
            KilledState = new EnemyKilledState(this);
        }

        protected override IAnimator InitAnimator()
        {
            return _enemyAnimator;
        }

        protected override int ChangeDamageTake(int damage)
        {
            _hitParticleSystem.Play();
            return base.ChangeDamageTake(damage);
        }
    }
}