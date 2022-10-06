using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    // Parts of this script is violently borrowed from teacher Jesper
    
    [SerializeField] private Animator m_animator;
    private NavMeshAgent navMeshAgent;
    private Rigidbody rb;
    private Transform player;
    private float aggroRange = 15f;
    [NonSerialized] public bool dead;
    public float damageAmount;
    private Collider attackArmCollider;


    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        rb = gameObject.GetComponent<Rigidbody>();
        m_animator = gameObject.GetComponent<Animator>();
    }

    void Start()
    {
        player = GameObject.Find("Player").transform;
        
        // Change this please
        attackArmCollider = transform.Find("basic_rig/basic_rig Pelvis/basic_rig Spine/basic_rig Spine1/basic_rig L Clavicle/basic_rig L UpperArm/basic_rig L Forearm/basic_rig L Hand").gameObject.GetComponent<Collider>();
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
        m_animator.SetTrigger("Dead");
    }
}