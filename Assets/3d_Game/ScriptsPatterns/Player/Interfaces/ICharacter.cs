using UnityEngine;

public interface ICharacter
{
    Animator Animator { get; }
    Rigidbody Rigidbody { get; }
    PlayerStatsSO Stats { get; }
    IInputHandler InputHandler { get; }
    bool IsGrounded { get; }
    Transform CharacterTransform { get; }
    Quaternion Rotation { get; set; }
    void UpdateGroundStatus();
}