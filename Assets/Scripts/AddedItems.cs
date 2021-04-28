using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddedItems : MonoBehaviour
{
    public SpriteRenderer sprite;
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
