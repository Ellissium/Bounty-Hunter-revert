using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyPath enemyPath;
    public StateMachine state;
    public EnemyPatrollingState enemyPatrollingState;

    private Rigidbody2D rbody;
    public void Move()
    {
        enemyPath.PathFollow();
        /*rbody.velocity = followPoint * movementSpeed;*/
    }
  
    public void StopMovement()
    {
        
        rbody.velocity = Vector2.zero;
    }

    private void Start()
    {
        state = new StateMachine();
        enemyPath = GetComponent<EnemyPath>();

        rbody = GetComponent<Rigidbody2D>();
        enemyPatrollingState = new EnemyPatrollingState(gameObject, state);
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
