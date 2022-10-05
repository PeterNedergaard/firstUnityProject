using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject magPrefab;
    public float rpm;
    public float reloadDelay;
    public int maxAmmo;
    private GameObject barrelObject;
    private Vector3 bulletVector;
    private float bulletForce = 10;
    private float bulletSpawnOffset;
    private float bulletTime;
    [NonSerialized] public int ammoInMag;
    private PlayerTextHUD playerTextHUD;


    void Start()
    {
        barrelObject = transform.Find("Barrel").gameObject;
        bulletSpawnOffset = barrelObject.transform.localScale.z + bulletPrefab.transform.localScale.z / 2;
        ammoInMag = maxAmmo;
    }


    void Update()
    {
    }


    public void Shoot()
    {
        // Spawn bullet, add force 
        if (Time.unscaledTime - bulletTime > 60/rpm && ammoInMag > 0)
        {
            ammoInMag -= 1;

            bulletTime = Time.unscaledTime;

            bulletVector = barrelObject.transform.position + barrelObject.transform.forward * bulletSpawnOffset;
            GameObject currBullet = Instantiate(bulletPrefab, bulletVector, Quaternion.identity);

            Rigidbody currBulletRb = currBullet.GetComponent<Rigidbody>();
                
            currBulletRb.AddForce(barrelObject.transform.forward * bulletForce, ForceMode.Impulse);
        }
    } 
}