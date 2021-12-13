using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderItemComplex : Item
{
    public Slider slider;
    public Animator handleAnim;
    public float animTime = 0.5f;   // Tiempo de la animacion de las semillas o la inclinación 

    [Tooltip("Añadir de 2 en 2 para añadir pares a una lista")]
    public float[] limits;
    public float[] fixedPos;
    public Animator[] animators;   // Para cada elemento que pueda activar este tipo de slider

    List<KeyValuePair<float, float>> limitsList;

    private Animator targetAnim;

    public float speed = 5.0f;

    public Image outLineImage;

    private void Start()
    {
        if (slider == null)
            slider = GetComponent<Slider>();

        limitsList = new List<KeyValuePair<float, float>>();

        int i = 0;
        while (i < limits.Length)
        {
            limitsList.Add(new KeyValuePair<float, float>(limits[i], limits[i + 1]));
            i += 2;
        }
    }

    private void Update()
    {
        if (first && GameManager.instance.GetGameActive())
        {
            outLineImage.enabled = true;
            first = false;
        }
    }

    protected void Activate()
    {
        base.Activate();
        slider.interactable = true;

        if (first)
        {
            outLineImage.enabled = true;
            first = false;
        }
    }

    protected void Deactivate()
    {
        base.Deactivate();
        slider.interactable = false;
        
        outLineImage.enabled = false;
    }

    public void OnEndDrag()
    {
        bool animFound = false;

        for (int i = 0; i < limitsList.Count; i++)
        {
            float first = limitsList[i].Key;
            float second = limitsList[i].Value;

            if (slider.value >= first && slider.value <= second)
            {
                animFound = true;
                StartCoroutine(setOnPosition(i));
                Deactivate();                
                break;
            }
        }

        if (!animFound)
            StartCoroutine(backToOriginal());
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

    IEnumerator setOnPosition(int idx)
    {
        float target = fixedPos[idx];

        if (slider.value < target)
        {
            while (slider.value < target)
            {
                slider.value += Time.deltaTime;
                yield return 0;
            }

            slider.value = target;
        }
        else if(slider.value > target)
        {
            while (slider.value > target)
            {
                slider.value -= Time.deltaTime;
                yield return 0;
            }

            slider.value = target;
        }

        if (idx < animators.Length)
        {
            targetAnim = animators[idx];
            Invoke("delayAnim", animTime/3.0f);
        }

        handleAnim.SetTrigger("Water");      // Animacion de la regadera/semillero

        Invoke("original", animTime);        
    }

    private void delayAnim()
    {
        targetAnim.SetTrigger("Water");    // Animacion de la planta/pajaro
        if(this.gameObject.tag == "Sprinkler")
        {
            handleAnim.gameObject.GetComponent<AudioSource>().Play();
        }
    }

}
