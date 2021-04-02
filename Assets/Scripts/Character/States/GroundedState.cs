using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : State
{
    protected float movementSpeed = 1f;

    public Vector2 inputVector;

    private Character character;

    private bool isShoot;

    public GroundedState(GameObject entity, StateMachine stateMachine) : base(entity, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        character = entity.GetComponent<Character>();
        inputVector = new Vector2(0f, 0f);
        isShoot = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void HandleInput()
    {
        base.HandleInput();
        inputVector.x = Input.GetAxisRaw("Horizontal");
        inputVector.y = Input.GetAxisRaw("Vertical");

        isShoot = Input.GetButtonDown("Shoot");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isShoot && CharacterStats.instance.AmmoInCylinder > 0)
        {
            stateMachine.ChangeState(character.shooting);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        character.Move(inputVector, movementSpeed);
    }
}
