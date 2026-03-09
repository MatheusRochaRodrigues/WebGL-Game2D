using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileItem : MonoBehaviour
{
    public Image _img;
    public Item _item;

    public void setScripItem(Item item)
    {
        _item = item;
        _img.sprite = item.img;
    }
}
