using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    
    public Equipment[] currentEquipment;
    PlayerInventory inventory;
    [SerializeField] Transform leftHandTransform;
    [SerializeField] Transform rightHandTransform;
    string handItem = "HandItem";
    Fighter fighter;
    CharacterStats characterStats;
    UIAssigner uiAssigner;
    [SerializeField] FixedButton unEquipButton;
    [SerializeField] Equipment defaultEquipment;
    void Start()
    {
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        inventory = GetComponentInParent<PlayerInventory>();
        fighter = GetComponent<Fighter>();
        uiAssigner = GetComponent<UIAssigner>();
        characterStats = GetComponent<CharacterStats>();
        Equip(defaultEquipment);
    }
    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipmentSlot;
        Equipment oldItem = null;
        if(currentEquipment[slotIndex]!=null)
        {
            oldItem = currentEquipment[slotIndex];
            characterStats.UpdateHealth(oldItem.healthModifier,newItem.healthModifier,newItem.isPercent);
            characterStats.UpdateSpeed(oldItem.speedModifier,newItem.speedModifier, 0f);
            if (oldItem.GetDisplayName() != "Default")
            { inventory.AddToFirstEmptySlot(oldItem, 1); }
        }
        currentEquipment[slotIndex] = newItem;
        EquipItem(newItem);
        characterStats.UpdateStrength(newItem.damageModifier, newItem.isPercent,0f);
        characterStats.UpdateArmor(newItem.armorModifier, newItem.isPercent,0f);
        
    }

    public void Use(float modifier,bool isPercent,ItemType itemType, float lastingTime)
    {
        switch ((int)itemType)
        { case 0:
                characterStats.UpdateHealth(0, modifier, isPercent);
                break;
            case 1:
                characterStats.UpdateStrength(modifier, isPercent, lastingTime);
                break;
            case 2:
                characterStats.UpdateArmor(modifier, isPercent, lastingTime);
                break;
            case 3:
                characterStats.UpdateSpeed(0,modifier, lastingTime);
                break;
        }
        
    }

    void EquipItem(Equipment equipment)
    {
        switch (equipment.equipmentSlot)
        {
            case EquipmentSlot.hands:
                DestroyOldWeapon(rightHandTransform,leftHandTransform);
                if (equipment.equipedPrefab1 != null && equipment.equipedPrefab2 != null)
                {
                    GameObject leftHand = Instantiate(equipment.equipedPrefab1, leftHandTransform);
                    GameObject rightHand = Instantiate(equipment.equipedPrefab2, rightHandTransform);
                    leftHand.name = handItem;
                    rightHand.name = handItem;
                }
                else
                {
                    Transform handTransform = equipment.isRightHanded ? rightHandTransform : leftHandTransform;
                    GameObject hand = Instantiate(equipment.equipedPrefab1, handTransform);
                    hand.name = handItem;
                }
                break;
        }
        //var overrideController = fighter.playerAnimator.runtimeAnimatorController as AnimatorOverrideController;
        if(currentEquipment[2].overrideController !=null && currentEquipment[2]!=null)
        {
            fighter.playerAnimator.runtimeAnimatorController = currentEquipment[2].overrideController;
        }
    }
    void DestroyOldWeapon(Transform rightHand,Transform leftHand)
    {
        Transform oldWeapon1 = rightHand.Find(handItem);
        Transform oldWeapon2 = leftHand.Find(handItem);
        if (oldWeapon1 == null && oldWeapon2 == null) return;
        if(oldWeapon1!=null)
        {
            Destroy(oldWeapon1.gameObject);
        }
        if(oldWeapon2!=null)
        {
            Destroy(oldWeapon2.gameObject);
        }
    }
    private void Update()
    {
        if(unEquipButton==null)
        {
            unEquipButton = uiAssigner.GetFixedButtons()[7];
        }
        if(Input.GetKeyDown("u")||unEquipButton.Pressed)
        {
            UnEquip(2);
        }
    }

    void UnEquip(int slotIndex)
    {
        if(currentEquipment[slotIndex] !=null)
        {
            Equip(defaultEquipment);
        }
    }
}
