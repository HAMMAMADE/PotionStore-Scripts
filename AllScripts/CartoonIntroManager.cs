using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartoonIntroManager : MonoBehaviour {
    //인트로 카툰에서 작동하는 스크립트: 배경음악, 카툰넘기기 코루틴

    void Start()
    {
        Application.targetFrameRate = 55;
        StartCoroutine("FirstCartoonSlide");
        PlayBackGround();
        StartCoroutine("MusicVolumUp");
    }

    IEnumerator MusicVolumUp()
    {
        yield return new WaitForSeconds(0.5f);
        if (SoundManager.sounds["OpeningMusic"].volume < 0.5f)
        {
            SoundManager.sounds["OpeningMusic"].volume += 0.15f;
            StartCoroutine("MusicVolumUp");
        }
        else
        {
            StopCoroutine("MusicVolumUp");
            yield break;
        }
    }

    IEnumerator FirstCartoonSlide()
    {
        yield return null;

        GameObject cartoon = GameObject.Find("Cartoon Set");

        if (cartoon.transform.position.y >= 0f)
        {
            StopCoroutine("FirstCartoonSlide");
            yield break;
        }
        cartoon.transform.position = Vector3.Lerp(cartoon.transform.position, Vector3.zero, 0.2f);

        StartCoroutine("FirstCartoonSlide");
    }

    void PlayBackGround()
    {
        SoundManager.sounds["OpeningMusic"].Play();
    }
}
