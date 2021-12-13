using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public bool big = false;

    public float upSpeed = 1.0f;
    public float hSpeed = 1.0f;
    public float ampRed = 3.0f;

    private float timer = 0;

    private Vector3 originalPos;

    private void Awake()
    {
        originalPos = transform.position;

        if (!big)
            originalPos -= new Vector3(0.75f + Random.Range(-0.125f, 0.125f), 0.0f, 0.0f);
    }

    private void Start()
    {
        timer = Random.Range(0.0f, 6.28319f);

        //if (Random.Range(0,2) == 0)
          //  transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        Invoke("fadeOut", Random.Range(0.5f, 1.25f));
        Destroy(this.gameObject, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * hSpeed;

        if (!big)
        {
            originalPos += new Vector3(0.0f, 1f * Time.deltaTime * upSpeed / ampRed, 0.0f);
            transform.position = originalPos + new Vector3(Mathf.Sin(timer)/ampRed, 0, 0);
        }
        else
        {
            transform.position += new Vector3(0, 1f, 0) * Time.deltaTime * upSpeed;
        }

    }

    private void fadeOut()
    {
        GetComponent<Fade>().FadeOut();
    }
}
