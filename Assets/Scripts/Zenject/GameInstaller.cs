using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<StarCollectSignal>().OptionalSubscriber();
        Container.DeclareSignal<LevelFinishedSignal>().OptionalSubscriber();
        Container.DeclareSignal<LevelFailedSignal>().OptionalSubscriber();
        Container.DeclareSignal<CandyDestroyedSignal>().OptionalSubscriber();

        Container.Bind<Candy>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
    }
}

public class StarCollectSignal { }
public class LevelFinishedSignal { }
public class LevelFailedSignal { }
public class CandyDestroyedSignal { }