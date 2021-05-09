using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.Play("MenuMusic");
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("EllissiumScene");
      
    }

    public void ExitGame() 
    {
        Application.Quit();
    }

}
