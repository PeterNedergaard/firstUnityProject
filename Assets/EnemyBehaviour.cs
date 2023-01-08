using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyBehaviour : MonoBehaviour
{
    public float damageAmount;
    [SerializeField] private Animator m_animator;
    [SerializeField] private Transform attackArm;
    [SerializeField] private HealthHandler healthHandler;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private AudioClip deathClip;
    [NonSerialized] public bool dead;
    [NonSerialized] public NavMeshAgent navMeshAgent;
    [NonSerialized] public Transform target;
    private GameHandler gameHandler;
    private CapsuleCollider attackArmCollider;
    private AudioHandler audioHandler;
    private float barrierDmgTimer;
    private float maxMoveSpeed;
    private float timeSinceUpdate;


    private void Awake()
    {
        target = GameObject.Find("FlyingBurger").transform;
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        m_animator = GetComponent<Animator>();
        maxMoveSpeed = navMeshAgent.speed;
        audioHandler = GetComponent<AudioHandler>();
        
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

            // Start walk animation
            m_animator.SetFloat("MoveSpeed", navMeshAgent.speed * 3);

            // Start or stop attacking
            if (Vector3.Distance(transform.position, target.position) < 1.3)
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
                
                float pitch = Random.Range(0.9f, 1.1f);
                audioHandler.PlayClipAt(hitClip, transform.position, 0.35f, pitch);
            }
        }
    }


    private void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Barrier"))
        {
            BarrierScript barrierScript = collisionInfo.gameObject.GetComponent<BarrierScript>();
            
            if (Time.time - barrierDmgTimer > barrierScript.dmgDelay && barrierScript.damage > 0)
            {
                barrierDmgTimer = Time.time;
                healthHandler.TakeDamage(barrierScript.damage);
            }
        }
    }
    

    private void Die()
    {
        dead = true;
        gameHandler.aliveEnemyAmnt -= 1;
        GetComponent<RagdollHandler>().GoRagdoll(true);
        
        float pitch = Random.Range(0.4f, 0.8f);
        audioHandler.PlayClipAt(deathClip, transform.position, 1f, pitch);


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