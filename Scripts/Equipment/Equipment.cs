using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Project0/Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipmentSlot;
    public GameObject equipedPrefab1=null;
    public GameObject equipedPrefab2=null;
    public bool isRightHanded;
    public AnimatorOverrideController overrideController = null;
    public float armorModifier = 0;
    public float speedModifier = 0;
    public float damageModifier = 0;
    public float healthModifier;
    public override void Use()
    {
        FindObjectOfType<ItemManager>().Equip(this);
    }
    
}
public enum EquipmentSlot
{
    head, legs, hands, chest, feet
}