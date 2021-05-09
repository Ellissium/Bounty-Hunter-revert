using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrollingState : State
{
    public Vector3 lastRotation;
    private Enemy enemy;

    bool isCoroutineStarted = false;
    public EnemyPatrollingState(GameObject entity, StateMachine state): base(entity,state) { }
    public override void Enter()
    {
        base.Enter();
        isCoroutineStarted = false;
        enemy = entity.GetComponent<Enemy>();
        enemy.EnemyAnimator.Play("Idle");
        enemy.EnemyAILerp.speed = 0.5f;
        enemy.DestionationSetter.target.position = enemy.transform.position;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (!enemy.EnemyAILerp.reachedDestination && !enemy.EnemyAILerp.reachedEndOfPath)
        {
            lastRotation = new Vector3(enemy.EnemyRotation.x, enemy.EnemyRotation.y, enemy.EnemyRotation.z);
            enemy.EnemyAnimator.SetFloat("horizontal", enemy.EnemyRotation.x);
            enemy.EnemyAnimator.SetFloat("vertical", enemy.EnemyRotation.y);
            enemy.EnemyAnimator.Play("Move");
        }
        else
        {
            enemy.EnemyAnimator.Play("Idle");
            if (!isCoroutineStarted)
            {
                isCoroutineStarted = true;
                enemy.StartCoroutine(CoroutineChangeTargetPosition());
            }
        }
        RaycastHit2D hitN = Physics2D.Raycast(new Vector2(enemy.transform.position.x, enemy.transform.position.y+0.1f), Vector2.up, 2f);
        if (hitN.collider != null)
        {
            if (hitN.collider.GetComponent<BulletBotCollider>() != null)
            {
                enemy.EnemyAnimator.SetFloat("horizontal", 0);
                enemy.EnemyAnimator.SetFloat("vertical", 1);
                enemy.attackState.x = 0;
                enemy.attackState.y = 1;
                enemy.State.ChangeState(enemy.attackState);
            }
        }
        RaycastHit2D hitS = Physics2D.Raycast(new Vector2(enemy.transform.position.x, enemy.transform.position.y + 0.1f), Vector2.down, 2f);
        if (hitS.collider != null)
        {
            if (hitS.collider.GetComponent<BulletBotCollider>() != null)
            {
                enemy.EnemyAnimator.SetFloat("horizontal", 0);
                enemy.EnemyAnimator.SetFloat("vertical", -1);
                enemy.attackState.x = 0;
                enemy.attackState.y = -1;
                enemy.State.ChangeState(enemy.attackState);
            }
        }
        RaycastHit2D hitE = Physics2D.Raycast(new Vector2(enemy.transform.position.x, enemy.transform.position.y + 0.1f), Vector2.right, 2f);
        if (hitE.collider != null)
        {
            if (hitE.collider.GetComponent<BulletBotCollider>() != null)
            {
                enemy.EnemyAnimator.SetFloat("horizontal", 1);
                enemy.EnemyAnimator.SetFloat("vertical", 0);
                enemy.attackState.x = 1;
                enemy.attackState.y = 0;
                enemy.State.ChangeState(enemy.attackState);
            }
        }
        RaycastHit2D hitW = Physics2D.Raycast(new Vector2(enemy.transform.position.x, enemy.transform.position.y + 0.1f), Vector2.left, 2f);
        if (hitW.collider != null)
        {
            if (hitW.collider.GetComponent<BulletBotCollider>() != null)
            {
                enemy.EnemyAnimator.SetFloat("horizontal", -1);
                enemy.EnemyAnimator.SetFloat("vertical", 0);
                enemy.attackState.x = -1;
                enemy.attackState.y = 0;
                enemy.State.ChangeState(enemy.attackState);
            }
        }
    }

    private IEnumerator CoroutineChangeTargetPosition()
    {
        yield return new WaitForSeconds(Random.Range(2f, 4f));
        isCoroutineStarted = false;
        if(enemy.State.CurrentState == enemy.patrollingState)
            ChangeTargetPosition();
    }

    private void ChangeTargetPosition()
    {
        enemy.DestionationSetter.target = enemy.PatrolTarget;
        enemy.DestionationSetter.target.position = new Vector3(enemy.EnemySpawnPosition.x + Random.Range(-0.5f, 0.5f), enemy.EnemySpawnPosition.y + Random.Range(-0.5f, 0.5f), enemy.DestionationSetter.target.position.z);
    }
}
