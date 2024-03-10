using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    //resolution
    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;
    
    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
           
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
       
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    //fullscreen
    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    //volume
    [SerializeField] private Slider musicSlider;

    public AudioMixer audioMixer;

    public void SetVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("volume", volume);
    }

    //back button
    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
