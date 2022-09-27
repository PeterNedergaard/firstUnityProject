using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100;
    [NonSerialized] public float health;
    private bool dead = false;
    
    void Start()
    {
        health = maxHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && !dead)
        {
            health -= collision.gameObject.GetComponent<BulletBehavior>().bulletDamage;
            GetComponent<EnemyInfoUI>().UpdateHPbar();
        }
    }

    void Update()
    {
        if (health <= 0 && !dead)
        {
            health = 0;
            GetComponent<EnemyBehaviour>().Die();
            dead = true;
        }
    }
    
}
