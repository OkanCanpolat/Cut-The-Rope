using System.Collections;
using UnityEngine;
using Zenject;

public class SpiderRope : RopeBasic
{
    [Header("Spider")]
    [SerializeField] private GameObject spider;
    [SerializeField] private float travelTimeToEachLink;
    [SerializeField] private float spiderDestroyForce;
    [SerializeField] private AudioClip winClip;
    [SerializeField] private AudioClip loseClip;
    private Coroutine movementC;
    private Animator spiderAnimator;
    private SignalBus signalBus;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        this.signalBus = signalBus;
    }
    private new void Awake()
    {
        base.Awake();
        spiderAnimator = spider.GetComponent<Animator>();
    }

    public override void GenerateRope()
    {
        base.GenerateRope();
        StartCoroutine(StartMovement());
    }

    private IEnumerator StartMovement()
    {
        spiderAnimator.SetTrigger("WakeUp");
        yield return spiderAnimator.WaitForAnimation("WakeUp");
        movementC = StartCoroutine(SpiderMovemenet());
    }
    public override void DestroyRope()
    {
        base.DestroyRope();
        StopCoroutine(movementC);
        DestroySpider();
    }
    private IEnumerator SpiderMovemenet()
    {
        spiderAnimator.SetTrigger("Walk");
        int linkIndex = 0;
        Vector3 spiderSclae = spider.transform.localScale;

        while (linkIndex < linkCount)
        {
            float t = 0;
            Transform parent = ropeLinks[linkIndex].transform;
            spider.transform.SetParent(parent);
            spider.transform.localScale = new Vector3(spiderSclae.x / parent.localScale.x, spiderSclae.y / parent.localScale.y, spiderSclae.z / parent.localScale.z);
            spider.transform.localRotation = Quaternion.Euler(Vector3.zero);
            Transform start = ropeLinks[linkIndex].GetComponent<HingeJoint2D>().connectedBody.transform;
            Transform end = ropeLinks[linkIndex].transform;

            while (t < 1)
            {
                Vector3 startPos = start.position;
                Vector3 endPos = end.position;
                t += Time.deltaTime / travelTimeToEachLink;
                spider.transform.position = Vector2.Lerp(startPos, endPos, t);
                yield return null;
            }

            spider.transform.position = end.position;
            linkIndex++;
        }

        StartCoroutine(OnSpiderWin());
    }
    private void DestroySpider()
    {
        spider.transform.parent = null;
        Rigidbody2D spiderRb = spider.GetComponent<Rigidbody2D>();
        spiderRb.simulated = true;
        spiderRb.AddForce(Vector2.up * spiderDestroyForce);
        spiderAnimator.SetTrigger("Lose");
        source.PlayOneShot(loseClip);
        Destroy(spider, 10f);
    }
    private IEnumerator OnSpiderWin()
    {
        candy.transform.parent = spider.transform;
        candy.transform.position = spider.transform.position + spider.transform.up / 2f;
        candy.GetComponent<Rigidbody2D>().simulated = false;
        candy.GetComponent<Collider2D>().enabled = false;
        spider.transform.parent = null;
        Rigidbody2D spiderRb = spider.GetComponent<Rigidbody2D>();
        spiderRb.simulated = true;
        spiderRb.AddForce(Vector2.up * spiderDestroyForce);
        spiderAnimator.SetTrigger("Win");
        source.PlayOneShot(winClip);
        Destroy(spider, 10f);
        signalBus.TryFire<CandyDestroyedSignal>();
        yield return new WaitForSeconds(2f);
        signalBus.TryFire<LevelFailedSignal>();
    }
}
