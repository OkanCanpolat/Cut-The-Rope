using UnityEngine;
using Zenject;

public class CharacterIdleState : ICharacterState
{
    private float openMouthDistance;
    private Candy candy;
    private Character character;
    private Animator animator;
    private SignalBus signalBus;
    private bool candyDestroyed;
    [Inject(Id = "MouthOpen")] private ICharacterState mouthOpenState;
    [Inject(Id = "Sad")] private ICharacterState sadState;

    public CharacterIdleState(Character character, Candy candy, float openMouthDistance, SignalBus signalBus)
    {
        this.signalBus = signalBus;
        this.character = character;
        this.candy = candy;
        this.openMouthDistance = openMouthDistance;
        animator = character.GetComponent<Animator>();

        signalBus.Subscribe<LevelFailedSignal>(OnLevelFailed);
        signalBus.Subscribe<CandyDestroyedSignal>(OnCandyDestroyed);
    }
    public void OnEnter()
    {
        animator.SetTrigger("Idle");
    }
    public void OnExit()
    {
        animator.ResetTrigger("Idle");
    }
    public void OnUpdate()
    {
        if (candyDestroyed) return;

        if (Vector2.Distance(character.transform.position, candy.transform.position) < openMouthDistance)
        {
            character.StateMachine.ChangeState(mouthOpenState);
        }
    }

    private void OnLevelFailed()
    {
        character.StateMachine.ChangeState(sadState);
    }
    private void OnCandyDestroyed()
    {
        candyDestroyed = true;
    }
}
