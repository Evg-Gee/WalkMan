using UnityEngine;

public class RunningState : BaseState
{
    private readonly IMovementHandler _movementHandler;
    private readonly IAnimationController _animationController;
    private readonly IRotationHandler _rotationHandler;

    public RunningState(
        ICharacter character,
        StateMachine stateMachine,
        IAnimationController animationController,
        IRotationHandler rotationHandler,
        IMovementHandler movementHandler)
        : base(character, stateMachine)
    {
        _animationController = animationController;
        _rotationHandler = rotationHandler;
        _movementHandler = movementHandler;
    }

    public override void Enter()
    {
        _animationController.SetState(AnimationState.Running);
    }

    public override void Exit()
    {
        _animationController.SetState(AnimationState.Idle);
    }

    public override void PhysicsUpdate()
    {
        Vector3 movementDirection = _movementHandler.CalculateMovementDirection(Character.InputHandler.MoveInput, Character.CharacterTransform);
        _movementHandler.UpdateVelocity(movementDirection, Character.Rigidbody);
        _movementHandler.ApplyMovement(Character.Rigidbody);

        if (movementDirection != Vector3.zero)
        {
            _rotationHandler.CalculateTurnParameters(Character.InputHandler.MoveInput, Character.CharacterTransform);
        }
    }

    public override void Update()
    {
        HandleRotation();
        _animationController.SetTurn(_rotationHandler, Character.InputHandler.MoveInput);
    }

    private void HandleRotation()
    {
        Vector3 rotationInput = _rotationHandler.GetEffectiveRotationInput(Character.InputHandler.LookInput);
        if (rotationInput != Vector3.zero)
        {
            Quaternion targetRotation = _rotationHandler.GetTargetRotation(rotationInput, Character.CharacterTransform);
            Character.Rotation = Quaternion.Slerp(Character.Rotation, targetRotation, _rotationHandler.RotationSpeed * Time.deltaTime);
        }
    }

    public override void HandleInput()
    {
        if (Character.InputHandler.MoveInput == Vector2.zero)
            TryChangeState<IdleState>();

        if (Character.InputHandler.JumpPressed && Character.IsGrounded)
            TryChangeState<JumpingState>();

        if (Character.InputHandler.AttackPressed)
            TryChangeState<AttackState>();
        if (Character.InputHandler.PickUpPressed)
        TryChangeState<PickUpItemState>();
    }
}



