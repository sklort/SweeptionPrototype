using UnityEngine;

public class Soundtrack : MonoBehaviour
{
    [SerializeField] private AudioSource _source;

    [SerializeField] private AudioClip _music;
    private static Soundtrack Instance;
    void Awake()
    {
        //Singleton pattern
        //When creating an instance, check if one already exists,
        //and if the existing one is the one that is trying to be created
        if (Instance != null && Instance != this)
        {
            //If a different instance already exists,
            //destroy the instance that is currently created
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        
    }

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
