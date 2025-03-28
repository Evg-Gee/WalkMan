using UnityEngine;

public class IdleState : BaseState
{
    private readonly IRotationHandler _rotationHandler;
    public IdleState(ICharacter character, StateMachine stateMachine, IRotationHandler rotationHandler) 
        : base(character, stateMachine) 
        {
            _rotationHandler = rotationHandler;
        }
    public override void Update()
    {
        HandleRotation();
        base.Update();
    }    
    public override void Enter() => Character.Animator.SetBool("IsIdle", true);
    public override void Exit() => Character.Animator.SetBool("IsIdle", false);
    private void HandleRotation()
    {
        Vector2 lookInput = Character.InputHandler.LookInput;
        if (lookInput == Vector2.zero) return;

        Vector3 rotationInput = new Vector3(lookInput.x, 0, lookInput.y);
        Quaternion targetRotation = _rotationHandler.GetTargetRotation(
            rotationInput,
            Character.CharacterTransform
        );
        
        Character.Rotation = Quaternion.Slerp(
            Character.Rotation,
            targetRotation,
            Character.Stats.RotationSpeed * Time.deltaTime
        );
    }
    public override void HandleInput()
    {
        if (Character.InputHandler.MoveInput != Vector2.zero)
            TryChangeState<RunningState>();
        
        if (Character.InputHandler.JumpPressed && Character.IsGrounded)
            TryChangeState<JumpingState>();
        
        if (Character.InputHandler.AttackPressed)
            TryChangeState<AttackState>();
    }
}