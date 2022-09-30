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
    
    void Start()
    {
        health = maxHealth;
        enemyInfoUI = GetComponent<EnemyInfoUI>();
        enemyBehaviour = GetComponent<EnemyBehaviour>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        enemyInfoUI.hpBarTimer = Time.unscaledTime;
        if (collision.gameObject.CompareTag("Bullet") && !enemyBehaviour.dead)
        {
            health -= collision.gameObject.GetComponent<BulletBehavior>().bulletDamage;
            GetComponent<EnemyInfoUI>().UpdateHPbar();
        }
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
    
}
