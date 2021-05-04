using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFirePointPosition : MonoBehaviour
{
    public Vector2 N;
    public Vector2 E;
    public Vector2 S;
    public Vector2 W;
    public Enemy enemy;
    private Transform _transform;
    float x;
    float y;

    void Start()
    {
        _transform = transform;
        x = 0;
        y = 0;
    }
    private void Update()
    {
    }

    void FixedUpdate()
    {
        x = enemy.attackState.x;
        y = enemy.attackState.y;
        if (x == 0 && y == 1)
        {
            _transform.localPosition = N;
        }
        else if (x == 0 && y == -1)
        {
            _transform.localPosition = S;
        }
        else if (x == -1 && y == 0)
        {
            _transform.localPosition = W;
        }
        else if (x == 1 && y == 0)
        {
            _transform.localPosition = E;
        }
    }
}
