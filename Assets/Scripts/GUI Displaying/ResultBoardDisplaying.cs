using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultBoardDisplaying : MonoBehaviour
{
    public Image board;
    public Sprite gameOverSprite;
    public Sprite winSprite;
    public float goalPosY;
    private bool wasPlayed = false;

    void Start()
    {
        
    }

    private void Update()
    {
        if (CharacterStats.instance.HealthPoint <= 0)
        {
            if (!wasPlayed)
            {
                GameManager.instance.player.GetComponent<Character>().record = false;
                GameManager.instance.recordsInSeconds.Add(GameManager.instance.player.GetComponent<Character>().seconds);
                AudioManager.instance.Play("GameOver");
                wasPlayed = true;
            }
            board.sprite = gameOverSprite;
            AudioManager.instance.Stop("GameMusic");
            StartCoroutine(MoveBoard());
            StartCoroutine(LoadMenu());
        }
        if (GameManager.instance.countOfEnemies <= 0)
        {
            if (!wasPlayed)
            {
                GameManager.instance.player.GetComponent<Character>().record = false;
                GameManager.instance.recordsInSeconds.Add(GameManager.instance.player.GetComponent<Character>().seconds);
                AudioManager.instance.Play("Win");
                wasPlayed = true;
            }
            board.sprite = winSprite;
            AudioManager.instance.Stop("GameMusic");
            StartCoroutine(MoveBoard());
            StartCoroutine(LoadMenu());
        }
    }
    private IEnumerator MoveBoard()
    {
        while(board.rectTransform.position.y > goalPosY)
        {
            board.rectTransform.position = new Vector3(board.rectTransform.position.x, board.rectTransform.position.y-0.05f, board.rectTransform.position.z);
            yield return new WaitForEndOfFrame();
        }
    }
    private IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("MainMenu");
    }
}
