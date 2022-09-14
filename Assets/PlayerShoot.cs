using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private Vector3 bulletLocation;
    private List<GameObject> bulletList = new();
    // private GameObject currBullet;
    
    public GameObject bulletPrefab;
    void Start()
    {
        
    }


    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                bulletLocation = hit.point;

                GameObject currBullet = Instantiate(bulletPrefab, bulletLocation, Quaternion.identity);

                currBullet.AddComponent<Rigidbody>();
                currBullet.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
                
                bulletList.Add(currBullet);
            }
        }

        if (bulletList.Count > 20)
        {
            Destroy(bulletList[0]);
            
            bulletList.Remove(bulletList[0]);
        }
    }
}
