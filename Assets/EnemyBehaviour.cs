using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    
    [SerializeField] private Animator m_animator;
    private NavMeshAgent navMeshAgent;
    private Transform player;
    private float aggroRange = 15f;
    [NonSerialized] public bool dead;
    public float damageAmount;
    [SerializeField] private Transform attackArm;
    private CapsuleCollider attackArmCollider;


    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        m_animator = GetComponent<Animator>();
    }

    void Start()
    {
        attackArmCollider = attackArm.GetComponent<CapsuleCollider>();
        attackArmCollider.enabled = false;
    }
    

    void Update()
    {
        if (!dead)
        {
            if (Vector3.Distance(transform.position, player.position) < aggroRange)
            {
                navMeshAgent.isStopped = false; 
        
                navMeshAgent.SetDestination(player.position);
            
                m_animator.SetFloat("MoveSpeed", navMeshAgent.speed * 3);
            
                if (Vector3.Distance(transform.position, player.position) < 2.5)
                {
                    m_animator.SetTrigger("Attack");
                }
                else
                {
                    m_animator.ResetTrigger("Attack");
                }
            }
            else
            {
                navMeshAgent.isStopped = true;
                m_animator.SetFloat("MoveSpeed", 0);
            }
        }
    }
    
    
    public void Die()
    {
        dead = true;
        navMeshAgent.enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        
        GetComponent<RagdollHandler>().GoRagdoll();
    }

    
    public void ActivateArm(string state)
    {
        attackArmCollider.enabled = state.Equals("on");
    }
    
}