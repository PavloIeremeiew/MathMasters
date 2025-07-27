using UnityEngine;
using Zenject;

public class MenuInstaller : MonoInstaller
{
    [SerializeField]
    private Canvas _canvas;
    public override void InstallBindings()
    {
        Container.Bind<Canvas>()
            .FromComponentInNewPrefab(_canvas)
            .AsSingle()
            .NonLazy();
    }
}