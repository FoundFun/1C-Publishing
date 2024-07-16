using BaseStateMachine;
using Factories;
using States;
using UI;
using UnityEngine;
using VContainer;

namespace BaseUnit
{
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private UnitConfig _unitConfig;
        
        private StateMachine _stateMachine;
        private SpawnContext _spawnContext;
        private GameStatistic _gameStatistic;
        private InputService _inputService;
        private Camera _camera;
        private IAnimator _animator;
        private float _speedMovement;
        private int _health;
        
        public UnitStateBase KilledState { get; protected set; }
        protected UnitStateBase MovingState { get; set; }

        public IState CurrentState => _stateMachine.CurrentState;
        public IAnimator Animator => _animator;
        public Rigidbody2D Rigidbody2D => _rigidbody2D;
        public SpawnContext SpawnContext => _spawnContext;
        public GameStatistic GameStatistic => _gameStatistic;
        public InputService InputService => _inputService;
        public Camera Camera => _camera;
        public float SpeedMovement => _speedMovement;
        public bool IsALive => _health > 0;
        protected int Health => _health;

        [Inject]
        public void Construct(SpawnContext spawnContext, GameStatistic gameStatistic, 
            Camera camera, InputService inputService)
        {
            _spawnContext = spawnContext;
            _gameStatistic = gameStatistic;
            _camera = camera;
            _inputService = inputService;
        }

        private void Awake()
        {
            _animator = InitAnimator();
            InitializeStates();
            InitializeConfigValue();
        }

        private void FixedUpdate()
        {
            _stateMachine?.OnUpdate();
        }

        public void Reset()
        {
            InitializeStates();
            InitializeConfigValue();
        }

        public void ChangeState(IState state)
        {
            _stateMachine.ChangeState(state);
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
            {
                return;
            }
            
            _health -= ChangeDamageTake(damage);

            if (_health <= 0)
            {
                ChangeState(KilledState);
            }
        }

        public void SetSpeedMovement(float speedMovement)
        {
            _speedMovement = speedMovement;
        }

        protected abstract IAnimator InitAnimator();

        public abstract void PreDie();

        protected abstract void OnInitializeStates();

        protected virtual int ChangeDamageTake(int damage)
        {
            return damage;
        }

        protected virtual void InitializeConfigValue()
        {
            _speedMovement = _unitConfig.SpeedMovement;
            _health = _unitConfig.Health;
        }

        private void InitializeStates()
        {
            _stateMachine = new StateMachine();

            OnInitializeStates();

            _stateMachine.ChangeState(MovingState);
        }
    }
}