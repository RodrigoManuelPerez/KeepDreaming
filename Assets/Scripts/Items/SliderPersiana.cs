using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderPersiana : Item  // Persiana
{
    public Slider slider;
    public float animTime = 0.5f;   // Tiempo de la animacion de las semillas o la inclinación 

    public float limit = 0.8f;
    public float fixedPos;
    
    public float speed = 5.0f;

    public Image outLineImage;

    private void Start()
    {
        if (slider == null)
            slider = GetComponent<Slider>();
    }

    protected void Activate()
    {
        AnimOn();
        base.Activate();
        StartCoroutine(backToOriginal());

        if (first)
        {
            outLineImage.enabled = true;
            first = false;
        }

        // ToDo: animTimeThird es el tiempo que tarda en ponerse en la posición original subida        
        anim.SetTrigger("Activate");
        Invoke("AnimOff", 0.5f);
    }    

    private void AnimOn()
    {
        anim.enabled = true;
    }

    private void AnimOff()
    {
        anim.enabled = false;
    }

    protected void Deactivate()
    {
        base.Deactivate();
        slider.interactable = false;

        outLineImage.enabled = false;
    }

    public void OnEndDrag()
    {
        if (slider.value <= limit)
        {
            StartCoroutine(setOnPosition());            
            Deactivate();
        }
        else
        {
            StartCoroutine(backToOriginal());
        }
    }

    public void OnSelect()
    {
        // Anular la rutina que devuelve a la posición        
        StopCoroutine(backToOriginal());
    }

    public void FixPos()
    {
        if (slider.value > 0.8f)
            slider.value = 0.8f;
    }


    private void original()
    {
        StartCoroutine(backToOriginal());
    }

    IEnumerator backToOriginal()
    {
        if (slider.value < 0.8f)
        {
            while (slider.value < 0.8f)
            {
                slider.value += Time.deltaTime * speed;
                yield return 0;
            }
        }
        else
        {
            while (slider.value > 0.8f)
            {
                slider.value -= Time.deltaTime * speed;
                yield return 0;
            }
        }
    }

    IEnumerator setOnPosition()
    {
        float target = fixedPos;

        while (slider.value < target)
        {
            slider.value += Time.deltaTime;
            yield return 0;
        }

        slider.value = target;        
    }
}
