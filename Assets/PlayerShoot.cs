using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class PlayerShoot : MonoBehaviour
{
    private PlayerGunInteract playerGunInteract;
    private PlayerReload playerReload;
    

    private Vector3 recoilPos;

    private void Awake()
    {
        playerGunInteract = GetComponent<PlayerGunInteract>();
        playerReload = GetComponent<PlayerReload>();
    }

    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetMouseButton(0) && playerReload.magParent && playerGunInteract.gunScript.ammoInMag > 0)
        {
            playerGunInteract.gunScript.Shoot();
        }
    }
}



