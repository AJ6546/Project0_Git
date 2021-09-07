using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] PlayerController nearestTarget=null;
    [SerializeField] float chaseDistance = 5f, suspicionTime=5f, timeSinceLastSawTarget=Mathf.Infinity;
    [SerializeField] bool targetLocked = false;
    Vector3 targetPos;
    EnemyMover em;
    EnemyFighter ef;
    [SerializeField] int enemyBehaviour;
    void Start()
    {
        em = GetComponent<EnemyMover>();
        ef = GetComponent<EnemyFighter>();
    }

    void Update()
    {
        timeSinceLastSawTarget += Time.deltaTime;
        if (nearestTarget != null)
        { targetPos = nearestTarget.transform.position; }
        if (targetLocked)
        {
            timeSinceLastSawTarget = 0;
            transform.LookAt(nearestTarget.transform);
            em.EnemyBehaviour(1, targetPos);
        }
        else if(timeSinceLastSawTarget<=suspicionTime)
        {
            em.EnemyBehaviour(3, targetPos);
        }
        else
        {
            em.EnemyBehaviour(0, targetPos);
        }
        ef.UpdateTargetPos(targetPos);
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

    public Vector3 GetTargetPosition()
    {
        return nearestTarget.transform.position;
    }

    public void StartAttacking()
    {
        ef.AtackBehaviour(nearestTarget.GetComponent<Health>());
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
}
