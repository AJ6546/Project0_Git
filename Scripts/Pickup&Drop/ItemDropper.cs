using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    public void DropItem(Item item, int number)
    {
        SpawnPickup(item, GetDropLocation(),number);
    }
    public void SpawnPickup(Item item, Vector3 spawnLocation,int number)
    {
        var pickup = item.SpawnPickup(spawnLocation, number);
    }

    Vector3 GetDropLocation()
    {
        return new Vector3(GetComponentInChildren<Interact>().transform.position.x,
            GetComponentInChildren<Interact>().transform.position.y + 3f, GetComponentInChildren<Interact>().transform.position.z + 3f);
    }
}
