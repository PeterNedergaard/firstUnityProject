using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;
using MouseButton = Unity.VisualScripting.MouseButton;

public class V2PlayerMovement : MonoBehaviour
{
    private CharacterController cc;
    private bool groundedPlayer;

    private float verticalVelocity;
    private float playerSpeed = 7;
    private float jumpHeight = 3;
    private float gravityValue = 9.81f;
    private int layerMask = 1 << 8;
    private Vector2 turn;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        layerMask = ~layerMask;
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void FixedUpdate()
    {
        Vector3 move = transform.TransformDirection(Vector3.forward);

        move *= playerSpeed * Input.GetAxis("Vertical");
        
        

        Vector3 move2 = transform.TransformDirection(Vector3.right);

        move2 *= playerSpeed * Input.GetAxis("Horizontal");
        
        

        transform.Rotate(Vector3.up,2f * Input.GetAxis("Horizontal"));
        
        verticalVelocity -= gravityValue * Time.deltaTime;
        
        
        if (groundedPlayer && verticalVelocity < 0)
        {
            verticalVelocity = 0;
        }
        
        if (Input.GetButton("Jump") && groundedPlayer)
        {
            verticalVelocity += Mathf.Sqrt(jumpHeight * gravityValue);
        }

        move.y = verticalVelocity;

        cc.Move(move * Time.deltaTime);

        cc.Move(move2 * Time.deltaTime);
    }

    
    void Update()
    {
        groundedPlayer = Physics.Raycast(transform.position, Vector3.down, 1 + cc.skinWidth, layerMask);
        
        turn.x += Input.GetAxis("Mouse X");
        
        if (turn.y + Input.GetAxis("Mouse Y") is >= -90 and <= 90)
        {
            turn.y += Input.GetAxis("Mouse Y");
        }
        
        transform.rotation = Quaternion.Euler(-turn.y, turn.x, 0);
    }
}
