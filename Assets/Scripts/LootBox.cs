using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LootBoxType
{
    Money,
    Bullet,
    Health
}

public class LootBox : MonoBehaviour
{
    [SerializeField] private LootBoxType lootType;

    [SerializeField] private GameObject addedItem; 
    [SerializeField] private GameObject getKeyE;
    [SerializeField] private GameObject lootBox;
    [SerializeField] private GameObject player;
    private GameObject temp;

    [SerializeField] private Sprite yellowButtonSprite;
    [SerializeField] private Sprite redButtonSprite;
    [SerializeField] private Sprite emptyBag;
    [SerializeField] private Sprite emptyBox;

    private Vector3 startButtonPos;

    private bool isLooted = false;
    private bool moveUp = true;

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
        if (lootType == LootBoxType.Bullet)
        {
            LootBoxBullet();
        }
        else if (lootType == LootBoxType.Money)
        {
            LootBoxMoney();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            getKeyE.SetActive(false);
            CancelInvoke("ChangeButton");
            CancelInvoke("ButtonMove");
        }
    }

    private void LootBoxBullet()
    {
        if (Input.GetKey(KeyCode.E) && isLooted == false)
        {
            temp = Instantiate(addedItem, startButtonPos, Quaternion.identity);
            temp.GetComponent<AddedItems>().lootType = lootType;
            Destroy(temp, 1f);
            getKeyE.SetActive(false);
            CharacterStats.instance.AmmoReload += 1;
            CharacterStats.instance.ReloadAmmo();
            CharacterStats.instance.onAmmoChanged();
            lootBox.GetComponent<SpriteRenderer>().sprite = emptyBox;
            isLooted = true;
            AudioManager.instance.PlayNew("PickUp");
        }
    }

    private void LootBoxMoney()
    {
        if (Input.GetKey(KeyCode.E) && isLooted == false)
        {
            temp = Instantiate(addedItem, startButtonPos, Quaternion.identity);
            temp.GetComponent<AddedItems>().lootType = lootType;
            Destroy(temp, 10f);
            getKeyE.SetActive(false);
            CharacterStats.instance.Money += 10;
            CharacterStats.instance.onAmmoChanged();
            lootBox.GetComponent<SpriteRenderer>().sprite = emptyBag;
            isLooted = true;
            AudioManager.instance.PlayNew("PickUp");
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

