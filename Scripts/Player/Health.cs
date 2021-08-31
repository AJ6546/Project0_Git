using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float healthPoints, startHealth = 100,maxHealth;
    [SerializeField] float armor=0;
    [SerializeField] bool isPercent;

    void Start()
    {
        healthPoints = startHealth;
        maxHealth = startHealth;
    }
    public float GetStartHealth()
    {
        return startHealth;
    }
    public void TakeDamage(float damage)
    {
        damage = GetDamage(damage);
        healthPoints -= damage;
        GetComponentInChildren<DamageTextSpawner>().Spawn(damage);
    }

    public float GetHealthFactor()
    {
        return healthPoints / maxHealth;
    }
    public string GetPlayer()
    {
        return PlayerStats.USERID;
    }
    float GetDamage(float damage)
    {
        if(isPercent)
        {
            damage -= armor * damage / 100;
        }
        else
        {
            damage -= armor;
        }
        return damage;
    }
    public float ArmorModifier(float mod, bool isPercent, float lastingTime)
    {
        if(lastingTime>0)
        {
            armor += mod;
            StartCoroutine(Protection(lastingTime,mod));
        }
        else{ armor = mod; }
        this.isPercent = isPercent;
        return armor;
    }

    IEnumerator Protection(float lastingTime, float mod)
    {
        yield return new WaitForSeconds(lastingTime);
        armor -= mod;
        GetComponent<CharacterStats>().UpdateArmor(armor);
    }

    public float HealthModifier(float oldHealthMod,float newHealthMod, bool isPercent)
    {
        if(this.isPercent)
        { healthPoints -= startHealth * oldHealthMod / 100; maxHealth-= startHealth * oldHealthMod / 100; }
        else { healthPoints -= oldHealthMod;maxHealth -= oldHealthMod; }
        this.isPercent = isPercent;
        if(isPercent)
        { healthPoints += startHealth * newHealthMod / 100; maxHealth += startHealth * newHealthMod / 100; }
        else { healthPoints += newHealthMod;maxHealth += newHealthMod; }
        return healthPoints;
    }

    public float GetBaseArmor()
    {
        return armor;
    }
}