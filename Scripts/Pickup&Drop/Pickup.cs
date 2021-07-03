using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] Item item;
    PlayerInventory inventory;
    [SerializeField] float pickupRadius = 3f;
    [SerializeField] int number = 1;
    void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        inventory = player.GetComponent<PlayerInventory>();
    }

   public void PickupItem()
    {
        bool foundSlot = inventory.AddToFirstEmptySlot(item,number);
        if (foundSlot)
        {
            Destroy(gameObject);
        }
    }
    public void Setup(Item item,int number)
    {
        this.item = item;
        this.number = number;
    }
    public float GetPickupRadius()
    {
        return pickupRadius;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}
