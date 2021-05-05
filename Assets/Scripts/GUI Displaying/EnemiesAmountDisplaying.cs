using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemiesAmountDisplaying : MonoBehaviour
{
    [SerializeField] private Text enemiesAmount;
    private int maxEnemies;
    private int currentEnemies;
    private void Start()
    {
        currentEnemies = maxEnemies;
        DrawAmountOfEnemies();
        maxEnemies = GameManager.instance.countOfEnemies;
    }
    
    public void DrawAmountOfEnemies()
    {
        if (GameManager.instance.countOfEnemies > maxEnemies)
        {
            maxEnemies = GameManager.instance.countOfEnemies;
        }
        currentEnemies = GameManager.instance.countOfEnemies;
        enemiesAmount.text = $"{currentEnemies}/{maxEnemies}";
    }
}
