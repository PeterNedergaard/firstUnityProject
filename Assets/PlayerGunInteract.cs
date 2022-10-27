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
                    string hitTag = hit.transform.tag;

                    if (hitTag.Equals("Weapon") || hitTag.Equals("BuildHammer"))
                    {
                        SetWeapon(hit.transform.gameObject);
                    }
                }
            }
        }
        
    }

    
    void SetWeapon(GameObject gunObj)
    {
        gunObj.GetComponent<Rigidbody>().isKinematic = true;
        gunObj.transform.SetParent(transform.Find("GunObject"));
        gunObj.transform.localPosition = Vector3.zero;
        gunObj.transform.localRotation = Quaternion.identity;
        
        if (gunObj.CompareTag("BuildHammer"))
        {
            gunInfo.hammerObject = gunObj;
        }
        else
        {
            gunInfo.pickUp = true;
            gunInfo.gunObject = gunObj;
            gunInfo.gunScript = gunObj.GetComponent<GunScript>();
        }
        
        
        
    }
    
    
    void RemoveWeapon()
    {
        if (gunInfo.hammerObject)
        {
            Rigidbody hammerRB = gunInfo.hammerObject.GetComponent<Rigidbody>();
            hammerRB.isKinematic = false;

            gunInfo.hammerObject.transform.parent = null;
            gunInfo.hammerObject = null;
        }
        else
        {
            Rigidbody gunRB = gunInfo.gunObject.GetComponent<Rigidbody>();
            
            gunInfo.gunScript = null;
            gunInfo.magParent = null;
            gunRB.isKinematic = false;
            gunInfo.gunObject.transform.parent = null;
            gunInfo.gunObject = null;
        }
        
    }
    
}
