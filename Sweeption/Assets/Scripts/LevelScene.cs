using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScene : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _win;
    
    private void Start()
    {
        _source = GetComponent<AudioSource>();
        _source.clip = _win;
        _source.Play();
    }
    
    public void levelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
