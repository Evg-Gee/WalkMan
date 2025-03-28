using UnityEngine;

public class AttackState : BaseState
{
    private readonly float _attackDuration;
    private float _attackTimer;

    public AttackState(ICharacter character, StateMachine stateMachine) 
        : base(character, stateMachine) => _attackDuration = character.Stats.meleeCooldown;

    public override void Enter()
    {
        Character.Animator.SetTrigger("Attack");
        _attackTimer = _attackDuration;
    }

    public override void Exit() => Character.Animator.ResetTrigger("Attack");

    public override void Update()
    {
        _attackTimer -= Time.deltaTime;
        if (_attackTimer <= 0)
        {
            TryChangeState<IdleState>();
        }
            
    }
}