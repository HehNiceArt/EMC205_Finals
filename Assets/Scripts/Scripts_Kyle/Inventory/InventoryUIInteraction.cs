//@Kyle Rafael
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryUIInteraction : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerExitHandler
{
    [SerializeField] GameObject ClickedItemUI;

    public Transform DraggedItemParent;
    public Transform DraggedItem;
    public void OnBeginDrag(PointerEventData eventData)
    {

        if (transform.GetComponent<Slot>().ItemInSlot == null)
            return;
        print("Begin Drag");
        DraggedItemParent = transform;
        DraggedItem = DraggedItemParent.GetComponentInChildren<RawImage>().transform;
        DraggedItemParent.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        DraggedItem.parent = FindObjectOfType<Canvas>().transform;
    }


    public void OnDrag(PointerEventData eventData)
    {
        print("Dragging");
        DraggedItem.position = Input.mousePosition;
        DraggedItem.GetComponent<RawImage>().raycastTarget = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DraggedItem.parent = DraggedItemParent;
        DraggedItem.localPosition = new Vector3(0, 0, 0);
        DraggedItem.GetComponent<RawImage>().raycastTarget = true;
        DraggedItemParent.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
        DraggedItemParent.GetComponent<Slot>().SetStats();
        DraggedItem = null;
        DraggedItemParent = null;
        print("End Drag");

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerClick.GetComponent<Slot>().ItemInSlot == null || ClickedItemUI.activeInHierarchy)
            return;

        Vector3 _newPosition = new Vector3(900, 450, 300);
        ClickedItemUI.transform.position = _newPosition;
        ClickedItemUI.GetComponent<ClickedItem>().ClickedSlot = eventData.pointerClick.GetComponent<Slot>();
        ClickedItemUI.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ClickedItemUI.SetActive(false);
    }

}
