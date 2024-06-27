using UnityEngine;
using Zenject;

public class StarUIController : MonoBehaviour
{
    [SerializeField] private GameObject[] starsImages;
    [SerializeField] private AudioClip[] starCollectSounds;
    private int collectedStarCount;
    private SignalBus signalBus;
    private AudioSource source;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        this.signalBus = signalBus;
    }

    private void Awake()
    {
        signalBus.Subscribe<StarCollectSignal>(OnStarCollected);
        source = GetComponent<AudioSource>();
    }

    private void OnStarCollected()
    {
        collectedStarCount++;

        GameObject currentStar = starsImages[collectedStarCount - 1];
        currentStar.GetComponent<Animator>().SetTrigger("OnCollect");
        source.PlayOneShot(starCollectSounds[collectedStarCount - 1]);
    }

}
