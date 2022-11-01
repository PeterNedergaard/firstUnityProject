using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

public class PlayerBuild : MonoBehaviour
{

    [SerializeField] private List<Object> barriers;
    [SerializeField] private GameObject barrierObject;
    private PlayerGunInfo _playerGunInfo;
    private int barrierIndex;
    private GameObject ghostBarrier;
    
    private Camera mainCamera;
    private RaycastHit hit;
    private LayerMask _layerMask;
    

    void Awake()
    {
        _playerGunInfo = GetComponent<PlayerGunInfo>();
        mainCamera = Camera.main;
        _layerMask = ~LayerMask.GetMask("Barrier", "Enemy");
    }
    

    void Update()
    {
        if (_playerGunInfo.hammerObject)
        {
            // Find position for barrier
            Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, 100, _layerMask);
            Vector3 newPos = hit.point;

            // Either create or reposition the transparent barrier
            if (!ghostBarrier)
            {
                SetGhostBarrier();
            }
            else
            {
                if (ghostBarrier.transform.position != newPos)
                {
                    ghostBarrier.transform.position = newPos;
                }
                
                ghostBarrier.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            }
            
            

            
            // Place barrier
            if (Input.GetMouseButtonDown(0))
            {
                GameObject barrierObj = Instantiate(barrierObject, hit.point, Quaternion.identity).GameObject();
                GameObject barrierModel = Instantiate(barriers[barrierIndex], hit.point, ghostBarrier.transform.rotation).GameObject();

                BarrierHpBar barrierHpBar = barrierObj.GetComponent<BarrierHpBar>();
                BarrierScript barrierScript = barrierModel.GetComponent<BarrierScript>();

                barrierHpBar._barrierScript = barrierScript;
                barrierScript._barrierHpBar = barrierHpBar;
                
                barrierModel.GetComponent<MeshCollider>().convex = true;
                barrierModel.AddComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                barrierModel.transform.SetParent(barrierObj.transform);
            }
            
            // Change barrier
            if (Input.GetMouseButtonDown(1))
            {
                barrierIndex += 1;
                
                if (barrierIndex > barriers.Count - 1)
                {
                    barrierIndex = 0;
                }
                
                SetGhostBarrier();
            }
            
        }
        else
        {
            // Makes sure a barrier isn't placed when dropping hammer
            if (ghostBarrier)
            {
                Destroy(ghostBarrier);
            }
        }
    }


    void SetGhostBarrier()
    {
        if (ghostBarrier)
        {
            Destroy(ghostBarrier);
        }
        
        ghostBarrier = Instantiate(barriers[barrierIndex], hit.point, Quaternion.identity).GameObject();
        ghostBarrier.GetComponent<NavMeshObstacle>().enabled = false;
        Destroy(ghostBarrier.GetComponent<BarrierScript>());
        
        ghostBarrier.layer = 16;
        ghostBarrier.tag = "Untagged";

        ghostBarrier.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        MakeObjTransparent(ghostBarrier);
    }


    void MakeObjTransparent(GameObject obj)
    {
        // Apparently, all of this is needed to change an 'Opaque' object to 'Transparent'

        Material mat = obj.GetComponent<MeshRenderer>().material;
                
        mat.SetFloat("_Mode", 3);
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.DisableKeyword("_ALPHABLEND_ON");
        mat.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = 3000;

        Color col = mat.color;
        col.a = 0.6f;
        mat.color = col;
    }
}
