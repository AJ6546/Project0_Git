using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] Item item = null;
    [SerializeField] int number =1;
    void Awake()
    {
        SpawnPickup();
    }

    void SpawnPickup()
    {
        var spawnedPickup = item.SpawnPickup(transform.position, number);
        spawnedPickup.transform.SetParent(transform);
    }
}
