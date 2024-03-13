using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAudio : MonoBehaviour
{
    public GameObject jump;
    // Start is called before the first frame update
    void Start()
    {
        jump.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("SpaceBar"))
        {
            JumpSound();
        }

        if (Input.GetKeyUp("SpaceBar"))
        {
            StopJumpSound();
        }
    }

    void JumpSound()
    {
        jump.SetActive(true);
    }

    void StopJumpSound()
    {
        jump.SetActive(false);
    }
}
