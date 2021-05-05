using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        AudioManager.instance.Play("MenuMusic");
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("MikloshScene");
    }

    public void ExitGame() 
    {
        Application.Quit();
    }

}
