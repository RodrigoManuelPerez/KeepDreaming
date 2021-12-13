using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleepometer : MonoBehaviour
{
    public SpriteRenderer[] markers;
    public Animator alarm;
    public Animator alarm2;

    public Animator person;

    int lastValue = 0;

    private bool alarmActive;

    private AudioSource juke;
    private int activeItems = 1;

    private void Awake()
    {
        juke = GetComponent<AudioSource>();
    }

    public void UpdateSleepometer(float discomfort)
    {
        int D = (int)discomfort;

        if (lastValue < 75 && D >= 75)
            person.SetTrigger("Next");
        else if (lastValue >= 75 && D < 75)
            person.SetTrigger("Prev");

        else if (lastValue < 50 && D >= 50)
            person.SetTrigger("Next");
        else if (lastValue >= 50 && D < 50)
            person.SetTrigger("Prev");

        else if (lastValue < 25 && D >= 25)
            person.SetTrigger("Next");
        else if (lastValue >= 25 && D < 25)
            person.SetTrigger("Prev");

        lastValue = D;

        D = D / 10;

        D += 1;

        if (D != activeItems)
        {
            juke.Play();
            activeItems = D;
        }

        for (int i = 0; i < D && i < 10; i++)        
            markers[i].enabled = true;        
        for (int i = D; i < 10; i++)        
            markers[i].enabled = false;

        if (D >= 8)
            alarm.SetTrigger("Active");
        if (D >= 9)
            alarm2.SetTrigger("Active");

        if (D < 8)
            alarm.SetTrigger("Deactive");
        if (D < 9)
            alarm2.SetTrigger("Deactive");        
    }
}
