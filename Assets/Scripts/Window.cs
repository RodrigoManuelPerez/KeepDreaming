using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Window : MonoBehaviour
{
    public Slider slider;
    public float speed = 5.0f;
    
    [SerializeField]
    private float minLimit = 0.25f;
    [SerializeField]
    private float maxLimit = 0.75f;

    private bool open = false;

    public TemperatureSystem temperature;

    private void Awake()
    {
        if (slider == null)
            slider = GetComponent<Slider>();
    }

    public bool IsOpen()
    {
        return open;
    }

    public void OnEndDrag()
    {
        if (open)
        {
            if (slider.value <= minLimit)
            {
                open = false;
                StartCoroutine(closeWindow());
                temperature.disableOutline();
            }
            else
            {
                StartCoroutine(openWindow());
            }
        }
        else
        {
            if (slider.value >= maxLimit)
            {
                open = true;
                StartCoroutine(openWindow());
                temperature.disableOutline();
            }
            else
            {
                StartCoroutine(closeWindow());
            }
        }        
    }
    
    IEnumerator closeWindow()
    {
        while (slider.value > 0.0f)
        {
            slider.value -= Time.deltaTime * speed;
            yield return 0;
        }
    }

    IEnumerator openWindow()
    {
        while (slider.value < 1.0f)
        {
            slider.value += Time.deltaTime * speed;
            yield return 0;
        }
    }

}
