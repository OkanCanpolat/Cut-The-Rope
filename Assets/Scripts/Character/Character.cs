using UnityEngine;
using Zenject;

public class Character : MonoBehaviour
{
    public CharacterStateMachine StateMachine;

    [Inject]
    public void Construct(CharacterStateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }
    private void Update()
    {
        StateMachine.CurrentState.OnUpdate();
    }

}
