using UnityEngine;

public class timeUp10 : MonoBehaviour
{
    [SerializeField] private GameObject gameBossObject;

    [SerializeField] private GameBoss gameBoss;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameBossObject = GameObject.Find("GameBoss");
        gameBoss = gameBossObject.GetComponent<GameBoss>();
    }

    public void addTimeUp10()
    {
        if (gameBoss.playerMoney >= 25)
        {
            gameBoss.timeUp10++;
            gameBoss.playerMoney -= 25;
        }
    }
}
