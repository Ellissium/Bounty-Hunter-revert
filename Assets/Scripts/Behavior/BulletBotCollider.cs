using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBotCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<BulletBot>() != null)
        {
            Destroy(collision.gameObject);
            StartCoroutine(MakeRed());
            CharacterStats.instance.HealthPoint -= 1;
            CharacterStats.instance.onHeatlhChanged();
        }
    }
    private IEnumerator MakeRed()
    {
        GameManager.instance.player.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.3f);
        GameManager.instance.player.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
