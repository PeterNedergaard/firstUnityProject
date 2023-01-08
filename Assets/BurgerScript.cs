using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BurgerScript : MonoBehaviour
{

    [SerializeField] private HealthHandler healthHandler;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private AudioClip deathClip;
    private StartScreenScript startScreenScript;
    private AudioHandler audioHandler;
    
    void Awake()
    {
        startScreenScript = GameObject.Find("StartScreen").GetComponent<StartScreenScript>();
        audioHandler = GetComponent<AudioHandler>();
    }
    
    void Update()
    {

    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("EnemyArm"))
        {
            healthHandler.TakeDamage(collision.transform.root.GetComponent<EnemyBehaviour>().damageAmount);
            
            if (healthHandler.health <= 0)
            {
                Die();
            }

            float pitch = Random.Range(0.8f, 1.2f);
            audioHandler.PlayClipAt(hitClip, transform.position, 0.1f, pitch);
        }
    }


    private void Die()
    {
        startScreenScript.GameOver();
        healthHandler.health = healthHandler.maxHealth;
        
        audioHandler.PlayClipAt(deathClip, transform.position, 0.3f, 0.6f);
    }
    
}
