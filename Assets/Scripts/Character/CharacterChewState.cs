using UnityEngine;
using Zenject;

public class CharacterChewState : ICharacterState
{
    private Animator animator;
    private SignalBus signalBus;
    private AudioSource source;
    [Inject(Id = "Chew_Clip")] private AudioClip chewClip;
    public CharacterChewState(Character character, Candy candy, SignalBus signalBus)
    {
        this.signalBus = signalBus;
        animator = character.GetComponent<Animator>();
        source = character.GetComponent<AudioSource>();
    }
    public void OnEnter()
    {
        source.PlayOneShot(chewClip);
        animator.SetBool("Chew", true);
        signalBus.TryFire<LevelFinishedSignal>();
    }
    public void OnExit()
    {
    }
    public void OnUpdate()
    {
    }
}
