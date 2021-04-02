using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyPath : MonoBehaviour
{
    public Transform target;
    public float speed = 1f;
    private float nextWaypointDistance =3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Seeker seeker;
    Rigidbody2D rb;





    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        seeker.StartPath(rb.position, target.position, OnPathComplete);

    }

    void OnPathComplete(Path p) 
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void Update()
    {
        
    }
}
