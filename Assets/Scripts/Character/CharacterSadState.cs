using UnityEngine;
using Zenject;

public class CharacterSadState : ICharacterState
{
    private Character character;
    private Animator animator;
    private AudioSource source;
    [Inject(Id = "Sad_Clip")] private AudioClip sadClip;

    public CharacterSadState(Character character)
    {
        this.character = character;
        animator = character.GetComponent<Animator>();
        source = character.GetComponent<AudioSource>();
    }
    public void OnEnter()
    {
        animator.SetTrigger("Sad");
        source.PlayOneShot(sadClip);
    }
    public void OnExit()
    {
    }
    public void OnUpdate()
    {
    }
}
