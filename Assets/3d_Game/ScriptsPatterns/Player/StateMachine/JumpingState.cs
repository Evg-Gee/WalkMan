using UnityEngine;

public class JumpingState : BaseState
{
    private readonly float _jumpForce;
    private bool _hasJumped;

    public JumpingState(ICharacter character, StateMachine stateMachine) 
        : base(character, stateMachine) => _jumpForce = character.Stats.jumpForce;

    public override void Enter()
    {
        Character.Animator.SetBool("IsJumping", true);
        _hasJumped = false;
    }

    public override void Exit() => Character.Animator.SetBool("IsJumping", false);

    public override void PhysicsUpdate()
    {
        if (!_hasJumped)
        {
            Character.Rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _hasJumped = true;
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