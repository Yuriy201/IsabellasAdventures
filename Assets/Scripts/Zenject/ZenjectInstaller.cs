using UnityEngine;
using Zenject;

public class ZenjectInstaller : MonoInstaller
{
    [SerializeField] private InputHandler _InputHandler;
    
    public override void InstallBindings()
    {
        Container.Bind<InputHandler>().FromInstance(_InputHandler).AsSingle().NonLazy();
    }
}