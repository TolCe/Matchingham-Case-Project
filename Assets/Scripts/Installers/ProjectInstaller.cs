using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        InstallSignalBus();

        Container.BindInterfacesAndSelfTo<InputManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<CameraManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<SaveManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<LevelManager>().AsSingle();

        Container.BindInterfacesAndSelfTo<MergeManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<MergeObjectsMovementManager>().AsSingle();

        Container.BindInterfacesAndSelfTo<SuccessChecker>().AsSingle();
        Container.BindInterfacesAndSelfTo<TimeManager>().AsSingle();

        Container.BindInterfacesAndSelfTo<TilesManager>().AsSingle();
    }

    public void InstallSignalBus()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<GameSignals.CallInputStart>();
        Container.DeclareSignal<GameSignals.CallInputRelease>();

        Container.DeclareSignal<GameSignals.CallLevelLoaded>();
        Container.DeclareSignal<GameSignals.CallLevelEnd>();
    }
}