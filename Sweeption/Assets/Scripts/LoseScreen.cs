using UnityEngine;

public class LoseAudio : MonoBehaviour
{

    private AudioSource _source;

    [SerializeField] private AudioClip _lose;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _source = GetComponent<AudioSource>();
        _source.clip = _lose;
        _source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
