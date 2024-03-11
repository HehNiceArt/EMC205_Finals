//@Kyle Rafael
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{

    [SerializeField] public Slot[] Slots = new Slot[40];
    [SerializeField] GameObject InventoryUI;
    public GameObject ItemDescription;
    private bool menuActivated;


    PlayerMovement pm;
   

    private void Awake()
    {
        pm = GetComponent<PlayerMovement>();
        
        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i].ItemInSlot == null)
            {
                for (int k = 0; k < Slots[i].transform.childCount; k++)
                {
                    Slots[i].transform.GetChild(k).gameObject.SetActive(false);
                }
            }
        }
    }

    private void Update()
    {
        if (!InventoryUI.activeInHierarchy && Input.GetKeyDown(KeyCode.E))
        {
            pm.IsInterrupted = true;
            InventoryUI.SetActive(true);
        }
        else if (InventoryUI.activeInHierarchy && (Input.GetKeyDown(KeyCode.E)))
        {
            pm.IsInterrupted = false;
            InventoryUI.SetActive(false);
           
        }
        else if (InventoryUI.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
        {
            pm.IsInterrupted = false;
            InventoryUI.SetActive(false); 
        }

    }
 
    public void PickUpItem(ItemObject obj)
    {

        for (int i = 0; i < Slots.Length; i++)
        {
            if (Slots[i].ItemInSlot != null && Slots[i].ItemInSlot.ItemID == obj.ItemStats.ItemID && Slots[i].AmountInSlot != Slots[i].ItemInSlot.MaxQuantity)
            {
                if (!WillHitMaxStack(i, obj.Amount))
                {
                    Slots[i].AmountInSlot += obj.Amount;
                    Destroy(obj.gameObject);
                    Slots[i].SetStats();
                    return;
                }
                else
                {
                    int result = NeededToFill(i);
                    obj.Amount = RemainingAmount(i, obj.Amount);
                    Slots[i].AmountInSlot += result;
                    Slots[i].SetStats();
                    PickUpItem(obj);
                    return;
                }
            }
            else if (Slots[i].ItemInSlot == null)
            {
                Slots[i].ItemInSlot = obj.ItemStats;
                Slots[i].AmountInSlot += obj.Amount;
                Destroy(obj.gameObject);
                Slots[i].SetStats();
                return;

            }
        }
    }

    bool WillHitMaxStack(int index, int amount)
    {
        if (Slots[index].ItemInSlot.MaxQuantity <= Slots[index].AmountInSlot + amount)
            return true;
        else
            return false;
    }

    int NeededToFill(int index)
    {
        return Slots[index].ItemInSlot.MaxQuantity - Slots[index].AmountInSlot;
    }
    int RemainingAmount(int index, int amount)
    {
        return (Slots[index].AmountInSlot + amount) - Slots[index].ItemInSlot.MaxQuantity;
    }
}
