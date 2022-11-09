using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BurgerScript : MonoBehaviour
{

    [SerializeField] private HealthHandler healthHandler;
    
    void Awake()
    {
        
    }
    
    void Update()
    {
        
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("EnemyArm"))
        {
            healthHandler.TakeDamage(collision.transform.root.GetComponent<EnemyBehaviour>().damageAmount);
        }
    }

    
}
