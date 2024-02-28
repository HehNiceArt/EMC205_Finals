using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [Header("Fullscreen")]
    [SerializeField] private Toggle fullScreen = null;

    //[Header("Resolution")]
    //[SerializeField] private Dropdown resolutionList = null;
    
    [Header("Volume Setting")]
    [SerializeField] private Slider volumeSlider = null;

    public void FullscreenChange()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void VolumeSetting()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
    }
}
