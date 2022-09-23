using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunInteract : MonoBehaviour
{
    
    
    void Start()
    {
    }


    void Update()
    {

        if (Input.GetKeyDown("e"))
        {
            if (gameObject.transform.Find("GunObject").childCount >= 1)
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
        PlayerShoot playerShoot = gameObject.GetComponent<PlayerShoot>();
        
        gunObj.GetComponent<Rigidbody>().isKinematic = true;
        gunObj.transform.SetParent(gameObject.transform.Find("GunObject"));
        gunObj.transform.localPosition = Vector3.zero;
        gunObj.transform.localRotation = Quaternion.identity;
        
        playerShoot.gunObject = gunObj;
        playerShoot.gunScript = gunObj.GetComponent<GunScript>();
    }
    
    
    void RemoveWeapon()
    {
        PlayerShoot playerShoot = gameObject.GetComponent<PlayerShoot>();
        GameObject gunObject = playerShoot.gunObject;
        Rigidbody gunRB = gunObject.GetComponent<Rigidbody>();

        gunRB.isKinematic = false;
        gunObject.transform.parent = null;
                
        playerShoot.gunObject = null;
        playerShoot.gunScript = null;
    }
}
