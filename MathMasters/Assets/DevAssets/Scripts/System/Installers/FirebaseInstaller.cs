using MathMasters;
using MathMasters.Services;
using Zenject;

public class FirebaseInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.QueueForInject(this);
        Container.BindInterfacesAndSelfTo<FirebaseAuthService>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<FirebaseSaver>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<FirebaseLeaderboardLoader>().AsSingle().NonLazy();
    }
}