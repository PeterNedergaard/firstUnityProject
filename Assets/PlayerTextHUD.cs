using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTextHUD : MonoBehaviour
{
    private GameObject lookAtTextObject;
    private Text lookAtText;

    void Start()
    {
        lookAtTextObject = GameObject.Find("Canvas/LookingAt").gameObject;
        lookAtText = lookAtTextObject.GetComponent<Text>();
    }
    
    
    void Update()
    {
        RaycastHit hit;
        string text;
        
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            // To help save resources. Idk if it matters much
            if (!hit.transform.name.Equals(lookAtText.text))
            {
                // In theory, if it has a barrel, it's a gun
                if (hit.transform.Find("Barrel"))
                {
                    text = "Press 'E' to pick up " + hit.transform.name;
                }
                else
                {
                    text = "";
                }

                lookAtText.text = text;
            }
        }
    }
}
