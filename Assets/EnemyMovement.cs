using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    // This script is violently borrowed from teacher Jesper
    
    // private NavMeshAgent navMeshAgent;
    public Transform Player;
    // private float aggroRange = 0f; 
    void Start()
    {
        // navMeshAgent = GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {
        // navMeshAgent.SetDestination(Player.position);
        //
        // if (Vector3.Distance(transform.position, Player.position) < aggroRange)
        // {
        //     navMeshAgent.isStopped = false; 
        //
        //     navMeshAgent.SetDestination(Player.position);
        // }
        // else
        // {
        //     navMeshAgent.isStopped = true;
        // }
        
        gameObject.transform.LookAt(Player);
    }
}
