using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioSourcePlayerManager : MonoBehaviour
{
    public AudioClip mainMenuBGM;
    public AudioClip gameBGM;

    private static AudioSourcePlayerManager instance = null;
    private new AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        PlaySceneBGM();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        if (instance == this) return;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // Check for scene changes
        if (SceneManager.GetActiveScene().name == "MASTER_Start" && audio.clip != mainMenuBGM)
        {
            audio.clip = mainMenuBGM;
            PlaySceneBGM();
        }

        else if (SceneManager.GetActiveScene().name == "MASTER_MainLevel" && audio.clip != gameBGM)
        {
            audio.clip = gameBGM;
            PlaySceneBGM();
        }
    }

    void PlaySceneBGM()
    {
        audio.Stop();
        audio.Play();
    }
}