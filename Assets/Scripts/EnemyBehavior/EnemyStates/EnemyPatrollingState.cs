using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrollingState : State
{
    private Enemy enemy;

    private Vector2 startPoint;
    private Vector2 followPoint;
    private Vector2 xCathetus;
    private Vector2 yCathetus;
    private bool xAxisMovementCompleted = false;
    private bool yAxisMovementCompleted = false;
    private bool isCoroutineExist = false;

    public override void Enter()
    {
        base.Enter();
        enemy = entity.GetComponent<Enemy>();
        startPoint = enemy.transform.position;
        ChangePointPosition();
        //TODO: REMOVE ON RELEASE(ONLY FOR DEBUG)
        enemy.position = new Vector3(followPoint.x, followPoint.y, 5);
        enemy.xCathetus = xCathetus;
        enemy.yCathetus = yCathetus;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Input.GetKeyDown(KeyCode.D)) ChangePointPosition();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        FollowOnEachAxis();
    }

    public void ChangePointPosition()
    {
        followPoint = new Vector2(startPoint.x + Random.Range(-1f, 1f), startPoint.y + Random.Range(-1f, 1f));
        xCathetus = new Vector2(followPoint.x, enemy.transform.position.y);
        yCathetus = new Vector2(xCathetus.x, followPoint.y);
        xAxisMovementCompleted = yAxisMovementCompleted = false;
        //isXCathetusBigger = Vector2.Distance(enemy.position, xCathetus) > Vector2.Distance(enemy.position, yCathetus) ? true : false;
        //TODO: Only for DEBUG, REMOVE ON RELEASE
        enemy.position = new Vector3(followPoint.x, followPoint.y, 5);
        enemy.xCathetus = xCathetus;
        enemy.yCathetus = yCathetus;
    }

    public EnemyPatrollingState(GameObject entity, StateMachine stateMachine): base(entity,stateMachine)
    {

    }

    private IEnumerator ChangeFollow()
    {
        if (isCoroutineExist) yield break;
        yield return new WaitForSeconds(Random.Range(1f,3f));
        ChangePointPosition();
        isCoroutineExist = false;
    }

    private void FollowOnEachAxis()
    {
        if (!xAxisMovementCompleted)
        {
            Follow(xCathetus, out xAxisMovementCompleted);
            return;
        }
        if (!yAxisMovementCompleted)
        {
            Follow(yCathetus, out yAxisMovementCompleted);
            return;
        }
        enemy.StartCoroutine(ChangeFollow());
        isCoroutineExist = true;
    }
    private void Follow(Vector2 cathetus, out bool followCompleted)
    {
        if (Vector2.Distance(cathetus, enemy.transform.position) > 0.01f)
        {
            enemy.Move((cathetus - (Vector2)enemy.transform.position).normalized, 0.5f);
            followCompleted = false;
            return;
        }
        enemy.StopMovement();
        followCompleted = true;
    }
    
}
