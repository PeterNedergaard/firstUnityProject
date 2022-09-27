using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    // Parts of this script is violently borrowed from teacher Jesper
    
    private NavMeshAgent navMeshAgent;
    private Rigidbody rb;
    private Transform player;
    private float aggroRange = 15f;
    private bool dead = false;
    
    
    
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        if (!dead)
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
        }
    }
    
    
    public void Die()
    {
        dead = true;
        navMeshAgent.enabled = false;
        rb.freezeRotation = false;
    }
}