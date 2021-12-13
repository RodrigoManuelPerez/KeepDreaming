using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovilTecho : MonoBehaviour
{
    public Window w;    
    private bool lastValue = false;


    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if((w.IsOpen() && !lastValue) || (!w.IsOpen() && lastValue))
        {
            lastValue = !lastValue;
            if (lastValue)
                anim.SetTrigger("In");
            else
                anim.SetTrigger("Out");
        }
    }
}
