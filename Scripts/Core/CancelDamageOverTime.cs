using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelDamageOverTime : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DamageOverTime>())
        {
            DamageOverTime dot = other.GetComponent<DamageOverTime>();
            switch (tag)
            {
                case "Water":
                    if(CheckDamageType(dot,"Fire"))
                    {
                        dot.ResetDamage("Fire");
                    }
                    break;
                case "Antidote":
                    if(CheckDamageType(dot,"Poison"))
                    {
                        dot.ResetDamage("Poison");
                    }
                    break;
            }
        }
    }
    bool CheckDamageType(DamageOverTime dot, string type)
    {
        return dot.damageTickTimer.ContainsKey(type);
    }
}
