using UnityEngine;
using Zenject;

public class CharacterInstaller : MonoInstaller
{
    [SerializeField] private Character character;
    [SerializeField] private float mouthOpenCloseDistance = 1.5f;
    [SerializeField] private float eatDistance = 0.5f;

    [Header("Sounds")]
    [SerializeField] private AudioClip chewClip;
    [SerializeField] private AudioClip openClip;
    [SerializeField] private AudioClip closeClip;
    [SerializeField] private AudioClip sadClip;

    public override void InstallBindings()
    {
        Container.Bind<Character>().FromInstance(character).AsSingle();
        Container.Bind<CharacterStateMachine>().AsSingle();
        Container.Bind<ICharacterState>().WithId("Idle").To<CharacterIdleState>().AsSingle().WithArguments(mouthOpenCloseDistance);
        Container.Bind<ICharacterState>().WithId("MouthOpen").To<CharacterMouthOpenState>().AsSingle().WithArguments(mouthOpenCloseDistance, eatDistance);
        Container.Bind<ICharacterState>().WithId("Chew").To<CharacterChewState>().AsSingle();
        Container.Bind<ICharacterState>().WithId("Sad").To<CharacterSadState>().AsSingle();

        Container.Bind<AudioClip>().WithId("Open_Clip").FromInstance(openClip).AsCached();
        Container.Bind<AudioClip>().WithId("Close_Clip").FromInstance(closeClip).AsCached();
        Container.Bind<AudioClip>().WithId("Chew_Clip").FromInstance(chewClip).AsCached();
        Container.Bind<AudioClip>().WithId("Sad_Clip").FromInstance(sadClip).AsCached();
    }
}