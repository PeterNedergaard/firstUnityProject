using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private PlayerGunInteract playerGunInteract;
    private PlayerReload playerReload;
    void Start()
    {
        playerGunInteract = GetComponent<PlayerGunInteract>();
        playerReload = GetComponent<PlayerReload>();
    }


    void Update()
    {
        if (Input.GetMouseButton(0) && playerGunInteract.gunScript && playerReload.magParent)
        {
            playerGunInteract.gunScript.Shoot();
        }
    }
}



