using UnityEngine;

public class TakeDamageState : BaseState
{
    private readonly IAnimationController _animationController;
    private readonly float _damageDuration = 0.5f;
    private float _damageTimer;

    public TakeDamageState(ICharacter character, StateMachine stateMachine, IAnimationController animationController)
        : base(character, stateMachine) 
        {
            _animationController = animationController;
        }

    public override void Enter()
    {
        _animationController.SetState(AnimationState.TakeDamage);
        _damageTimer = _damageDuration;
    }

    public override void Exit() => _animationController.SetState(AnimationState.Idle);
    

    public override void Update()
    {
        _damageTimer -= Time.deltaTime;
        if (_damageTimer <= 0)
        {
            TryChangeState<IdleState>();
        }
    }
}
