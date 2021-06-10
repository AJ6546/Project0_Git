using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextSpawner : MonoBehaviour
{
    [SerializeField] DamageText damageText;
    public void Spawn(float damage)
    {
        DamageText instance=Instantiate<DamageText>(damageText, transform);
        instance.SetDamageText(damage);
    }
}
