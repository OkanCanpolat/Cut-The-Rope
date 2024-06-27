using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class Bubble : MonoBehaviour, IInteractable, IRaycastTarget, ICandyAttachment
{
    [SerializeField] private LayerMask attachedLayer;
    [SerializeField] private Collider2D attachedCollider;
    [SerializeField] private Collider2D initialCollider;
    [SerializeField] private AudioClip attachmentClip;
    [SerializeField] private AudioClip popClip;
    private Animator animator;
    private Candy candy;
    private float lastGravityScale;
    private float lastDrag;
    private AudioSource source;

    [Inject]
    public void Construct(Candy candy)
    {
        this.candy = candy;
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }
    public void OnInteract()
    {
        ICandyAttachment attachment = candy.TryGetAttachment(GetType());

        if (attachment != null)
        {
            StartCoroutine(Pop());
            return;
        }

        candy.AddAttachment(this);
        source.PlayOneShot(attachmentClip);
    }
    public void OnHit()
    {
        attachedCollider.enabled = false;

        Rigidbody2D rb = candy.GetComponent<Rigidbody2D>();
        rb.gravityScale = lastGravityScale;
        rb.drag = lastDrag;

        candy.RemoveAttachment(this);

        StartCoroutine(Pop());
    }
    public void OnAttach()
    {
        animator.SetTrigger("Flight");

        transform.SetParent(candy.transform);
        transform.localPosition = Vector3.zero;

        initialCollider.enabled = false;
        attachedCollider.enabled = true;

        gameObject.layer = LayerMask.NameToLayer("RaycastTarget");

        Rigidbody2D rb = candy.GetComponent<Rigidbody2D>();
        lastGravityScale = rb.gravityScale;
        lastDrag = rb.drag;
        rb.gravityScale = -0.7f;
        rb.drag = 4f;
    }
    private IEnumerator Pop()
    {
        source.PlayOneShot(popClip);
        animator.SetTrigger("Pop");
        yield return animator.WaitForAnimation("Pop");
        Destroy(gameObject);
    }


}
