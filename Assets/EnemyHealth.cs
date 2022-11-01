using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100;
    [NonSerialized] public float health;
    private EnemyInfoUI enemyInfoUI;
    private EnemyBehaviour enemyBehaviour;
    private float barrierDmgTimer;
    
    void Awake()
    {
        health = maxHealth;
        enemyInfoUI = GetComponent<EnemyInfoUI>();
        enemyBehaviour = GetComponent<EnemyBehaviour>();
    }


    void Update()
    {
        if (health <= 0 && !enemyBehaviour.dead)
        {
            health = 0;
            GetComponent<EnemyBehaviour>().Die();
            transform.Find("Canvas/HPbars").gameObject.SetActive(false);
        }
    }
    
    
    private void OnCollisionEnter(Collision collision)
    {
        if (!enemyBehaviour.dead)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                TakeDamage(collision.gameObject.GetComponent<BulletBehavior>().bulletDamage);
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
                TakeDamage(barrierScript.damage);
            }
        }
    }
    
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        enemyInfoUI.UpdateHPbar();
    }
    
}
