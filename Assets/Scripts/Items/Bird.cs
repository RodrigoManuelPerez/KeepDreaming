using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public enum state { waitin, inside };
    private Animator anim;

    state s = state.waitin;

    float limit;
    int loops = 3;

    private AudioSource juke;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        juke = GetComponent<AudioSource>();
    }

    private void Start()
    {
        limit = Random.Range(15.0f, 20.0f);
        StartCoroutine(birdAnim());
    }
    
    IEnumerator birdAnim()
    {
        float timer = 0.0f;
        int loopsCont = 0;
        
        while (true)
        {
            timer += Time.deltaTime;

            if (timer >= limit)
            {
                timer = 0.0f;

                switch (s)
                {
                    case state.waitin:
                        anim.SetTrigger("In");
                        juke.volume = 1.0f;
                        loops = Random.Range(3, 20);
                        loopsCont = 0;
                        limit = Random.Range(0.75f, 1.75f);
                        s = state.inside;
                        break;
                    case state.inside:
                        
                        loopsCont++;
                        
                        if(loopsCont >= loops)
                        {
                            anim.SetTrigger("Out");
                            juke.volume = 0.0f;
                            limit = Random.Range(15.0f, 20.0f);
                            s = state.waitin;
                            loopsCont = 0;
                        }
                        else
                        {
                            limit = Random.Range(0.75f, 1.75f);
                            anim.SetTrigger("Loop");
                        }
                        
                        break;
                }                
            }

            yield return 0;
        }
    }

}
