using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsAudio : MonoBehaviour
{
    public GameObject footstep;
    // Start is called before the first frame update
    void Start()
    {
        footstep.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //true
        if (Input.GetKeyDown("W"))
        {
            Footsteps();
        }
        if (Input.GetKeyDown("A"))
        {
            Footsteps();
        }
        if (Input.GetKeyDown("S"))
        {
            Footsteps();
        }
        if (Input.GetKeyDown("D"))
        {
            Footsteps();
        }

        //false
        if (Input.GetKeyUp("W"))
        {
            StopFootsteps();
        }
        if (Input.GetKeyUp("A"))
        {
            StopFootsteps();
        }
        if (Input.GetKeyUp("S"))
        {
            StopFootsteps();
        }   
        if (Input.GetKeyUp("D"))
        {
            StopFootsteps();
        }
    }

    void Footsteps()
    {
        footstep.SetActive (true);
    }

    void StopFootsteps()
    {
        footstep.SetActive(false);
    }
}
