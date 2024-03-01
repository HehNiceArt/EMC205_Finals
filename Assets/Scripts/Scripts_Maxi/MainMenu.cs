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

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    /**public void Credits()
    {

    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }**/
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
