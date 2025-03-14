using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Slider healthSlider;
    [SerializeField] GameLogic gameLogic;
    public int healthbarHealthValue;
    private AudioSource _source;
    private int x;

    [SerializeField] private AudioClip _loseHealth;
    
    
    public void SetBomb(int health)
    {
        healthSlider.value = health;
       healthbarHealthValue = health;
       gameLogic.mineCount = healthbarHealthValue;
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

        healthbarHealthValue = gameLogic.currentMineCount;
        healthSlider.maxValue = gameLogic.mineCount;
        healthSlider.value = healthbarHealthValue;
        SetBomb(healthbarHealthValue);
        
        x = healthbarHealthValue;
        _source = GetComponent<AudioSource>();
    }
}
