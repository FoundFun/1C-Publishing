using BaseUnit;
using UnityEngine;

namespace States.Player
{
    public class PlayerMovingState : UnitStateBase
    {
        private Vector2 _direction;
        
        public PlayerMovingState(Unit unit) : base(unit) { }
        
        public override void OnEnter()
        {
            _unit.Animator.StopRun();
        }

        public override void OnUpdate()
        {
            _direction = _unit.InputService.Player.Move.ReadValue<Vector2>();

            if (_direction != Vector2.zero)
            {
                _unit.Animator.PlayRun();
            }
            else
            {
                _unit.Animator.StopRun();
            }
            
            Vector3 worldBottomLeft = _unit.Camera.ViewportToWorldPoint(new Vector3(0.1f, 0.1f, _unit.Camera.nearClipPlane));
            Vector3 worldTopRight = _unit.Camera.ViewportToWorldPoint(new Vector3(0.9f, 0.9f, _unit.Camera.nearClipPlane));
        
            Vector2 nextPosition = _unit.transform.position;
            nextPosition += _direction * (Time.fixedDeltaTime * _unit.SpeedMovement);
        
            bool isOutOfBoundsX = nextPosition.x < worldBottomLeft.x || nextPosition.x > worldTopRight.x;
            bool isOutOfBoundsY = nextPosition.y < worldBottomLeft.y || nextPosition.y > _unit.SpawnContext.FinishPoint.transform.localPosition.y;

            if (isOutOfBoundsX || isOutOfBoundsY)
            {
                nextPosition.x = Mathf.Clamp(nextPosition.x, worldBottomLeft.x, worldTopRight.x);
                nextPosition.y = Mathf.Clamp(nextPosition.y, worldBottomLeft.y, _unit.SpawnContext.FinishPoint.transform.localPosition.y);
            }
  
            _unit.Rigidbody2D.MovePosition(nextPosition);
        }

        public override void OnExit()
        {
            _unit.Animator.StopRun();
        }
    }
}