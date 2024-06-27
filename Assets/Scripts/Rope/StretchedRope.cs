using UnityEngine;
using Zenject;

public class StretchedRope : RopeBasic
{
    [SerializeField] private StretchedRope[] neighbourRopes;
    [SerializeField] private Color releaseColor;
    [Header("Force")]
    [SerializeField] private float forceStrength;
    private bool released;
    private SignalBus signalBus;

    [Inject]
    public void Constrcut(SignalBus signalBus)
    {
        this.signalBus = signalBus;
        signalBus.Subscribe<CandyDestroyedSignal>(OnCandyDestroy);
    }
    private new void Awake()
    {
        base.Awake();
    }

    public override void DestroyRope()
    {
        if (!released)
        {
            ApplyForce();
        }

        base.DestroyRope();
    }
    public void Release()
    {
        if (released) return;

        released = true;

        foreach(GameObject link in ropeLinks)
        {
            LineRenderer lineRenderer = link.GetComponent<LineRenderer>();
            lineRenderer.startColor = releaseColor;
            lineRenderer.endColor = releaseColor;
        }
    }
    private void ApplyForce()
    {
        Vector2 forceDirection = (candy.transform.position - transform.position).normalized;
        Rigidbody2D candyRb = candy.GetComponent<Rigidbody2D>();
        candyRb.AddForce(forceDirection * forceStrength, ForceMode2D.Impulse);
        Release();
        foreach(StretchedRope rope in neighbourRopes)
        {
            rope.Release();
        }
    }

    private void OnCandyDestroy()
    {
        Release();

        foreach (StretchedRope rope in neighbourRopes)
        {
            rope.Release();
        }
    }
}
