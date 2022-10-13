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
    [NonSerialized] public int ammoInMag;
    public float recoilAmount;
    public bool shotgun;
    private GameObject barrelObject;
    private float bulletForce = 10;
    private float bulletSpawnOffset;
    private float bulletTime;
    private PlayerTextHUD playerTextHUD;
    private GameObject muzzleFlash;
    private GameObject muzzleFlashObject;
    private PlayerRecoil playerRecoil;
    

    private void Awake()
    {
        barrelObject = transform.Find("Barrel").gameObject;
        muzzleFlashObject = transform.Find("MuzzleFlashObject").gameObject;
        playerRecoil = GameObject.Find("Player").GetComponent<PlayerRecoil>();
    }

    void Start()
    {
        bulletSpawnOffset = barrelObject.transform.localScale.z / 2;
        ammoInMag = maxAmmo;

        Vector3 flashVector = muzzleFlashObject.transform.position;
        muzzleFlash = Instantiate(muzzleFlashPrefab, flashVector, transform.rotation);
        muzzleFlash.transform.parent = muzzleFlashObject.transform;
        muzzleFlash.SetActive(false);
    }


    void Update()
    {
        if (Time.unscaledTime - bulletTime > 0.02 && muzzleFlash.activeInHierarchy)
        {
            muzzleFlash.SetActive(false);
        }
    }


    public void Shoot()
    {
        if (Time.unscaledTime - bulletTime > 60/rpm && ammoInMag > 0 || bulletTime == 0)
        {
            ammoInMag -= 1;

            bulletTime = Time.unscaledTime;
            
            
            // Spawn bullet, add force 
            Vector3 bulletSpawn = barrelObject.transform.position + barrelObject.transform.forward * bulletSpawnOffset;

            if (!shotgun)
            {
                GameObject currBullet = Instantiate(bulletPrefab, bulletSpawn, Quaternion.identity);
                Rigidbody currBulletRb = currBullet.GetComponent<Rigidbody>();
                currBulletRb.AddForce(barrelObject.transform.forward * bulletForce, ForceMode.Impulse);
            }
            else
            {
                int shotCount = 10;
                float shotAngle = 7;

                for (int i = 0; i < shotCount-1; i++)
                {
                    float randomRotX = Random.Range(-shotAngle / 2, shotAngle / 2);
                    float randomRotY = Random.Range(-shotAngle / 2, shotAngle / 2);

                    GameObject currBullet = Instantiate(bulletPrefab, bulletSpawn, Quaternion.identity);
                    Rigidbody currBulletRb = currBullet.GetComponent<Rigidbody>();
                    
                    currBulletRb.transform.rotation = barrelObject.transform.rotation * Quaternion.Euler(randomRotX, randomRotY, 1);
                    currBulletRb.AddForce((barrelObject.transform.forward + currBulletRb.transform.forward) * bulletForce, ForceMode.Impulse);
                }
            }
            
            // Muzzle flash
            muzzleFlash.transform.localRotation = Quaternion.Euler(0,0,Random.Range(0, 360));
            muzzleFlash.SetActive(true);
            
            // Recoil
            playerRecoil.ApplyRecoil();
        }
    } 
}