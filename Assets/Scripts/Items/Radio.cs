using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : TouchableItem
{
    private bool musicOn = true;
    public Sprite radioOn;
    public Sprite radioOff;
    private SpriteRenderer rend;

    public AudioClip lofiLoop;

    private bool firstPart = true;

    private float volume = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        
        if (radioOn != null)
            rend.sprite = radioOn;

        SetActive();
    }

    // Update is called once per frame
    void Update()
    {
        if (firstPart)
        {
            if (!jukebox.isPlaying) // La primera que se acaba la musica es porque hemos acabado la intro
            {                
                jukebox.clip = lofiLoop;
                jukebox.loop = true;
                jukebox.Play();
                firstPart = false;
            }
        }
    }

    private void OnMouseUpAsButton()
    {
        if (GameManager.instance.GetGameActive())
        {
            musicOn = !musicOn;

            if (musicOn)
            {
                if (radioOn != null)
                    rend.sprite = radioOn;

                Activate();
            }
            else
            {
                if (radioOff != null)
                    rend.sprite = radioOff;

                Deactivate();
            }
        }
    }

    protected void Activate()
    {
        if (jukebox != null)
            jukebox.volume = volume;

        if (anim != null)
            anim.SetTrigger("Activate");
    }

    protected void Deactivate()
    {
        outLine.enabled = false;

        if (jukebox != null)
            jukebox.volume = 0;

        if (anim != null)
            anim.SetTrigger("Deactivate");
    }
}
