using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float healthPoints, startHealth=100; 
    
    void Start()
    {
        healthPoints = startHealth;
    }

    void Update()
    {
        
    }
    public void TakeDamage(float damage)
    {
        healthPoints -= damage;
    }

    public float GetHealthFactor()
    {
        return healthPoints / startHealth;
    }
    public string GetPlayer()
    {
        return PlayerStats.USERID;
    }
}
