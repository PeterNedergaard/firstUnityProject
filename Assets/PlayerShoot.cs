using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [NonSerialized] public GameObject gunObject;
    [NonSerialized] public GunScript gunScript;


    void Start()
    {
        gunObject = gameObject.transform.Find("GunObject").GetChild(0).gameObject;
        gunScript = gunObject.GetComponent<GunScript>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gunScript.Shoot();
        }
    }
}



