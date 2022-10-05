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
    private bool jump;

    private float verticalVelocity;
    private float playerSpeed = 7;
    private float jumpHeight = 3;
    private float gravityValue = 9.81f;
    private float mouseSensitivity = 2;
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
        // Front and backwards
        Vector3 move = transform.TransformDirection(Vector3.forward);

        move *= playerSpeed * Input.GetAxis("Vertical");

        // Right and left
        Vector3 move2 = transform.TransformDirection(Vector3.right);

        move2 *= playerSpeed * Input.GetAxis("Horizontal");


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
        turn.x += Input.GetAxis("Mouse X") * mouseSensitivity;
        turn.y += Input.GetAxis("Mouse Y") * mouseSensitivity;

        if (turn.y < -90) turn.y = -90;
        if (turn.y > 90) turn.y = 90;

        transform.rotation = Quaternion.Euler(-turn.y, turn.x, 0);


        // Apparently, you arent supposed to call cc.Move more than once. Living on the edge...
        cc.Move(move * Time.deltaTime);
        cc.Move(move2 * Time.deltaTime);
    }

    
    void Update()
    {
        groundedPlayer = Physics.Raycast(transform.position, Vector3.down, 1 + cc.skinWidth, layerMask);

        if (Input.GetButton("Jump") && groundedPlayer)
        {
            jump = true;
        }
    }
}
