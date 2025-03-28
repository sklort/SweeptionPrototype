using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameBoss gameBoss;
    [SerializeField] private GameObject gameBossObject;
    [SerializeField] private PlayerHealth playerHealthScript;
    [SerializeField] private GameObject coRoutines;
    public float elapsedTime;
    private int timeLimit;
    private int playerHealth;
    private bool overTime = false;
    [SerializeField] Camera camera;
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _tickOn;
    private bool tickSound = false;
    public bool tickOn;
    private Color firstColor;


    void Start()
    {
        gameBossObject = GameObject.Find("GameBoss");
        gameBoss = gameBossObject.GetComponent<GameBoss>();
        _source = GetComponent<AudioSource>();
        timeLimit = gameBoss.timeLimit;
        playerHealth = gameBoss.playerHealthMain;
        elapsedTime = timeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        if (elapsedTime > 0.1)
        {
            tickSound = false;
            tickOn = false;
            elapsedTime -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            camera.backgroundColor = new Color(.71f, .586f, .565f);;
            coRoutines.GetComponent<NewMonoBehaviourScript>().enabled = true;
        }

        if (elapsedTime <= 0.1)
        {
            tickOn = true;
            camera.backgroundColor = Color.red;

            if (tickSound == false)  
            {
                _source.clip = _tickOn;
                _source.Play();
                tickSound = true;
            }
        }
        
    }
    
    
    public void stopTick()
    {
        if (tickOn == false)
        {
            StopAllCoroutines();
        }
    }




}
