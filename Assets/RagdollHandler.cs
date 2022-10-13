using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollHandler : MonoBehaviour
{
    
    public void GoRagdoll()
    {
        GetComponent<Animator>().enabled = false;

        foreach (var component in GetComponentsInChildren<Rigidbody>())
        {
            component.isKinematic = false;
            component.useGravity = true;
        }
    }
    
}
