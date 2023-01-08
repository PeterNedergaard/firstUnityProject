using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenScript : MonoBehaviour
{
    [SerializeField] private GameObject buttonCard;
    [SerializeField] private Transform startCam;
    [SerializeField] private GameObject gameHandler;
    private GameObject player;
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
        
        player = GameObject.Find("Player");
        canvas = transform.Find("Canvas").gameObject;
        mainCam = Camera.main.transform;
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
            EnableGameActors();
            translateCam = false;
        }
    }
    
    
    private void EnableGameActors()
    {
        player.GetComponent<V2PlayerMovement>().enabled = true;
        player.transform.Find("Canvas").gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;

        GameObject flyingBurger = GameObject.Find("FlyingBurger");
        flyingBurger.GetComponent<BurgerScript>().enabled = true;
        flyingBurger.GetComponent<HealthHandler>().enabled = true;
        
        gameHandler.SetActive(true);
        gameHandler.GetComponent<GameHandler>().RestartGame();

        gameObject.SetActive(false);
    }
    

    private bool IsCamAtTarget()
    {
        bool result = false;
        float posDiff = startCam.position.sqrMagnitude - targetPos.sqrMagnitude;
        
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

    public void GameOver()
    {
        player.SetActive(false);
        startCam.GetComponent<AudioListener>().enabled = true;
        
        targetPos = mainCam.position;
        targetRot = mainCam.eulerAngles;

        canvas.SetActive(true);
        title.text = "GAME OVER";
        transform.Find("CameraPivot").GetComponent<Animation>().enabled = true;
        
        player.GetComponent<V2PlayerMovement>().enabled = false;
        player.transform.Find("Canvas").gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;

        GameObject flyingBurger = GameObject.Find("FlyingBurger");
        flyingBurger.GetComponent<BurgerScript>().enabled = false;
        flyingBurger.GetComponent<HealthHandler>().enabled = false;
        gameHandler.GetComponent<GameHandler>().ClearEnemies();

        gameHandler.SetActive(false);
        gameObject.SetActive(true);
    }
    
    void StartGame()
    {
        player.SetActive(true);
        startCam.GetComponent<AudioListener>().enabled = false;
        
        translateCam = true;
        canvas.SetActive(false);
        transform.Find("CameraPivot").GetComponent<Animation>().enabled = false;
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
