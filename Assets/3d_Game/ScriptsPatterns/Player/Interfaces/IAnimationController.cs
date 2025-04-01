using System;
using UnityEngine;

public interface IAnimationController
{
    public event Action <AnimationState> OnAnimationComplete;

    void SetState(AnimationState state);
    void SetTurn(IRotationHandler rotationHandler, Vector2 moveInput);
    void ResetTurns();
   
}