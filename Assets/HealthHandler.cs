using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHandler : MonoBehaviour
{
    public float maxHealth;
    [SerializeField] private Transform canvas;
    [NonSerialized] public float health;
    private Camera mainCam;
    private RectTransform hpBars;
    private Slider hpBarSlider;
    private float hpBarTimer;
    private float hpBarFadeTime = 1.5f;

    void Awake()
    {
        mainCam = Camera.main;
        hpBars = canvas.Find("HPbars").GetComponent<RectTransform>();
        hpBarSlider = canvas.Find("HPbars").GetComponent<Slider>();
        hpBars.gameObject.SetActive(false);
        health = maxHealth;
    }
    
    void Update()
    {
        Vector3 worldPos = transform.position;
        worldPos.y += transform.localScale.y;

        hpBars.position  = mainCam.WorldToScreenPoint(worldPos);

        if (hpBars.position.z < 0)
        {
            hpBars.position *= -1;
        }

        if (Time.time - hpBarTimer < hpBarFadeTime && health > 0)
        {
            hpBars.gameObject.SetActive(true);
        }
        else
        {
            hpBars.gameObject.SetActive(false);
        }
    }
    

    public void TakeDamage(float damage)
    {
        health -= damage;
        UpdateHpBar();
        hpBarTimer = Time.time;
    }

    private void UpdateHpBar()
    {
        hpBarSlider.value = hpBarSlider.maxValue * (health/maxHealth);
    }

    
    
    IEnumerator StartHpBarFade()
    {
        hpBarTimer = Time.time;
        
        yield return new WaitUntil(() => Time.time - hpBarTimer > hpBarFadeTime);
        
        hpBars.gameObject.SetActive(false);
    }
}
