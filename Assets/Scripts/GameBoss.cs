using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GameBoss : MonoBehaviour
{
    public static GameBoss Instance;
    
    [SerializeField] private GameObject playerHealthObject;
    [SerializeField] private GameObject timerObject;
    [SerializeField] private Timer timer;
    public PlayerHealth playerHealthScript;
    public int width;
    public int height;
    public int mineCount;
    public float globalDifficulty;
    public int timeLimit;
    public int initialHealth = 10;
    public int playerMaxHealth = 10;
    public int playerHealthMain;
    public int playerMoney;
    public int timeUp10;
    public int timeUp30;
    
    void Awake()
    {
        //Singleton pattern
        //When creating an instance, check if one already exists,
        //and if the existing one is the one that is trying to be created
        if (Instance != null && Instance != this)
        {
            //If a different instance already exists,
            //destroy the instance that is currently created
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        playerHealthScript.SetMaxHealth(playerMaxHealth);
        
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        globalDifficulty = 1f;
        playerHealthMain = initialHealth;
        playerHealthObject = GameObject.Find("PlayerHealthBar");
        playerHealthScript = playerHealthObject.GetComponent<PlayerHealth>();
        // timerObject = GameObject.Find("Canvas");
        // timer = timerObject.GetComponent<Timer>();
    }


    // private void timeUp10()
    // {
    //     timer.elapsedTime += 10;
    // }
    //
    // private void timeUp30()
    // {
    //     timer.elapsedTime += 30;
    // }
    // Update is called once per frame
    void Update()
    {
        if (timeUp10 < 0)
        {
            timeUp10 = 0;
        }

        if (timeUp30 < 0)
        {
            timeUp30 = 0;
        }
        
    }
}
