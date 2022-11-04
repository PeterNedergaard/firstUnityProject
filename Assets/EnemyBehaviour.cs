using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    
    [SerializeField] private Animator m_animator;
    [SerializeField] private Transform attackArm;
    [NonSerialized] public bool dead;
    public float damageAmount;
    private float maxMoveSpeed;
    private NavMeshAgent navMeshAgent;
    private Transform target;
    private float aggroRange = 15f;
    private CapsuleCollider attackArmCollider;
    private float timeSinceUpdate;
    private float pathUpdateInterval = 0.5f;


    private void Awake()
    {
        // player = GameObject.Find("Player").transform;
        target = GameObject.Find("FlyingBurger").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        m_animator = GetComponent<Animator>();
        maxMoveSpeed = navMeshAgent.speed;
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
            if (Vector3.Distance(transform.position, target.position) < aggroRange)
            {
                navMeshAgent.isStopped = false;

                // Update path
                if (Time.time - timeSinceUpdate > pathUpdateInterval)
                {
                    navMeshAgent.SetDestination(target.position);
                    timeSinceUpdate = Time.time;
                }
                
                // Start walk animation
                m_animator.SetFloat("MoveSpeed", navMeshAgent.speed * 3);
                
                // Start or stop attacking
                if (Vector3.Distance(transform.position, target.position) < 1.5f)
                {
                    m_animator.SetTrigger("Attack");
                    navMeshAgent.speed = 0;
                }
                else
                {
                    m_animator.ResetTrigger("Attack");
                    navMeshAgent.speed = maxMoveSpeed;
                }
                
            }
            else
            {
                navMeshAgent.isStopped = true;
                m_animator.SetFloat("MoveSpeed", 0);
            }
            
            
            // Attack if a barrier is in front of enemy
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit,2))
            {
                if (hit.transform.CompareTag("Barrier"))
                {
                    m_animator.SetTrigger("Attack");
                }
                else
                {
                    m_animator.ResetTrigger("Attack");
                }
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