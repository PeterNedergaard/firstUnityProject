using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTextHUD : MonoBehaviour
{
    private GameObject lookAtTextObject;
    [NonSerialized] public Text lookAtText;

    void Start()
    {
        lookAtTextObject = GameObject.Find("Canvas/LookingAt").gameObject;
        lookAtText = lookAtTextObject.GetComponent<Text>();
    }
    
    
    void Update()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            string text = hit.transform.name;
            
            if (text.Equals("Plane"))
            {
                text = "";
            }
            
            if (!hit.transform.name.Equals(lookAtText.text))
            {
                lookAtText.text = text;
            }   
        }
        else
        {
            lookAtText.text = "";
        }
    }
}
