using UnityEngine;

public interface IRotationHandler
{
    void CalculateTurnParameters(Vector2 moveInput, Transform characterTransform);
    Quaternion GetTargetRotation(Vector3 rotationInput, Transform characterTransform);
    Vector3 GetEffectiveRotationInput(Vector2 lookInput);
    bool IsTurningBackward();
    float TurnDirection { get; }
    bool IsTurning { get; }
    float RotationSpeed { get; }
}