using UnityEngine;

public class AudioManage : MonoBehaviour
{
    public static AudioManage instance;

    public AudioSource walkingAudioSource;
    public AudioSource runningAudioSource;
    public AudioSource pickupAudioSource;

    public AudioClip walkingSound;
    public AudioClip runningSound;
    public AudioClip pickupSound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // Assign audio clips to audio sources
        walkingAudioSource.clip = walkingSound;
        runningAudioSource.clip = runningSound;
        pickupAudioSource.clip = pickupSound;
    }

    public void PlayWalkingSound(bool isWalking)
    {
        if (isWalking)
        {
            if (!walkingAudioSource.isPlaying)
            {
                walkingAudioSource.Play();
            }
        }
        else
        {
            walkingAudioSource.Stop();
        }
    }

    public void PlayRunningSound(bool isRunning)
    {
        if (isRunning)
        {
            if (!runningAudioSource.isPlaying)
            {
                runningAudioSource.Play();
            }
        }
        else
        {
            runningAudioSource.Stop();
        }
    }

    public void PlayPickupSound()
    {
        pickupAudioSource.Play();
    }
}
