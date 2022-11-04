using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrierHpBar : MonoBehaviour
{
    
    private Camera cam;
    [NonSerialized] public BarrierScript _barrierScript;
    private Slider hpBarSlider;
    [NonSerialized] public RectTransform hpBars;
    [NonSerialized] public float hpBarTimer;
    [NonSerialized] public float hpBarFadeTime = 1.5f;
    
    void Awake()
    {
        cam = Camera.main;
        hpBars = transform.Find("Canvas/HPbars").GetComponent<RectTransform>();
        hpBarSlider = transform.Find("Canvas/HPbars").GetComponent<Slider>();
        hpBars.gameObject.SetActive(false);
    }
    
    
    void Update()
    {
        Vector3 worldPos = transform.position;
        worldPos.y += transform.localScale.y;

        hpBars.position  = cam.WorldToScreenPoint(worldPos);

        if (hpBars.position.z < 0)
        {
            hpBars.position *= -1;
        }

        if (Time.time > hpBarTimer + hpBarFadeTime)
        {
            hpBars.gameObject.SetActive(false);
        }
    }
    
    public void UpdateHPbar()
    {
        hpBarTimer = Time.time;

        float currHp = _barrierScript.health;
        float maxHp = _barrierScript.maxHealth;
        
        float sliderValue = hpBarSlider.maxValue * (currHp/maxHp);
        hpBarSlider.value = sliderValue;
        
        hpBars.gameObject.SetActive(true);
    }
}
