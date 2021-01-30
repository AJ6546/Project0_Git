using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float damage01,damage02,damage03,attackRange=3f;
    Animator playerAnimator;
   
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    
    void Update()
    {
        if(Input.GetKeyDown("1"))
        {
            playerAnimator.SetTrigger("Attack01");
        }
        if (Input.GetKeyDown("2"))
        {
            playerAnimator.SetTrigger("Attack02");
        }
    }
    bool InAttackRange()
    {
        return Vector3.Distance(transform.position, target.transform.position) <= attackRange;
    }

    #region Damage
    void Hit01()
    {
        if (InAttackRange())
        { target.GetComponent<Health>().TakeDamage(damage01); }
    }

    void Hit02()
    {
        if (InAttackRange())
        { target.GetComponent<Health>().TakeDamage(damage02); }
    }
    void Hit03()
    {
        if (InAttackRange())
        { target.GetComponent<Health>().TakeDamage(damage03); }
    }
    #endregion
}
