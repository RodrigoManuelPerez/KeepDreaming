using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public Fade logo;
    public Fade credits;

    private void Awake()
    {
        Cursor.visible = false;
        StartCoroutine(intro());
    }

    IEnumerator intro()
    {
        yield return new WaitForSeconds(1.0f);

        logo.FadeIn(0.5f);
        credits.FadeIn(0.5f);

        yield return new WaitForSeconds(5.0f);

        logo.FadeOut();
        credits.FadeOut();

        yield return new WaitForSeconds(2.0f);
        
        SceneManager.LoadScene(1);

        yield return 0;
    }
}
