//@Kyle Rafael
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public TreeGrowthItems ItemInSlot;
    public int AmountInSlot;

    RawImage _icon;
    TextMeshProUGUI _textAmount;

    InventorySystem inventorySystem; 

    private void Start()
    {
        inventorySystem = FindObjectOfType<InventorySystem>();
    }

    public void SetStats()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

        _icon = GetComponentInChildren<RawImage>();
        _textAmount = GetComponentInChildren<TextMeshProUGUI>();

        if (ItemInSlot == null || AmountInSlot <= 0) 
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
                AmountInSlot--;
                TreeScaleCalculation.Instance.IncreaseScale(ItemInSlot);
                SetStats();
            }
        }
    }
}
