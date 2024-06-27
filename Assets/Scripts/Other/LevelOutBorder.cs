using UnityEngine;
using Zenject;

public class LevelOutBorder : MonoBehaviour, IInteractable
{
    private SignalBus signalBus;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        this.signalBus = signalBus;
    }

    public void OnInteract()
    {
        signalBus.TryFire<LevelFailedSignal>();
    }

}
