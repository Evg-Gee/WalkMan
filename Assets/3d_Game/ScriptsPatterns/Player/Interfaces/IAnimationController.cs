using UnityEngine;

public interface IAnimationController
{
public bool IsAnimationPlaying(AnimationState state);
    void SetState(AnimationState state);
    void SetTurn(IRotationHandler rotationHandler, Vector2 moveInput);
    void ResetTurns();
   
}