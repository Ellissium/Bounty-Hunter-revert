using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotFollow : MonoBehaviour
{
    [SerializeField] private Transform followedObject;

    private Vector3 _basicOffset;
    private Vector3 _differenceOffset;

    private void Start()
    {
       _basicOffset = (followedObject.position - transform.position);
    }

    private void FixedUpdate()
    {
        _differenceOffset = (followedObject.position - transform.position) - _basicOffset;
        transform.position += _differenceOffset;
        followedObject.localPosition = _basicOffset;
    }
}
