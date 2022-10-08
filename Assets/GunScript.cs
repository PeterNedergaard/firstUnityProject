using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GunScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject magPrefab;
    [SerializeField] private GameObject muzzleFlashPrefab;
    public float rpm;
    public float reloadDelay;
    public int maxAmmo;
    private GameObject barrelObject;
    private float bulletForce = 10;
    private float bulletSpawnOffset;
    private float bulletTime;
    [NonSerialized] public int ammoInMag;
    private PlayerTextHUD playerTextHUD;
    private GameObject muzzleFlash;
    private GameObject muzzleFlashObject;
    

    private void Awake()
    {
        barrelObject = transform.Find("Barrel").gameObject;
        muzzleFlashObject = transform.Find("MuzzleFlashObject").gameObject;
        
    }

    void Start()
    {
        bulletSpawnOffset = barrelObject.transform.localScale.z + bulletPrefab.transform.localScale.z / 2;
        
        ammoInMag = maxAmmo;
        
        Vector3 flashVector = muzzleFlashObject.transform.position;
        muzzleFlash = Instantiate(muzzleFlashPrefab, flashVector, transform.rotation);
        muzzleFlash.transform.parent = muzzleFlashObject.transform;
        muzzleFlash.SetActive(false);
    }


    void Update()
    {
        if (Time.unscaledTime - bulletTime > 0.02)
        {
            muzzleFlash.SetActive(false);
        }
    }


    public void Shoot()
    {
        // Spawn bullet, add force 
        if (Time.unscaledTime - bulletTime > 60/rpm && ammoInMag > 0)
        {
            ammoInMag -= 1;

            bulletTime = Time.unscaledTime;

            Vector3 bulletVector = barrelObject.transform.position + barrelObject.transform.forward * bulletSpawnOffset;
            
            GameObject currBullet = Instantiate(bulletPrefab, bulletVector, Quaternion.identity);
            Rigidbody currBulletRb = currBullet.GetComponent<Rigidbody>();
            currBulletRb.AddForce(barrelObject.transform.forward * bulletForce, ForceMode.Impulse);

            muzzleFlash.transform.localRotation = Quaternion.Euler(0,0,Random.Range(0, 360));
            muzzleFlash.SetActive(true);
        }
    } 
}