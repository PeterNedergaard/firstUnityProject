using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerReload : MonoBehaviour
{
    private PlayerGunInteract playerGunInteract;
    private Transform magObject;
    [NonSerialized] public Transform magParent;
    private float reloadTime;
    private float reloadDelay;
    private List<Transform> magList;
    private bool reloading;
    void Start()
    {
        playerGunInteract = GetComponent<PlayerGunInteract>();
        magList = new List<Transform>();
    }


    void Update()
    {
        if (playerGunInteract.pickUp)
        {
            getMag();
            reloadDelay = playerGunInteract.gunScript.reloadDelay;
            reloading = false;
            
            if (!magParent)
            {
                reloadTime = Time.unscaledTime;
            }
        }
        
        if (Input.GetKeyDown("r") && playerGunInteract.gunObject && !reloading)
        {
            reloading = true;
            reloadTime = Time.unscaledTime;
            
            if (magParent)
            {
                removeMag();
            }

        }

        if (Time.unscaledTime - reloadTime > reloadDelay && playerGunInteract.gunObject && !magParent)
        {
            insertMag();
            reloading = false;
        }
        
    }

    private void insertMag()
    {
        Vector3 magVector = magObject.transform.position;
        GameObject newMag = Instantiate(playerGunInteract.gunScript.magPrefab, magVector, transform.rotation);
        newMag.transform.parent = magObject.transform;
        
        magParent = newMag.transform;
        playerGunInteract.gunScript.ammoInMag = playerGunInteract.gunScript.maxAmmo;
    }
    
    private void removeMag()
    {
        if (magParent)
        {
            magParent.parent = null;
            Rigidbody magRb = magParent.AddComponent<Rigidbody>();
            magRb.isKinematic = false;
            magRb.AddForce(magObject.transform.up * -200);

            magList.Add(magParent);
            magParent = null;
        }
        
        if (magList.Count > 10)
        {
            var magToDestroy = magList[0].gameObject;
            magList.Remove(magToDestroy.transform);
            Destroy(magToDestroy);
        }
    }
    

    private void getMag()
    {
        magObject = playerGunInteract.gunObject.transform.Find("MagObject");

        if (magObject.childCount > 0)
        {
            magParent = magObject.GetChild(0);
        }

        playerGunInteract.pickUp = false;
    }
}
