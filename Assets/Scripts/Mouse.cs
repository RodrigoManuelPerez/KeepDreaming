using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    [SerializeField] private Sprite hand = null;
    [SerializeField] private Sprite pointer = null;
    [SerializeField] private SpriteRenderer rend = null;

    [SerializeField] private Fade mouseFade = null;

    private void Start()
    {
        mouseFadeIn();
    }    

    public void mouseFadeIn()
    {
        mouseFade.FadeIn(0.5f);
    }

    public void mouseFadeOut()
    {
        mouseFade.FadeOut();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pz.z = 0;
        transform.position = pz;
    }

    // ToDo: On trigger enter, on enter/exit de los sliders
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.GetComponent<Item>() != null)
        {
            if (collision.GetComponent<Item>().IsActive())
            {
                if (collision.gameObject.tag == "Clock")
                    enter();
                else
                    OnEnter();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Item>() != null)
        {    
            exit();                        
        }
    }

    public void OnEnter()
    {
        if (GameManager.instance.GetGameActive())
        {
            enter();
        }
    }

    private void enter()
    {
        rend.sprite = hand;
    }

    private void exit()
    {
        rend.sprite = pointer;
    }
}
