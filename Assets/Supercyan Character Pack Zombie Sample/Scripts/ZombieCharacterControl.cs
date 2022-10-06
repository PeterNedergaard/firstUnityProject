using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Timeline;

public class ZombieCharacterControl : MonoBehaviour
{
    [SerializeField] private Animator m_animator;

    private NavMeshAgent navMeshAgent;
    private Transform player;
    private float aggroRange = 15f;
    // [NonSerialized] public bool dead;
    private static readonly int MoveSpeed = Animator.StringToHash("MoveSpeed");


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        // rb = gameObject.GetComponent<Rigidbody>();
    }


    private void Awake()
    {
        if (!m_animator) { gameObject.GetComponent<Animator>(); }
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, player.position) < aggroRange)
        {
            navMeshAgent.isStopped = false; 
        
            navMeshAgent.SetDestination(player.position);
            
            m_animator.SetFloat("MoveSpeed", navMeshAgent.speed * 2);
            
            if (Vector3.Distance(transform.position, player.position) < 3)
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
            m_animator.SetFloat(MoveSpeed, 0);
        }
        
        
    }
    
}