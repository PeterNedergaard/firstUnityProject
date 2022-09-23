using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    private Vector3 bulletVector;
    private float bulletForce = 10;
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
        // In short: Spawn bullet, add to list, add force 
        
        bulletVector = barrelObject.transform.position + barrelObject.transform.forward * bulletSpawnOffset;
        GameObject currBullet = Instantiate(bulletPrefab, bulletVector, Quaternion.identity);
        
        bulletList.Add(currBullet);
        
        Rigidbody currBulletRb = currBullet.GetComponent<Rigidbody>();
            
        currBulletRb.AddForce(barrelObject.transform.forward * bulletForce, ForceMode.Impulse);
        
        
        // Hopefully saves resources
        if (bulletList.Count > 15)
        {
            Destroy(bulletList[0]);
            bulletList.Remove(bulletList[0]);
        }
        
    } 
}