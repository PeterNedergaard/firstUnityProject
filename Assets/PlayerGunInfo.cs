using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunInfo : MonoBehaviour
{
    [NonSerialized] public GameObject gunObjectParent;
    [NonSerialized] public GameObject gunObject;
    [NonSerialized] public GunScript gunScript;
    
    [NonSerialized] public Transform magObject;
    [NonSerialized] public Transform magParent;
    
    [NonSerialized] public bool pickUp;
    

    public void getMag()
    {
        magObject = gunObject.transform.Find("MagObject");

        if (magObject.childCount > 0)
        {
            magParent = magObject.GetChild(0);
        }

        pickUp = false;
    }
}
