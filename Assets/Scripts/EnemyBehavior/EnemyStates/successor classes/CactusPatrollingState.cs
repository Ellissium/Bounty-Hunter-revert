using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusPatrollingState : EnemyPatrollingState
{
    public EnemyCactus enemy;

    public CactusPatrollingState(GameObject entity, StateMachine stateMachine) : base(entity, stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        enemy = entity.GetComponent<EnemyCactus>();
        Debug.Log(GameManager.instance == null);
        player = GameManager.instance.player.GetComponent<Transform>();
        ChangePointPosition();
        //TODO: REMOVE ON RELEASE(ONLY FOR DEBUG)
        enemy.position = new Vector3(followPoint.x, followPoint.y, 5);
        enemy.xCathetus = xCathetus;
        enemy.yCathetus = yCathetus;
    }

    public override void ChangePointPosition()
    {
        base.ChangePointPosition();
        Debug.Log("ora" + enemy.transform.position);
        if (Vector2.Distance(enemy.transform.position, player.position) <= pursuitDistance/* &&*/ /*Vector2.Distance(enemy.transform.position, player.position) > shootingDistance*/)
        {
            stateMachine.ChangeState(enemy.cactusPursuitState);
        }
        else if (Vector2.Distance(enemy.transform.position, player.position) > pursuitDistance)
        {
            Debug.Log(enemy.StartPoint);
            followPoint = new Vector2(enemy.StartPoint.x + Random.Range(-1f, 1f), enemy.StartPoint.y + Random.Range(-1f, 1f));
        }
        /*else if (Vector2.Distance(enemy.transform.position, player.position) <= shootingDistance) 
        {
            followPoint = new Vector2(player.transform.position.x, player.transform.position.y );
            stateMachine.ChangeState(enemy.enemyShootingState);
            *//*isPatroll = false;
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

    public override void FollowOnEachAxis()
    {
        base.FollowOnEachAxis();
        if (!followCompleted && Vector2.Distance(enemy.transform.position, player.position) > pursuitDistance)
        {
            
            enemy.Move(followPoint, out followCompleted);
            return;
        }
        else if (Vector2.Distance(enemy.transform.position, player.position) <= pursuitDistance)
        {
            ChangePointPosition();
        }
        enemy.StartCoroutine(ChangeFollow());
        isCoroutineExist = true;
    }
}

