using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider;
    [SerializeField] private GameObject gameBossObject;
    [SerializeField] GameBoss gameBoss;
    [SerializeField] GameLogic gameLogic;
    public int healthbarHealthValue;
    private AudioSource _source;
    private int x;

    [SerializeField] private AudioClip _loseHealth;
    
    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
    }
    
    public void SetHealth(int health)
    {
        healthSlider.value = health;
       healthbarHealthValue = health;
       gameBoss.playerHealthMain = healthbarHealthValue;
       audioCheck();
       
    }

    private void audioCheck()
    {
        if (x > healthbarHealthValue)
        {
            hit();
        }
    }

    private void hit()
    {
        _source.clip = _loseHealth;
        _source.Play();
        x = healthbarHealthValue;
    }
    public void Start()
    {
        gameBossObject = GameObject.Find("GameBoss");
        gameBoss = gameBossObject.GetComponent<GameBoss>();
        healthbarHealthValue = gameBoss.playerHealthMain;
        healthSlider.maxValue = gameBoss.playerMaxHealth;
        healthSlider.value = healthbarHealthValue;
        SetHealth(healthbarHealthValue);
        
        x = healthbarHealthValue;
        _source = GetComponent<AudioSource>();
    }
}
