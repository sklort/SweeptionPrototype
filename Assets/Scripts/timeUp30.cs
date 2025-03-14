using UnityEngine;

public class timeUp30 : MonoBehaviour
{
    [SerializeField] private GameObject gameBossObject;

    [SerializeField] private GameBoss gameBoss;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameBossObject = GameObject.Find("GameBoss");
        gameBoss = gameBossObject.GetComponent<GameBoss>();
    }

    public void addTimeUp30()
    {
        if (gameBoss.playerMoney >= 50)
        {
            gameBoss.timeUp30++;
            gameBoss.playerMoney -= 50;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
