using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fighter : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float damage01, damage02, damage03, attackRange = 3f;
    Animator playerAnimator;
    [SerializeField] FixedButtonAssigner fba;
    [SerializeField] FixedButton attack01Button, attack02Button, attack03Button;
    [SerializeField] CooldownTimer cd;
    [SerializeField] int timer, a01, a02, a03;
    [SerializeField] GameObject attack01Fill, attack02Fill, attack03Fill;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        fba = GetComponent<FixedButtonAssigner>();
        cd = GetComponent<CooldownTimer>();
    }


    void Update()
    {
        timer = (int)Time.time;
        a01 = cd.nextAttackTime["Attack01"];
        a02 = cd.nextAttackTime["Attack02"];
        a03 = cd.nextAttackTime["Attack03"];
        if (attack01Button==null || attack01Button==null || attack01Button==null)
        {
            attack01Button = fba.GetFixedButtons()[3];
            attack02Button = fba.GetFixedButtons()[4];
            attack03Button = fba.GetFixedButtons()[5];
            attack01Fill = attack01Button.transform.Find("Fill").gameObject;
            attack02Fill = attack02Button.transform.Find("Fill").gameObject;
            attack03Fill = attack03Button.transform.Find("Fill").gameObject;
        }
        Refill();
        if (cd.nextAttackTime["Attack01"]
            <Time.time &&(Input.GetKeyDown("1") || attack01Button.Pressed))
        {
            playerAnimator.SetTrigger("Attack01");
            cd.nextAttackTime["Attack01"] = cd.coolDownTime["Attack01"] + (int)Time.time;
            attack01Fill.GetComponent<Image>().fillAmount = 1;
        }
        if (cd.nextAttackTime["Attack02"]
            < Time.time && (Input.GetKeyDown("2") || attack02Button.Pressed))
        {
            playerAnimator.SetTrigger("Attack02");
            cd.nextAttackTime["Attack02"] = cd.coolDownTime["Attack02"] + (int)Time.time;
            attack02Fill.GetComponent<Image>().fillAmount = 1;
        }
        if (cd.nextAttackTime["Attack03"]
            < Time.time && (Input.GetKeyDown("3") || attack03Button.Pressed))
        {
            playerAnimator.SetTrigger("Attack03");
            cd.nextAttackTime["Attack03"] = cd.coolDownTime["Attack03"] + (int)Time.time;
            attack03Fill.GetComponent<Image>().fillAmount = 1;
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
    void Hit04()
    {
        GetComponent<ProjectileInstantiator>().SpawnProjectile(gameObject);
    }
    #endregion
    #region GetTargetPosition
    public Vector3 GetAimLocation()
    {
        CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
        if (targetCapsule == null)
        {
            return target.transform.position;
        }
        return target.transform.position + Vector3.up * targetCapsule.height / 2;
    }
    #endregion
    #region Refill
    void Refill()
    {
        if (attack01Fill.GetComponent<Image>().fillAmount > 0)
        {
            attack01Fill.GetComponent<Image>().fillAmount -=
                (1.0f / cd.coolDownTime["Attack01"]) * Time.deltaTime;
            attack01Fill.GetComponentInChildren<Text>().text =
                (cd.nextAttackTime["Attack01"] - (int)Time.time).ToString();
        }
        else {attack01Fill.GetComponentInChildren<Text>().text = ""; }
        if (attack02Fill.GetComponent<Image>().fillAmount > 0)
        {
            attack02Fill.GetComponent<Image>().fillAmount -=
            (1.0f / cd.coolDownTime["Attack02"]) * Time.deltaTime;
            attack02Fill.GetComponentInChildren<Text>().text =
                (cd.nextAttackTime["Attack02"] - (int)Time.time).ToString();
        }
        else { attack02Fill.GetComponentInChildren<Text>().text = ""; }
        if (attack03Fill.GetComponent<Image>().fillAmount > 0)
        {
            attack03Fill.GetComponent<Image>().fillAmount -=
                (1.0f / cd.coolDownTime["Attack03"]) * Time.deltaTime;
            attack03Fill.GetComponentInChildren<Text>().text =
                (cd.nextAttackTime["Attack03"] - (int)Time.time).ToString();
        }
        else { attack03Fill.GetComponentInChildren<Text>().text = ""; }
    }
    #endregion
}



