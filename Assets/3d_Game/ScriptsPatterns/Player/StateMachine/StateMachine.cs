using System;
using System.Collections.Generic;

public class StateMachine
{
    private readonly Dictionary<Type, IState> _states = new();
    public IState CurrentState { get; private set; }

    public void AddState<T>(T state) where T : IState => 
        _states[typeof(T)] = state;

    public void ChangeState<T>() where T : class, IState
    {
        if (!_states.TryGetValue(typeof(T), out var newState)) return;
        
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
    public void ChangeState(IState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
     public void Update()
    {
        CurrentState?.HandleInput();
        CurrentState?.Update();
    }
}