using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerReload : MonoBehaviour
{
    
    [SerializeField] private AudioClip magReleaseClip;
    [SerializeField] private AudioClip magInsertClip;
    
    private float reloadTime;
    private float reloadDelay;
    private List<Transform> magList;
    private bool reloading;
    private PlayerGunInfo gunInfo;
    private AudioSource audioSrc;

    private void Awake()
    {
        gunInfo = GetComponent<PlayerGunInfo>();
        magList = new List<Transform>();
        audioSrc = GetComponent<AudioSource>();
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
        
        float pitch = Random.Range(0.85f, 1.15f);
        audioSrc.pitch = pitch;
        audioSrc.PlayOneShot(magInsertClip, 0.15f);
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
            
            float pitch = Random.Range(0.85f, 1.15f);
            audioSrc.pitch = pitch;
            audioSrc.PlayOneShot(magReleaseClip, 0.15f);
        }
        
        if (magList.Count > 10)
        {
            var magToDestroy = magList[0].gameObject;
            magList.Remove(magToDestroy.transform);
            Destroy(magToDestroy);
        }
    }
}
