using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float bulletDamage;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    
}
