using MathMasters;
using Zenject;

public class FirebaseInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IAuthService>().To<FirebaseAuthService>().AsSingle().NonLazy();
    }
}