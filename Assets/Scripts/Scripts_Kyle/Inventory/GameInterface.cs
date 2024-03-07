using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInterface : MonoBehaviour
{
    public GameObject PlayerItems, Inventory;

    private bool playerActive = true; 
    private bool inventoryActive = false; 

    public void Update()
    {
        PlayerFreeze();
    }

    public void PlayerFreeze()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerActive = !playerActive;
            PlayerItems.SetActive(playerActive);

            if (playerActive) 
            {
                Inventory.SetActive(true);
                inventoryActive = false;
            }
            else 
            {
                inventoryActive = !inventoryActive;
                Inventory.SetActive(inventoryActive);
            }
        }
    }
}
