using System.Collections;
using UnityEngine;
using Zenject;

public class Star : MonoBehaviour, IInteractable
{
    private Animator animator;
    private SignalBus signalBus;
    private bool interacted;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        this.signalBus = signalBus;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void OnInteract()
    {
        if (interacted) return;
        interacted = true;
        StartCoroutine(Disappear());
    }
    
    private IEnumerator Disappear()
    {
        signalBus.TryFire<StarCollectSignal>();
        animator.SetTrigger("Disappear");
        yield return animator.WaitForAnimation("Disappear");
        Destroy(gameObject);
    }
}
