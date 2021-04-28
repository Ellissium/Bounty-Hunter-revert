using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private GameObject addedItem;
    [SerializeField] private MoneyDisplaying moneyDisplaying;
    public static GameManager instance = null;
    public GameObject getKeyE = null;
    public GameObject board = null;
    public GameObject player;

    private GameObject temp;
   

    public Sprite yellowButtonSprite;
    public Sprite redButtonSprite;
    public Vector3 startButtonPos;
    private bool moveUp = true;
    private bool inRangeOfBuy = false;
    private bool repeatingText = false;

    public static GameManager Instance { get { return instance; } }
    public bool RepeatingText { get { return repeatingText; } set { repeatingText = value; } }
    public GameObject AddedItem { get { return addedItem; } set { addedItem = value; } }


    private void Start()
    {
        getKeyE.SetActive(false);
        board.SetActive(false);
        startButtonPos = new Vector3(getKeyE.transform.position.x, getKeyE.transform.position.y, getKeyE.transform.position.z);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            getKeyE.SetActive(true);
            board.SetActive(true);
            inRangeOfBuy = true;
            InvokeRepeating("ChangeButton", 1f, 1f);
            InvokeRepeating("ButtonMove", 1f, 1f);  
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            getKeyE.SetActive(false);
            board.SetActive(false);
            inRangeOfBuy = false;
            CancelInvoke("ChangeButton");
            CancelInvoke("ButtonMove");
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


    private void BuyItem() 
    {
        if (Input.GetKeyDown(KeyCode.E) && CharacterStats.instance.Money >= 10)
        {   
            temp = Instantiate(addedItem, startButtonPos, Quaternion.identity);
            Destroy(temp, 1f);
            CharacterStats.instance.Money -= 10;
            CharacterStats.instance.AmmoReload += 1;
            CharacterStats.instance.onAmmoChanged();   
        }
        else if (Input.GetKeyDown(KeyCode.E) && repeatingText == false)
        {
            repeatingText = true;
            StartCoroutine(moneyDisplaying.DrawNoMoney());
        }
       
    }

    private void Update()
    {
        if (inRangeOfBuy) 
        {
            BuyItem();
        } 
    }
}
