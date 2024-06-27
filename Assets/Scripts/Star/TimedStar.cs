using UnityEngine;
using Zenject;
using System.Collections;

public class TimedStar : MonoBehaviour, IInteractable
{
    [SerializeField] private Sprite[] timeSprites;
    [SerializeField] private float destroyTime;
    [SerializeField] private SpriteRenderer timerRenderer;
    private Animator animator;
    private SignalBus signalBus;
    private bool interacted;
    private Coroutine timerCoroutine;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        this.signalBus = signalBus;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        timerCoroutine = StartCoroutine(StartTimer());

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
        StopCoroutine(timerCoroutine);
        timerRenderer.enabled = false;
        yield return animator.WaitForAnimation("Disappear");
        Destroy(gameObject);
    }

    private IEnumerator StartTimer()
    {
        int index = 0;
        float timeInterval = destroyTime / timeSprites.Length;

        while (index < timeSprites.Length)
        {
            timerRenderer.sprite = timeSprites[index];
            yield return new WaitForSeconds(timeInterval);
            index++;
        }

        Destroy(gameObject);
    }
}
