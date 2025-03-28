using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    private GameObject gameBossObject;
    private GameBoss gameBoss;
    [SerializeField] private GameLogic gameLogic;

    [SerializeField] private TextMeshProUGUI mineMessage;

    private int flagCount;

    public void Start()
    {
        gameBossObject = GameObject.Find("GameBoss");
        gameBoss = gameBossObject.GetComponent<GameBoss>();

    }

    private void Update()
    {
        flagCount = gameLogic.flagCount;
        mineMessage.text = string.Format("{0}", flagCount);
    }
}
