using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    private Vector3 bulletVector;
    private float bulletForce = 75;
    private GameObject barrelObject;
    private float bulletSpawnOffset;
    private List<GameObject> bulletList = new();

    void Start()
    {
        barrelObject = gameObject.transform.Find("Barrel").gameObject;
        bulletSpawnOffset = barrelObject.transform.localScale.z + bulletPrefab.transform.localScale.z / 2;
    }


    void Update()
    {
    }


    public void Shoot()
    {
        bulletVector = barrelObject.transform.position + barrelObject.transform.forward * bulletSpawnOffset;

        GameObject currBullet = Instantiate(bulletPrefab, bulletVector, Quaternion.identity);
        bulletList.Add(currBullet);
        
        Rigidbody currBulletRb = currBullet.AddComponent<Rigidbody>();
        currBulletRb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            
        currBulletRb.AddForce(barrelObject.transform.forward * bulletForce, ForceMode.Impulse);

        if (bulletList.Count > 15)
        {
            var bulletToDestroy = bulletList[0];
            bulletList.Remove(bulletList[0]);
            Destroy(bulletToDestroy);
        }
    } 
}