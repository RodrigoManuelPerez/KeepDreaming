using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchableItem : Item
{
    [SerializeField]
    private int touchesNeeded = 1;
    private int touchCount = 0;

    public GameObject[] hearts;

    protected void Activate()
    {
        base.Activate();

        if (first)
        {
            outLine.enabled = true;
            first = false;
        }

    }

    protected void OnMouseUpAsButton()
    {
        if (IsActive())
        {
            touchCount++;                        

            if (touchCount >= touchesNeeded)
            {
                touchCount = 0;

                if (outLine.enabled)                
                    outLine.enabled = false;

                if (tag == "Dog")
                {
                    GameObject h = Instantiate(hearts[1]);
                    h.transform.position = transform.position;
                }

                Deactivate();
            }
            else
            {
                if (tag == "Dog")
                {
                    GameObject h = Instantiate(hearts[0]);                    
                }
            }
        }
    }
}
