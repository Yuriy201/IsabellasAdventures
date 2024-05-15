using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private GameConfig _gameConfig;

    [Header("Player")]
    [SerializeField] private Player _playerGameObject;

    [Header("Input")]
    [SerializeField] private MobileInputContainer _mobileInputContainer;
    
    public override void InstallBindings()
    {
        BindStats();
        BindPlayer();
        BindInputHandler();
    }

    private void BindStats()
    {
        var stats = new PlayerStats(10, 10, 5, 5);
        Container.Bind<PlayerStats>().FromInstance(stats).AsSingle().NonLazy();
    }

    private void BindPlayer()
    {
        Container.Bind<Player>().FromInstance(_playerGameObject).AsSingle().NonLazy();
    }

    private void BindInputHandler()
    {
        switch (_gameConfig.PlatfotmType)
        {
            case PlatfotmType.PC:
                Container.Bind<InputHandler>().To<PcInputHandler>().FromNew().AsSingle().NonLazy();
                break;
            case PlatfotmType.Mobile:
                InputHandler inputHandler = new MobileInputHandler(_mobileInputContainer);
                Container.Bind<InputHandler>().FromInstance(inputHandler).AsSingle().NonLazy();
                break;
        }
    }
}

