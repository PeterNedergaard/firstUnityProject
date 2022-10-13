using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerRecoil : MonoBehaviour
{
    private PlayerGunInteract playerGunInteract;
    private V2PlayerMovement playerMovement;
    
    private void Awake()
    {
        playerGunInteract = GetComponent<PlayerGunInteract>();
        playerMovement = GetComponent<V2PlayerMovement>();
    }

    void Start()
    {
    }

    void Update()
    {
        if (playerGunInteract.gunObject)
        {
            playerGunInteract.gunObject.transform.localPosition = Vector3.MoveTowards(playerGunInteract.gunObject.transform.localPosition, Vector3.zero, 0.015f);


            if (playerGunInteract.gunObject.name.Equals("UZI"))
            {
                Transform bolt = playerGunInteract.gunObject.transform.Find("Bolt");

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
        float recoilAmount = playerGunInteract.gunScript.recoilAmount;
        
        playerMovement.turn.y += Random.Range(-0.2f, recoilAmount);
        playerMovement.turn.x += Random.Range(-0.4f, 0.4f);

        Vector3 gunPos = playerGunInteract.gunObject.transform.localPosition;
            
        if (gunPos.z > -recoilAmount/10)
        {
            playerGunInteract.gunObject.transform.localPosition += new Vector3(0, 0, -recoilAmount/10);
        }

        
        
        var gunObj = playerGunInteract.gunObject;
        
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
