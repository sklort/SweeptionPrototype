using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    [SerializeField] private GameBoss gameBoss;
    [SerializeField] private GameObject gameBossObject;
    [SerializeField] private PlayerHealth playerHealthScript;

    [SerializeField] private bool init = false;

    private bool inCoroutine = false;

    private Coroutine _coroutine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    private void Update()
    {
        if (init == false)
        {
            gameBossObject = GameObject.Find("GameBoss");
            gameBoss = gameBossObject.GetComponent<GameBoss>();
            init = true;
        }
        
        if (!inCoroutine)
        {
            _coroutine = StartCoroutine(startTick());
        }
    }


    public void loseHealth()
    {
        gameBoss.playerHealthMain -= 1;
        int playerHealth = gameBoss.playerHealthMain;
        playerHealthScript.SetHealth(playerHealth);
    }
    IEnumerator startTick()
    {
            inCoroutine = true;
            if (inCoroutine)
            {
                loseHealth();
                Debug.Log("yup");
                yield return new WaitForSeconds(10);
                inCoroutine = false;
                Debug.Log(inCoroutine);
            }
    } 
    
}
