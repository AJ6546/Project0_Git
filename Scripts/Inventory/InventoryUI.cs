using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] InventorySlot inventorySlotPrefab = null;
    PlayerInventory playerInventory;
    void Awake()
    {
        playerInventory = PlayerInventory.GetPlayerInventory();
        playerInventory.inventoryUpdated += Redraw;
    }

    void Start()
    {
        Redraw();
    }

    void Redraw()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        for (int i=0;i<playerInventory.GetSize();i++)
        {
            var slotUi = Instantiate(inventorySlotPrefab, transform);
            slotUi.Setup(playerInventory, i);
        }
    }
}
