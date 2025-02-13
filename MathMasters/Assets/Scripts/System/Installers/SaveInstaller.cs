using MathMasters;
using UnityEngine;
using Zenject;

public class SaveInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ISaver>().To<SaverWithPlayerPrefs>().AsSingle();
    }
}