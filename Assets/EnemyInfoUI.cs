using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInfoUI : MonoBehaviour
{
    private Text healthText;
    private Camera cam;

    private RectTransform hpBarGreen;
    private RectTransform hpBarRed;
    private float hpBarMaxWidth;
    private RectTransform hpBars;

    void Start()
    {
        healthText = transform.Find("Canvas/HealthText").GetComponent<Text>();
        healthText.transform.position = new Vector3(100,100);
        cam = Camera.main;
        
        hpBars = transform.Find("Canvas/HPbars").GetComponent<RectTransform>();
        hpBarGreen = hpBars.Find("HPbarGreen").GetComponent<RectTransform>();
        hpBarRed = hpBars.Find("HPbarRed").GetComponent<RectTransform>();
        hpBarMaxWidth = hpBarRed.rect.width;
    }
    
    
    void Update()
    {
        healthText.text = GetComponent<EnemyHealth>().health.ToString();
        Vector3 worldPos = transform.position;
        
        worldPos.y += transform.localScale.y + 0.5f;
        healthText.transform.position = cam.WorldToScreenPoint(worldPos);
        
        // hpBars.transform.position = cam.WorldToScreenPoint(worldPos);
        // hpBars.anchoredPosition = cam.WorldToScreenPoint(worldPos);
    }


    public void UpdateHPbar()
    {
        EnemyHealth enemyHealth = GetComponent<EnemyHealth>();

        float currHp = enemyHealth.health;
        float maxHp = enemyHealth.maxHealth;
        
        float barWidth = hpBarMaxWidth * (currHp/maxHp);

        Debug.Log(barWidth);
        
        hpBarGreen.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, -20, barWidth);
    }
}
