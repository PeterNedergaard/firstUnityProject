using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public float maxSpeed;
    public float accel;
    public float decel;
    
    public float wSpeed;
    public float sSpeed;
    public float aSpeed;
    public float dSpeed;

    public bool w;
    public bool s;
    public bool a;
    public bool d;
    void Start()
    {
        maxSpeed = 10;
        accel = 30;
        decel = 20;
    }

    void Update()
    {
        w = Input.GetKey("w");
        
        s = Input.GetKey("s");
        
        a = Input.GetKey("a");
        
        d = Input.GetKey("d");
    }

    private void FixedUpdate()
    {
        
        if (w)
        {
            if (wSpeed < maxSpeed)
            {
                wSpeed += accel * Time.deltaTime;
            }
            transform.Translate(Vector3.forward * (wSpeed * Time.deltaTime));
        }
        else
        {
            if (wSpeed - accel / decel > 0)
            {
                wSpeed -= accel / decel;
                transform.Translate(Vector3.forward * (wSpeed * Time.deltaTime));
            }
            else
            {
                wSpeed = 0;
            }
        }
        
        if (s)
        {
            if (sSpeed < maxSpeed)
            {
                sSpeed += accel * Time.deltaTime;
            }
            transform.Translate(Vector3.back * (sSpeed * Time.deltaTime));
        }
        else
        {
            if (sSpeed - accel / decel > 0)
            {
                sSpeed -= accel / decel;
                transform.Translate(Vector3.back * (sSpeed * Time.deltaTime));
            }
            else
            {
                sSpeed = 0;
            }
        }

        if (a)
        {
            if (aSpeed < maxSpeed)
            {
                aSpeed += accel * Time.deltaTime;
            }
            transform.Translate(Vector3.left * (aSpeed * Time.deltaTime));
        }
        else
        {
            if (aSpeed - accel / decel > 0)
            {
                aSpeed -= accel / decel;
                transform.Translate(Vector3.left * (aSpeed * Time.deltaTime));
            }
            else
            {
                aSpeed = 0;
            }
            
        }
        
        if (d)
        {
            if (dSpeed < maxSpeed)
            {
                dSpeed += accel * Time.deltaTime;
            }
            transform.Translate(Vector3.right * (dSpeed * Time.deltaTime));
        }
        else
        {
            if (dSpeed - accel / decel > 0)
            {
                dSpeed -= accel / decel;
                transform.Translate(Vector3.right * (dSpeed * Time.deltaTime));
            }
            else
            {
                dSpeed = 0;
            }
            
        }

    }
}
