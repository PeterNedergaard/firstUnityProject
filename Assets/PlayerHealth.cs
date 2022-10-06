using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    [NonSerialized] public float health;
    private float damageTimer;
    public float damageDelay = 1;
    private PlayerTextHUD playerTextHUD;
    
    void Start()
    {
        health = maxHealth;
        playerTextHUD = GetComponent<PlayerTextHUD>();
    }


    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.transform.CompareTag("EnemyArm"))
        {
            TakeDamage(collisionInfo.transform.root.GetComponent<EnemyBehaviour>().damageAmount);
            damageTimer = Time.unscaledTime;
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
