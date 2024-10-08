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

    [Space(5)]
    [Header("Mobile Controls")]
    [SerializeField] private MobileControls _mobileControls;

    public override void InstallBindings()
    {
        BindStats();
        BindInputHandler();
        BindGameConfig();
        BindAudioMixer();
        BindPlayerSpawner();
        BindMobileInputContainer();
        BindMobileControls();
    }

    private void BindAudioMixer()
    {
        Container.Bind<AudioMixer>().FromInstance(_audioMixer).AsSingle().NonLazy();
    }
    private void BindGameConfig()
    {
        Container.Bind<GameConfig>().FromInstance(_gameConfig).AsSingle().NonLazy();
    }
    private void BindPlayerSpawner()
    {
        Container.Bind<PlayerSpawner>().FromInstance(_playerSpawner).AsSingle().NonLazy();
    }
    private void BindMobileInputContainer()
    {
        Container.Bind<MobileInputContainer>().FromInstance(_mobileInputContainer).AsSingle().NonLazy();
    }

    private void BindMobileControls()
    {
        Container.Bind<MobileControls>().FromInstance(_mobileControls).AsSingle().NonLazy();
    }
    private void BindStats()
    {
        StatsContainer container = new StatsContainer(100, 100, 100, 100, 0, 0);
        var stats = new PlayerStats(container);
        Container.Bind<PlayerStats>().FromInstance(stats).AsSingle().NonLazy();
    }
    private void BindInputHandler()
    {
        Container.Bind<InputHandler>().To<PcInputHandler>().FromNew().AsSingle().NonLazy();
    }
}

