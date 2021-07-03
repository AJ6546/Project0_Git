using System.Collections.Generic;
using UnityEngine;

public class SyncManager : MonoBehaviour
{
    [System.Serializable]
    public class Target
    {
        public GameObject enemy;
        public float health;
    }

    [SerializeField] List<Target> targets;
    [SerializeField] float distanceToTarget;
    void Update()
    {
        targets = GetTargets();
    }
    #region GetTargets
    public List<Target> GetTargets()
    {
       
        List<Target> temp = new List<Target>();
        foreach (GameObject t in GameObject.FindGameObjectsWithTag("Player"))
        {
            Target target = new Target();
            //if (!t.GetComponent<xHealth>().isDead)
            //{
            //    target.enemy = t.gameObject;
            //    target.health = t.GetComponent<xHealth>().GetHealth();
            //    temp.Add(target);
            //}
        }
        foreach (GameObject t in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            //Target target = new Target();
            //if (!t.GetComponent<xHealth>().isDead)
            //{
            //    target.enemy = t.gameObject;
            //    target.health = t.GetComponent<xHealth>().GetHealth();
            //    temp.Add(target);
            //}
        }
        return temp;
    }
    #endregion
    #region GetTaget
    public string GetNearestTarget(GameObject player)
    {
        float minDist = Mathf.Infinity;
        GameObject nearestTarget = player;
        foreach (Target target in targets)
        {
            if (target.enemy != player && minDist > DistanceToTarget(target.enemy,player) )
            {
                nearestTarget = target.enemy;
                minDist = DistanceToTarget(target.enemy,player);
                distanceToTarget = minDist;
            }
        }
        if (nearestTarget.CompareTag("Enemy"))
            return nearestTarget.name;
        else
            return nearestTarget.GetComponent<xFighter>().GetPlayerID();
    }
    #endregion
    #region Calculate Distance To Target
    float DistanceToTarget(GameObject target,GameObject player)
    {
        float distance = Vector3.Distance(target.transform.position, player.transform.position);
        return distance;
    }
    #endregion
}
