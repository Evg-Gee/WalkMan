using UnityEngine;
public class RunningState : BaseState
{
    private readonly float _speed;
    private readonly float _rotationSpeed;
    private readonly float _acceleration;
    private readonly float _deceleration;
    
    private readonly IAnimationController _animationController;
    private readonly IRotationHandler _rotationHandler;
    
    private Vector3 _currentVelocity;
    private Vector3 _lastMovementDirection;

    public RunningState(
        ICharacter character,
        StateMachine stateMachine,
        IAnimationController animationController,
        IRotationHandler rotationHandler) 
        : base(character, stateMachine)
    {
        _speed = character.Stats.Speed;
        _rotationSpeed = character.Stats.RotationSpeed;
        _acceleration = character.Stats.Acceleration;
        _deceleration = character.Stats.Deceleration;
        _animationController = animationController;
        _rotationHandler = rotationHandler;
    }

    public override void Enter()
    {
        _animationController.SetRunning(true);
        _lastMovementDirection = Character.CharacterTransform.forward;
    }

    public override void Exit()
    {
        _animationController.SetRunning(false);
        _animationController.ResetTurns();
    }

    public override void PhysicsUpdate()
    {
        Vector3 movementDirection = CalculateMovementDirection();
        UpdateVelocity(movementDirection);
        ApplyMovement();
        
        if (movementDirection != Vector3.zero)
        {
            _lastMovementDirection = movementDirection.normalized;
            _rotationHandler.CalculateTurnParameters(
                Character.InputHandler.MoveInput, 
                Character.CharacterTransform
            );
        }
    }

    private Vector3 CalculateMovementDirection()
    {
        Vector2 moveInput = Character.InputHandler.MoveInput;
        if (moveInput == Vector2.zero) return _lastMovementDirection;
        
        Vector3 inputDirection = new Vector3(moveInput.x, 0, moveInput.y);
        return Character.CharacterTransform.TransformDirection(inputDirection).normalized;
    }

    private void UpdateVelocity(Vector3 targetDirection)
    {
        Vector3 targetVelocity = targetDirection * _speed;
        float lerpSpeed = targetDirection != Vector3.zero ? _acceleration : _deceleration;
        
        _currentVelocity = Vector3.Lerp(
            _currentVelocity,
            targetVelocity,
            lerpSpeed * Time.fixedDeltaTime
        );
    }

    private void ApplyMovement()
    {
        Character.Rigidbody.velocity = new Vector3(
            _currentVelocity.x,
            Character.Rigidbody.velocity.y,
            _currentVelocity.z
        );
    }

    public override void Update()
    {
        HandleRotation();
        UpdateAnimations();
    }

    private void HandleRotation()
    {
        Vector3 rotationInput = GetEffectiveRotationInput();
        if (rotationInput == Vector3.zero) return;

        Quaternion targetRotation = _rotationHandler.GetTargetRotation(
            rotationInput,
            Character.CharacterTransform
        );
        
        Character.Rotation = Quaternion.Slerp(
            Character.Rotation,
            targetRotation,
            _rotationSpeed * Time.deltaTime
        );        
    }
   
    private Vector3 GetEffectiveRotationInput()
    {
        Vector2 lookInput = Character.InputHandler.LookInput;
        return lookInput != Vector2.zero 
            ? new Vector3(lookInput.x, 0, lookInput.y).normalized 
            : Vector3.zero;
    }

    private void UpdateAnimations()
    {
        _animationController.SetRunning(true);
        
        if (IsMovingPureForward())
        {
            _animationController.ResetTurns();
            return;
        }

        _rotationHandler.CalculateTurnParameters(
            Character.InputHandler.MoveInput, 
            Character.CharacterTransform
        );

        if (_rotationHandler.IsTurning && ShouldUpdateTurnAnimation())
        {
            _animationController.SetTurn(_rotationHandler.TurnDirection);
        }
        else
        {
            _animationController.ResetTurns();
        }
    }
    private bool IsMovingPureForward()
    {
        Vector2 moveInput = Character.InputHandler.MoveInput;
        return moveInput.y > 0 && Mathf.Abs(moveInput.x) < 0.1f;
    }
    private bool ShouldUpdateTurnAnimation()
    {
        bool isBackward = Character.InputHandler.MoveInput.y < 0;
        bool hasSideInput = Mathf.Abs(Character.InputHandler.MoveInput.x) > 0.2f;
        
        return (!isBackward || hasSideInput) && !_rotationHandler.IsTurningBackward();
    }

    public override void HandleInput()
    {
        if (Character.InputHandler.MoveInput == Vector2.zero)
            TryChangeState<IdleState>();
        
        if (Character.InputHandler.JumpPressed && Character.IsGrounded)
            TryChangeState<JumpingState>();
        
        if (Character.InputHandler.AttackPressed)
            TryChangeState<AttackState>();
    }
}