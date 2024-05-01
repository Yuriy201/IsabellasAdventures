using UnityEngine;
using UnityEngine.Audio;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private PlatformType _platformType;
    [SerializeField] private AudioMixer _PlayerAudioMixer;
    
    public override void InstallBindings()
    {
        Container.Bind<InputHandler>().To<PcInputHandler>().FromNew().AsSingle().NonLazy();
        Container.Bind<AudioMixer>().FromInstance(_PlayerAudioMixer).AsSingle().NonLazy();
    }

    public enum PlatformType
    {
        PC,
        Mobile,
        Console,
        MultiplayYG,
        MultiplayerGamePush
    }
}

