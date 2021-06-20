using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] int inventorySize = 10;

    Item[] slots;

    public event Action inventoryUpdated;

    private void Awake()
    {
        slots = new Item[inventorySize];
        slots[0] = Item.GetFromID("90c1cee7-c1ba-4dd5-a4a9-448b25c52d0a");
        slots[1] = Item.GetFromID("6c82e4f5-056e-4ac1-ae8b-2e9b4ad755e3");
    }

    public int GetSize()
    {
        return slots.Length;
    }

    public static PlayerInventory GetPlayerInventory()
    {
        var player = GameObject.FindWithTag("Player");
        return player.GetComponent<PlayerInventory>();
    }
    public Item GetItemFromSlot(int slot)
    {
        return slots[slot];
    }
    public void RemoveFromSlot(int slot)
    {
        slots[slot] = null;
        if (inventoryUpdated != null)
        {
            inventoryUpdated();
        }
    }
    public bool AddItemToSlot(int slot, Item item)
    {
        if (slots[slot] != null)
        {
            return AddToFirstEmptySlot(item);
        }
        slots[slot] = item;
        if (inventoryUpdated != null)
        {
            inventoryUpdated();
        }
        return true;
    }
    public bool AddToFirstEmptySlot(Item item)
    {
        int i = FindSlot(item);
        if (i < 0)
        {
            return false;
        }
        slots[i] = item;
        if (inventoryUpdated != null)
        {
            inventoryUpdated();
        }
        return true;
    }

    int FindSlot(Item item)
    {
        return FindEmptySlot();
    }

    int FindEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                return i;
            }
        }
        return -1;
    }
    public bool HasSpaceForItem(Item item)
    {
        return FindSlot(item) >= 0;
    }
}
