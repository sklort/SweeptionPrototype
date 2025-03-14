using UnityEngine;
using UnityEngine.SceneManagement;

public class EasyLevel : MonoBehaviour
{
    [SerializeField] private GameObject gameBossObject;   
    [SerializeField] private GameBoss gameBoss;
    public void generateEasy()
    {
        gameBoss.globalDifficulty += 0.3f;
        float difficulty = gameBoss.globalDifficulty;
        float levelTimeLimit = Random.Range(90, 240);
        float multLimit = levelTimeLimit / difficulty;
        int timeLimit = Mathf.CeilToInt(multLimit);

        float randWidth = Random.Range(4, 10);
        float floatWidth = randWidth * difficulty;
        int width = Mathf.CeilToInt(floatWidth);

        int height = width;

        float randMine = Random.Range(4, 8);
        float floatMine = randMine * difficulty;
        int mineCount = Mathf.CeilToInt(floatMine);

        gameBoss.timeLimit = timeLimit;
        gameBoss.width = width;
        gameBoss.height = height;
        gameBoss.mineCount = mineCount;

        float moneyFloat = gameBoss.playerMoney + (25);
        int money = Mathf.FloorToInt(moneyFloat);
        gameBoss.playerMoney += money;
        SceneManager.LoadScene("Minesweeper");
    }

    private void Start()
    {
        gameBossObject = GameObject.Find("GameBoss");
        gameBoss = gameBossObject.GetComponent<GameBoss>();
    }
}
