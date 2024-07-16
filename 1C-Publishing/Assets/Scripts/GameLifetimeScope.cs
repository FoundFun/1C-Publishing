using Factories;
using UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private Camera _camera;
    [SerializeField] private SpawnContext _spawnContext;
    [SerializeField] private PlayerFactory _playerFactory;
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private GameStatistic _gameStatistic;
    [SerializeField] private WinLosePopup _winLosePopup;

    private InputService _inputService;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<IObjectResolver, Container>(Lifetime.Scoped);
            
        builder.RegisterInstance(_camera);
        builder.RegisterInstance(_spawnContext);
        builder.RegisterInstance(_winLosePopup);

        builder.RegisterComponent(_gameStatistic);
        builder.RegisterComponent(_playerFactory);
        builder.RegisterComponent(_enemyFactory);

        InitializeInput(builder);
    }

    private void InitializeInput(IContainerBuilder builder)
    {
        _inputService = new InputService();
        _inputService.Enable();
            
        builder.RegisterInstance(_inputService);
    }
}