using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private Rigidbody2D rbody;
    [SerializeField] private Animator animator;
    
    
    public void SetBulletDirection(Vector2 lastInputVector)
    {
        animator.SetFloat("Horizontal", lastInputVector.x);
        animator.SetFloat("Vertical", lastInputVector.y);
        rbody.velocity = bulletSpeed * lastInputVector;
    }
}
