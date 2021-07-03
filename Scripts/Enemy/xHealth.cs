using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class xHealth : MonoBehaviourPun
{
    [SerializeField] float healthPoints, startHealth=100, healAfter=0, 
        healthRefillRate=0.01f, fixedhealTime=10,backToHealth=10;
    [SerializeField] bool defaultEnemy=false, isFullHealth=true;
    public bool isDead=false;
    [SerializeField] Rigidbody rb;
    [SerializeField] CapsuleCollider cc;
    [SerializeField] Animator playerAnimator;
    int sceneIndex;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();
        if (CompareTag("Player"))
        {
            playerAnimator = GetComponent<Animator>();
        }
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        healthPoints = startHealth;
        
    }
    public void TakeDamage(float damage,GameObject target)
    {
        if (sceneIndex == 2 && target.CompareTag("Player"))
        {
            if (target.GetComponent<PhotonView>().IsMine)
            {
                target.GetComponent<PhotonView>().RPC("RPC_TakeDamage", RpcTarget.AllBuffered, damage);
            }
        }
        else
        {
            healthPoints -= damage;
            healAfter = Time.time + fixedhealTime;
            GetComponentInChildren<DamageTextSpawner>().Spawn(damage);
            return;
        }
    }

    public float GetHealthFactor()
    {
        return healthPoints / startHealth;
    }
    public float GetHealth()
    {
        return healthPoints;
    }
    
    public void SetHealth(float healthPoints)
    {
        healthPoints=this.healthPoints;
    }
    public float GetNextHealTime()
    {
        return healAfter;
    }
    public void SetNextHealTime(float healAfter)
    {
        healAfter = this.healAfter;
    }
    
    private void Update()
    {
        if (defaultEnemy && healthPoints <= 0)
        {
            healthPoints = startHealth;
        }
        isFullHealth = Mathf.Approximately(healthPoints, startHealth) ? true : false;
        
        if(healthPoints <= 0 && isDead==false && CompareTag("Player"))
        {
            isDead = true;
            playerAnimator.SetTrigger("Death");
        }

        if (CompareTag("Player"))
        {
            if (sceneIndex == 2)
            {
                if (GetComponent<PhotonView>().IsMine)
                {
                    if (healthPoints < startHealth * 0.8f && Time.time > healAfter)
                    {
                        GetComponent<PhotonView>().RPC("RPC_Heal",
                            RpcTarget.AllBuffered, healthRefillRate);
                    }
                }
            }
            else
            {
                if (healthPoints < startHealth * 0.8f && Time.time > healAfter)
                {
                    healthPoints += healthRefillRate;
                }
            }
            if (healthPoints >= backToHealth && isDead)
            {
                playerAnimator.SetTrigger("BackToLife");
                BackToLife();
            }
        }
    }

    void Dead()
    {
        rb.useGravity = false;
        rb.isKinematic = true;
        cc.enabled = false;
    }

    void BackToLife()
    {
        cc.enabled = true;
        rb.useGravity = true;
        rb.isKinematic = false;
        isDead = false;
        //GetComponent<Fighter>().DisplayMessage();
    }
    [PunRPC]
    private void RPC_TakeDamage(float damage)
    {
        healthPoints -= damage;
        healAfter = Time.time + fixedhealTime;
        GetComponentInChildren<DamageTextSpawner>().Spawn(damage);
        Debug.Log("I am: " + GetComponent<PhotonView>().Owner.NickName +
            "\ntaking damage " + damage + "\nCurrentHealth" + healthPoints);
    }
    [PunRPC]
    private void RPC_Heal(float healAmt)
    {
        healthPoints += healAmt;
    }

}
