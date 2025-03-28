using UnityEngine;

public class How2Grid : MonoBehaviour
{

    private GameObject gameBossObject;

    private GameBoss gameBoss;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameBossObject = GameObject.Find("GameBoss");
        gameBoss = gameBossObject.GetComponent<GameBoss>();
        gameBoss.width = 6;
        gameBoss.height = 6;
        gameBoss.mineCount = 4;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
