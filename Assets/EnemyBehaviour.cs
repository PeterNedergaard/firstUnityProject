using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    // Parts of this script is violently borrowed from teacher Jesper
    
    private NavMeshAgent navMeshAgent;
    private Rigidbody rb;
    private Transform player;
    private float aggroRange = 15f;
    private bool dead = false;
    
    private Text healthText;
    private Camera cam;
    
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        rb = gameObject.GetComponent<Rigidbody>();

        
        healthText = transform.Find("Canvas/HealthText").GetComponent<Text>();
        healthText.transform.position = new Vector3(100,100);
        cam = Camera.main;
    }

    
    void Update()
    {
        ////////
        healthText.text = GetComponent<EnemyHealth>().health.ToString();
        Vector3 worldPos = transform.position;
        worldPos.y += 2;
        Vector3 screenPos = cam.WorldToScreenPoint(worldPos);
        healthText.transform.position = screenPos;
        ////////

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