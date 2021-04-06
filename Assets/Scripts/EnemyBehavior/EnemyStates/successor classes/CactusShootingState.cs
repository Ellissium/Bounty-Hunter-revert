using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusShootingState : EnemyShootingState
{
    public EnemyCactus enemy;

    public RaycastHit2D hitX;
    public RaycastHit2D hitY;

    public float distanceX;
    public float distanceY;
    public float xCathetusDistance;
    public float yCathetusDistance;

    public CactusShootingState(GameObject entity, StateMachine stateMachine) : base(entity, stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        enemy = entity.GetComponent<EnemyCactus>();
        player = GameManager.instance.player.transform;
        wasShot = false;
        layerMask = -layerMask;
        ChangePointPosition();
        
    /*    enemy.CharacterAnimator.Play("Shoot");
        enemy.Rbody.velocity = Vector2.zero;*/
    }

    public override void ChangePointPosition()
    {
        base.ChangePointPosition();
        if (Vector2.Distance(enemy.transform.position, player.position) <= pursuitDistance && Vector2.Distance(enemy.transform.position, player.position) > shootingDistance)
        {
            stateMachine.ChangeState(enemy.cactusPursuitState);
        }
        else if (Vector2.Distance(enemy.transform.position, player.position) > pursuitDistance)
        {
            stateMachine.ChangeState(enemy.cactusPatrollingState);
        }
        else if (Vector2.Distance(enemy.transform.position, player.position) <= shootingDistance)
        {
           
        }
        followCompleted = false;
        //TODO: Only for DEBUG, REMOVE ON RELEASE
        enemy.position = new Vector3(followPoint.x, followPoint.y, 5);
        enemy.xCathetus = xCathetus;
        enemy.yCathetus = yCathetus;
    }

    public override void rayCast()
    {
        base.rayCast();
        xCathetus = new Vector2(enemy.transform.position.y, player.transform.position.x);
        yCathetus = new Vector2(enemy.transform.position.x, player.transform.position.y);

        xCathetusDistance = Vector2.Distance(enemy.transform.position, xCathetus);
        yCathetusDistance = Vector2.Distance(enemy.transform.position, yCathetus);

        hitX = Physics2D.Raycast(enemy.transform.position, xCathetus, 10f, layerMask);
        hitY = Physics2D.Raycast(enemy.transform.position, yCathetus, 10f, layerMask);

        if (hitX.collider != null)
            distanceX = Mathf.Abs(hitX.point.x - enemy.transform.position.x);
        if (hitY.collider != null)
            distanceY = Mathf.Abs(hitX.point.y - enemy.transform.position.y);

        if (distanceX > xCathetusDistance && distanceY > yCathetusDistance)
        {
            ChooseAxis();
        }
        else if (distanceX < xCathetusDistance && distanceY < yCathetusDistance)
        {

        }
        else 
        {
            ChooseAxisFromOne();
        }
    }

    public override void ChooseAxis()
    {
        base.ChooseAxis();
        if (xCathetusDistance >= yCathetusDistance)
        {
            MoveToAxisX();
        }
        else 
        {
            MoveToAxisY();
        }
    }

    public override void ChooseAxisFromOne()
    {
        base.ChooseAxisFromOne();
        if (distanceX > xCathetusDistance && distanceY <= yCathetusDistance)
        {
            MoveToAxisX();
        }
        else if (distanceX <= xCathetusDistance && distanceY > yCathetusDistance)
        {
            MoveToAxisY();
        }

    }

    public override void MoveToAxisX()
    {
        enemy.Move(xCathetus, out followCompleted);
        
        base.MoveToAxisX();
    }

    public override void MoveToAxisY()
    {

        enemy.Move(yCathetus, out followCompleted);
        base.MoveToAxisY();
    }


    public override void Shoot()
    {
        base.Shoot();
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
