using System;
using UnityEngine;

public enum AnimationState { Idle, Running, Jumping, RightTurn, LeftTurn, TakeDamage, TakeItem}
public class AnimationController : IAnimationController
{
    private readonly Animator _animator;

    private static readonly int IsRunning = Animator.StringToHash("IsRunning");
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");
    private static readonly int IsRunningRightTurn = Animator.StringToHash("IsRunningRightTurn");
    private static readonly int IsRunningLeftTurn = Animator.StringToHash("IsRunningLeftTurn");

    private static readonly int IsTakeDamage = Animator.StringToHash("TakeDamage");
    private static readonly int IsTakeItem = Animator.StringToHash("IsTakeItem");

    public AnimationController(Animator animator)
    {
        _animator = animator;
    }
    
    public bool IsAnimationPlaying(AnimationState state)
    {
        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(state.ToString()) && stateInfo.normalizedTime < 1.0f;
    }

    public void SetState(AnimationState state)
    {
        ResetAllStates();

        switch (state)
        {
            case AnimationState.Running:
                _animator.SetBool(IsRunning, true);
                break;
            case AnimationState.Jumping:
                _animator.SetBool(IsJumping, true);
                break;
            case AnimationState.RightTurn:
                _animator.SetBool(IsRunningRightTurn, true);
                break;
            case AnimationState.LeftTurn:
                _animator.SetBool(IsRunningLeftTurn, true);
                break;
            case AnimationState.TakeDamage:
            _animator.SetBool(IsTakeDamage, true);
                break;
            case AnimationState.TakeItem:
            _animator.SetBool(IsTakeItem, true);
                break;    
            case AnimationState.Idle:
            default:
             // Всё уже сброшено ResetAllStates(), ничего делать не надо
            break;
        }
    }

    public void SetTurn(IRotationHandler rotationHandler, Vector2 moveInput)
    {
        if (moveInput.y < 0) 
        {
            ResetTurns();
            return;
        }

        if (rotationHandler.IsTurning && !rotationHandler.IsTurningBackward())
        {
            if (rotationHandler.TurnDirection > 0.1f)
            {
                _animator.SetBool(IsRunningRightTurn, true);
                _animator.SetBool(IsRunningLeftTurn, false);
            }
            else if (rotationHandler.TurnDirection < -0.1f)
            {
                _animator.SetBool(IsRunningLeftTurn, true);
                _animator.SetBool(IsRunningRightTurn, false);
            }
        }
        else
        {
            ResetTurns();
        }
    }

    public void ResetTurns()
    {
        _animator.SetBool(IsRunningRightTurn, false);
        _animator.SetBool(IsRunningLeftTurn, false);
    }

    private void ResetAllStates()
    {
        _animator.SetBool(IsRunning, false);
        _animator.SetBool(IsJumping, false);
        _animator.SetBool(IsRunningRightTurn, false);
        _animator.SetBool(IsRunningLeftTurn, false);
        _animator.SetBool(IsTakeItem, false);
        
    }
}

