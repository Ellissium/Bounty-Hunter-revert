using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public Vector2 maxPosition;
    public Vector2 minPosition;
    void Update()
    {
        Vector3 player = new Vector3(target.position.x,target.position.y,transform.position.z);
        player.x = Mathf.Clamp(player.x, minPosition.x, maxPosition.x);
        player.y = Mathf.Clamp(player.y, minPosition.y, maxPosition.y);
        transform.position = new Vector3(player.x + offset.x, player.y + offset.y, offset.z);
    }
}
