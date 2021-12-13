using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Public variables
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    [SerializeField] private Item[] items = null;
    [SerializeField] private TemperatureSystem temperature = null;
    [SerializeField] private Sleepometer sleepometer = null;
    [SerializeField] private AudioSource[] audioSources = null;
    [SerializeField] private AudioSource lofiSource = null;
    [SerializeField] private float soundSpeed = 1.0f;

    [SerializeField] private Mouse mouse = null;

    // Variable publica de la persona durmiendo

    // Numero de minutos/segundos que queremos que dure el juego
    [SerializeField] private int maxTimer = 270;  // Desde las 6:30 hasta las 11:00
    // Script que controla el mostrar el tiempo
    [SerializeField] private Clock clock = null;

    // Malestar de sueño límite
    [SerializeField] private float maxDiscomfort = 100.0f;
    // Recuperacion del sueño cada segundo que no hay nada molestando
    [SerializeField] private float discomfortRecover = 2.5f;

    // Fade negro de entrada/salida
    [SerializeField] private Fade foreGround = null;
    [SerializeField] private Fade badDream = null;
    [SerializeField] private Fade yourBed = null;
    [SerializeField] private Fade thanks = null;
    [SerializeField] private Fade credits = null;
    [SerializeField] private Fade logo = null;
    [SerializeField] private Fade wonderfulDream = null;
    [SerializeField] private Fade timeToWork = null;

    // Private variables
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~    

    private float timer;
    // Segundos/Minutos transcurridos
    private int actualSecs;
    // Cada segundo pasa un minuto
    private int timeStep = 1;
    // Malestar actual
    private float actualDiscomfort = 0.0f;

    // Variable principal de control sobre el mainLoop
    private bool gameActive = false;

    private bool fadedOut = false;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        Cursor.visible = false;

        // Tiempo hasta que se quiera empezar
        Invoke("startGame", 3.0f);
    }       

    public bool GetGameActive()
    {
        return (gameActive && fadedOut);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (gameActive && fadedOut)
        {
            int timeNormB = (int)timer;
            timer += Time.deltaTime;
            int timeNormA = (int)timer;
            
            if (timeNormA - timeNormB >= timeStep)
            {
                actualSecs += timeStep;
                clock.IncreaseMinute();                

                if (actualSecs == maxTimer)
                {
                    endGame(true);
                }

                temperature.UpdateTemperature();                

                float discomfort = 0.0f;
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i].IsActive())
                    {
                        discomfort += items[i].GetDiscomfortStep();
                    }
                }

                discomfort += temperature.GetDiscomfortStep();

                if (discomfort > 0.0f)
                {
                    actualDiscomfort += discomfort;

                    if (actualDiscomfort >= maxDiscomfort)
                        endGame(false);                    
                }
                
                actualDiscomfort -= discomfortRecover;

                if (actualDiscomfort < 0.0f)
                    actualDiscomfort = 0.0f;
                

                sleepometer.UpdateSleepometer(actualDiscomfort);
            }
        }
    }

    private void startGame()
    {
        clock.startGame();
        gameActive = true;        
    }    

    public void ActiveItems()
    {        
        for (int i = 0; i < items.Length; i++)
        {
            items[i].StartProcess(Random.Range(-5,5));
        }
    }

    public void fadeOut()
    {        
        foreGround.FadeOut(true);        
    }

    public void SetFadedOut()
    {
        fadedOut = true;
        clock.HideNumbersPart();
    }

    private void endGame(bool result)
    {
        gameActive = false;

        mouse.mouseFadeOut();

        if (result) // Victoria
        {
            StartCoroutine(victory());
        }
        else // Derrota
        {
            StartCoroutine(defeat());
        }

        StartCoroutine(fadeSounds());
    }

    IEnumerator victory()
    {             
        foreGround.FadeInLimit(0.3f, 0.5f);
        wonderfulDream.FadeIn(0.5f);

        yield return new WaitForSeconds(1.5f);

        timeToWork.FadeIn(0.5f);

        yield return new WaitForSeconds(2.5f);

        wonderfulDream.FadeOut();

        yield return new WaitForSeconds(2.0f);

        logo.FadeIn();

        yield return new WaitForSeconds(1.0f);

        thanks.FadeIn();

        yield return new WaitForSeconds(1.0f);

        credits.FadeIn();

        yield return new WaitForSeconds(10.0f);

        Application.Quit();
    }

    IEnumerator defeat()
    {
        clock.HideNumbers();

        yield return new WaitForSeconds(1.0f);
        
        badDream.FadeIn(0.75f);

        yield return new WaitForSeconds(1.5f);

        yourBed.FadeIn(0.75f);

        yield return new WaitForSeconds(2.5f);

        foreGround.FadeIn(0.5f);

        yield return new WaitForSeconds(3.5f);

        badDream.FadeOut();
        yourBed.FadeOut();

        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene(1);        
    }

    IEnumerator fadeSounds()
    {
        bool on = false;

        while (!on)
        {
            on = true;

            for (int i = 0; i < audioSources.Length; i++)
            {
                audioSources[i].volume -= Time.deltaTime * soundSpeed;

                if (audioSources[i].volume >= 0)
                    on = false;
            }

            lofiSource.volume -= Time.deltaTime * soundSpeed;

            if (lofiSource.volume >= 0)
                on = false;

            yield return 0;
        }
    }
}
