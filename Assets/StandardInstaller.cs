using UnityEngine;
using Zenject;
using GeomCatch;

public class StandardInstaller : MonoInstaller
{
    [SerializeField] private EventManager eventManagerInstance;

    public override void InstallBindings()
    {
        Container.Bind<EventManager>().FromInstance(eventManagerInstance).AsSingle();
    }
}