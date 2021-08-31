using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName =("Project0/Inventory/Item"))]
public class Item : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] string itemID = null;
    [SerializeField] string displayName = null;
    [SerializeField] [TextArea] string description = null;
    [SerializeField] Sprite icon = null;
    [SerializeField] bool stackable = false;
    [SerializeField] Pickup pickup = null;
    static Dictionary<string, Item> itemLookupCache;
    [SerializeField] float modifier, lastingTime;
    public bool isPercent;
    [SerializeField] ItemType itemType;
    public static Item GetFromID(string itemID)
    {
        if(itemLookupCache==null)
        {
            itemLookupCache = new Dictionary<string, Item>();
            var itemList = Resources.LoadAll<Item>("");
            foreach(var item in itemList)
            {
                if (itemLookupCache.ContainsKey(item.itemID))
                    continue;
                itemLookupCache[item.itemID] = item;
            }
        }
        if (itemID == null || !itemLookupCache.ContainsKey(itemID)) return null;
        return itemLookupCache[itemID];
    }

    public string GetItemID()
    {
        return itemID;
    }
    public string GetDisplayName()
    {
        return displayName;
    }
    public string GetDescription()
    {
        return description;
    }
    public Sprite GetIcon()
    {
        return icon;
    }
    public bool IsStackable()
    {
        return stackable;
    }
    public Pickup SpawnPickup(Vector3 position,int number)
    {
        var pickup = Instantiate(this.pickup);
        pickup.transform.position = position;
        pickup.Setup(this,number);
        return pickup;
    }
    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
         if(string.IsNullOrWhiteSpace(itemID))
        {
            itemID = System.Guid.NewGuid().ToString();
        }
    }
    void ISerializationCallbackReceiver.OnAfterDeserialize()
    { }
    public virtual void Use()
    {
        FindObjectOfType<ItemManager>().Use(modifier,isPercent,itemType,lastingTime);
    }
}
public enum ItemType
{
    Health, PowerUp, Protection, Speed, None
}
