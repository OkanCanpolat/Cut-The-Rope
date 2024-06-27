using System.Collections;
using UnityEngine;
using Zenject;

public class AirCushion : MonoBehaviour, IRaycastTarget
{
    [Header("Pump")]
    [SerializeField] private LayerMask pumpTargetLayer;
    [SerializeField] private AudioClip[] pumpClips;
    [SerializeField] private Transform cushionMouth;
    [SerializeField] private float pumpDistance;
    [SerializeField] private float pumpForce;

    [Header("Bubble")]
    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private float bubbleTravelTime;

    private Animator animator;
    private AudioSource source;
    private Rigidbody2D candyRb;

    [Inject]
    public void Construct(Candy candy)
    {
        candyRb = candy.GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    public void OnHit()
    {
        animator.SetTrigger("Pump");
        PlayRandomClip();
        ApplyForce();
        GenerateBubble();
    }

    private void PlayRandomClip()
    {
        int index = Random.Range(0, pumpClips.Length);
        source.PlayOneShot(pumpClips[index]);
    }
    private void GenerateBubble()
    {
        GameObject bubble = Instantiate(bubblePrefab, cushionMouth.position, Quaternion.identity);
        StartCoroutine(MoveBubble(bubble));
    }
    private IEnumerator MoveBubble(GameObject bubble)
    {
        float t = 0;
        Vector3 startPos = bubble.transform.position;
        Vector3 targetPos = startPos + (cushionMouth.up * pumpDistance);

        SpriteRenderer spriteRenderer = bubble.GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;

        while (t < 1)
        {
            yield return null;
            t += Time.deltaTime / bubbleTravelTime;
            bubble.transform.position = Vector2.Lerp(startPos, targetPos, t);
            color.a = 1 - t;
            spriteRenderer.color = color;
        }

        Destroy(bubble);
    }
    private void ApplyForce()
    {
        RaycastHit2D hit = Physics2D.Raycast(cushionMouth.position, cushionMouth.up, pumpDistance, pumpTargetLayer);
        RaycastHit2D hit2 = Physics2D.BoxCast(cushionMouth.position + cushionMouth.up * pumpDistance / 2,
            new Vector2(0.3f, pumpDistance), transform.eulerAngles.z, cushionMouth.up, 0f, pumpTargetLayer);

        if (hit2)
        {
            candyRb.AddForce(cushionMouth.up * pumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(cushionMouth.localPosition + cushionMouth.localPosition.normalized * pumpDistance / 2, new Vector2(0.3f, pumpDistance));
    }
}
