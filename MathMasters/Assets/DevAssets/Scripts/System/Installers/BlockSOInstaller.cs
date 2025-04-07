using MathMasters;
using MathMasters.Entities;
using UnityEngine;
using Zenject;

public class BlockSOInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
       Container.Bind<BlockProvider>().AsSingle();
    }
}