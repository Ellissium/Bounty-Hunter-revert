using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBot : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private Rigidbody2D rbody;
    [SerializeField] private Animator animator;

    public Animator Animator { get { return animator; } }
    private void Start()
    {
        animator.Play("Shoot");
    }
    public void SetBulletDirection(Vector2 dir)
    {
        animator.SetFloat("Horizontal", dir.x);
        animator.SetFloat("Vertical", dir.y);
        rbody.velocity = bulletSpeed * dir;
    }

    public void OnBulletDestroyed()
    {
        Destroy(gameObject);
    }
}
