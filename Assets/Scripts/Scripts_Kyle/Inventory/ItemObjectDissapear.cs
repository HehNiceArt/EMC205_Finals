using UnityEngine;

public class ItemObjectDissapear : MonoBehaviour
{
    public GameObject Inventory;

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
            AudioManage.instance.PlayFreezeSound();
        }
    }
}
