using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplaying : MonoBehaviour
{
    [SerializeField] private LocalizationMainScene localizationMainScene;
    [SerializeField] private Text moneyAmount;
    [SerializeField] private NPC npc;
    [SerializeField] private GameObject background;
    [SerializeField] private Sprite shortBackground;
    [SerializeField] private Sprite longBackground;
    [SerializeField] private Text currentMoneyText;

    private int money;
    private Vector3 offset = new Vector3(0f, 0f, 0f);
   
    private void Start()
    {
        money = CharacterStats.instance.Money;
        currentMoneyText.rectTransform.position += new Vector3(offset.x, offset.y, offset.z);
        CharacterStats.instance.onAmmoChanged += DrawMoneyStats;
        DrawMoneyStats();
    }

    public void DrawMoneyStats()
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

                if (localizationMainScene.Localizationtype == "en")
                {
                    currentMoneyText.fontSize = 14;
                    currentMoneyText.fontStyle = FontStyle.Normal;
                    currentMoneyText.text = "Not   Enough   Money!";
                }
                else
                {
                    currentMoneyText.fontSize = 10;
                    currentMoneyText.fontStyle = FontStyle.Bold;
                    currentMoneyText.text = "Õ≈   ƒŒ—“¿“Õ‹Œ    Œÿ“≤¬!";
                }
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
