using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInfoUI : MonoBehaviour
{
    private Camera cam;

    private Slider hpBarSlider;
    private float sliderMaxValue;
    private RectTransform hpBars;
    [NonSerialized] public float hpBarTimer;
    [NonSerialized] public float hpBarFadeTime = 1;
    private EnemyBehaviour enemyBehaviour;


    void Start()
    {
        cam = Camera.main;
        enemyBehaviour = GetComponent<EnemyBehaviour>();

        hpBars = transform.Find("Canvas/HPbars").GetComponent<RectTransform>();
        hpBarSlider = transform.Find("Canvas/HPbars").GetComponent<Slider>();
        sliderMaxValue = 1;
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

        if (Time.unscaledTime > hpBarTimer + hpBarFadeTime)
        {
            hpBars.gameObject.SetActive(false);
        }
        else if (!enemyBehaviour.dead)
        {
            hpBars.gameObject.SetActive(true);
        }
        
    }


    public void UpdateHPbar()
    {
        EnemyHealth enemyHealth = GetComponent<EnemyHealth>();

        float currHp = enemyHealth.health;
        float maxHp = enemyHealth.maxHealth;
        
        float sliderValue = sliderMaxValue * (currHp/maxHp);
        
        hpBarSlider.value = sliderValue;
    }
}
