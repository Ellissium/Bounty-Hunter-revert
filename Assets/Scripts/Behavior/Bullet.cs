using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private Rigidbody2D rbody;
    [SerializeField] private Animator animator;
    
    public Animator Animator { get { return animator; } }
    
    public void SetBulletDirection(Vector2 lastInputVector)
    {
        animator.SetFloat("Horizontal", lastInputVector.x);
        animator.SetFloat("Vertical", lastInputVector.y);
        rbody.velocity = bulletSpeed * lastInputVector;
    }

    public void OnBulletDestroyed()
    {
        Destroy(gameObject);
    }
}
