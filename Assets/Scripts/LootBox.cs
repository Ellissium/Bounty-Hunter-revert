using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : MonoBehaviour
{
    [SerializeField] private GameObject addedItem;
    public static GameManager instance = null;
    public GameObject getKeyE = null;
    public GameObject player;
    private GameObject temp;
    public Sprite yellowButtonSprite;
    public Sprite redButtonSprite;
    public Vector3 startButtonPos;
    private bool isLooted = false;
    private bool moveUp = true;

    public static GameManager Instance { get { return instance; } }


    private void Start()
    {
        getKeyE.SetActive(false);
        startButtonPos = new Vector3(getKeyE.transform.position.x, getKeyE.transform.position.y, getKeyE.transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player" && isLooted == false)
        {
            getKeyE.SetActive(true);
            InvokeRepeating("ChangeButton", 1, 1);
            InvokeRepeating("ButtonMove", 1f, 1f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.E) && isLooted == false)
        {
            temp = Instantiate(addedItem, startButtonPos, Quaternion.identity);
            Destroy(temp, 1f);
            getKeyE.SetActive(false);
            CharacterStats.instance.AmmoReload += 1;
            CharacterStats.instance.ReloadAmmo();
            CharacterStats.instance.onAmmoChanged();
            isLooted = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            getKeyE.SetActive(false);
            CancelInvoke("ChangeButton");
            CancelInvoke("ButtonMove");
            Debug.Log("o");
        }
    }

    private void ChangeButton()
    {
        if (getKeyE.GetComponent<SpriteRenderer>().sprite == yellowButtonSprite)
        {
            getKeyE.GetComponent<SpriteRenderer>().sprite = redButtonSprite;
            return;
        }
        else
        {
            getKeyE.GetComponent<SpriteRenderer>().sprite = yellowButtonSprite;
            return;
        }
    }

    private void ButtonMove()
    {
        if (getKeyE.transform.position.y >= (startButtonPos.y + 0.001f))
        {
            moveUp = false;  
        }
        else if (getKeyE.transform.position.y <= startButtonPos.y)
        {
            moveUp = true;
        }
        if (moveUp)
        {
            getKeyE.transform.Translate(0, 0.01f, 0);
        }
        else { getKeyE.transform.Translate(0, -0.01f, 0); }
    }
}

