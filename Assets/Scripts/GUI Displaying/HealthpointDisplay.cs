using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthpointDisplay : MonoBehaviour
{

    [SerializeField] private Image healthPoint;

    private int healthPoints;
    private List<Image> healthPointsList = new List<Image>();
    private Vector3 offset = new Vector3(122f, -24f,0f);
    private Vector3 basicOffset;
    private void Start()
    {
        basicOffset = new Vector3(offset.x, offset.y, offset.z);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
