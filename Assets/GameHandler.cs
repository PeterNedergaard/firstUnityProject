using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameHandler : MonoBehaviour
{

    [SerializeField] private Transform canvas;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform spawnPointsObj;
    private List<GameObject> enemyList = new();
    private List<Transform> spawnPointList = new();
    private TextMeshProUGUI roundTitle;
    private TextMeshProUGUI AmountInfo;
    private TextMeshProUGUI SpeedInfo;
    private TextMeshProUGUI DamageInfo;
    [NonSerialized] public int aliveEnemyAmnt;
    private bool inRound;
    private int difficultyType;
    
    private int roundNumber = 1;
    private int enemyAmount = 5;
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

        foreach (Transform spawnPoint in spawnPointsObj)
        {
            spawnPointList.Add(spawnPoint);
        }

        StartCoroutine(InstantiateEnemies());
    }
    
    
    void Update()
    {
        if (aliveEnemyAmnt < 1 && inRound)
        {
            inRound = false;
            roundNumber += 1;
            AnnounceNextRound();
        }
    }


    public void AnnounceNextRound()
    {
        roundTitle.text = "ROUND " + roundNumber;
        AmountInfo.text = enemyAmount.ToString();
        SpeedInfo.text = (enemySpeed + "x").Replace(",", ".");
        DamageInfo.text = (enemyDamage + "x").Replace(",", ".");
        
        canvas.gameObject.SetActive(true);
        roundInfoTimer = Time.time;
        canvasGroup.alpha = 1;

        StartCoroutine(FadeOutRoundInfo());
    }

    
    private void StartNextRound()
    {
        StartCoroutine(SpawnEnemies());
        inRound = true;
    }


    public void RestartGame()
    {
        roundNumber = 1;
        enemyDamage = 1;
        enemySpeed = 1;
        inRound = false;

        ClearEnemies();
        
        enemyAmount = 5;
        
        StartCoroutine(InstantiateEnemies());
        AnnounceNextRound();
    }

    public void ClearEnemies()
    {
        for (int i = 0; i < enemyList.Count - 1; i++)
        {
            Destroy(enemyList[i]);
        }
        
        enemyList.Clear();
    }
    
    IEnumerator SpawnEnemies()
    {
        aliveEnemyAmnt = 0;
        
        for (int i = 0; i < enemyAmount; i++)
        {
            GameObject enemy = enemyList[i];
            EnemyBehaviour eb = enemy.GetComponent<EnemyBehaviour>();
            eb.navMeshAgent.enabled = true;
            eb.damageAmount *= enemyDamage;
            eb.navMeshAgent.speed *= enemySpeed;

            Vector3 spawnPos = spawnPointList[Random.Range(0, spawnPointList.Count)].position;
            spawnPos.x += Random.Range(-5, 6);
            spawnPos.z += Random.Range(-5, 6);
            enemy.transform.position = spawnPos;
            
            enemy.SetActive(true);
            eb.navMeshAgent.SetDestination(eb.target.position);
            aliveEnemyAmnt += 1;

            yield return new WaitForSeconds(0.2f);
        }

        switch (difficultyType)
        {
            case 0:
                enemyAmount += 5;
                break;
            case 1:
                enemySpeed += 0.1f;
                break;
            case 2:
                enemyDamage += 0.2f;
                difficultyType = -1;
                break;
        }

        difficultyType += 1;

    }


    IEnumerator InstantiateEnemies()
    {
        for (int i = 0; i < 50; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            enemyList.Add(enemy);
            
            yield return new WaitForSeconds(0.2f);
        }
    }
    
    
    IEnumerator FadeOutRoundInfo()
    {
        while (t < 1)
        {
            if (Time.time - roundInfoTimer > 10)
            {
                canvasGroup.alpha = Mathf.Lerp(1, 0, t);
            
                t += 0.5f * Time.deltaTime;
            }

            yield return null;
        }

        t = 0;
        canvas.gameObject.SetActive(false);
        
        StartNextRound();
    }
}
