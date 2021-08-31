using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] PlayerController nearestTarget=null;
    [SerializeField] float chaseDistance = 5f;
    [SerializeField] bool targetLocked = false;
    void Start()
    {
        
    }

    void Update()
    {
        if(targetLocked)
        {
            transform.LookAt(nearestTarget.transform);
        }
        
    }
    public bool IsTargetLocked()
    {
        return targetLocked;
    }
    public void UpdateNearestTarget(PlayerController nearestTarget)
    {
        if (nearestTarget.GetComponent<Health>().GetHealthFactor() > 0)
        { this.nearestTarget = nearestTarget; }
        StartCoroutine(LockTarget());
    }

    IEnumerator LockTarget()
    {
        targetLocked = IsInRange() ? true : false;
        yield return new WaitForSeconds(5f);
        StartCoroutine(LockTarget());
    }
    bool IsInRange()
    {
        return Vector3.Distance(transform.position, nearestTarget.transform.position) <= chaseDistance;
    }
}
