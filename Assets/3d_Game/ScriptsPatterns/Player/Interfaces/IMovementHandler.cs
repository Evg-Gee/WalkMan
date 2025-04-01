using UnityEngine;

public interface IMovementHandler
{
    Vector3 CalculateMovementDirection(Vector2 moveInput, Transform characterTransform);
    void UpdateVelocity(Vector3 targetDirection, Rigidbody characterRigidbody);
    void ApplyMovement(Rigidbody characterRigidbody);
}

