using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image img;
    public DraggableItem itemDrag = null;

    public void Start()
    {
        img = this.transform.parent.GetComponent<Image>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject objectDropped = eventData.pointerDrag;
            itemDrag = objectDropped.GetComponent<DraggableItem>();
            itemDrag._myParentDrag = transform;
        }
    }
}
