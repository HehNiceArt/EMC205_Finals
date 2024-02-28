using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /**public CanvasGroup StartMenu;
    public CanvasGroup SettingsMenu;
    public CanvasGroup CreditsMenu;   
    public CanvasGroup QuitOption;   **/ 

    public void PlayGame()
    {
        SceneManager.LoadScene("MASTER_MainLevel");
    }

    /**public void Settings()
    {
        StartMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    public void Credits()
    {

    }

    public void Back()
    {

    }**/
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
