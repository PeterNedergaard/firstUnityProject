using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public float maxSpeed;
    public float accel;
    public float jumpSpeed;
    public Vector3 turnSpeed;

    public bool w;
    public bool s;
    public bool a;
    public bool d;
    public bool space;
    public bool onGround;

    Rigidbody _rigidbody;
    int _layerMask = 1 << 8;
    
    void Start()
    {
        maxSpeed = 10;
        accel = 50;
        jumpSpeed = 350;
        turnSpeed = new Vector3(0,100,0);

        _rigidbody = GetComponent<Rigidbody>();
        _layerMask = ~_layerMask;
    }

    void Update()
    {
        w = Input.GetKey("w");
        
        s = Input.GetKey("s");
        
        a = Input.GetKey("a");
        
        d = Input.GetKey("d");
        
        space = Input.GetKey("space");

    }

    private void FixedUpdate()
    {
        
        // W - Forward \\
        if (w)
        {
            _rigidbody.velocity = transform.forward * maxSpeed;
        }
        else if (_rigidbody.GetPointVelocity(_rigidbody.position).z - accel * Time.fixedDeltaTime > 0)
        {
            _rigidbody.AddForce(transform.forward * -accel, ForceMode.Acceleration);
        }
        
        // S - Back \\
        if (s)
        {
            _rigidbody.velocity = transform.forward * -maxSpeed;
        }
        else if (_rigidbody.GetPointVelocity(_rigidbody.position).z + accel * Time.fixedDeltaTime < 0)
        {
            _rigidbody.AddForce(transform.forward * accel, ForceMode.Acceleration);
        }
        
        // A - Rotate left \\
        if (a)
        {
            Quaternion deltaRotation = Quaternion.Euler(-turnSpeed * Time.fixedDeltaTime);
            _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);
        }
        // else if (_rigidbody.GetPointVelocity(_rigidbody.position).x + accel * Time.fixedDeltaTime < 0)
        // {
        //     _rigidbody.AddForce(transform.right * accel, ForceMode.Acceleration);
        // }
        
        // D - Rotate right \\
        if (d)
        {
            Quaternion deltaRotation = Quaternion.Euler(turnSpeed * Time.fixedDeltaTime);
            _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);
        }
        // else if (_rigidbody.GetPointVelocity(_rigidbody.position).x - accel * Time.fixedDeltaTime > 0)
        // {
        //     _rigidbody.AddForce(transform.right * -accel, ForceMode.Acceleration);
        // }
        //
        // // SPACE - Up \\
        if (space)
        {
            onGround = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 1, _layerMask); ;
        
            if (onGround)
            {
                _rigidbody.AddForce(transform.up * jumpSpeed);    
            }
        }

    }
}
