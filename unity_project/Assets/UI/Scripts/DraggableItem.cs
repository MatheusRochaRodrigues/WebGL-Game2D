using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector]public Transform _myParentDrag;
    public Image _img;
    public Item _item;
    public int _count = 1;
    public Text _text;

    public void setScripItem(Item item)
    {
        _item = item;
        _img.sprite = item.img;
        refreshCountText();
    }

    public void refreshCountText()
    {
        _text.text = _count.ToString();
        bool visible = _count > 1;
        _text.gameObject.SetActive(visible);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _myParentDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        _img.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(_myParentDrag);
        _img.raycastTarget = true;
    }
}
