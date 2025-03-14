using UnityEngine;

public class MaxHPUp : MonoBehaviour
{
    [SerializeField] private GameObject gameBossObject;

    [SerializeField] private GameBoss gameBoss;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameBossObject = GameObject.Find("GameBoss");
        gameBoss = gameBossObject.GetComponent<GameBoss>();
    }

    public void addMaxHP()
    {
        if (gameBoss.playerMoney >= 50)
        {
            gameBoss.playerHealthMain += 5;
            gameBoss.playerMaxHealth += 5;
            gameBoss.playerMoney -= 50;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
