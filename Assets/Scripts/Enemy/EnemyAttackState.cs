using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState :State
{
    public float x = 0;
    public float y = 0;
    private Enemy enemy;
    private bool wasShot;
    public EnemyAttackState(GameObject entity, StateMachine state) : base(entity, state) { }

    public override void Enter()
    {
        base.Enter();
        wasShot = false;
        enemy = entity.GetComponent<Enemy>();
        enemy.EnemyAILerp.speed = 0f;
        enemy.DestionationSetter.target.position = enemy.transform.position;
        enemy.EnemyAnimator.Play("Attack");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (enemy.EnemyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.6f && !wasShot && enemy.EnemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            wasShot = true;
            enemy.CreateBullet(new Vector2(x,y));
        }
        if (enemy.EnemyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f && enemy.EnemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            enemy.State.ChangeState(enemy.patrollingState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
