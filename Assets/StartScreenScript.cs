using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenScript : MonoBehaviour
{
    public GameObject buttonCard;
    public Transform startCam;
    public GameObject player;
    public GameObject gameHandler;
    private Transform mainCam;
    private GameObject canvas;
    private bool translateCam;

    private float smoothTime = 10f;
    private Vector3 velocityPos;
    private Vector3 velocityRot;
    private Vector3 targetPos;
    private Vector3 targetRot;

    private TextMeshProUGUI title;
    private float colorChange;

    private float colorMin = 0.4f;
    private float colorMax = 0.8f;
    private float t;
    

    void Awake()
    {
        Button startBtn = buttonCard.transform.Find("StartButton").GetComponent<Button>();
        Button quitBtn = buttonCard.transform.Find("QuitButton").GetComponent<Button>();
        
        canvas = transform.Find("Canvas").gameObject;
        mainCam = player.transform.Find("Main Camera");
        title = canvas.transform.Find("Title").GetComponent<TextMeshProUGUI>();
        
        targetPos = mainCam.position;
        targetRot = mainCam.eulerAngles;

        startBtn.onClick.AddListener(StartGame);
        quitBtn.onClick.AddListener(QuitGame);
    }


    void Update()
    {
        if (translateCam)
        {
            TranslateToPlayerCam();
        }
        else
        {
            PulseTitleColor();
        }
    }
    


    private void TranslateToPlayerCam()
    {
        startCam.position = Vector3.SmoothDamp(startCam.position, targetPos, ref velocityPos, smoothTime * Time.deltaTime);
        startCam.eulerAngles = Vector3.SmoothDamp(startCam.eulerAngles, targetRot, ref velocityRot, smoothTime * Time.deltaTime);

        if (IsCamAtTarget())
        {
            ToggleGameActors();
            translateCam = false;
        }
    }
    
    
    private void ToggleGameActors()
    {
        player.SetActive(!player.activeSelf);
        gameHandler.SetActive(!gameHandler.activeSelf);
        gameHandler.GetComponent<GameHandler>().AnnounceNextRound();
        gameObject.SetActive(!gameObject.activeSelf);
    }
    

    private bool IsCamAtTarget()
    {
        bool result = false;
        float posDiff = startCam.position.sqrMagnitude - mainCam.position.sqrMagnitude;
        
        if (posDiff < 0) posDiff *= -1;

        if (posDiff < 1)
        {
            result = true;
        }

        return result;
    }

    
    private void PulseTitleColor()
    {
        Color color = new Color(Mathf.Lerp(colorMin, colorMax, t), title.color.g, title.color.b);
        title.color = color;

        t += 0.5f * Time.deltaTime;

        if (t >= 1f)
        {
            // colorMax and colorMin values gets switched. Alternative to using a temporary variable
            (colorMax, colorMin) = (colorMin, colorMax);
            t = 0;
        }
    }

    
    void StartGame()
    {
        translateCam = true;
        canvas.SetActive(false);
        transform.Find("CameraPivot").GetComponent<Animation>().enabled = false;
        
        if (startCam.eulerAngles.y > 180) targetRot.y += 360;
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
