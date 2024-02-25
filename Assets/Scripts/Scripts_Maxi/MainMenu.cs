using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("MASTER_MainLevel");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
