using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool menuActivated;
    public static bool GameIsPaused = false;
    [SerializeField] GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && menuActivated)
        {
                Time.timeScale = 1;
                pauseMenuUI.SetActive(false);
                menuActivated = false;
            }

            else if (Input.GetKeyUp(KeyCode.Escape) && !menuActivated)
            {
                Time.timeScale = 0;
                pauseMenuUI.SetActive(true);
                menuActivated = true;
            }
        }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
    }
}