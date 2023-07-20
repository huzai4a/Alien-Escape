using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject playerScreenUI;
    public GameObject winScreenUI;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScreenUI.activeInHierarchy)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        } else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void gameOver()
    {
        Time.timeScale = 0;
        playerScreenUI.SetActive(false);
        gameOverUI.SetActive(true);
    }

   /* public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        playerScreenUI.SetActive(true);
        Time.timeScale = 1;
    }*/

    public void win()
    {
        playerScreenUI.SetActive(false);
        winScreenUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void mainMenu ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void quit()
    {
        Application.Quit(); 
    }
}
