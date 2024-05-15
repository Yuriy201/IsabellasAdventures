using UnityEngine;
using UnityEngine.Audio;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private AudioMixer _playerAudioMixer;
    [SerializeField] private GameObject _player;
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private EnemyStats _enemyStats;
    
    public override void InstallBindings()
    {
        Container.Bind<InputHandler>().To<PcInputHandler>().FromNew().AsSingle().NonLazy();
        Container.Bind<EnemyStats>().FromInstance(_enemyStats).AsSingle().NonLazy();
        Container.Bind<GameObject>().FromInstance(_player).AsSingle().NonLazy();
        Container.Bind<PlayerStats>().FromInstance(_playerStats).AsSingle().NonLazy();
        Container.Bind<AudioMixer>().FromInstance(_playerAudioMixer).AsSingle().NonLazy();
    }
}

