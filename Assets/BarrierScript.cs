using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierScript : MonoBehaviour
{
    public float damage;
    public float maxHealth;
    [NonSerialized] public HealthHandler healthHandler;
    [NonSerialized] public float dmgDelay = 1;

    private void Update()
    {
        if (healthHandler.health <= 0)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("EnemyArm"))
        {
            healthHandler.TakeDamage(collision.transform.root.GetComponent<EnemyBehaviour>().damageAmount);
        }
    }

}
