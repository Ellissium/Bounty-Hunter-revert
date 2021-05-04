using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddedItems : MonoBehaviour
{
    public LootBoxType lootType;

    [SerializeField] private Sprite addBulletSprite;
    [SerializeField] private Sprite addHealthSprite;
    [SerializeField] private Sprite addMoneySprite;
    [SerializeField] private SpriteRenderer sprite;

    private Vector3 startButtonPos;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        Color color = sprite.material.color;
        color.a = 0f;
        sprite.material.color = color;
        startButtonPos = new Vector3(sprite.transform.position.x, sprite.transform.position.y, sprite.transform.position.z);
        StartCoroutine(Invisible());
    }

    public IEnumerator Invisible()
    {
        if (lootType == LootBoxType.Money)
        {
            GetComponent<SpriteRenderer>().sprite = addMoneySprite;
        }
        else if (lootType == LootBoxType.Bullet )
        {
            GetComponent<SpriteRenderer>().sprite = addBulletSprite;
        }
        else if (lootType == LootBoxType.Health)
        {
            GetComponent<SpriteRenderer>().sprite = addHealthSprite;
        }
            Color color = sprite.material.color;
            color.a = 1f;
            sprite.material.color = color;
            for (float f = 1f; f >= -0.05f; f -= 0.05f)
            {
                sprite.transform.Translate(0, 0.01f, 0);
                color.a = f;
                sprite.material.color = color;
                yield return new WaitForSeconds(0.05f);
            }
            sprite.transform.position = startButtonPos;
    }
}
