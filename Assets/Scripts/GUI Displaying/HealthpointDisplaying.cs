using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthpointDisplaying : MonoBehaviour
{
    [SerializeField] private LocalizationMainScene localizationMainScene;
    [SerializeField] private Text healthAmount;
    [SerializeField] private Image healthPoint;
    [SerializeField] private NPC npc;
    [SerializeField] private GameObject background;

    private Text currentHealthText;
    private int healthPoints;
    private List<Image> healthPointsList = new List<Image>();
    private Vector3 offset;
    private Vector3 basicOffset;
    private void Start()
    {
        currentHealthText = Instantiate(healthAmount, transform);
        currentHealthText.text = "";
        background.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, 17f);
        basicOffset = new Vector3(145f, -25f, 0f);
        DrawHearts();
        CharacterStats.instance.onHeatlhChanged += DrawHearts;
    }
    private void DrawHearts()
    {
        offset = new Vector3(basicOffset.x,basicOffset.y,basicOffset.z);
        ClearHearts();
        healthPoints = CharacterStats.instance.HealthPoint;
        Image currentPoint;
        for (int i = 0; i < healthPoints; i++)
        {
            currentPoint = Instantiate(healthPoint, transform);
            currentPoint.transform.position += offset;
            RectTransform rect = currentPoint.GetComponent<RectTransform>();
            offset = new Vector3(offset.x + rect.rect.width * 4.5f, offset.y, offset.z);
            healthPointsList.Add(currentPoint);
        }
    }

    private void ClearHearts()
    {
        foreach (Image i in healthPointsList)
        {
            Destroy(i.gameObject);
        }
        healthPointsList.Clear();
    }

    public IEnumerator DrawMaxHealth()
    {
        for (int i = 1; i < 15; i++)
        {
            background.GetComponent<RectTransform>().sizeDelta += new Vector2(10f, 0f);
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(0.43f);

        for (int i = 1; i < 4; i++)
        {
            if (localizationMainScene.Localizationtype == "en")
            {
                currentHealthText.fontSize = 14;
                currentHealthText.fontStyle = FontStyle.Normal;
                currentHealthText.text = "You Have Max Health";
            }
            else
            {
                currentHealthText.fontSize = 8;
                currentHealthText.fontStyle = FontStyle.Bold;
                currentHealthText.text = "Ó ÂÀÑ ÌÀÊÑÈÌÀËÜÍÅ ÇÄÎÐÎÂ'ß";
            }
            yield return new WaitForSeconds(1f);
            currentHealthText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
       
        for (int i = 1; i < 15; i++)
        {
            background.GetComponent<RectTransform>().sizeDelta -= new Vector2(10f, 0f);
            yield return new WaitForSeconds(0.02f);
        }
        npc.MaxHealthText = false;
    }
}
