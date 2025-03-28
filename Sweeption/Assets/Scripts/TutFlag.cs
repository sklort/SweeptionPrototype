using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TutFlag : MonoBehaviour
{
    [SerializeField] private Image noFlag;
    [SerializeField] private Image flagImage;
    private bool flagged;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            
            flagged = !flagged;
            if (flagged == false)
            {
                noFlag.gameObject.SetActive(true);
                flagImage.gameObject.SetActive(false);
            }

            if (flagged)
            {
                noFlag.gameObject.SetActive(false);
                flagImage.gameObject.SetActive(true);
            }
        }
    }
    
}
