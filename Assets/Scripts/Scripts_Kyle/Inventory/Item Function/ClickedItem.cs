using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ClickedItem : MonoBehaviour
{
    public Slot clickedSlot;

    [SerializeField] RawImage image;

    [SerializeField] TextMeshProUGUI txt_Name;
   

    [SerializeField] TextMeshProUGUI txt_Description;

    private void OnEnable()
    {
        SetUp();
    }

    void SetUp()
    {
        txt_Name.text = clickedSlot.ItemInSlot.name;
        txt_Description.text = clickedSlot.ItemInSlot.description;
    }

}
