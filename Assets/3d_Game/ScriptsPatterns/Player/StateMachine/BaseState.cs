public abstract class BaseState : IState
{
    protected readonly ICharacter Character;
    protected readonly StateMachine StateMachine;

    protected BaseState(ICharacter character, StateMachine stateMachine)
    {
        Character = character;
        StateMachine = stateMachine;
    }
    
    protected void TryChangeState<T>() where T : class, IState
    {
        if (StateMachine.CurrentState.GetType() != typeof(T))
        {
            StateMachine.ChangeState<T>();
        }
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void HandleInput() { }
    public virtual void Update() { }
    public virtual void PhysicsUpdate() { }
}