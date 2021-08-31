using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{
    [SerializeField] List<PlayerController> players = new List<PlayerController>();
    [SerializeField] List<EnemyController> enemies = new List<EnemyController>();
    void Start()
    {
        
    }

    
    void Update()
    {
        players = GetAllPlayers();
        enemies = GetAllEnemies();
        
        foreach(EnemyController enemy in enemies)
        {
            if(!enemy.IsTargetLocked())
            {
                enemy.UpdateNearestTarget(FindNearestTarget(enemy));
            }
        }
    }

    private PlayerController FindNearestTarget(EnemyController enemy)
    {
        float minDist = Mathf.Infinity;
        PlayerController nearestPlayer = null;
        foreach(PlayerController player in players)
        {
            float dist = FindDistance(enemy, player);
            if(minDist>dist)
            {
                nearestPlayer = player;
                minDist = dist;
            }
        }
        return nearestPlayer;
    }

    private float FindDistance(EnemyController enemy, PlayerController player)
    {
        return Vector3.Distance(enemy.transform.position, player.transform.position);
    }

    private List<EnemyController> GetAllEnemies()
    {
        List<EnemyController> temp = new List<EnemyController>();
        foreach(EnemyController ec in FindObjectsOfType<EnemyController>())
        {
            if(ec.GetComponent<Health>().GetHealthFactor()>0)
            {
                temp.Add(ec);
            }
        }
        return temp;
    }

    private List<PlayerController> GetAllPlayers()
    {
        List<PlayerController> temp = new List<PlayerController>();
        foreach (PlayerController pc in FindObjectsOfType<PlayerController>())
        {
            if (pc.GetComponent<Health>().GetHealthFactor() > 0)
            {
                temp.Add(pc);
            }
        }
        return temp;
    }
}
