using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class InventoryItem : MonoBehaviour
{
    [SerializeField] GameObject textContainer = null;
    [SerializeField] TextMeshProUGUI itemNumber = null;
    public void SetItem(Item item,int number)
    {
        var icon = GetComponent<Image>();
        if (item == null)
        { icon.enabled = false; }
        else { icon.enabled = true;
            icon.sprite = item.GetIcon();
        }
        if(itemNumber)
        {
            if(number<=1)
            {
                textContainer.SetActive(false);
            }
            else
            {
                textContainer.SetActive(true);
                itemNumber.text = number.ToString();
            }
        }
    }

}
