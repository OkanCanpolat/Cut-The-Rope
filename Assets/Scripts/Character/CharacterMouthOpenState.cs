using UnityEngine;
using Zenject;

public class CharacterMouthOpenState : ICharacterState
{
    private Character character;
    private float mouthCloseDistance;
    private float eatDistance;
    private Candy candy;
    private Animator animator;
    private SignalBus signalBus;
    private bool candyDestroyed;
    [Inject( Id = "Open_Clip")] private AudioClip openClip;
    [Inject(Id = "Close_Clip")] private AudioClip closeClip;
    private AudioSource source;
    [Inject(Id = "Idle")] private ICharacterState idleState;
    [Inject(Id = "Chew")] private ICharacterState chewState;


    public CharacterMouthOpenState(Character character, float mouthCloseDistance, float eatDistance, Candy candy, SignalBus signalBus)
    {
        this.character = character;
        this.mouthCloseDistance = mouthCloseDistance;
        this.eatDistance = eatDistance;
        this.candy = candy;
        this.signalBus = signalBus;

        animator = character.GetComponent<Animator>();
        source = character.GetComponent<AudioSource>();

        signalBus.Subscribe<CandyDestroyedSignal>(OnCandyDestroyed);
    }
    public void OnEnter()
    {
        animator.SetBool("OpenMouth", true);
        source.PlayOneShot(openClip);
    }
    public void OnExit()
    {
        animator.SetBool("OpenMouth", false);
    }
    public void OnUpdate()
    {
        if (candyDestroyed) return;

        if (Vector2.Distance(character.transform.position, candy.transform.position) > mouthCloseDistance)
        {
            character.StateMachine.ChangeState(idleState);
            source.PlayOneShot(closeClip);
            return;
        }

        if (Vector2.Distance(character.transform.position, candy.transform.position) <= eatDistance)
        {
            Object.Destroy(candy.gameObject);
            character.StateMachine.ChangeState(chewState);
            return;
        }
    }

    private void OnCandyDestroyed()
    {
        candyDestroyed = true;
    }
}
