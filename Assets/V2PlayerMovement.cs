using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;
using MouseButton = Unity.VisualScripting.MouseButton;
using Object = UnityEngine.Object;

public class V2PlayerMovement : MonoBehaviour
{
    private CharacterController cc;
    private bool groundedPlayer;
    private bool jump;

    private float verticalVelocity;
    private float playerSpeed = 7;
    private float jumpHeight = 3;
    private float gravityValue = 9.81f;
    private float mouseSensitivity = 2;
    private int layerMask = 1 << 8;
    [NonSerialized] public Vector2 turn;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        layerMask = ~layerMask;
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void FixedUpdate()
    {
        // Front and backwards
        Vector3 moveZ = transform.TransformDirection(Vector3.forward);
        moveZ *= playerSpeed * Input.GetAxis("Vertical");

        // Right and left
        Vector3 moveX = transform.TransformDirection(Vector3.right);
        moveX *= playerSpeed * Input.GetAxis("Horizontal");

        Vector3 move = moveZ + moveX;


        // Jump
        verticalVelocity -= gravityValue * Time.deltaTime;

        if (groundedPlayer && verticalVelocity < 0)
        {
            verticalVelocity = 0;
        }

        if (jump)
        {
            verticalVelocity += Mathf.Sqrt(jumpHeight * gravityValue);
            jump = false;
        }
        
        move.y = verticalVelocity;
        

        // Mouse movement
        turn.x += Input.GetAxis("Mouse Y") * mouseSensitivity;
        turn.y += Input.GetAxis("Mouse X") * mouseSensitivity;
        
        if (turn.x < -90) turn.x = -90;
        if (turn.x > 90) turn.x = 90;
        
        transform.rotation = Quaternion.Euler(-turn.x, turn.y, 0);
        // transform.rotation = Quaternion.Euler(0, turn.y, 0);


        cc.Move(move * Time.deltaTime);
    }
    
    
    void Update()
    {
        groundedPlayer = Physics.Raycast(transform.position, Vector3.down, 1 + cc.skinWidth, layerMask);
        // groundedPlayer = Physics.Raycast(transform.position, Vector3.down, cc.skinWidth, layerMask);

        if (Input.GetButton("Jump") && groundedPlayer)
        {
            jump = true;
        }
    }
    
}
