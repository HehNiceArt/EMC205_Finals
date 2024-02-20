using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandItems : MonoBehaviour
{
    public GameObject[] TreeItems;
    public GameObject[] TreeItemBG;
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
