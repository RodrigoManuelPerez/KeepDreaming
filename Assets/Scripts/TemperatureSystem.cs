using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureSystem : MonoBehaviour
{
    public Window window;
    public float discomfortStep = 0.75f;

    private float Incremento = 0.125f;

    private float temperature = 0.0f;
    private float maxTemperature = 5.0f;
    private int TemperatureThreshold = 2;

    public SpriteRenderer[] heatMarkers;
    public SpriteRenderer[] coldMarkers;

    private AudioSource juke;
    private int activeItems = 0;

    [SerializeField]
    protected Image outLine;

    private bool first = true;    

    private void Awake()
    {
        juke = GetComponent<AudioSource>();
    }

    public void UpdateTemperature()
    {
        if (window.IsOpen())        
            temperature -= Incremento;        
        else        
            temperature += Incremento;        
        
        if(Mathf.Abs(temperature) > maxTemperature)
        {
            if (temperature < 0)
                temperature = -maxTemperature;
            else
                temperature = maxTemperature;
        }

        renderTemperature();
    }

    public void disableOutline()
    {
        if(outLine.enabled)
            outLine.enabled = false;
    }

    private void renderTemperature()
    {
        int T = (int)temperature;

        if (T != activeItems)
        {
            if (first)
            {
                outLine.enabled = true;
                first = false;
            }

            juke.Play();
            activeItems = T;
        }

        if (T > 0)
        {
            for (int i = 0; i < 5; i++)          
                coldMarkers[i].enabled = false;            
            for (int i = 0; i < T; i++)            
                heatMarkers[i].enabled = true;
            for (int i = T; i < 5; i++)
                heatMarkers[i].enabled = false;
        }
        else if (T < 0)
        {
            T = -T;
            for (int i = 0; i < 5; i++)            
                heatMarkers[i].enabled = false;            
            for (int i = 0; i < T; i++)            
                coldMarkers[i].enabled = true;
            for (int i = T; i < 5; i++)
                coldMarkers[i].enabled = false;
        }
        else
        {
            for (int i = 0; i < 5; i++)
                heatMarkers[i].enabled = false;

            for (int i = 0; i < 5; i++)
                coldMarkers[i].enabled = false;
        }
    }

    public float GetDiscomfortStep()
    {
        int T = (int)temperature;

        if (Mathf.Abs(T) > TemperatureThreshold)        
            return discomfortStep * (Mathf.Abs(T) - TemperatureThreshold);        
        else
            return 0.0f;
    }
}
