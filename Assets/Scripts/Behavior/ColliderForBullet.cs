using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderForBullet : MonoBehaviour
{
    [SerializeField] private GameObject instance;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Bullet>() != null)
        {
            collision.GetComponent<Bullet>().Animator.Play("Bullet Destroyed");
            collision.GetComponent<Bullet>().SetBulletDirection(Vector2.zero);
        }
    }
}
