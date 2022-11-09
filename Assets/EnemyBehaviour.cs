using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private Animator m_animator;
    [SerializeField] private Transform attackArm;
    [SerializeField] private HealthHandler healthHandler;
    public float damageAmount;
    [NonSerialized] public bool dead;
    [NonSerialized] public NavMeshAgent navMeshAgent;
    private GameHandler gameHandler;
    private Transform target;
    private CapsuleCollider attackArmCollider;
    private float barrierDmgTimer;
    private float maxMoveSpeed;
    private float timeSinceUpdate;
    private float pathUpdateInterval = 0.5f;


    private void Awake()
    {
        target = GameObject.Find("FlyingBurger").transform;
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        m_animator = GetComponent<Animator>();
        maxMoveSpeed = navMeshAgent.speed;
        
        attackArmCollider = attackArm.GetComponent<CapsuleCollider>();
        attackArmCollider.enabled = false;
    }


    void Update()
    {
        if (healthHandler.health <= 0 && !dead)
        {
            Die();
        }
        
        
        if (!dead)
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


            // Attack if a barrier is in front of enemy
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 2))
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

    
    private void OnCollisionEnter(Collision collision)
    {
        if (!dead)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                healthHandler.TakeDamage(collision.gameObject.GetComponent<BulletBehavior>().bulletDamage);
            }
        }
    }


    private void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Barrier"))
        {
            Debug.Log("Barrier collision");
            BarrierScript barrierScript = collisionInfo.gameObject.GetComponent<BarrierScript>();
            
            if (Time.time - barrierDmgTimer > barrierScript.dmgDelay && barrierScript.damage > 0)
            {
                barrierDmgTimer = Time.time;
                healthHandler.TakeDamage(barrierScript.damage);
                Debug.Log("Take damage");
            }
        }
    }
    

    private void Die()
    {
        dead = true;
        gameHandler.aliveEnemyAmnt -= 1;
        GetComponent<RagdollHandler>().GoRagdoll(true);

        StartCoroutine(StartDecay());
    }


    IEnumerator StartDecay()
    {
        float timer = Time.time;
        
        yield return new WaitUntil(() => Time.time - timer > 3 && dead);
        
        gameObject.SetActive(false);
        dead = false;
        healthHandler.health = healthHandler.maxHealth;
        GetComponent<RagdollHandler>().GoRagdoll(false);
    }
    
    
    public void ActivateArm(string state)
    {
        attackArmCollider.enabled = state.Equals("on");
    }
}