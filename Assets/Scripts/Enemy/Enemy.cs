using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    protected StateMachine state;
    private AIDestinationSetter destinationSetter;
    private AILerp lerp;

    private EnemyPatrollingState patrollingState;
    public AIDestinationSetter DestinationSetter { get { return destinationSetter; } }
    public AILerp Lerp { get { return lerp; } }
    public Vector2 SpawnPosition { get; private set; }

    private void Start()
    {
        
        state = new StateMachine();
        destinationSetter = GetComponent<AIDestinationSetter>();
        lerp = GetComponent<AILerp>();
        SpawnPosition = transform.position;
        patrollingState = new EnemyPatrollingState(gameObject, state);
        state.Initialize(patrollingState);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            patrollingState.ChangeTargetPosition();
        }
    }
    private void FixedUpdate()
    {
        state.CurrentState.PhysicsUpdate();
    }
    private void OnDrawGizmos()
    {
        if (patrollingState != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(patrollingState.target.transform.position, 0.01f);
        }
    }
}
