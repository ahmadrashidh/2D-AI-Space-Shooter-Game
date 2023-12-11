using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject healthCanvas;
    public GameObject menuCanvas;
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
        gameOverScreen.SetActive(true);
        healthCanvas.SetActive(false);
        player.SetActive(false);
    }
}
