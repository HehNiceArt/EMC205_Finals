//@Kyle Rafael
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ClickedItem : MonoBehaviour
{
    public Slot ClickedSlot;

    [SerializeField] RawImage Image;

    [SerializeField] TextMeshProUGUI TextName;
   

    [SerializeField] TextMeshProUGUI TextDescription;

    private void OnEnable()
    {
        SetUp();
    }

    void SetUp()
    {
        TextName.text = ClickedSlot.ItemInSlot.name;
        TextDescription.text = ClickedSlot.ItemInSlot.Description;
    }

}
