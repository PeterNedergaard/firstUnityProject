using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class PlayerShoot : MonoBehaviour
{
    
    private PlayerGunInfo gunInfo;
    private Vector3 recoilPos;

    private void Awake()
    {
        gunInfo = GetComponent<PlayerGunInfo>();
    }

    void Start()
    {
    }


    void Update()
    {
        if (Input.GetMouseButton(0) && gunInfo.magParent && gunInfo.gunScript.ammoInMag > 0)
        {
            gunInfo.gunScript.Shoot();
        }
    }
}



