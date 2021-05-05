using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public State CurrentState { get; private set; }

    public void Initialize(State startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    public void ChangeState(State newState)
    {
        if (CurrentState != null)
        {
            if (newState != null)
            {
                CurrentState.Exit();
            }

            CurrentState = newState;
            if (newState != null)
            {
                newState.Enter();
            }
        }
    }

    public override string ToString()
    {
        return CurrentState.ToString();
    }
}
