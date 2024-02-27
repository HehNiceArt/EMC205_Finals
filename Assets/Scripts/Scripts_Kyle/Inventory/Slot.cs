//@Kyle Rafael 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class Slot : MonoBehaviour, IDropHandler
{
    public Items ItemInSlot;
    public int AmountInSlot;

    RawImage _icon;
    TextMeshProUGUI _textAmount;



    public void SetStats()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

        _icon = GetComponentInChildren<RawImage>();
        _textAmount = GetComponentInChildren<TextMeshProUGUI>();

        if (ItemInSlot == null)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            return;
        }


        _icon.texture = ItemInSlot.Icon;
        _textAmount.text = $"{AmountInSlot}x";
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        InventoryUIInteraction draggableItem = dropped.GetComponent<InventoryUIInteraction>();
        Slot slot = draggableItem.DraggedItemParent.GetComponent<Slot>();

        if (slot == this)
            return;

        ItemInSlot = slot.ItemInSlot;
        AmountInSlot = slot.AmountInSlot;

        slot.ItemInSlot = null;
        slot.AmountInSlot = 0;

        SetStats();
    }
}
