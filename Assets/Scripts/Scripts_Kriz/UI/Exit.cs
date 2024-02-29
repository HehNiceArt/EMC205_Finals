using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] GameObject quitPanel;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenAreYouSure()
    {
        quitPanel.SetActive(true);
    }

    public void CloseAreYouSure()
    {
        quitPanel.SetActive(false);
    }
}
