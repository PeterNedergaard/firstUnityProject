using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    public Transform canvas;
    private TextMeshProUGUI roundTitle;
    private TextMeshProUGUI AmountInfo;
    private TextMeshProUGUI SpeedInfo;
    private TextMeshProUGUI DamageInfo;
    
    private int roundNumber = 1;
    private int enemyAmount = 10;
    private float enemySpeed = 1.0f;
    private float enemyDamage = 1.0f;

    private CanvasGroup canvasGroup;
    private float roundInfoTimer;
    private float t;
    
    void Awake()
    {
        Transform infoStats = canvas.Find("RoundInfo/InfoPanel/InfoStats");
        roundTitle = canvas.Find("RoundTitle").GetComponent<TextMeshProUGUI>();
        canvasGroup = canvas.GetComponent<CanvasGroup>();
        
        AmountInfo = infoStats.Find("Amount/AmountInfo").GetComponent<TextMeshProUGUI>();
        SpeedInfo = infoStats.Find("Speed/SpeedInfo").GetComponent<TextMeshProUGUI>();
        DamageInfo = infoStats.Find("Damage/DamageInfo").GetComponent<TextMeshProUGUI>();
    }
    
    
    void Update()
    {
        
    }


    public void AnnounceNextRound()
    {
        //TODO: Change round-number and info text
        roundTitle.text = "ROUND " + roundNumber;
        AmountInfo.text = enemyAmount.ToString();
        SpeedInfo.text = enemySpeed + "x";
        DamageInfo.text = enemyDamage + "x";
        
        //TODO: Activate canvas
        canvas.gameObject.SetActive(true);
        roundInfoTimer = Time.time;
        canvasGroup.alpha = 1;

        StartCoroutine(FadeOutRoundInfo());

        //TODO: Increment roundNumber
        roundNumber += 1;
    }
    
    
    IEnumerator FadeOutRoundInfo()
    {
        while (t < 1)
        {
            if (Time.time - roundInfoTimer > 2)
            {
                canvasGroup.alpha = Mathf.Lerp(1, 0, t);
            
                t += 0.5f * Time.deltaTime;
            }

            yield return null;
        }

        t = 0;
        canvas.gameObject.SetActive(false);
    }
}
