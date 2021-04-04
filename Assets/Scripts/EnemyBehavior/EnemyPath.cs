using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyPath : MonoBehaviour
{
    
    public float speed = 50f;
    public  float nextWaypointDistance = 0.1f;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Seeker seeker;
    Rigidbody2D rb;
    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();   
        UpdatePath();
        InvokeRepeating("UpdatePath", .5f, .5f);
    }

    public void UpdatePath() 
    {
        
        Vector2 followPoint = enemy.enemyPatrollingState.FollowPoint;
        if (seeker.IsDone())
        seeker.StartPath(rb.position, followPoint, OnPathComplete);
    }
    private void OnPathComplete(Path p) 
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    public void PathFollow()
    {
        if (path == null)
            return;
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        } 
        else 
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;   
        rb.AddForce(force);  
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
    }
}
