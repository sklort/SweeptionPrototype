using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;

public class TutReveal : MonoBehaviour
{

    [SerializeField] private Image revealed;
    [SerializeField] private Image notRevealed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
       
        
    }

    private void OnMouseEnter()
    {
    }

    public void OnClick()
    {
        revealed.GameObject().SetActive(true);
        notRevealed.GameObject().SetActive(false);
    }
}


