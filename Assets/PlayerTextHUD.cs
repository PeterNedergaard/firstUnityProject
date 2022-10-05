using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTextHUD : MonoBehaviour
{
    private GameObject lookAtTextObject;
    private Transform canvas;
    private Text lookAtText;
    private Text AmmoInMagText;
    private PlayerGunInteract playerGunInteract;
    private Slider hpBarSlider;
    private float sliderMaxValue;

    void Start()
    {
        canvas = transform.Find("Canvas");
        
        lookAtTextObject = canvas.transform.Find("LookingAt").gameObject;
        lookAtText = lookAtTextObject.GetComponent<Text>();
        
        AmmoInMagText = canvas.transform.Find("AmmoPanel/AmmoInMagText").GetComponent<Text>();
        playerGunInteract = GetComponent<PlayerGunInteract>();
        
        hpBarSlider = transform.Find("Canvas/HPpanel/HPbars").GetComponent<Slider>();
        sliderMaxValue = 1;
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

        AmmoInMagText.text = playerGunInteract.gunObject ? playerGunInteract.gunScript.ammoInMag.ToString() : "";
    }
    
    
    public void UpdateHPbar()
    {
        PlayerHealth playerHealth = GetComponent<PlayerHealth>();
        
        float sliderValue = sliderMaxValue * (playerHealth.health/playerHealth.maxHealth);
        
        hpBarSlider.value = sliderValue;
    }
}
