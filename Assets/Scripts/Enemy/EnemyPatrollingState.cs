using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyPatrollingState : State
{
    private Enemy enemy;
    private Path path;
    private bool coroutineStarted = false;
    public GameObject target;
    public override void Enter()
    {
        base.Enter();
        enemy = entity.GetComponent<Enemy>();
        enemy.Lerp.speed = 0.5f;
        
        
        target = new GameObject("Target position");
        target.transform.position = new Vector3(enemy.SpawnPosition.x, enemy.SpawnPosition.y, 0);
        ChangeTargetPosition();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if ((enemy.Lerp.reachedDestination || enemy.Lerp.reachedEndOfPath) && !coroutineStarted)
        {
            Debug.Log("Started!");
            enemy.StartCoroutine(ChangeTargetPositionCoroutine());
            coroutineStarted = true;
        }
    }

    public EnemyPatrollingState(GameObject entity, StateMachine state): base(entity,state) { }

    public IEnumerator ChangeTargetPositionCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(2f, 4f));
        ChangeTargetPosition();
        coroutineStarted = false;
    }
    public void ChangeTargetPosition()
    {
        target.transform.position = new Vector3(enemy.SpawnPosition.x + Random.Range(-1f, 1f), enemy.SpawnPosition.y + Random.Range(-1f, 1f), 0);
        enemy.DestinationSetter.target = target.transform;
        Debug.Log("POSITION CHANGED!");
    }

    
}
