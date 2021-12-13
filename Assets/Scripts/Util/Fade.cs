using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public float speed = 1.0f;
    SpriteRenderer rend;

    private bool fadedOut = false;

    private float fadeLimit;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    public void FadeIn(float s = 0.0f)
    {
        if (s != 0.0f)
            speed = s;

        StartCoroutine(fadeIn());
    }

    public void FadeInLimit(float limit, float s = 0.0f)
    {
        fadeLimit = limit;
        if (s != 0.0f)
            speed = s;

        StartCoroutine(fadeInLimit());
    }

    public void FadeOut(bool v = false)
    {
        fadedOut = v;
        StartCoroutine(fadeOut());
    }

    IEnumerator fadeIn()
    {
        Color c = rend.color;

        do
        {
            c = rend.color;

            c.a += Time.deltaTime * speed;

            if (c.a > 1.0f)
                c.a = 1.0f;

            rend.color = c;

            yield return 0;
        }
        while (c.a < 1.0f);
        
    }

    IEnumerator fadeInLimit()
    {
        Color c = rend.color;

        do
        {
            c = rend.color;

            c.a += Time.deltaTime * speed;

            if (c.a > fadeLimit)
                c.a = fadeLimit;

            rend.color = c;

            yield return 0;
        }
        while (c.a < fadeLimit);
        
    }

    IEnumerator fadeOut()
    {
        Color c = rend.color;

        do
        {
            c = rend.color;

            c.a -= Time.deltaTime * speed;

            if (c.a < 0.0f)
                c.a = 0.0f;

            rend.color = c;

            yield return 0;
        }
        while (c.a > 0.0f);

        if (fadedOut)
        {
            fadedOut = false;
            GameManager.instance.SetFadedOut();
        }
    }
}
