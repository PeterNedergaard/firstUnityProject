using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierScript : MonoBehaviour
{
    [NonSerialized] public float dmgDelay = 1;
    [NonSerialized] public float health;
    public float damage;
    public float maxHealth;
    [NonSerialized] public BarrierHpBar _barrierHpBar;


    void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(transform.parent.gameObject);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("EnemyArm"))
        {
            TakeDamage(15);
        }
    }

    private void TakeDamage(float hitDamage)
    {
        health -= hitDamage;
        _barrierHpBar.UpdateHPbar();
    }
    
}
