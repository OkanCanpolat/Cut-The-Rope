using System.Collections;
using UnityEngine;

public static class AnimatorExtension 
{
    public static IEnumerator WaitForAnimation(this Animator animator, string animationName, int layer = 0)
    {
        yield return null;
        yield return new WaitForAnimationToFinish(animator, animationName);
    }
}
