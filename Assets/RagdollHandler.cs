using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RagdollHandler : MonoBehaviour
{
    
    public void GoRagdoll(bool state)
    {
        GetComponent<Animator>().enabled = !state;
        GetComponent<CapsuleCollider>().enabled = !state;
        GetComponent<NavMeshAgent>().enabled = !state;

        foreach (var component in GetComponentsInChildren<Rigidbody>())
        {
            component.isKinematic = !state;
            component.useGravity = state;
        }
    }
    
}
