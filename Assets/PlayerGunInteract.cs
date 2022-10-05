using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGunInteract : MonoBehaviour
{
    private GameObject gunObjectParent;
    [NonSerialized] public GameObject gunObject;
    [NonSerialized] public GunScript gunScript;
    [NonSerialized] public bool pickUp;
    private PlayerReload playerReload;

    void Start()
    {
        gunObjectParent = transform.Find("GunObject").gameObject;
        playerReload = GetComponent<PlayerReload>();
        
        if (gunObjectParent.transform.childCount >= 1)
        {
            gunObject = gameObject.transform.Find("GunObject").GetChild(0).gameObject;
            gunScript = gunObject.GetComponent<GunScript>();
        }
    }


    void Update()
    {

        if (Input.GetKeyDown("e"))
        {
            if (gunObjectParent.transform.childCount >= 1)
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
        pickUp = true;
        
        gunObj.GetComponent<Rigidbody>().isKinematic = true;
        gunObj.transform.SetParent(gameObject.transform.Find("GunObject"));
        gunObj.transform.localPosition = Vector3.zero;
        gunObj.transform.localRotation = Quaternion.identity;
        
        gunObject = gunObj;
        gunScript = gunObj.GetComponent<GunScript>();
    }
    
    
    void RemoveWeapon()
    {
        Rigidbody gunRB = gunObject.GetComponent<Rigidbody>();

        gunRB.isKinematic = false;
        gunObject.transform.parent = null;
                
        gunObject = null;
        gunScript = null;

        playerReload.magParent = null;
    }
    
}
