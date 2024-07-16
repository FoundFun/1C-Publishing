using BaseUnit;
using UnityEngine;

namespace States.Enemy
{
    public class EnemyMovingState : UnitStateBase
    {
        public EnemyMovingState(Unit unit) : base(unit) { }

        public override void OnEnter()
        {
            _unit.Animator.PlayRun();
        }

        public override void OnUpdate()
        {
            Vector2 direction = (new Vector2(_unit.Rigidbody2D.position.x, _unit.SpawnContext.FinishPoint.position.y) - _unit.Rigidbody2D.position).normalized;
            Vector2 nextPosition = _unit.Rigidbody2D.position + direction * _unit.SpeedMovement * Time.fixedDeltaTime;

            if (Mathf.Abs(_unit.Rigidbody2D.position.y - _unit.SpawnContext.FinishPoint.position.y) > 0.1f && _unit.IsALive)
            {
                _unit.Animator.PlayRun();
                _unit.Rigidbody2D.MovePosition(nextPosition);
            }
            else
            {
                if (_unit.CurrentState != _unit.KilledState)
                {
                    _unit.Animator.StopRun();
                    _unit.ChangeState(_unit.KilledState);
                }
            }
        }

        public override void OnExit()
        {
            _unit.Animator.StopRun();
        }
    }
}