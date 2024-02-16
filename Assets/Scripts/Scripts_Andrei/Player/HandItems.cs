using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandItems : MonoBehaviour
{
    public GameObject[] TreeItems;

    private void Start()
    {
        foreach (var item in TreeItems)
        {
            item.SetActive(false);
        }
    }

    public void ItemCheck()
    {
        foreach (var item in TreeItems)
        {
           if(item.activeInHierarchy == true)
            {
            } 
        }
    }
}
