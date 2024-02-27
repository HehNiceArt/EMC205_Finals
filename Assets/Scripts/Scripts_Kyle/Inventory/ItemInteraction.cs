//@Kyle Rafael
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Rendering;
public class ItemInteraction : MonoBehaviour
{
    Transform cam;
    [SerializeField] LayerMask ItemLayer;
    InventorySystem inventorySystem;

    [SerializeField] TextMeshProUGUI TextHoveredItem;
    void Start()
    {
        cam = Camera.main.transform;
        inventorySystem = GetComponent<InventorySystem>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, 2, ItemLayer))
        {
            if (!hit.collider.GetComponent<ItemObject>())
                return;
            TextHoveredItem.text = $"Press 'F' to pick up {hit.collider.GetComponent<ItemObject>().Amount}x {hit.collider.GetComponent<ItemObject>().ItemStats.name}";

            if (Input.GetKeyDown(KeyCode.F))
            {
                inventorySystem.PickUpItem(hit.collider.GetComponent<ItemObject>());
            }



        }
        else
        {
            TextHoveredItem.text = string.Empty;
        }
    }
}
