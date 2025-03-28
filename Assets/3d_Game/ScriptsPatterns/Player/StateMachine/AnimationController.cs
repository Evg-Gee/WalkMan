using UnityEngine;

public class AnimationController : IAnimationController
{
    private readonly Animator _animator;
    
    public AnimationController(Animator animator)
    {
        _animator = animator;
    }

    public void SetRunning(bool isRunning) 
    {
        if (isRunning) ResetTurns();
        _animator.SetBool("IsRunning", isRunning);
    }
    
    public void SetTurn(float turnDirection)
    {
            if (turnDirection > 0.1f)
        {
            _animator.SetBool("IsRunningRightTurn", true);
            _animator.SetBool("IsRunningLeftTurn", false);
        }
        else if (turnDirection < -0.1f)
        {
            _animator.SetBool("IsRunningLeftTurn", true);
            _animator.SetBool("IsRunningRightTurn", false);
        }
        else
        {
            ResetTurns();
        }
    }

    public void ResetTurns()
    {
        _animator.SetBool("IsRunningRightTurn", false);
        _animator.SetBool("IsRunningLeftTurn", false);
    }
}