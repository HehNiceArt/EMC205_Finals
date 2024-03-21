using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHeld : MonoBehaviour
{
    public GameObject pitchfork, slingshot;
    PlayerRaycast _playerRay;
    private KeyCode[] _keyCodes =
    {
        KeyCode.Alpha1,
        KeyCode.Alpha2
    };
    private void Start()
    {
        _playerRay = GetComponent<PlayerRaycast>();
    }
    private void Update()
    {
        // Handle item selection via number keys
        if (Input.GetKeyDown(_keyCodes[0]))
        {
            _playerRay.IsPitchfork = true;
            _playerRay.IsSlingshot = false;
            SetActiveItem(pitchfork);
        }
        else if (Input.GetKeyDown(_keyCodes[1]))
        {
            _playerRay.IsPitchfork = false;
            _playerRay.IsSlingshot = true;
            SetActiveItem(slingshot);
        }

        // Handle item selection via mouse scroll wheel
        //float scroll = Input.GetAxisRaw("Mouse ScrollWheel");
        //if (scroll != 0f)
        //{
        //    if (scroll > 0f)
        //    {
        //        // Scroll up
        //        ScrollUp();
        //    }
        //    else
        //    {
        //        // Scroll down
        //        ScrollDown();
        //    }
        //}
    }

    private void SetActiveItem(GameObject item)
    {
        pitchfork.SetActive(item == pitchfork);
        slingshot.SetActive(item == slingshot);
    }

    //private void ScrollUp()
    //{
    //    int activeIndex = GetActiveIndex();
    //    if (activeIndex < 2)
    //    {
    //        activeIndex++;
    //        SetActiveItem(GetItemAtIndex(activeIndex));
    //    }
    //}

    //private void ScrollDown()
    //{
    //    int activeIndex = GetActiveIndex();
    //    if (activeIndex > 0)
    //    {
    //        activeIndex--;
    //        SetActiveItem(GetItemAtIndex(activeIndex));
    //    }
    //}

    private int GetActiveIndex()
    {
        if (pitchfork.activeSelf)
            return 0;
        if (slingshot.activeSelf)
            return 1;
        return 0; 
    }

    private GameObject GetItemAtIndex(int index)
    {
        switch (index)
        {
            case 0:
                return pitchfork;
            case 1:
                return slingshot;
            default:
                return null;
        }
    }
}
