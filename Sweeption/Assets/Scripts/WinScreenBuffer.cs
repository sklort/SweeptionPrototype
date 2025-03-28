using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreenBuffer : MonoBehaviour
{
    [SerializeField] private GameObject maxHP;
    [SerializeField] private GameObject money;
    [SerializeField] private GameObject time30;
    [SerializeField] private GameObject time10;
    [SerializeField] private GameObject nextLevel;

    [SerializeField] private GameObject gameBossObject;
    [SerializeField] private GameBoss gameBoss;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameBossObject = GameObject.Find("GameBoss");
        gameBoss = gameBossObject.GetComponent<GameBoss>();
        
        maxHP.SetActive(false);
        money.SetActive(false);
        time30.SetActive(false);
        time10.SetActive(false);
        nextLevel.SetActive(false);

        gameBoss.levelsBeat++;
        
        StartCoroutine(waitSecs());
    }

    IEnumerator waitSecs()
    {
        yield return new WaitForSeconds(1);
        doneWait();
    }

    private void doneWait()
    {
        maxHP.SetActive(true);
        money.SetActive(true);
        time30.SetActive(true);
        time10.SetActive(true);
        nextLevel.SetActive(true);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
