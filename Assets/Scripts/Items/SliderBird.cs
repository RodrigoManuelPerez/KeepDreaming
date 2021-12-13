using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBird : Item
{    
    public Slider slider;
    public Animator handleAnim;
    public float animTime = 0.5f;   // Tiempo de la animacion de las semillas o la inclinación 
    
    public float limit = 0.8f;
    public float fixedPos;
    public float animTimeThird = 1.5f;  // Tiempo de la animacion de lo que muestra que tienes que hacer
    public Animator thirdAnim;   // Para cada elemento que pueda activar este tipo de slider
    
    public float speed = 5.0f;


    private void Start()
    {
        if (slider == null)
            slider = GetComponent<Slider>();        
    }

    protected void Activate()
    {
        base.Activate();

        if(thirdAnim != null)
            thirdAnim.SetTrigger("Activate");

        // ToDo: animTimeThird es el tiempo de animación del pajaro en llegar
        Invoke("activeSlider", animTimeThird);
    }

    private void activeSlider()
    {
        slider.interactable = true;
    }

    protected void Deactivate()
    {
        base.Deactivate();
        slider.interactable = false;
    }

    public void OnEndDrag()
    {
        if (slider.value >= limit)
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

    private void original()
    {
        StartCoroutine(backToOriginal());
    }

    IEnumerator backToOriginal()
    {
        while (slider.value > 0.0f)
        {
            slider.value -= Time.deltaTime * speed;
            yield return 0;
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

        if (thirdAnim != null)
            thirdAnim.SetTrigger("Activate");    // Animacion del pajaro comiendo y siguiente estado se va

        handleAnim.SetTrigger("Activate");      // Animacion de la semillero echando, dejando de echar

        Invoke("original", animTime);           // Lo devuelve al sitio original
    }
}
