using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerADS : MonoBehaviour
{
    private Transform gunObject;
    private bool ads;
    private float adsSpeed = 3;
    private Vector3 adsPos;
    private Vector3 hipPos;
    private PlayerTextHUD playerTextHUD;
    private PlayerGunInfo gunInfo;
    
    private void Awake()
    {
        playerTextHUD = GetComponent<PlayerTextHUD>();
        gunInfo = GetComponent<PlayerGunInfo>();
    }

    void Start()
    {
        adsPos = new Vector3(0, 0.55f, 1f);
        hipPos = transform.Find("GunObject").localPosition;
    }
    
    
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && gunInfo.gunObject)
        {
            ads = !ads;
        }

        if (gunInfo.gunObject)
        {
            gunObject = gunInfo.gunObjectParent.transform;

            switch (ads)
            {
                case true when gunObject.localPosition != adsPos:
                    ChangeGunPos(adsPos);
                    playerTextHUD.mainCamera.fieldOfView = 40;
                    break;
                case false when gunObject.localPosition != hipPos:
                    ChangeGunPos(hipPos);
                    playerTextHUD.mainCamera.fieldOfView = playerTextHUD.defaultFOV;
                    break;
            }
        }
    }


    private void ChangeGunPos(Vector3 targetPos)
    {
        gunObject.localPosition = Vector3.MoveTowards(gunObject.localPosition, targetPos, adsSpeed * Time.deltaTime);
    }
}
