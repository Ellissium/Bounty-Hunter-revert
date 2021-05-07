using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance = null;
    public GameObject player;
    public int countOfEnemies;
    private bool wasExploded = false;
    public List<long> recordsInSeconds;
    public static GameManager Instance { get { return instance; } }


    private void Awake()
    {
        AudioManager.instance.Stop("MenuMusic");
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            recordsInSeconds = new List<long>();
        }
        AudioManager.instance.Play("GameMusic");
    }

    private void InitializeGameManager()
    {
        countOfEnemies = 0;
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
        if (CharacterStats.instance.HealthPoint <= 0)
        {
            player.GetComponent<Character>().grounding.movementSpeed = 0f;
            player.GetComponent<Character>().state.ChangeState(null);
            player.GetComponent<Character>().animator.Play("Destroy");
            player.GetComponent<Character>().rbody.velocity = Vector2.zero;
            if (!wasExploded)
            {
                AudioManager.instance.Play("Explosion");
                wasExploded = true;
            }
        }
    }
}
