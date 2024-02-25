using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public CanvasGroup SettingsPanel;
    public CanvasGroup CreditsPanel;

    public void PlayGame()
    {
        SceneManager.LoadScene("MASTER_MainLevel");
    }

    public void Option()
    {
        SettingsPanel.alpha = 1;
        SettingsPanel.blockRaycast = true;
    }

    public void Credits()
    {
        
    }

    public void Back()
    {
        SettingsPanel.alpha = 0;
        SettingsPanel.blockRaycast = false;
    }

    public void Quit()
    {
        
    }
}
