using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : TouchableItem
{
    // Private
    private int hours = 7;
    private int minutes = 30;
    private bool flashing = false;
    [SerializeField]
    private float timeLimit = 0.6f;

    // Public
    public SpriteRenderer[] clockDigits;
    public SpriteRenderer dots;
    public Sprite[] digits;

    public void startGame()
    {
        if (jukebox != null)
            jukebox.Play();

        StartCoroutine(flashStart());
    }

    public void IncreaseMinute()
    {
        minutes++;
        if (minutes == 60)
        {
            minutes = 0;
            hours++;
        }

        if (!flashing)
            renderTime();
    }

    public void HideNumbers()
    {
        for (int i = 0; i < clockDigits.Length; i++)
        {
            clockDigits[i].sortingOrder = 0;
        }
        dots.sortingOrder = 0;
    }

    public void HideNumbersPart()
    {
        for (int i = 0; i < clockDigits.Length; i++)
        {
            clockDigits[i].sortingOrder = 8;
        }
        dots.sortingOrder = 8;
    }

    private void renderTime()
    {
        int first = hours / 10;
        int second = hours % 10;
        int third = minutes / 10;
        int fourth = minutes % 10;

        if (fourth / 5 == 1)
            fourth = 5;
        else
            fourth = 0;

        clockDigits[0].sprite = digits[first];
        clockDigits[1].sprite = digits[second];
        clockDigits[2].sprite = digits[third];
        clockDigits[3].sprite = digits[fourth];
    }

    protected void OnMouseUpAsButton()
    {
        bool a = IsActive();

        base.OnMouseUpAsButton();

        if (a)
        {
            StartCoroutine(flashPostpone());
        }
    }

    protected void Activate()
    {
        base.Activate();

        StartCoroutine(flashAlarm());
    }

    protected void Deactivate()
    {
        base.Deactivate();

        flashing = false;

        StartCoroutine(flashAlarm());
    }

    public void finalAlarm()
    {
        StartCoroutine(endAlarm());
    }

    IEnumerator flashPostpone()
    {
        flashing = true;

        int min = cooldown / 10;
        int min2 = cooldown % 10;

        clockDigits[0].sprite = digits[10];
        clockDigits[1].sprite = digits[0];     // Simbolo +
        clockDigits[2].sprite = digits[min];
        clockDigits[3].sprite = digits[min2];

        clockDigits[0].color = new Color(0, 0, 0, 0);
        clockDigits[1].color = new Color(0, 0, 0, 0);
        clockDigits[2].color = new Color(0, 0, 0, 0);
        clockDigits[3].color = new Color(0, 0, 0, 0);
        dots.color = new Color(0, 0, 0, 0);

        bool shown = false;
        int cont = 0;
        float timer = 0.0f;

        while (cont < 7)
        {
            timer += Time.deltaTime;

            if (timer >= timeLimit)
            {
                cont++;
                timer = 0.0f;
                shown = !shown;

                if (shown)
                {
                    clockDigits[0].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    clockDigits[1].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    clockDigits[2].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    clockDigits[3].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    dots.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                }
                else
                {
                    clockDigits[0].color = new Color(0, 0, 0, 0);
                    clockDigits[1].color = new Color(0, 0, 0, 0);
                    clockDigits[2].color = new Color(0, 0, 0, 0);
                    clockDigits[3].color = new Color(0, 0, 0, 0);
                    dots.color = new Color(0, 0, 0, 0);
                }
            }

            yield return 0;
        }

        clockDigits[0].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        clockDigits[1].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        clockDigits[2].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        clockDigits[3].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        dots.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        renderTime();

        flashing = false;

    }

    IEnumerator flashAlarm()
    {
        flashing = true;

        clockDigits[0].color = new Color(0, 0, 0, 0);
        clockDigits[1].color = new Color(0, 0, 0, 0);
        clockDigits[2].color = new Color(0, 0, 0, 0);
        clockDigits[3].color = new Color(0, 0, 0, 0);
        dots.color = new Color(0, 0, 0, 0);

        bool shown = false;
        float timer = 0.0f;

        while (flashing)
        {
            timer += Time.deltaTime;

            if (timer >= timeLimit)
            {
                timer = 0.0f;
                shown = !shown;

                if (shown)
                {
                    clockDigits[0].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    clockDigits[1].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    clockDigits[2].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    clockDigits[3].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    dots.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                }
                else
                {
                    clockDigits[0].color = new Color(0, 0, 0, 0);
                    clockDigits[1].color = new Color(0, 0, 0, 0);
                    clockDigits[2].color = new Color(0, 0, 0, 0);
                    clockDigits[3].color = new Color(0, 0, 0, 0);
                    dots.color = new Color(0, 0, 0, 0);
                }
            }

            yield return 0;
        }

        clockDigits[0].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        clockDigits[1].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        clockDigits[2].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        clockDigits[3].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        dots.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        renderTime();

    }

    IEnumerator flashStart()
    {
        flashing = true;

        bool shown = false;
        int cont = 0;
        float timer = 0.0f;

        while (flashing)
        {
            timer += Time.deltaTime;

            if (timer >= timeLimit)
            {
                cont++;

                if (cont == 2)
                {
                    SetActive();
                }

                timer = 0.0f;
                shown = !shown;

                if (shown)
                {
                    clockDigits[0].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    clockDigits[1].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    clockDigits[2].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    clockDigits[3].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    dots.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                }
                else
                {
                    clockDigits[0].color = new Color(0, 0, 0, 0);
                    clockDigits[1].color = new Color(0, 0, 0, 0);
                    clockDigits[2].color = new Color(0, 0, 0, 0);
                    clockDigits[3].color = new Color(0, 0, 0, 0);
                    dots.color = new Color(0, 0, 0, 0);
                }
            }

            yield return 0;
        }

        clockDigits[0].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        clockDigits[1].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        clockDigits[2].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        clockDigits[3].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        dots.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        renderTime();

        GameManager.instance.fadeOut();
        GameManager.instance.ActiveItems();

        flashing = false;
    }

    IEnumerator endAlarm()
    {
        flashing = true;

        clockDigits[0].color = new Color(0, 0, 0, 0);
        clockDigits[1].color = new Color(0, 0, 0, 0);
        clockDigits[2].color = new Color(0, 0, 0, 0);
        clockDigits[3].color = new Color(0, 0, 0, 0);
        dots.color = new Color(0, 0, 0, 0);

        bool shown = false;
        float timer = 0.0f;

        while (flashing)
        {
            timer += Time.deltaTime;

            if (timer >= timeLimit)
            {
                timer = 0.0f;
                shown = !shown;

                if (shown)
                {
                    clockDigits[0].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    clockDigits[1].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    clockDigits[2].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    clockDigits[3].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    dots.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                }
                else
                {
                    clockDigits[0].color = new Color(0, 0, 0, 0);
                    clockDigits[1].color = new Color(0, 0, 0, 0);
                    clockDigits[2].color = new Color(0, 0, 0, 0);
                    clockDigits[3].color = new Color(0, 0, 0, 0);
                    dots.color = new Color(0, 0, 0, 0);
                }
            }

            yield return 0;
        }

        clockDigits[0].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        clockDigits[1].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        clockDigits[2].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        clockDigits[3].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        dots.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        renderTime();
    }
}
