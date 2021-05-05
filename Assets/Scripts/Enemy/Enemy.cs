using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class Enemy : MonoBehaviour
{
    [SerializeField] public GameObject bullet;
    public EnemyPatrollingState patrollingState;
    public Transform firePoint;
    public EnemiesAmountDisplaying enemiesAmountDisplaying;
    public EnemyAttackState attackState;
    protected AILerp ailerp;
    protected AIDestinationSetter destinationSetter;
    protected Seeker seeker;
    [SerializeField] private GameObject animateEnemy;
    private Transform patrolTarget;
    private bool wasExploded = false;
    public Animator EnemyAnimator { get; private set; }
    public Vector2 EnemySpawnPosition { get; private set; }
    public StateMachine State { get; private set; }
    public AIDestinationSetter DestionationSetter { get { return destinationSetter; } }
    public AILerp EnemyAILerp { get { return ailerp; } }
    public Transform PatrolTarget { get { return patrolTarget; } }
    public bool IsPlayerEntered { get; set; }
    public Vector3 EnemyRotation
    {
        get
        {
            Vector3 currentRotation = (destinationSetter.target.position - transform.position);
            return (currentRotation.normalized);
        }
    }
    public int HealthPoint { get; set; }
    public Seeker EnemySeeker { get { return seeker; } }

    public void Start()
    {
        GameManager.instance.countOfEnemies++;
        enemiesAmountDisplaying.DrawAmountOfEnemies();
        ailerp = GetComponent<AILerp>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        seeker = GetComponent<Seeker>();
        patrolTarget = new GameObject("targetObject").transform;
        destinationSetter.target = patrolTarget; 
        destinationSetter.target.transform.position = transform.position;
        EnemySpawnPosition = transform.position;
        EnemyAnimator = animateEnemy.GetComponent<Animator>();
        patrollingState = new EnemyPatrollingState(gameObject, State);
        attackState = new EnemyAttackState(gameObject, State);
        State = new StateMachine();
        State.Initialize(patrollingState);
        HealthPoint = 3;
    }

    public void Update()
    {
        if (HealthPoint > 0)
            State.CurrentState.LogicUpdate();
        else
        {
            EnemyAnimator.Play("CoffinDestroy");
            EnemyAILerp.speed = 0;
            if (!wasExploded)
            {
                AudioManager.instance.Play("Explosion");
                wasExploded = true;
            }
        }
    }

    public void FixedUpdate()
    {
        if (HealthPoint > 0)
            State.CurrentState.PhysicsUpdate();
    }
    public void OnDrawGizmos()
    {
        if (destinationSetter != null)
            Gizmos.DrawSphere(DestionationSetter.target.position, 0.02f);
    }

    public void CreateBullet(Vector2 direction)
    {
        GameObject instanceBullet = Instantiate(bullet, firePoint.transform.position,firePoint.transform.rotation);
        instanceBullet.GetComponent<BulletBot>().SetBulletDirection(direction);
        AudioManager.instance.PlayNew("EnemyShot");
    }
    public void OnEnemyDestroy()
    {
        GameManager.instance.countOfEnemies--;
        enemiesAmountDisplaying.DrawAmountOfEnemies();
        Destroy(transform.parent.gameObject);
    }
}
