using MathMasters.Services;
using Zenject;

public class SceneNavigatorInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ISceneNavigator>().To<SceneNavigator>().AsSingle();
    }
}