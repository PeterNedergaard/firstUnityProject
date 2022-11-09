using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerRecoil : MonoBehaviour
{
    private V2PlayerMovement playerMovement;
    private PlayerGunInfo gunInfo;

    private Transform bolt;
    
    private void Awake()
    {
        playerMovement = GetComponent<V2PlayerMovement>();
        gunInfo = GetComponent<PlayerGunInfo>();
    }
    

    void Update()
    {
        if (gunInfo.gunObject)
        {
            // Move gun back to original position
            gunInfo.gunObject.transform.localPosition = Vector3.MoveTowards(gunInfo.gunObject.transform.localPosition, Vector3.zero, 0.01f);
            
            
            // Bolt movement limited to the UZI
            if (gunInfo.gunObject.name.Equals("UZI"))
            {
                if (!bolt)
                {
                    bolt = gunInfo.gunObject.transform.Find("Bolt");
                }

                var zeroPos = new Vector3(0, 0.021f, 0.145f);

                if (bolt.localPosition != zeroPos)
                {
                    bolt.localPosition = Vector3.MoveTowards(bolt.localPosition, zeroPos, 0.025f);
                }
            }
            
        }
    }


    public void ApplyRecoil()
    {
        float recoilAmount = gunInfo.gunScript.recoilAmount;

        playerMovement.turn.x += Random.Range(-0.2f, recoilAmount);
        playerMovement.turn.y += Random.Range(-0.4f, 0.4f);
        
        Vector3 gunPos = gunInfo.gunObject.transform.localPosition;
            
        if (gunPos.z > -recoilAmount/10)
        {
            gunInfo.gunObject.transform.localPosition = new Vector3(0, 0, -recoilAmount/10);
        }
        
        
        
        // Bolt movement limited to the UZI
        var gunObj = gunInfo.gunObject;
        
        if (gunObj.name.Equals("UZI"))
        {
            var bolt = gunObj.transform.Find("Bolt");
        
            if (bolt.localPosition.z > -0.08)
            {
                bolt.localPosition = new Vector3(0, 0.021f, -0.08f);
            }
            
        }
    }
}
