using System;
using System.Collections.Generic;
using UnityEngine;


public class FiniteStateMachine<TContext>
{
    public readonly TContext _Context;

    private readonly Dictionary<Type, State> _StateCache = new Dictionary<Type, State>();

    public State CurrentState { get; private set; }

    private State _PendingState;

    public FiniteStateMachine(TContext context)
    {
        _Context = context;
    }

    public void Update()
    {
        PerformPendingTransition();
        Debug.Assert(CurrentState != null, "Updating FSM with null current state. Did you forget to transition to a starting state?");
        CurrentState.Update();
        PerformPendingTransition();
    }

    public void TransitionTo<TState>() where TState : State
    {
        _PendingState = GetOrCreateState<TState>();
    }

    private void PerformPendingTransition()
    {
        if (_PendingState != null)
        {
            if (CurrentState != null) CurrentState.OnExit();
            CurrentState = _PendingState;
            CurrentState.OnEnter();
            _PendingState = null;
        }
    }

    private TState GetOrCreateState<TState>() where TState : State
    {
        State state;
        if (_StateCache.TryGetValue(typeof(TState), out state))
        {
            return (TState)state;
        }
        else
        {
            var newState = Activator.CreateInstance<TState>();
            newState.Parent = this;
            newState.Init();
            _StateCache[typeof(TState)] = newState;
            return newState;
        }
    }

    public void Clear()
    {
        foreach (var state in _StateCache.Values)
        {
            state.CleanUp();
        }
        _StateCache.Clear();
    }

    public abstract class State
    {
        //the state machine for this state
        internal FiniteStateMachine<TContext> Parent { get; set; }

        //easy access to this state's context
        protected TContext Context { get { return Parent._Context; } }

        //used to have this state call for a transition to a different state
        protected void TransitionTo<TState>() where TState : State
        {
            Parent.TransitionTo<TState>();
        }

        //called when state is first created
        public virtual void Init() { }

        //called when state becomes active
        public virtual void OnEnter() { }

        //called when state becomes inactive
        public virtual void OnExit() { }

        //update method
        public virtual void Update() { }

        //called when state machine in cleared
        public virtual void CleanUp() { }
    }
}


