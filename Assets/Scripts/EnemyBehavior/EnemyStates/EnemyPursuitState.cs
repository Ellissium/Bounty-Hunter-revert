using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPursuitState : State
{
    private Enemy enemy;
    private Transform player;

    private float pursuitDistance = 1.5f;
    private float shootingDistance = 0.5f;

    private Vector2 startPoint;
    private Vector2 followPoint;
    private Vector2 xCathetus;
    private Vector2 yCathetus;

    private bool followCompleted = false;
    private bool isCoroutineExist = false;
    private bool isPursuit = false;
    private bool isPatroll = false;
    private bool isShoot = false;

    public override void Enter()
    {
        base.Enter();
        enemy = entity.GetComponent<Enemy>();
        player = GameManager.instance.player.transform;
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        FollowOnEachAxis();
    }

    public void ChangePointPosition()
    {
        if (Vector2.Distance(enemy.transform.position, player.position) <= pursuitDistance /*&& *//*Vector2.Distance(enemy.transform.position, player.position) > shootingDistance*/)
        {
            followPoint = new Vector2(player.transform.position.x, player.transform.position.y);
        }
        else if (Vector2.Distance(enemy.transform.position, player.position) > pursuitDistance)
        {
            stateMachine.ChangeState(enemy.enemyPatrollingState);
        }
       /* else if (Vector2.Distance(enemy.transform.position, player.position) <= shootingDistance)
        {
            followPoint = new Vector2(player.transform.position.x, player.transform.position.y);
            stateMachine.ChangeState(enemy.enemyShootingState);
            *//* isPatroll = false;
             isPursuit = false;*//*

        }*/
        /*xCathetus = new Vector2(followPoint.x, enemy.transform.position.y);
        yCathetus = new Vector2(xCathetus.x, followPoint.y);*/
        followCompleted = false;
        //isXCathetusBigger = Vector2.Distance(enemy.position, xCathetus) > Vector2.Distance(enemy.position, yCathetus) ? true : false;
        //TODO: Only for DEBUG, REMOVE ON RELEASE
        enemy.position = new Vector3(followPoint.x, followPoint.y, 5);
        enemy.xCathetus = xCathetus;
        enemy.yCathetus = yCathetus;
    }

    public EnemyPursuitState(GameObject entity, StateMachine stateMachine) : base(entity, stateMachine)
    {

    }

    private IEnumerator ChangeFollow()
    {
        if (isCoroutineExist) yield break;
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        ChangePointPosition();
        isCoroutineExist = false;
    }

    private void FollowOnEachAxis()
    {
            if (!followCompleted && Vector2.Distance(enemy.transform.position, player.position) <= pursuitDistance)
            {
                followPoint = new Vector2(player.transform.position.x, player.transform.position.y);
                enemy.Move(followPoint, out followCompleted);
                return;
            }
            else if (Vector2.Distance(enemy.transform.position, player.position) < pursuitDistance)
            {
                ChangePointPosition();
            }
            else
            {
                enemy.StartCoroutine(ChangeFollow());
                isCoroutineExist = true;
            }
      /*  if (isShoot)
        {
            if (!followCompleted && Vector2.Distance(enemy.transform.position, player.position) <= shootingDistance)
            {
                followPoint = new Vector2(player.transform.position.x, player.transform.position.y);
                enemy.Move(followPoint, out followCompleted);
                return;
            }
            else
            {
                enemy.StartCoroutine(ChangeFollow());
                isCoroutineExist = true;
            }
        }*/
    }
}