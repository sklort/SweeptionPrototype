using TMPro;
using UnityEngine;

public class LevelsBeat : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI message;
    private GameObject gameBossObject;
    private GameBoss gameBoss;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameBossObject = GameObject.Find("GameBoss");
        gameBoss = gameBossObject.GetComponent<GameBoss>();


        if (gameBoss.levelsBeat == 1)
        {
            message.text = string.Format("{0} {1} {2}", "You beat", gameBoss.levelsBeat, "level!");
        }

        if (gameBoss.levelsBeat > 1 || gameBoss.levelsBeat == 0)
        {
            message.text = string.Format("{0} {1} {2}", "You beat", gameBoss.levelsBeat, "levels!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
