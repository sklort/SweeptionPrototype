using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TutRevealMine : MonoBehaviour
{
    [SerializeField] private Image revealed;
    [SerializeField] private Image notRevealed;

    private AudioSource _source;
    [SerializeField] AudioClip _explode;
    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }
    
    public void onClickMine()
    {
        _source.clip = _explode;
        _source.Play();
        
        revealed.GameObject().SetActive(true);
        notRevealed.GameObject().SetActive(false);
        
    }
}
