using BaseStateMachine;
using BaseUnit;

namespace States
{
    public abstract class UnitStateBase : IState
    {
        private protected readonly Unit _unit;
        
        protected UnitStateBase(Unit unit)
        {
            _unit = unit;
        }
        
        public abstract void OnEnter();
        public abstract void OnUpdate();
        public abstract void OnExit();
    }
}