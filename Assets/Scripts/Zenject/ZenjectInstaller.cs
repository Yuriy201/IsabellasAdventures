using UnityEngine;
using UnityEngine.Audio;
using Zenject;

public class ZenjectInstaller : MonoInstaller
{
    [SerializeField] private InputHandler _InputHandler;
    [SerializeField] private AudioMixer _PlayerAudioMixer;
    
    public override void InstallBindings()
    {
        Container.Bind<InputHandler>().FromInstance(_InputHandler).AsSingle().NonLazy();
        Container.Bind<AudioMixer>().FromInstance(_PlayerAudioMixer).AsSingle().NonLazy();
    }
}