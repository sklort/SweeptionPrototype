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
        float levelTimeLimit = Random.Range(60, 180);
        float multLimit = levelTimeLimit / difficulty;
        int timeLimit = Mathf.CeilToInt(multLimit);

        float randWidth = Random.Range(6, 10);
        float floatWidth = randWidth * difficulty;
        int width = Mathf.CeilToInt(floatWidth);
        width = Mathf.Clamp(width, 0, 40);

        int height = width;

        float randMine = Random.Range(4, 8);
        float floatMine = randMine * (difficulty*difficulty);
        int mineCount = Mathf.CeilToInt(floatMine);

        gameBoss.timeLimit = timeLimit;
        gameBoss.width = width;
        gameBoss.height = height;
        gameBoss.mineCount = mineCount;
        gameBoss.playerMoney += 25;
        SceneManager.LoadScene("Minesweeper");
        
    }

    private void Start()
    {
        gameBossObject = GameObject.Find("GameBoss");
        gameBoss = gameBossObject.GetComponent<GameBoss>();
    }
}
