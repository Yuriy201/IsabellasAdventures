using InputSystem;
using UnityEngine;
using Zenject;
using Player;
using UnityEngine.Audio;
using TMPro;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private MobileInputContainer _mobileInputContainer;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private PlayerSpawner _playerSpawner;

    public override void InstallBindings()
    {
        BindStats();
        BindInputHandler();
        BindAudioMixer();
        BindPlayerSpawner();
        BindMobileInputContainer();
    }

    private void BindAudioMixer()
    {
        Container.Bind<AudioMixer>().FromInstance(_audioMixer).AsSingle().NonLazy();
    }
    private void BindPlayerSpawner()
    {
        Container.Bind<PlayerSpawner>().FromInstance(_playerSpawner).AsSingle().NonLazy();
    }
    private void BindMobileInputContainer()
    {
        Container.Bind<MobileInputContainer>().FromInstance(_mobileInputContainer).AsSingle().NonLazy();
    }
    private void BindStats()
    {
        StatsContainer container = new StatsContainer(100, 100, 100, 100, 0, 0);
        var stats = new PlayerStats(container);
        Container.Bind<PlayerStats>().FromInstance(stats).AsSingle().NonLazy();
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

