
using UnityEngine;

public class MovementHandler : IMovementHandler

{
    private readonly float _speed;
    private readonly float _acceleration;
    private readonly float _deceleration;

    private Vector3 _currentVelocity;
    private Vector3 _lastMovementDirection;

    public MovementHandler(float speed, float acceleration, float deceleration)
    {
        _speed = speed;
        _acceleration = acceleration;
        _deceleration = deceleration;
    }

    public Vector3 CalculateMovementDirection(Vector2 moveInput, Transform characterTransform)
    {
        if (moveInput == Vector2.zero) return _lastMovementDirection;

        Vector3 inputDirection = new Vector3(moveInput.x, 0, moveInput.y);
        return characterTransform.TransformDirection(inputDirection).normalized;
    }

    public void UpdateVelocity(Vector3 targetDirection, Rigidbody characterRigidbody)
    {
        Vector3 targetVelocity = targetDirection * _speed;
        float lerpSpeed = targetDirection != Vector3.zero ? _acceleration : _deceleration;

        _currentVelocity = Vector3.Lerp(_currentVelocity, targetVelocity, lerpSpeed * Time.fixedDeltaTime);
    }

    public void ApplyMovement(Rigidbody characterRigidbody)
    {
        characterRigidbody.velocity = new Vector3(_currentVelocity.x, characterRigidbody.velocity.y, _currentVelocity.z);
    }
}
