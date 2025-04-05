using MathMasters;
using MathMasters.Services;
using UnityEngine;
using Zenject;

public class AddressablesInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ILoadRemoteAddressables>().To<LoadRemoteAddressables>().AsSingle();
    }
}