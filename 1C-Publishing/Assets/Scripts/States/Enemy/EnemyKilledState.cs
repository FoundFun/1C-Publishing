using System;
using System.Threading;
using BaseUnit;
using Cysharp.Threading.Tasks;

namespace States.Enemy
{
    public class EnemyKilledState : UnitStateBase
    {
        private CancellationTokenSource _cancellationTokenSource;
        
        public EnemyKilledState(Unit unit) : base(unit)
        {
        }

        public override void OnEnter()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();

            _cancellationTokenSource = new CancellationTokenSource();
            
            Die(_cancellationTokenSource.Token).Forget();
        }

        public override void OnUpdate()
        {
        }

        public override void OnExit()
        {
        }
        
        private async UniTaskVoid Die(CancellationToken token)
        {
            _unit.PreDie();
                    
            if (_unit.IsALive)
            {
                _unit.GameStatistic.RemovePlayerScore();
            }
            
            await UniTask.Delay(TimeSpan.FromSeconds(1.5f), cancellationToken: token);
            
            _unit.gameObject.SetActive(false);
        }
    }
}