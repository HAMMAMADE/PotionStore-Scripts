using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditManager : MonoBehaviour
{
    public Animator Credit;

    void Start()
    {
        //Screen.SetResolution(Screen.width, Screen.width * 9 / 16, true);
        Credit.SetTrigger("StartCredit");
        SoundManager.sounds["CartoonBGM"].Play();
        StartCoroutine("CreditVolum");
        StartCoroutine("Creditend");
    }

    IEnumerator CreditVolum()
    {
        yield return new WaitForSeconds(0.5f);
        if (SoundManager.sounds["CartoonBGM"].volume < 0.5f)
        {
            SoundManager.sounds["CartoonBGM"].volume += 0.05f;
            StartCoroutine("CreditVolum");
        }
        else
        {
            StopCoroutine("CreditVolum");
            yield break;
        }
    }

    IEnumerator Creditend()
    {
        yield return new WaitForSeconds(65f);
        StartCoroutine("ScreenFadeOut");
    }

    IEnumerator ScreenFadeOut()
    {
        UI2DSprite spriteRenderer = GameObject.Find("UIFadeOut").GetComponent<UI2DSprite>();
        Color currentColor = spriteRenderer.color;

        currentColor.a += 0.05f;
        spriteRenderer.color = currentColor;

        SoundManager.sounds["CreditMusic"].volume -= 0.05f;

        if (currentColor.a >= 1)
        {
            SceneManager.LoadScene("MainMenu");
            yield break;
        }

        yield return new WaitForSeconds(0.1f);


        StartCoroutine("ScreenFadeOut");

    }
}
