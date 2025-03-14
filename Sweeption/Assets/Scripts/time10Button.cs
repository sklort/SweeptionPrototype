using UnityEngine;

public class time10Button : MonoBehaviour
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


    public void add10Game()
    {
        if (gameBoss.timeUp10 >= 1)
        {
            timer.elapsedTime -= 10;
            gameBoss.timeUp10 -= 1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
