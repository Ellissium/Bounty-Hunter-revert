using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplaying : MonoBehaviour
{
    [SerializeField] private Text moneyAmount;
    [SerializeField] private NPC npc;
    [SerializeField] private GameObject background;
    [SerializeField] private Sprite shortBackground;
    [SerializeField] private Sprite longBackground;
    
    private int money;
    private Text currentMoneyText;
    private Vector3 offset = new Vector3(-50f, -50f, 0);
   
    private void Start()
    {
        money = CharacterStats.instance.Money;
        currentMoneyText = Instantiate(moneyAmount, transform);
        currentMoneyText.rectTransform.position += new Vector3(offset.x, offset.y, offset.z);
        CharacterStats.instance.onAmmoChanged += DrawMoneyStats;
        DrawMoneyStats();
    }

    private void DrawMoneyStats()
    {
        money = CharacterStats.instance.Money;
        currentMoneyText.text = $"{money.ToString()}$";
        background.GetComponent<Image>().sprite = shortBackground;
        background.GetComponent<RectTransform>().sizeDelta = new Vector2(53f, 17f);

    }

    public IEnumerator DrawNoMoney()
    {
        for (int i = 1; i < 4; i++)
        {
            if (CharacterStats.instance.Money <= 0)
            {
                background.GetComponent<Image>().sprite = longBackground;
                background.GetComponent<RectTransform>().sizeDelta = new Vector2(140f, 17f);
                currentMoneyText.text = "Not Enough Money!";
                yield return new WaitForSeconds(1f);
            }
         
            if (CharacterStats.instance.Money <= 0)
            {
                background.GetComponent<Image>().sprite = shortBackground;
                background.GetComponent<RectTransform>().sizeDelta = new Vector2(53f, 17f);
                currentMoneyText.text = $"{money.ToString()}$";
                yield return new WaitForSeconds(0.5f);
            }
        }
        npc.RepeatingText = false;
    }
}
