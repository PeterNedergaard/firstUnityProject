using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    [NonSerialized] public float health;
    private PlayerTextHUD playerTextHUD;
    
    private void Awake()
    {
        playerTextHUD = GetComponent<PlayerTextHUD>();
    }

    void Start()
    {
        health = maxHealth;
    }


    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.CompareTag("EnemyArm"))
        {
            TakeDamage(collisionInfo.transform.root.GetComponent<EnemyBehaviour>().damageAmount);
        }
    }

    void Update()
    {
        
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        playerTextHUD.UpdateHPbar();
    }
    
    
}
