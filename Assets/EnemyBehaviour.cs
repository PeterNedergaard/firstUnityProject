using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    // This script is violently borrowed from teacher Jesper
    
    private NavMeshAgent navMeshAgent;
    private Transform player;
    private float aggroRange = 15f; 
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
    }

    
    void Update()
    {
        navMeshAgent.SetDestination(player.position);
        
        if (Vector3.Distance(transform.position, player.position) < aggroRange)
        {
            navMeshAgent.isStopped = false; 
        
            navMeshAgent.SetDestination(player.position);
        }
        else
        {
            navMeshAgent.isStopped = true;
        }
        
        // gameObject.transform.LookAt(player);
    }
}