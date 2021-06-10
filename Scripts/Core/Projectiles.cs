using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [SerializeField] float speed = 10f, deactivateAfter = 15f, damage = 3f;
    [SerializeField] bool deactivateOnHit = true,moveToPosition=true;
    [SerializeField] string instantiator="default";
    [SerializeField] Vector3 targetPos;
    void Update()
    {
        if (moveToPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos,
                speed * Time.deltaTime);
        }
        if(transform.position==targetPos)
        {
            moveToPosition = false;
        }
        if (!moveToPosition)
            transform.Translate(transform.forward * speed/4 * Time.deltaTime);

        if (transform.position.y <= -0.2f)
        { StartCoroutine(Deactivate(0.1f)); }
        StartCoroutine(Deactivate(deactivateAfter));
    }
    IEnumerator Deactivate(float time)
    {
        yield return new WaitForSeconds(time);
        moveToPosition = true;
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }
    public void SetInstantiator(string instantiator)
    {
        this.instantiator = instantiator;
    }
    public string GetInstantator()
    {
        return instantiator;
    }
    public void SetAimLocation(Vector3 tPos)
    {
        targetPos = new Vector3(tPos.x,transform.position.y, tPos.z);
        moveToPosition = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        bool isPlayer = other.GetComponent<Fighter>();
        bool isEnemy = !other.GetComponent<Fighter>() && other.GetComponent<xHealth>();
        if (isPlayer || isEnemy)
        {
            if (isPlayer && instantiator != other.GetComponent<xFighter>().GetPlayerID())
            {
                other.GetComponent<xHealth>().TakeDamage(damage, other.gameObject);
            }
            else if(isEnemy)
            {
                other.GetComponent<xHealth>().TakeDamage(damage, other.gameObject);
            }
            if (deactivateOnHit)
            { StartCoroutine(Deactivate(0.1f)); }
        }
    }

}
