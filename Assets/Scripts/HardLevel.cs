using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class HardLevel : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _level;
    [SerializeField] private GameObject gameBossObject;   
    [SerializeField] private GameBoss gameBoss;
    public void generateHard()
    {
        gameBoss.globalDifficulty += 0.1f;
        float difficulty = gameBoss.globalDifficulty;
        float levelTimeLimit = Random.Range(60, 120);
        float multLimit = levelTimeLimit / difficulty;
        int timeLimit = Mathf.CeilToInt(multLimit);

        float randWidth = Random.Range(8, 16);
        float floatWidth = randWidth * difficulty;
        int width = Mathf.CeilToInt(floatWidth);

        int height = width;

        float randMine = Random.Range(12, 24);
        float floatMine = randMine * difficulty;
        int mineCount = Mathf.CeilToInt(floatMine);

        gameBoss.timeLimit = timeLimit;
        gameBoss.width = width;
        gameBoss.height = height;
        gameBoss.mineCount = mineCount;

        float moneyFloat = gameBoss.playerMoney + 50 * difficulty;
        int money = Mathf.FloorToInt(moneyFloat);
        gameBoss.playerMoney += money;
        SceneManager.LoadScene("Minesweeper");
    }

    private void Start()
    {
        _source = GetComponent<AudioSource>();
        _source.clip = _level;
        _source.Play();
        gameBossObject = GameObject.Find("GameBoss");
        gameBoss = gameBossObject.GetComponent<GameBoss>();
    }
}
