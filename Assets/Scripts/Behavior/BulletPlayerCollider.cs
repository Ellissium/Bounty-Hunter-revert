using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayerCollider : MonoBehaviour
{
    public GameObject instance;
    public GameObject enemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Bullet>() != null)
        {
            Destroy(collision.gameObject);
            enemy.GetComponent<Enemy>().HealthPoint -= 1;
            AudioManager.instance.Play("Damage");
            StartCoroutine(MakeRed());
        }
    }
    private IEnumerator MakeRed()
    {
        instance.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.3f);
        instance.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
