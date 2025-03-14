using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    private TextMeshProUGUI _message;
    private bool _inCoroutine = false;

    private Coroutine _coroutine;
    private void Start()
    {
        _message = GetComponent <TextMeshProUGUI>();
        
    }

    void Update()
    {
        if (!_inCoroutine)
        {
            _coroutine = StartCoroutine(Flicker());
            if (Input.GetKey(KeyCode.Period))
            {
                StopAllCoroutines();
                _inCoroutine = false;
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            float random = Random.value;
        }
    }

    IEnumerator Flicker()
    {
        
        _message.enabled = !_message.enabled;
        
        _inCoroutine = true;

        //Interrupt execution for one frame only
        //yield return null;

        yield return new WaitForSeconds(.65f);
        _inCoroutine = false;
    }
}
