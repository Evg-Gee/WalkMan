using UnityEngine;

public class JumpingState : BaseState
{
    private readonly IAnimationController _animationController;
    private readonly float _jumpForce;
    private bool _hasJumped;

    public JumpingState(ICharacter character, StateMachine stateMachine, 
        IAnimationController animationController) 
        : base(character, stateMachine) 
        {
            _animationController = animationController;
            _jumpForce = character.Stats.jumpForce;
        }

    public override void Enter()
    {
        _animationController.SetState(AnimationState.Jumping);
        _hasJumped = false;
    }

    public override void Exit() => _animationController.SetState(AnimationState.Idle);

    public override void PhysicsUpdate()
    {
        if (!_hasJumped)
        {
            Character.Rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _hasJumped = true;
            Debug.Log("Jump");
        }
    }

    public override void HandleInput()
    {
        if (Character.IsGrounded && _hasJumped)
        {
            TryChangeState<IdleState>();
        }
            
    }
}