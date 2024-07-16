using BaseUnit;
using States.Player;
using UnityEngine;

namespace PlayerLogic
{
    public class Player : Unit
    {
        [SerializeField] private PlayerAnimator _playerAnimator;

        private void OnEnable()
        {
            GameStatistic.PlayerDamaged += OnTakeDamage;
        }

        private void OnDisable()
        {
            GameStatistic.PlayerDamaged -= OnTakeDamage;
        }

        protected override void OnInitializeStates()
        {
            MovingState = new PlayerMovingState(this);
            KilledState = new PlayerKilledState(this);
        }

        protected override void InitializeConfigValue()
        {
            base.InitializeConfigValue();
            GameStatistic.UpdatePlayerHeath(Health);
        }

        protected override IAnimator InitAnimator()
        {
            return _playerAnimator;
        }

        public override void PreDie()
        {
            _playerAnimator.HideWeapon();
            _playerAnimator.PlayDie();
        }

        private void OnTakeDamage()
        {
            TakeDamage(1);
            GameStatistic.UpdatePlayerHeath(Health);
        }
    }
}