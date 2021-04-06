using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCactus : Enemy
{
    /*public StateMachine state;*/
    public CactusPatrollingState cactusPatrollingState;
    public CactusPursuitState cactusPursuitState;
    public CactusShootingState cactusShootingState;

    /*private EnemyPath enemyPath;
    private Rigidbody2D rbody;*/
   /* private Vector2 fPoint;*/
   /* private Vector2 startPoint;
*/
   /* public Vector2 FollowPoint { get { return fPoint; } } 
  *//*  public Vector2 StartPoint { get { return startPoint; } }*/

  /*  public override void Move(Vector2 followPoint, out bool followCompleted) 
    {
        base.Move(followPoint, out followCompleted);
       *//* if (Vector2.Distance(followPoint, transform.position) > 0.1f)
        {
            fPoint = followPoint;
            enemyPath.PathFollow();
            followCompleted = false;
            return;
        }
        StopMovement();
        followCompleted = true;*//*
    }*/

   /* public override void StopMovement()
    {
        base.StopMovement();
        *//*rbody.velocity = Vector2.zero;*//*
    }*/

    public override void Start()
    {
        base.Start();
        /*state = new StateMachine();
        enemyPath = GetComponent<EnemyPath>();
        rbody = GetComponent<Rigidbody2D>();
        startPoint = transform.position;*/

        cactusPatrollingState = new CactusPatrollingState(gameObject, state);
        cactusPursuitState = new CactusPursuitState(gameObject, state);
        cactusShootingState = new CactusShootingState(gameObject, state);
        state.Initialize(cactusPatrollingState);
    }

   /* public override void Update()
    {
        base.Update();
        *//*state.CurrentState.LogicUpdate();*//*
    }*/

 /*   public override void FixedUpdate()
    {
        base.FixedUpdate();
        *//*state.CurrentState.PhysicsUpdate();*//*
    }*/
    //TODO: REMOVE ON RELEASE!
  /*  #region DEBUG_DRAW_GIZMOS
    *//*public Vector3 position;
    public Vector3 xCathetus;
    public Vector3 yCathetus;*//*

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
       *//* Gizmos.color = Color.red;
        Gizmos.DrawSphere(position, 0.03f);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, xCathetus);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(xCathetus, yCathetus);*//*
    }
    #endregion
*/
}
