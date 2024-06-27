using UnityEngine;
using Zenject;

public class AutomaticRopeActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioClip generateClip;
    private bool ropeGenerated;
    private RopeBasic rope;
    private AudioSource source;
    private Animator animator;

    private void Awake()
    {
        rope = GetComponentInParent<RopeBasic>();
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    public void OnInteract()
    {
        if (ropeGenerated) return;

        source.PlayOneShot(generateClip);
        ropeGenerated = true;
        rope.GenerateRope();
        animator.SetTrigger("FadeOut");
    }

}
