using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageOverTime : MonoBehaviour
{
    public Dictionary<string,int> damageTickTimer = new Dictionary<string, int>();
    Health health;
    [SerializeField] UIAssigner uiAssigner;
    [SerializeField] Image message;
    [SerializeField] string characterType;
    
    void Start()
    {
        health = GetComponent<Health>();
        if(characterType=="Player")
        uiAssigner = GetComponent<UIAssigner>();
    }
    void Update()
    {
        if(message==null && characterType=="Player")
        {
            message = uiAssigner.GetMessageBox().GetComponent<Image>();
        }
    }

    public void ApplyDamageOverTime(int ticks,  float damage, float  inBetweenTime, string type)
    {
        if(!damageTickTimer.ContainsKey(type))
        {
            damageTickTimer.Add(type, ticks);
            StartCoroutine(SetDamageOverTime(damage, inBetweenTime, type));
        }
        else { damageTickTimer[type] += ticks;
        }
        SetDamge(type);
    }
    IEnumerator SetDamageOverTime(float damage, float inBetweenTime, string damageType)
    {
        while(damageTickTimer.ContainsKey(damageType) && damageTickTimer[damageType] > 0)
        {
            damageTickTimer[damageType]--;
            health.TakeDamage(damage);
            if(damageTickTimer[damageType]==0)
            {
                ResetDamage(damageType);
            }
            yield return new WaitForSeconds(inBetweenTime);
        }
    }


    void SetDamge(string damageType)
    {
        string s = "";
        if(characterType == "Player")
        {
            switch(damageType)
            {
                case "Fire":
                    s = "You are on Fire. Try to find Water";
                    break;
                case "Poison":
                    s = "You are poisoned. Try to find Antidote";
                    break;
            }
            StartCoroutine(DisplayMessage(s));
        }
    }
    public void ResetDamage(string damageType)
    {
        string s = "";
        if (characterType == "Player")
        {
            switch (damageType)
            {
                case "Fire":
                    s = "Fire put out";
                    break;
                case "Poison":
                    s = "You are cured of poison";
                    break;
            }
        }
        damageTickTimer.Remove(damageType);
        StartCoroutine(DisplayMessage(s));
        damageType = "";
    }
    IEnumerator DisplayMessage(string s)
    {
        Color color = message.color;
        color.a = 1f;
        message.color = color;
        message.GetComponentInChildren<Text>().text = s;
        yield return new WaitForSeconds(5f);
        color.a = 0f;
        message.color = color;
        message.GetComponentInChildren<Text>().text = "";
    }
    public string GetCharacterType()
    {
        return characterType;
    }
}
