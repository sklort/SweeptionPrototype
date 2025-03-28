using UnityEngine;

public class time30Button : MonoBehaviour
{
    [SerializeField] private GameObject gameBossObject;

    [SerializeField] private GameBoss gameBoss;
    
    [SerializeField] private Timer timer;

    [SerializeField] private GameObject thisObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameBossObject = GameObject.Find("GameBoss");
        gameBoss = gameBossObject.GetComponent<GameBoss>();
        
        if (gameBoss.timeUp30 > 0)
        {
            thisObject.SetActive(true);
        }
    }
   
    public void add30Game()
    {
        if (gameBoss.timeUp30 >= 1)
        {
            timer.elapsedTime += 30;
            gameBoss.timeUp30 -= 1;
        }
    }

    private void Update()
    {
        if (gameBoss.timeUp30 <= 0)
        {
            thisObject.SetActive(false);
        }
    }
}
