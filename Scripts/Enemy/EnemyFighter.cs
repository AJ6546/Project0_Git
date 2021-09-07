using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFighter : MonoBehaviour
{
    [SerializeField]
    float dmgFactor01 = 50, dmgFactor02 = 25, dmgFactor03 = 20,
      strength = 100,distanceToTarget,damagingDistance=1f;
    Animator animator;
    Health targetHealth;
    CooldownTimer cdTimer;
    Vector3 targetPos;


    void Start()
    {
        animator = GetComponent<Animator>();
        cdTimer = GetComponent<CooldownTimer>();
    }

    public void AtackBehaviour(Health targetHealth)
    {
        this.targetHealth = targetHealth;
        if (Time.time > cdTimer.nextAttackTime["Attack03"])
        {
            animator.SetTrigger("Attack03");
            cdTimer.nextAttackTime["Attack03"] = (int)Time.time + cdTimer.coolDownTime["Attack03"];
        }
        else if (Time.time > cdTimer.nextAttackTime["Attack02"])
        {
            animator.SetTrigger("Attack02");
            cdTimer.nextAttackTime["Attack02"] = (int)Time.time + cdTimer.coolDownTime["Attack02"];
        }
        else if (Time.time > cdTimer.nextAttackTime["Attack01"])
        {
            animator.SetTrigger("Attack01");
            cdTimer.nextAttackTime["Attack01"] =(int) Time.time + cdTimer.coolDownTime["Attack01"];
        }
    }

    #region Damage
    void Hit01()
    {
        if(AtDamagingDistance())
            targetHealth.TakeDamage(strength/dmgFactor01);
    }
    void Hit02()
    {
        if (AtDamagingDistance())
            targetHealth.TakeDamage(strength / dmgFactor02);
    }
    void Hit03()
    {
        if (AtDamagingDistance())
            targetHealth.TakeDamage(strength / dmgFactor03);
    }
    #endregion

    public void UpdateTargetPos(Vector3 targetPos)
    {
        this.targetPos = targetPos;
    }

    public bool AtDamagingDistance()
    {
        distanceToTarget = Vector3.Distance(transform.position, targetPos);
        return distanceToTarget <= damagingDistance;
    }
}
