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

    static Dictionary<string, Item> itemLookupCache;

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

    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
         if(string.IsNullOrWhiteSpace(itemID))
        {
            itemID = System.Guid.NewGuid().ToString();
        }
    }
    void ISerializationCallbackReceiver.OnAfterDeserialize()
    { }
}
