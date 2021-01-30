using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [SerializeField] float speed = 10f, deactivateAfter = 15f, damage = 3f;
    [SerializeField] string instantiator;
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        StartCoroutine(Deactivate(deactivateAfter));
    }
    IEnumerator Deactivate(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
    public void SetInstantiator(string instantiator)
    {
        instantiator = this.instantiator;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(instantiator!=other.GetComponent<Health>().GetPlayer())
        {
            other.GetComponent<Health>().TakeDamage(damage);
            StartCoroutine(Deactivate(0.1f));
        }
    }
}
