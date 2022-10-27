using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerAnimation : MonoBehaviour
{
    private V2PlayerMovement v2PlayerMovement;
    private Animator m_animator;
    private Transform upperBody;

    private Transform rightHandObj;
    
    private void Awake()
    {
        v2PlayerMovement = GetComponent<V2PlayerMovement>();
        m_animator = GetComponent<Animator>();
        upperBody = transform.Find("Armature/Root_M/Spine1_M");
        rightHandObj = transform.Find("Armature/Root_M/Spine1_M/Spine2_M/Chest_M/Scapula_R/Shoulder_R/Elbow_R/Wrist_R");

    }

    void Start()
    {
    }

    
    private void FixedUpdate()
    {
        m_animator.SetFloat("Forward", Input.GetAxis("Vertical"));

        if (Input.GetAxis("Vertical") > 0)
        {
            m_animator.SetFloat("Right", -Input.GetAxis("Horizontal"));
        }
        else
        {
            m_animator.SetFloat("Right", Input.GetAxis("Horizontal"));
        }
    }

    private void LateUpdate()
    {
        upperBody.rotation *= Quaternion.Euler(1, 1, v2PlayerMovement.turn.x);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        m_animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        m_animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
        m_animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
        m_animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
    }
}
