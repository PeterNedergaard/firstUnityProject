using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTextHUD : MonoBehaviour
{
    [NonSerialized] public Camera mainCamera;
    [NonSerialized] public float defaultFOV;
    private GameObject lookAtTextObject;
    private Transform canvas;
    private Text lookAtText;
    private Text AmmoInMagText;
    private Slider hpBarSlider;
    private float sliderMaxValue;
    private GameObject crossHair;

    private PlayerGunInfo gunInfo;

    private void Awake()
    {
        gunInfo = GetComponent<PlayerGunInfo>();
        mainCamera = transform.Find("Main Camera").GetComponent<Camera>();
        canvas = transform.Find("Canvas");
        lookAtTextObject = canvas.Find("LookingAt").gameObject;
        lookAtText = lookAtTextObject.GetComponent<Text>();
        AmmoInMagText = canvas.Find("AmmoPanel/AmmoInMagText").GetComponent<Text>();
        hpBarSlider = transform.Find("Canvas/HPpanel/HPbars").GetComponent<Slider>();
        crossHair = canvas.Find("Crosshair").gameObject;
    }

    void Start()
    {
        sliderMaxValue = 1;
        defaultFOV = mainCamera.fieldOfView;
    }
    
    
    void Update()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit))
        {
            // To help save resources. Idk if it matters much
            if (hit.transform.CompareTag("Weapon") || hit.transform.CompareTag("BuildHammer") && !hit.transform.name.Equals(lookAtText.text) && !gunInfo.gunObject)
            {
                lookAtText.text = "Press 'E' to pick up " + hit.transform.name;
                lookAtText.gameObject.SetActive(true);
            }
            else
            {
                lookAtText.gameObject.SetActive(false);
            }
        }
        
        if (gunInfo.gunObject)
        {
            AmmoInMagText.text = gunInfo.gunScript.ammoInMag.ToString();
            AmmoInMagText.gameObject.SetActive(true);
            crossHair.gameObject.SetActive(false);
        }
        else
        {
            AmmoInMagText.gameObject.SetActive(false);
            crossHair.gameObject.SetActive(true);
        }
    }
    
    
    public void UpdateHPbar()
    {
        PlayerHealth playerHealth = GetComponent<PlayerHealth>();
        
        float sliderValue = sliderMaxValue * (playerHealth.health/playerHealth.maxHealth);
        
        hpBarSlider.value = sliderValue;
    }
}
