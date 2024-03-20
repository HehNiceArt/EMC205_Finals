using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupAudio : MonoBehaviour
{
    public GameObject pickUp;
    // Start is called before the first frame update
    void Start()
    {
        pickUp.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("E"))
        {
            PickUpSound();
        }

        if (Input.GetKeyUp("E"))
        {
            StopPickUpSound();
        }
    }

    void PickUpSound()
    {
        pickUp.SetActive(true);
    }

    void StopPickUpSound()
    {
        pickUp.SetActive(false);
    }
}
