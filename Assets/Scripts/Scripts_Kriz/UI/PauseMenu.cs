using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!GameIsPaused)
            {
                Resume();
            }

            else
            {
                Pause();
            }
        }
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }


    //i'm not done yet sldjkfhjsdfhjksdhfn

    //[SerializeField] GameObject pauseMenu;

    //public void Settings()
    //{
    //SceneManager.LoadScene("Main Menu");
    //}
    // public void Home()
    //{
    // SceneManager.LoadScene("Main Menu");
    //}
    //public void ExitGame()
    //{

    //}
}