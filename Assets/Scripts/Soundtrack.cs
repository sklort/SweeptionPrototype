using UnityEngine;

public class Soundtrack : MonoBehaviour
{
    [SerializeField] private AudioSource _source;

    [SerializeField] private AudioClip _music;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _source = GetComponent<AudioSource>();
        _source.clip = _music;
        _source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
