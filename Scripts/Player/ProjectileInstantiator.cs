using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileInstantiator : MonoBehaviour
{
    Animator playerAnimator;
    [SerializeField] string projectile1;
    PoolManager poolManager;
    [SerializeField] Transform instantiatorTransform;
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        poolManager = PoolManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("3"))
        {
            playerAnimator.SetTrigger("Attack03");
        }
    }
    void Hit04()
    {
        Vector3 spawnPos = instantiatorTransform.position;
        poolManager.Spawn(projectile1, spawnPos, transform.rotation, PlayerStats.USERID);
    }
}
