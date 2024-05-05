using UnityEngine;
using UnityEngine.Audio;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private AudioMixer _PlayerAudioMixer;
    
    public override void InstallBindings()
    {
        //Container.Bind<InputHandler>().To<PcInputHandler>().FromNew().AsSingle().NonLazy();
        Container.Bind<AudioMixer>().FromInstance(_PlayerAudioMixer).AsSingle().NonLazy();
    }
}

