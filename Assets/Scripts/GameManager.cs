using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        } 
        else if (instance == this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        InitializeGameManager();
    }

    private void InitializeGameManager()
    {
        // TODO: When manager has been initialized   
    }

    private void Update()
    {
        
    }
}
