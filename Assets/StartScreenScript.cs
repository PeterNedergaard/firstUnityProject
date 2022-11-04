using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenScript : MonoBehaviour
{
    public GameObject buttonCard;
    public Transform startCam;
    public GameObject player;
    private Transform mainCam;
    private GameObject _canvas;
    private bool gameStarted;

    private float smoothTime = 10f;
    private Vector3 velocityPos;
    private Vector3 velocityRot;
    
    
    void Awake()
    {
        Button startBtn = buttonCard.transform.Find("StartButton").GetComponent<Button>();
        Button quitBtn = buttonCard.transform.Find("QuitButton").GetComponent<Button>();
        _canvas = transform.Find("Canvas").gameObject;
        mainCam = player.transform.Find("Main Camera");
        
        startBtn.onClick.AddListener(StartGame);
        quitBtn.onClick.AddListener(QuitGame);
    }


    void Update()
    {
        if (gameStarted)
        {
            Vector3 targetPos = mainCam.position;
            Vector3 targetRot = mainCam.eulerAngles;

            if (startCam.eulerAngles.y > 180) targetRot.y += 360;

            startCam.position = Vector3.SmoothDamp(startCam.position, targetPos, ref velocityPos, smoothTime * Time.deltaTime);
            startCam.eulerAngles = Vector3.SmoothDamp(startCam.eulerAngles, targetRot, ref velocityRot, smoothTime * Time.deltaTime);

            float posDiff = startCam.position.sqrMagnitude - mainCam.position.sqrMagnitude;
            if (posDiff < 0) posDiff *= -1;

            if (posDiff < 1)
            {
                player.SetActive(true);
                transform.gameObject.SetActive(false);
            }
        }
    }


    void StartGame()
    {
        gameStarted = true;
        _canvas.SetActive(false);
        transform.Find("CameraPivot").GetComponent<Animation>().enabled = false;
    }

    void QuitGame()
    {
        Application.Quit();
        // UnityEditor.EditorApplication.isPlaying = false;
    }
}
