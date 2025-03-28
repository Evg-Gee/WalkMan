using UnityEngine;

public interface IRotationHandler
{
    void CalculateTurnParameters(Vector2 moveInput, Transform characterTransform);
    Quaternion GetTargetRotation(Vector3 rotationInput, Transform characterTransform);
    bool IsTurningBackward();
    float TurnDirection { get; }
    bool IsTurning { get; }
}