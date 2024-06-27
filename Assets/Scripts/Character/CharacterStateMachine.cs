using UnityEngine;
using Zenject;

public class CharacterStateMachine
{
    public ICharacterState CurrentState;

    public CharacterStateMachine([Inject(Id = "Idle")] ICharacterState initialState)
    {
        CurrentState = initialState;
        CurrentState.OnEnter();
    }
    public void ChangeState(ICharacterState state)
    {
        CurrentState?.OnExit();
        CurrentState = state;
        CurrentState.OnEnter();
    }
}
