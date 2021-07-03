using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour, IDragContainer
{
    [SerializeField] InventoryItem icon = null;

    PlayerInventory inventory;
    int index;

    public void Setup(PlayerInventory inventory , int index)
    {
        this.inventory = inventory;
        this.index = index;
        icon.SetItem(inventory.GetItemFromSlot(index),inventory.GetNumberInSlot(index));
    }

    public Item GetItem()
    {
        return inventory.GetItemFromSlot(index);
    }
    public int GetNumber()
    {
        return inventory.GetNumberInSlot(index);
    }
    public void RemoveItem(int number)
    {
        inventory.RemoveFromSlot(index,number);
    }
    public void AddItems(Item item, int number)
    {
        inventory.AddItemToSlot(index, item, number);
    }
    public int MaxAcceptable(Item item)
    {
        if (inventory.HasSpaceForItem(item))
        { return int.MaxValue; }
        return 0;
    }
}
