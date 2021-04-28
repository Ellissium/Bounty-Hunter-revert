using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplaying : MonoBehaviour
{
    [SerializeField] private Text moneyAmount;
    [SerializeField] private NPC npc;

    private int money;
    private Text currentMoneyText;
    private Vector3 offset = new Vector3(122f, -76f, 0);
   
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
        if (money > 0)
        {
            currentMoneyText.text = $"{money.ToString()}$";
        }
        else if (money == 0)
        {
            currentMoneyText.text = $"{money.ToString()}$";
        }
    }
    public IEnumerator DrawNoMoney()
    {
        for (int i = 1; i < 4; i++)
        {
            if (CharacterStats.instance.Money == 0)
                currentMoneyText.text = "Not Enough Money!";
            yield return new WaitForSeconds(1f);
            if (CharacterStats.instance.Money == 0)
                currentMoneyText.text = $"{money.ToString()}$";
            yield return new WaitForSeconds(0.5f);
        }
        npc.RepeatingText = false;
    }
}
