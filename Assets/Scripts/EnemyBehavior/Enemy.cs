using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyPath enemyPath;
    public StateMachine state;
    public EnemyPatrollingState enemyPatrollingState;
    public EnemyPursuitState enemyPursuitState;
    public EnemyShootingState enemyShootingState;
    private Vector2 fPoint;
    public Vector2 startPoint;
    private Rigidbody2D rbody;

    public Vector2 FollowPoint { get { return fPoint; } }
    public Vector2 StartPoint { get { return startPoint; } }
    public void Move(Vector2 followPoint, out bool followCompleted)
    {
        if (Vector2.Distance(followPoint, transform.position) > 0.1f)
        {
            fPoint = followPoint;
            enemyPath.PathFollow();
            followCompleted = false;
            return;
        }
        StopMovement();
        followCompleted = true;
    }
  
    public void StopMovement()
    {
        
        rbody.velocity = Vector2.zero;
    }

    private void Start()
    {
        state = new StateMachine();
        enemyPath = GetComponent<EnemyPath>();
        startPoint = transform.position;
        Debug.Log(startPoint);
        rbody = GetComponent<Rigidbody2D>();
        enemyPatrollingState = new EnemyPatrollingState(gameObject, state);
        enemyPursuitState = new EnemyPursuitState(gameObject, state);
        enemyShootingState = new EnemyShootingState(gameObject, state);
        state.Initialize(enemyPatrollingState);

    }

    private void Update()
    {
        state.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        state.CurrentState.PhysicsUpdate();
    }
    //TODO: REMOVE ON RELEASE!
    #region DEBUG_DRAW_GIZMOS
    public Vector3 position;
    public Vector3 xCathetus;
    public Vector3 yCathetus;

    public void OnDrawGizmos()
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
