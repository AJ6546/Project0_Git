using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] int enemyBehaviour = 0;
    NavMeshAgent navMeshAgent;
    Animator animator;
    [SerializeField] Vector3 originalPosition, randomWaypoint, targetPos;

    [SerializeField] float wayPointTolrance = 1f, distanceToWaypoint, xMax = 10f, 
        zMax = 10f, distanceFromOriginalPos, offset = 3f, timeSinceArrivedAtWaypoint = Mathf.Infinity, 
        wayPointDwellTime = 5f, wayPointMoveTime = Mathf.Infinity, timeToReachWaypoint = 30f,
        distanceToTarget,stoppingDistance=1f,chaseSpeed=1f;
      
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        originalPosition = randomWaypoint = transform.position;
    }

    void Update()
    {
        distanceFromOriginalPos = Vector3.Distance(transform.position, originalPosition);
        if (enemyBehaviour == 0)
        {
            PatrolBehaviour();
        }
        else if(enemyBehaviour==3)
        {
            SuspicionBehaviour();
        }
        else if(enemyBehaviour==1)
        {
            ChaseBehaviour();
        }
        UpdateTimers();
        UpdateAnimator();
    }

    #region Suspicion Behaviour
    private void SuspicionBehaviour()
    {
        navMeshAgent.enabled = false;
        animator.SetBool("Suspicion", true);
    }
    #endregion

    #region Chase Behaviour
    private void ChaseBehaviour()
    {
        animator.SetBool("Suspicion", false);
        if (AtAttackingDistance())
        {
            enemyBehaviour = 2;
            navMeshAgent.enabled=false;
            GetComponent<EnemyController>().StartAttacking();
        }
        else
        {
            navMeshAgent.enabled = true;
            navMeshAgent.SetDestination(targetPos);
            enemyBehaviour = 1;
        }
    }

    public bool AtAttackingDistance()
    {
        distanceToTarget = Vector3.Distance(transform.position, targetPos);
        return distanceToTarget <= stoppingDistance;
    }
    #endregion

    #region Patrol Behaviour
    private void PatrolBehaviour()
    {
        animator.SetBool("Suspicion", false);
        if (AtWayPoint() || wayPointMoveTime > timeToReachWaypoint)
        {
            if (distanceFromOriginalPos <= 1f)
            {
                float xPoint = Random.Range(-xMax, xMax);
                float zPoint = Random.Range(-zMax, zMax);
                xPoint = xPoint < 0 ? transform.position.x + xPoint - offset : transform.position.x + xPoint + offset;
                zPoint = zPoint < 0 ? transform.position.z + zPoint - offset : transform.position.z + zPoint + offset;
                randomWaypoint = new Vector3(xPoint, transform.position.y, zPoint);
            }
            else
            {
                randomWaypoint = originalPosition;
            }
            timeSinceArrivedAtWaypoint = 0;
            wayPointMoveTime = 0;
            navMeshAgent.enabled = false;
        }
        if (timeSinceArrivedAtWaypoint > wayPointDwellTime)
        {
            navMeshAgent.enabled = true;
            navMeshAgent.SetDestination(randomWaypoint);
        }
    }

    private bool AtWayPoint()
    {
        distanceToWaypoint = Vector3.Distance(transform.position, randomWaypoint);
        return distanceToWaypoint <= wayPointTolrance;
    }
    #endregion
    
    void UpdateTimers()
    {
        timeSinceArrivedAtWaypoint += Time.deltaTime;
        wayPointMoveTime += Time.deltaTime;
    }
    
    public void EnemyBehaviour(int enemyBehaviour,Vector3 targetPos)
    {
        this.enemyBehaviour = enemyBehaviour;
        this.targetPos = targetPos;
    }
    void UpdateAnimator()
    {
        Vector3 velocity = navMeshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = enemyBehaviour==0?localVelocity.z:enemyBehaviour==1?localVelocity.z+chaseSpeed:0;
        float turn = Mathf.Atan2(localVelocity.x, localVelocity.z);
        animator.SetFloat("Forward", speed, 0.1f, Time.deltaTime);
        animator.SetFloat("Turn", turn, 0.1f, Time.deltaTime);
    }
}
