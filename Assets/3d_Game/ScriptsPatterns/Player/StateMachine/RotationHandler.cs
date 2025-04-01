using UnityEngine;

public class RotationHandler : IRotationHandler
{
    private const float TURN_ANGLE_THRESHOLD = 10f;
    private readonly float _rotationSpeed; 
    private float _turnAngle;
    public float RotationSpeed => _rotationSpeed;

    public bool IsTurning { get; private set; }
    public float TurnDirection { get; private set; }
    
     public RotationHandler(float rotationSpeed)
    {
        _rotationSpeed = rotationSpeed;
    }

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
        
        _turnAngle = Vector3.SignedAngle(characterTransform.forward, worldDirection.normalized, Vector3.up);
        TurnDirection = Mathf.Sign(_turnAngle);
        IsTurning = Mathf.Abs(_turnAngle) > TURN_ANGLE_THRESHOLD;
    }

    public bool IsTurningBackward() => _turnAngle < -160f;

    public Quaternion GetTargetRotation(Vector3 rotationInput, Transform characterTransform)
    {    
        Vector3 worldDirection = characterTransform.TransformDirection(rotationInput);
        return Quaternion.LookRotation(worldDirection);
    }

    public Vector3 GetEffectiveRotationInput(Vector2 lookInput)
    {
        return lookInput != Vector2.zero ? new Vector3(lookInput.x, 0, lookInput.y).normalized : Vector3.zero;
    }
}

