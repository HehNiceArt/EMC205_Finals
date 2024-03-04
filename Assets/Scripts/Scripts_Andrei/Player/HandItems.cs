using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandItems : MonoBehaviour
{
    public Sprite Pitchfork;
    public Sprite Slingshot;

    public Image Hand;
    private KeyCode[] _keyCodes =
    {
        KeyCode.Alpha1,
        KeyCode.Alpha2
    };

    private void Start()
    {
        Hand.sprite = Pitchfork;
    }
    private void Update()
    {
        if (Input.GetKeyDown(_keyCodes[0]))
        {
            Hand.sprite = Pitchfork;
        }
        else if (Input.GetKeyDown(_keyCodes[1]))
        {
            Hand.sprite = Slingshot;
        }
    }
}
