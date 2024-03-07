//@Kyle Rafael
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public PlayerItems ItemInSlot;
    public int AmountInSlot;

    RawImage _icon;
    TextMeshProUGUI _textAmount;

    InventorySystem inventorySystem; // Reference to the InventorySystem script

    private void Start()
    {
        inventorySystem = FindObjectOfType<InventorySystem>(); // Find the InventorySystem script in the scene
    }

    public void SetStats()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

        _icon = GetComponentInChildren<RawImage>();
        _textAmount = GetComponentInChildren<TextMeshProUGUI>();

        if (ItemInSlot == null || AmountInSlot <= 0) // Check if ItemInSlot is null or amount is zero
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (ItemInSlot != null && AmountInSlot > 0)
            {
                // Decrease the amount of the item
                AmountInSlot--;

                // Update the UI
                SetStats();
            }
        }
    }
}
