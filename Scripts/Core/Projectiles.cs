using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [SerializeField] float speed = 10f, deactivateAfter = 15f, damage = 3f;
    [SerializeField] bool deactivateOnHit = false,moveToPosition=true;
    [SerializeField] string instantiator="default";
    [SerializeField] Vector3 targetPos;
    [SerializeField] Vector3 startPosition;
    private void Start()
    {
        startPosition = transform.position;
        
    }
    public void Move(bool moveToPosition)
    {
        this.moveToPosition = moveToPosition;
    }
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
        { transform.position = new Vector3(0, 0, 0); }
        if (transform.position.y <= -0.2f)
        { StartCoroutine(Deactivate(0.1f)); }
        StartCoroutine(Deactivate(deactivateAfter));
    }
    IEnumerator Deactivate(float time)
    {
        deactivateOnHit = false;
        yield return new WaitForSeconds(time);
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
            if (isPlayer && instantiator != other.GetComponent<Fighter>().GetPlayerID())
            {
                other.GetComponent<xHealth>().TakeDamage(damage, other.gameObject);
                deactivateOnHit = true;
            }
            else if(isEnemy)
            {
                other.GetComponent<xHealth>().TakeDamage(damage, other.gameObject);
                deactivateOnHit = true;
            }
            if (deactivateOnHit)
            { StartCoroutine(Deactivate(0.1f)); }
        }
    }

}
