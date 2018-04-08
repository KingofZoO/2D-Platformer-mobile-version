using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    private GameManagerScript GameManager;

    private void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        GameManager.ShowGamePanel();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadScene2()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }

    public void LoadScene3()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(3);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
