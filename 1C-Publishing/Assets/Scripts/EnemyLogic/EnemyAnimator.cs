using BaseUnit;
using UnityEngine;

namespace EnemyLogic
{
    public class EnemyAnimator : MonoBehaviour, IAnimator
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Animator _weaponAnimator;

        private readonly int _isRunHash = Animator.StringToHash("IsRun");
        private readonly int _isDieHash = Animator.StringToHash("Die");
        private readonly int _explosionHash = Animator.StringToHash("Explosion");
        
        public void PlayRun()
        {
            _animator.SetBool(_isRunHash, true);
        }

        public void StopRun()
        {
            _animator.SetBool(_isRunHash, false);
        }

        public void PlayDie()
        {
            _animator.SetTrigger(_isDieHash);
        }

        public void PlayExplosion()
        {
            _weaponAnimator.SetTrigger(_explosionHash);
        }
    }
}