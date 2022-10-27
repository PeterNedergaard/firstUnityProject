using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerReload : MonoBehaviour
{
    private float reloadTime;
    private float reloadDelay;
    private List<Transform> magList;
    private bool reloading;
    private PlayerGunInfo gunInfo;

    private void Awake()
    {
        gunInfo = GetComponent<PlayerGunInfo>();
        magList = new List<Transform>();
    }
    

    void Update()
    {
        if (gunInfo.pickUp)
        {
            gunInfo.getMag();
            reloadDelay = gunInfo.gunScript.reloadDelay;
            reloading = false;
            
            if (!gunInfo.magParent)
            {
                reloadTime = Time.unscaledTime;
            }
        }
        
        if (Input.GetKeyDown("r") && gunInfo.gunObject && !reloading)
        {
            reloading = true;
            reloadTime = Time.unscaledTime;
            
            removeMag();
        }

        if (Time.unscaledTime - reloadTime > reloadDelay && gunInfo.gunObject && !gunInfo.magParent)
        {
            insertMag();
            reloading = false;
        }
        
    }

    private void insertMag()
    {
        Vector3 magVector = gunInfo.magObject.transform.position;
        GameObject newMag = Instantiate(gunInfo.gunScript.magPrefab, magVector, transform.rotation);
        newMag.transform.parent = gunInfo.magObject.transform;
        
        gunInfo.magParent = newMag.transform;
        gunInfo.gunScript.ammoInMag = gunInfo.gunScript.maxAmmo;
    }
    
    private void removeMag()
    {
        if (gunInfo.magParent)
        {
            gunInfo.magParent.parent = null;
            Rigidbody magRb = gunInfo.magParent.AddComponent<Rigidbody>();
            magRb.isKinematic = false;
            magRb.AddForce(gunInfo.magObject.transform.up * -200);

            magList.Add(gunInfo.magParent);
            gunInfo.magParent = null;
        }
        
        if (magList.Count > 10)
        {
            var magToDestroy = magList[0].gameObject;
            magList.Remove(magToDestroy.transform);
            Destroy(magToDestroy);
        }
    }
}
