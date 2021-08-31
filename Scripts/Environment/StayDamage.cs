using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayDamage : MonoBehaviour
{
    [SerializeField] float time = 5f, damge = 5f;
    [SerializeField] bool takeDamage = true;
    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<Health>())
        {
            Health health = other.GetComponent<Health>();
            StartCoroutine(TakeDamage(time, health));
        }
    }

    IEnumerator TakeDamage(float time, Health health)
    {
        if (takeDamage)
        {
            health.TakeDamage(damge);
            takeDamage = !takeDamage;
            yield return new WaitForSeconds(time);
            takeDamage = !takeDamage;
        }

    }
}
