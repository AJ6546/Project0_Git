using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static SyncManager;

public class xFighter : MonoBehaviourPun
{
    [SerializeField] private List<Target> targets;
    [SerializeField] GameObject target;

    [SerializeField]
    float damage01, damage02, damage03, attackRange = 3f, attackRange02=5f,
        waitTime = 3, messageDispalyTime = 0, fixedMesageDisplayTime = 5f, time,
        nextAttacktime, cooldwnTime;
    [SerializeField] Animator playerAnimator;
    [SerializeField] GameObject body;
    [SerializeField] FixedButtonAssigner fba;
    [SerializeField] FixedButton attack01Button;
    [SerializeField] FixedButton attack02Button;
    [SerializeField] FixedButton attack03Button;
    [SerializeField] float distanceToTarget;
    [SerializeField] SyncManager syncManager;
    [SerializeField] string playerId = "default", targetName = "default";
    [SerializeField] xHealth health;
    [SerializeField] Image message;
    [SerializeField] float[] cooldownTime, nextAttackTime = new float[3];
    [SerializeField] CooldownTimer cd;
    public bool freeze = false;
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (photonView.IsMine)
            {
                playerAnimator = GetComponent<Animator>();
                fba = GetComponent<FixedButtonAssigner>();
                health = GetComponent<xHealth>();
                cd = GetComponent<CooldownTimer>();
            }
            playerId = photonView.Owner.NickName;
        }
        else
        {
            playerAnimator = GetComponent<Animator>();
            fba = GetComponent<FixedButtonAssigner>();
            health = GetComponent<xHealth>();
            playerId = PlayerStats.USERID;
            cd = GetComponent<CooldownTimer>();
        }
        syncManager = FindObjectOfType<SyncManager>();
    }


    void Update()
    {
        targets = syncManager.GetTargets();
        if (targetName != syncManager.GetNearestTarget(gameObject))
        {
            targetName = syncManager.GetNearestTarget(gameObject);
            SetTarget(targetName);
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        { if (!photonView.IsMine) return; }
        if (attack01Button == null || attack02Button == null || attack03Button == null || message == null)
        {
            attack01Button = fba.GetFixedButtons()[3];
            attack02Button = fba.GetFixedButtons()[4];
            attack03Button = fba.GetFixedButtons()[5];
            message = fba.GetMessageBox().GetComponent<Image>();
        }
        #region Attack01
        if (cd.nextAttackTime["Attack01"] < Time.time &&
            (Input.GetKeyDown("1") || attack01Button.Pressed))
        {
            if (health.isDead)
            {
                DisplayMessage();
                return;
            }
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                CallRPCMethod("Attack01", cd.coolDownTime["Attack01"] + (int)Time.time);
            }
            else
            {
                cd.nextAttackTime["Attack01"] = cd.coolDownTime["Attack01"] + (int)Time.time;
            }
            playerAnimator.SetTrigger("Attack01");
        }
        #endregion
        #region Attack02
        if (cd.nextAttackTime["Attack02"]<Time.time && 
            (Input.GetKeyDown("2") || attack02Button.Pressed))
        {
            if (health.isDead)
            {
                DisplayMessage();
                return;
            }
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                CallRPCMethod("Attack02", cd.coolDownTime["Attack02"] + (int)Time.time);
            }
            else
            {
                cd.nextAttackTime["Attack02"] = cd.coolDownTime["Attack02"] + (int)Time.time;
            }
            playerAnimator.SetTrigger("Attack02");
        }
        #endregion
        #region Attack03
        if (cd.nextAttackTime["Attack03"] < Time.time &&
            (Input.GetKeyDown("3") || attack03Button.Pressed))
        {
            if (health.isDead)
            {
                DisplayMessage();
                return;
            }
            freeze = true;
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                CallRPCMethod("Attack03",cd.coolDownTime["Attack03"] + (int)Time.time);
            }
            else
            {
                cd.nextAttackTime["Attack03"] = cd.coolDownTime["Attack03"] + (int)Time.time;
            }
            Vector3 lookPos = new Vector3(GetAimLocation().x, transform.position.y
               , GetAimLocation().z);
            transform.LookAt(lookPos);
            playerAnimator.SetTrigger("Attack03");
        }
        #endregion
        if (Time.time > messageDispalyTime && message.color.a == 1)
        {
            Debug.Log("I am: " + playerId + "Turning off message");
            message.GetComponentInChildren<Text>().text = "";
            setAlpha(0f);
        }
    }

    private void CallRPCMethod(string attack, int nextAttackTime)
    {
        if (GetComponent<PhotonView>().IsMine)
        { GetComponent<PhotonView>().RPC("RPC_SetAttackTimer", RpcTarget.AllBuffered, attack, nextAttackTime); }
    }

    bool InAttackRange()
    {
        return Vector3.Distance(body.transform.position, target.transform.position) <= attackRange;
    }
    bool InAttackRange02()
    {
        return Vector3.Distance(body.transform.position, target.transform.position) <= attackRange02;
    }
    #region Damage
    void Hit01()
    {
        if (InAttackRange())
        {
            target.GetComponent<xHealth>().TakeDamage(damage01, target);
        }
    }

    void Hit02()
    {
        if (InAttackRange())
        {
            target.GetComponent<xHealth>().TakeDamage(damage02, target);
        }
    }
    void Hit03()
    {
        if (InAttackRange())
        {
            target.GetComponent<xHealth>().TakeDamage(damage03, target);
        }
    }

    void Hit04()
    {
        GetComponent<ProjectileInstantiator>().SpawnProjectile(gameObject);
        freeze = false;
    }
    #endregion
    #region SetTarget
    public void SetTarget(string targetNameOrID)
    {
        foreach (Target target in targets)
        {
            if (target.enemy.GetComponent<xFighter>())
            {
                if (target.enemy.GetComponent<xFighter>().playerId == targetNameOrID)
                {
                    this.target = target.enemy.gameObject;
                    return;
                }
            }
            else
            {
                if (target.enemy.name == targetNameOrID)
                {
                    this.target = target.enemy.gameObject;
                    return;
                }
            }
        }
    }
    #endregion
    #region GetPlayerid
    public string GetPlayerID()
    {
        return playerId;
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

    #region DisplayMessage
    public void DisplayMessage()
    {
        messageDispalyTime = fixedMesageDisplayTime + Time.time;
        if (health.isDead)
        {
            setAlpha(1f);
            message.GetComponentInChildren<Text>().text = "You Dead Bruh !";
        }
        else
        {
            setAlpha(1f);
            message.GetComponentInChildren<Text>().text = "Back from the dead!";
        }
    }
    void setAlpha(float alpha)
    {
        Debug.Log("Set Alpha " + alpha);
        Color color = message.color;
        color.a = alpha;
        message.color = color;
    }
    #endregion
    [PunRPC]
    private void RPC_SetAttackTimer(string attack,int attackTime)
    {
        GetComponent<CooldownTimer>().nextAttackTime[attack] = attackTime;
    }
}
