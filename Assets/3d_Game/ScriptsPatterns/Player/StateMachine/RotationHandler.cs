using UnityEngine;

public class RotationHandler : IRotationHandler
{
    private const float TURN_ANGLE_THRESHOLD = 10f;
    private float _turnAngle;
    
    public float TurnDirection { get; private set; }
    public bool IsTurning { get; private set; }

    public void CalculateTurnParameters(Vector2 moveInput, Transform characterTransform)
    {
        if (moveInput == Vector2.zero)
        {
            IsTurning = false;
            _turnAngle = 0f;
            return;
        }

        Vector3 localInput = new Vector3(moveInput.x, 0, moveInput.y);
        Vector3 worldDirection = characterTransform.TransformDirection(localInput);
        
        _turnAngle = Vector3.SignedAngle(
            characterTransform.forward,
            worldDirection.normalized,
            Vector3.up
        );    

        TurnDirection = Mathf.Sign(_turnAngle);
        IsTurning = Mathf.Abs(_turnAngle) > TURN_ANGLE_THRESHOLD;
    }
    public bool IsTurningBackward()
    {
        return _turnAngle < -160f;
    }
    public Quaternion GetTargetRotation(Vector3 rotationInput, Transform characterTransform)
    {
        Vector3 worldDirection = characterTransform.TransformDirection(rotationInput);
        return Quaternion.LookRotation(worldDirection);
    }
}
