using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class InventoryItem : MonoBehaviour
{
    public void SetItem(Item item)
    {
        var icon = GetComponent<Image>();
        if (item == null)
        { icon.enabled = false; }
        else { icon.enabled = true;
            icon.sprite = item.GetIcon();
        }
    }
    public Sprite GetItem()
    {
        var icon = GetComponent<Image>();
        if(!icon.enabled)
        { return null; }
        return icon.sprite;
    }
}
