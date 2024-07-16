using BaseUnit;
using UnityEngine;

namespace PlayerLogic
{
    public class PlayerAnimator : MonoBehaviour, IAnimator
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Animator _weaponAnimator;
        [SerializeField] private Animator _fireAnimator;

        private readonly int _isRunHash = Animator.StringToHash("IsRun");
        private readonly int _isDieHash = Animator.StringToHash("Die");
        private readonly int _hideHash = Animator.StringToHash("Hide");
        private readonly int _shootHash = Animator.StringToHash("Shoot");
        
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

        public void Shoot()
        {
            _fireAnimator.SetTrigger(_shootHash);
        }

        public void HideWeapon()
        {
            _weaponAnimator.SetTrigger(_hideHash);
        }
    }
}