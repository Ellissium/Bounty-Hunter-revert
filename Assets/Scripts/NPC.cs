using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private LootBoxType lootType;

    [SerializeField] private GameObject addedItem;
    [SerializeField] private GameObject getKeyE;
    [SerializeField] private GameObject board;
    [SerializeField] private GameObject player;
    private GameObject temp;

    [SerializeField] private MoneyDisplaying moneyDisplaying;
    [SerializeField] private HealthpointDisplaying healthpointDisplaying;

    [SerializeField] private Sprite yellowButtonSprite;
    [SerializeField] private Sprite redButtonSprite;
    private SpriteRenderer boardSprite;
    private SpriteRenderer buttonSprite;

    private Vector3 startButtonPos;

    private float transparency = 0f;

    private bool moveUp = true;
    private bool inRangeOfBuy = false;
    private bool repeatingText = false;
    private bool maxHealthText = false;
    private bool inCollision = false;

    public bool RepeatingText { get { return repeatingText; } set { repeatingText = value; } }
    public bool MaxHealthText { get { return maxHealthText; } set { maxHealthText = value; } }

    private void Start()
    {
        boardSprite = board.GetComponent<SpriteRenderer>();
        buttonSprite = getKeyE.GetComponent<SpriteRenderer>();
        Color colorBoard = boardSprite.material.color;
        Color colorButton = buttonSprite.material.color;
        colorBoard.a = 0f;
        colorButton.a = 0f;
        boardSprite.material.color = colorBoard;
        buttonSprite.material.color = colorButton;
        startButtonPos = new Vector3(getKeyE.transform.position.x, getKeyE.transform.position.y, getKeyE.transform.position.z);
    }


    public IEnumerator Visible() 
    {
        Color colorBoard = boardSprite.material.color;
        Color colorButton = buttonSprite.material.color;
        boardSprite.material.color = colorBoard;
        buttonSprite.material.color = colorButton;

        for (float f = transparency; f <= 1.05f; f += 0.1f)
        {
            if (inCollision == false)
            {
                colorBoard.a = f;
                colorButton.a = f;
                transparency = f;
                boardSprite.material.color = colorBoard;
                buttonSprite.material.color = colorButton;
                yield return new WaitForSeconds(0.05f);
            }
        } 
    }

    public IEnumerator Invisible()
    {
        Color colorBoard = boardSprite.material.color;
        Color colorButton = buttonSprite.material.color;
        boardSprite.material.color = colorBoard;
        buttonSprite.material.color = colorButton;
       
        for (float f = transparency; f >= -0.05f; f -= 0.1f)
        {
            if (inCollision == true)
            {
                colorBoard.a = f;
                colorButton.a = f;
                transparency = f;
                boardSprite.material.color = colorBoard;
                buttonSprite.material.color = colorButton;
                yield return new WaitForSeconds(0.05f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inCollision = false;
        if (collision.tag == "Player" && inCollision == false)
        {
            StartCoroutine(Visible());
            inRangeOfBuy = true;
            InvokeRepeating("ChangeButton", 1f, 1f);
            InvokeRepeating("ButtonMove", 1f, 1f);  
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inCollision = true;
        if (collision.tag == "Player" && inCollision == true)
        {
            StartCoroutine(Invisible());
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

    private void BuyBullet() 
    {
        if (Input.GetKeyDown(KeyCode.E) && CharacterStats.instance.Money >= 10)
        {
            temp = Instantiate(addedItem, startButtonPos, Quaternion.identity);
            temp.GetComponent<AddedItems>().lootType = lootType;
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

    private void BuyHealthPoint()
    {
        if (Input.GetKeyDown(KeyCode.E) && CharacterStats.instance.Money >= 10)
        {
            if (CharacterStats.instance.HealthPoint < CharacterStats.MAX_HEALTHPOINTS)
            {
                temp = Instantiate(addedItem, startButtonPos, Quaternion.identity);
                temp.GetComponent<AddedItems>().lootType = lootType;
                Destroy(temp, 1f);
                CharacterStats.instance.Money -= 10;
                CharacterStats.instance.HealthPoint += 1;
                CharacterStats.instance.onHeatlhChanged();
                Debug.Log(CharacterStats.instance.HealthPoint);
            }
            else if (Input.GetKeyDown(KeyCode.E) && maxHealthText == false)
            {
                maxHealthText = true;
                StartCoroutine(healthpointDisplaying.DrawMaxHealth());
            }
        }
        else if (Input.GetKeyDown(KeyCode.E) && repeatingText == false)
        {
            repeatingText = true;
            StartCoroutine(moneyDisplaying.DrawNoMoney());
        }
    }

    private void Update()
    {
        if (inRangeOfBuy && lootType == LootBoxType.Bullet)
        {
            BuyBullet();
        }
        else if (inRangeOfBuy && lootType == LootBoxType.Health) 
        {
            BuyHealthPoint();
        }
    }
}
