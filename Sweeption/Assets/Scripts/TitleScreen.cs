using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    private bool audioStarted;
    private AudioSource _source;

    [SerializeField] private AudioClip _start;
    private GameBoss gameBoss;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _source = GetComponent<AudioSource>();
        if (audioStarted == false)
        {
            _source.clip = _start;
            _source.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            SceneManager.LoadScene("LevelSelect");
        }
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SceneManager.LoadScene("Tutorial");
        }


    }
}
