using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelDamageOverTime : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(CompareTag("Water") && other.GetComponent<DamageOverTime>().damageTickTimer.ContainsKey("Fire"))
        {
            other.GetComponent<DamageOverTime>().ResetDamage("Fire");
        }
        else if (CompareTag("Antidote") && other.GetComponent<DamageOverTime>().damageTickTimer.ContainsKey("Poison"))
        {
            other.GetComponent<DamageOverTime>().ResetDamage("Poison");
        }
    }
}
