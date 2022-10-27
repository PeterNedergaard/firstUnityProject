using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

public class PlayerBuild : MonoBehaviour
{

    [SerializeField] private List<Object> barriers;
    private PlayerGunInfo _playerGunInfo;
    private GameObject currBarrier;
    
    private Camera mainCamera;
    private RaycastHit hit;
    private LayerMask _layerMask;

    void Awake()
    {
        _playerGunInfo = GetComponent<PlayerGunInfo>();
        mainCamera = Camera.main;
        _layerMask = ~LayerMask.GetMask("Barrier");
    }
    

    void Update()
    {
        if (_playerGunInfo.hammerObject)
        {
            Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, 100, _layerMask);

            if (!currBarrier)
            {
                currBarrier = Instantiate(barriers[0], hit.point, Quaternion.identity).GameObject();
                currBarrier.transform.GetComponent<NavMeshObstacle>().enabled = false;
            }
            else
            {
                Vector3 newPos = hit.point;
                
                if (currBarrier.transform.position != newPos)
                {
                    currBarrier.transform.position = newPos;
                    
                }
                
            }

            if (Input.GetMouseButtonDown(0))
            {
                currBarrier.transform.GetComponent<NavMeshObstacle>().enabled = true;
                currBarrier = null;
            }
            
        }
        else
        {
            if (currBarrier)
            {
                Destroy(currBarrier);
            }
        }
    }
}
