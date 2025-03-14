using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    [SerializeField] private GameObject gameBossObject;

    [SerializeField] private GameBoss gameBoss;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameBossObject = GameObject.Find("GameBoss");
        gameBoss = gameBossObject.GetComponent<GameBoss>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            gameBoss.playerHealthMain = 10;
            gameBoss.playerMoney = 0;
            gameBoss.playerMaxHealth = 10;
            gameBoss.globalDifficulty = 1;

            SceneManager.LoadScene("LevelSelect");
        }
    }
}
