using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public StateMachine state;
   /* public EnemyPatrollingState enemyPatrollingState;
    public EnemyPursuitState enemyPursuitState;
    public EnemyShootingState enemyShootingState;*/

    public EnemyPath enemyPath;
    public Rigidbody2D rbody;
    public Vector2 fPoint;
    public Vector2 startPoint;

    public Vector2 FollowPoint { get { return fPoint; } }
    public Vector2 StartPoint { get { return startPoint; } }

    public virtual void Move(Vector2 followPoint, out bool followCompleted)
    {
        if (Vector2.Distance(followPoint, transform.position) > 0.1f)
        {
            fPoint = followPoint;
           /* Debug.Log(FollowPoint);*/
            enemyPath.PathFollow();
            followCompleted = false;
            return;
        }
        StopMovement();
        followCompleted = true;
    }
  
    public virtual void StopMovement()
    {
        rbody.velocity = Vector2.zero;
    }

    public virtual void Start()
    {
        state = new StateMachine();
        enemyPath = GetComponent<EnemyPath>();
        rbody = GetComponent<Rigidbody2D>();
        startPoint = transform.position;

      /*  enemyPatrollingState = new EnemyPatrollingState(gameObject, state);
        enemyPursuitState = new EnemyPursuitState(gameObject, state);
        enemyShootingState = new EnemyShootingState(gameObject, state);
        state.Initialize(enemyPatrollingState);*/
    }

    public void Update()
    {
        state.CurrentState.LogicUpdate();
    }

    public void FixedUpdate()
    {
        state.CurrentState.PhysicsUpdate();
    }
    //TODO: REMOVE ON RELEASE!
    #region DEBUG_DRAW_GIZMOS
    public Vector3 position;
    public Vector3 xCathetus;
    public Vector3 yCathetus;

    public virtual void OnDrawGizmos()
    {   
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(position, 0.03f);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position,xCathetus);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(xCathetus,yCathetus);
    }
    #endregion

}
