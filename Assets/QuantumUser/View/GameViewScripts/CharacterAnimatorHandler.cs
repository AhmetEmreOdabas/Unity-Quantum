using UnityEngine;

public class CharacterAnimatorHandler : MonoBehaviour
{
    public Animator CharacterAnimator;
    private string _currentState;

    public void PlayAnimation(string animationName, float transitionDuration, bool isOverrideLastAnimation)
    {
        if (!isOverrideLastAnimation)
        {
            if (_currentState == animationName)
            {
                return;
            }
        }
        _currentState = animationName;
        CharacterAnimator.CrossFadeInFixedTime(animationName, transitionDuration, 0);
    }
    public void SetParameter(string parameterName, float value)
    {
        CharacterAnimator.SetFloat(parameterName, value);
    }
}
