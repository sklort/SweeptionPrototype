using System;
using TMPro;
using UnityEngine;

public class MoneyCount : MonoBehaviour
{

    [SerializeField] private GameObject gameBossObject;

    [SerializeField] private GameBoss gameBoss;

    [SerializeField] private TextMeshProUGUI message;

    private int money;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameBossObject = GameObject.Find("GameBoss");
        gameBoss = gameBossObject.GetComponent<GameBoss>();

    }

    // Update is called once per frame
    void Update()
    {
        money = gameBoss.playerMoney;
        message.text = string.Format("{0} {1}", money, "$");
    }
}
