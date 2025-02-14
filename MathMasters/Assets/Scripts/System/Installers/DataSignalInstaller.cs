using MathMasters.Entities;
using Zenject;

public class DataSignalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<LevelDataSignal>().AsSingle();
    }
}