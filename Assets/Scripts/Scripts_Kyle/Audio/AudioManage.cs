using UnityEngine;

public class AudioManage : MonoBehaviour
{
    public static AudioManage instance;

    [Header("Player Movements")]
    public AudioSource walkingAudioSource;
    public AudioSource runningAudioSource;
    public AudioSource pickupAudioSource;
    public AudioSource jumpAudioSource;
   
    public AudioClip walkingSound;
    public AudioClip runningSound;
    public AudioClip pickupSound;
    public AudioClip jumpSound;
   
    [Header("Player Weapons")]
    public AudioSource meleeAttackAudioSource; 
    public AudioClip meleeAttackSound; 

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

        walkingAudioSource.clip = walkingSound;
        runningAudioSource.clip = runningSound;
        pickupAudioSource.clip = pickupSound;
        jumpAudioSource.clip = jumpSound;

        meleeAttackAudioSource.clip = meleeAttackSound; 
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

    public void PlayJumpSound()
    {
        jumpAudioSource.Play();
    }

    public void PlayMeleeAttackSound() // New method to play melee attack sound
    {
        meleeAttackAudioSource.Play();
    }
}
