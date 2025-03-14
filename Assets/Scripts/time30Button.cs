using UnityEngine;

public class time30Button : MonoBehaviour
{
    [SerializeField] private GameObject gameBossObject;

    [SerializeField] private GameBoss gameBoss;
    
    [SerializeField] private Timer timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameBossObject = GameObject.Find("GameBoss");
        gameBoss = gameBossObject.GetComponent<GameBoss>();
    }
   
    public void add30Game()
    {
        if (gameBoss.timeUp30 >= 1)
        {
            timer.elapsedTime -= 30;
            gameBoss.timeUp30 -= 1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
