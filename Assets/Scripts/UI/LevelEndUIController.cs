using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

public class LevelEndUIController : MonoBehaviour
{
    [SerializeField] private GameObject[] stars;
    [SerializeField] private TMP_Text endLevelText;
    [SerializeField] private TMP_Text scorText;
    [SerializeField] private int score2PerStar;
    [SerializeField] private GameObject fadeObject;
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private AudioClip winClip;
    private string[] endLevelTexts = new string[] { "Bad!", "Good!", "Nice!", "Excellent!" };
    private SignalBus signalBus;
    private int collectedStarCount;
    private AudioSource source;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        this.signalBus = signalBus;
    }

    private void Awake()
    {
        signalBus.Subscribe<StarCollectSignal>(OnCollectStar);
        signalBus.Subscribe<LevelFinishedSignal>(OnLevelFinish);
        source = GetComponent<AudioSource>();
    }
    private void Start()
    {
        StartCoroutine(FadeOut());
    }
    private void OnCollectStar()
    {
        collectedStarCount++;
    }
    private void OnLevelFinish()
    {
        int score = score2PerStar * collectedStarCount;
        scorText.text = score.ToString();

        endLevelText.text = endLevelTexts[collectedStarCount];

        for (int i = 0; i < collectedStarCount; i++)
        {
            stars[i].SetActive(true);
        }

        StartCoroutine(OpenFinishUI());
    }

    private IEnumerator OpenFinishUI()
    {
        Animator fadeAnimator = fadeObject.GetComponent<Animator>();
        yield return new WaitForSeconds(1f);
        fadeObject.SetActive(true);
        fadeAnimator.SetTrigger("FadeIn");
        yield return fadeAnimator.WaitForAnimation("FadeIn");
        source.PlayOneShot(winClip);
        uiPanel.SetActive(true);
    }
    private IEnumerator FadeOut()
    {
        Animator fadeAnimator = fadeObject.GetComponent<Animator>();
        fadeObject.SetActive(true);
        fadeAnimator.SetTrigger("FadeOut");
        yield return fadeAnimator.WaitForAnimation("FadeOut");
        fadeObject.SetActive(false);
    }
}
