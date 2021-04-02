using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingState : State
{
    private Character character;
    private bool wasShot;

    public override void Enter()
    {
        base.Enter();
        character = entity.GetComponent<Character>();
        wasShot = false;
        character.Shoot();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (character.CharacterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f && !wasShot)
        {
            wasShot = true;
            character.CreateBullet();
        }
        if (character.CharacterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            character.state.ChangeState(character.grounding);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public ShootingState(GameObject entity, StateMachine stateMachine) : base(entity, stateMachine) { }
}
