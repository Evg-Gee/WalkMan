using UnityEngine;

public interface IInputHandler
{
    Vector2 MoveInput { get; }
    Vector2 LookInput { get; }
    bool JumpPressed { get; }
    bool PickUpPressed { get; }
    bool AttackPressed { get; }
    void ResetActions();
}