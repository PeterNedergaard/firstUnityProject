using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGunInteract : MonoBehaviour
{
    
    private PlayerGunInfo gunInfo;
    
    
    private void Awake()
    {
        gunInfo = GetComponent<PlayerGunInfo>();

        gunInfo.gunObjectParent = transform.Find("GunObject").gameObject;
        
        if (gunInfo.gunObjectParent.transform.childCount >= 1)
        {
            gunInfo.gunObject = transform.Find("GunObject").GetChild(0).gameObject;
            gunInfo.gunScript = gunInfo.gunObject.GetComponent<GunScript>();
        }
    }

    void Start()
    {
        
    }


    void Update()
    {
        
        if (Input.GetKeyDown("e"))
        {
            if (gunInfo.gunObjectParent.transform.childCount >= 1)
            {
                RemoveWeapon();
            }
            else
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    if (hit.transform.Find("Barrel"))
                    {
                        SetWeapon(hit.transform.gameObject);
                    }
                }
            }
        }
        
    }

    
    void SetWeapon(GameObject gunObj)
    {
        gunInfo.pickUp = true;
        
        gunObj.GetComponent<Rigidbody>().isKinematic = true;
        gunObj.transform.SetParent(gameObject.transform.Find("GunObject"));
        gunObj.transform.localPosition = Vector3.zero;
        gunObj.transform.localRotation = Quaternion.identity;

        gunInfo.gunObject = gunObj;
        gunInfo.gunScript = gunObj.GetComponent<GunScript>();
    }
    
    
    void RemoveWeapon()
    {
        Rigidbody gunRB = gunInfo.gunObject.GetComponent<Rigidbody>();

        gunRB.isKinematic = false;

        gunInfo.gunObject.transform.parent = null;

        gunInfo.gunObject = null;
        gunInfo.gunScript = null;
        
        gunInfo.magParent = null;
    }
    
}
