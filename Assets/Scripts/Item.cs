using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    // Public variables
    public int cooldown;
    public int cooldownRange = 0;   // +- value to add to the cooldown
    public float discomfortStep = 0.5f;
    public Animator anim;

    // Private variables
    private bool active = false;
    protected AudioSource jukebox;

    [SerializeField]
    protected SpriteRenderer outLine;

    protected bool first = true;

    private void Awake()
    {        
        if(anim == null)
            anim = GetComponent<Animator>();
        jukebox = GetComponent<AudioSource>();        
    }

    public void StartProcess(int add)
    {
        Invoke("Activate", Random.Range(cooldown - cooldownRange + add, cooldown + cooldownRange + 1 + add));
    }

    public bool IsActive()
    {
        return active;
    }

    protected void SetActive()
    {
        active = true;
    }

    public float GetDiscomfortStep()
    {
        return discomfortStep;
    }

    protected void Activate()
    {
        active = true;
        if (jukebox != null)
            jukebox.Play();

        if (anim != null)
            anim.SetTrigger("Activate");
    }

    protected void Deactivate()
    {
        active = false;
        if (jukebox != null)
            jukebox.Stop();

        Invoke("Activate", Random.Range(cooldown - cooldownRange, cooldown + cooldownRange + 1));
    }
}
