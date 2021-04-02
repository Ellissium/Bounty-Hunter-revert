using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected GameObject entity;
    protected StateMachine stateMachine;

    protected State(GameObject entity, StateMachine stateMachine)
    {
        this.entity = entity;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
    }

    public virtual void HandleInput()
    {

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Exit()
    {

    }
}