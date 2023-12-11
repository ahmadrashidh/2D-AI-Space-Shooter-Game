using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject healthCanvas;
    public GameObject menuCanvas;
    public GameObject winCanvas;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void startGame()
    {
        healthCanvas.SetActive(true);
        player.SetActive(true);
        menuCanvas.SetActive(false);
    }

    public void GameOver()
    {
        Text title = gameOverScreen.GetComponentInChildren<Text>();
        title.text = "Game Over";
        gameOverScreen.SetActive(true);
        healthCanvas.SetActive(false);
        player.SetActive(false);
    }

    public void Win()
    {
        Text title = gameOverScreen.GetComponentInChildren<Text>();
        Debug.Log("title:" + title);
        title.text = "You Win!";
        gameOverScreen.SetActive(true);
        healthCanvas.SetActive(false);
        player.SetActive(false);

    }
}
