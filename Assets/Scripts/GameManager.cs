using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance = null;
    public GameObject player;

    public static GameManager Instance { get { return instance; } }


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void InitializeGameManager()
    {
        // TODO: When manager has been initialized   
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.X) && Input.GetKeyDown(KeyCode.C))
        {
            CharacterStats.instance.AmmoReload += 1;
            CharacterStats.instance.ReloadAmmo();
            CharacterStats.instance.onAmmoChanged();
            Debug.Log(instance == null);
        }
    }
}
