using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour {//메뉴화면에서 작동하는 각종 애니메이션 관리 스크립트

    public ButtonManager buttonManager;
    GameObject backTop;
    GameObject backBottom;

    GameObject MainScreen1;
    GameObject MainScreen2;
    Vector3 Screen1Pos;
    Vector3 Screen2Pos;

    GameObject MainInFrame1;
    GameObject MainOutFrame1;
    Vector3 InFramePos1;
    Vector3 OutFramePos1;

    GameObject MainInFrame2;
    GameObject MainOutFrame2;
    Vector3 InFramePos2;
    Vector3 OutFramePos2;

    GameObject MainInFrame3;
    GameObject MainOutFrame3;
    Vector3 InFramePos3;
    Vector3 OutFramePos3;

    GameObject MainInFrame4;
    GameObject MainOutFrame4;
    Vector3 InFramePos4;
    Vector3 OutFramePos4;

    GameObject UIButtonSet;
    GameObject UIExitButtonSet;

    private void Awake()
    {
       // Screen.SetResolution(Screen.width, Screen.width * 9 / 16, true);
        Application.targetFrameRate = 50;

        backTop = GameObject.Find("MainMenu_inframe");
        backBottom = GameObject.Find("MainMenu_inframe2");


        UIButtonSet = GameObject.Find("ButtonSet");
        UIExitButtonSet = GameObject.Find("ExitButtonSet");


        MainScreen1 = GameObject.Find("MainMenu_BlackScreen");
        MainScreen2 = GameObject.Find("MainMenu_BlackScreen2");

        MainInFrame1 = GameObject.Find("MainMenu_frame");
        MainOutFrame1 = GameObject.Find("MainMenu_outframe");

        MainInFrame2 = GameObject.Find("MainMenu_frame2");
        MainOutFrame2 = GameObject.Find("MainMenu_outframe2");

        MainInFrame3 = GameObject.Find("MainMenu_frame3");
        MainOutFrame3 = GameObject.Find("MainMenu_outframe3");

        MainInFrame4 = GameObject.Find("MainMenu_frame4");
        MainOutFrame4 = GameObject.Find("MainMenu_outframe4");

        Screen1Pos = new Vector3(17.7f, 0.5f, -4f);
        Screen2Pos = new Vector3(-18f, 0.5f, -4f);

        InFramePos1 = new Vector3(-6.5f, -4.6f, -6f);
        OutFramePos1 = new Vector3(-7f, -5.5f, -5f);

        InFramePos2 = new Vector3(9f, -2.5f, -6f);
        OutFramePos2 = new Vector3(9.5f, -3.5f, -5f);

        InFramePos3 = new Vector3(6.9f, 4.6f, -6f);
        OutFramePos3 = new Vector3(7.5f, 5.5f, -5f);

        InFramePos4 = new Vector3(-7.5f, 4.5f, -6f);
        OutFramePos4 = new Vector3(-8.5f, 5.2f, -5f);
    }
 
    void Start () {
        StartCoroutine("TitleAnimationStarter");
        StartCoroutine("BackFlowAnimation");
    }

    IEnumerator BackFlowAnimation()
    {
        yield return null;

        float offset = 0.44f;

        Vector3 nextPos = backTop.transform.position;
        if (backTop.transform.position.x < -offset)
        {
            nextPos.x = offset;
            
        }
        else
        {
            nextPos.x = nextPos.x - 0.005f;
        }
        backTop.transform.position = nextPos;

       
        nextPos = backBottom.transform.position;
        if(backBottom.transform.position.x > offset)
        {
            nextPos.x = -offset;
        }
        else
        {
            nextPos.x = nextPos.x + 0.005f;
        }
        backBottom.transform.position = nextPos;

        StartCoroutine("BackFlowAnimation");

    }

    IEnumerator TitleAnimationStarter()
    {
        yield return new WaitForSeconds(2.5f);
        StartCoroutine("TitleAnimation");
    }
	
    IEnumerator TitleAnimation()
    {
        yield return null;

        if (MainScreen1.transform.position == Screen1Pos)
        {
            buttonManager.ButtonStart();
            StartCoroutine("UIAnimation");
            StopCoroutine("TitleAnimation");
            yield break;
        }
        MainScreen1.transform.position = Vector3.Lerp(MainScreen1.transform.position, Screen1Pos, 0.1f);
        MainScreen2.transform.position = Vector3.Lerp(MainScreen2.transform.position, Screen2Pos, 0.1f);


        StartCoroutine("TitleAnimation");

    }

    IEnumerator UIAnimation()
    {
        yield return null;
        //StartCoroutine("UIStartAnimation");

        if (UIButtonSet.transform.position == Vector3.zero)
        {
            SoundManager.sounds["TitleMusic"].Play();
            StopCoroutine("UIAnimation");
            yield break;
        }

        UIButtonSet.transform.position = Vector3.Lerp(UIButtonSet.transform.position, Vector3.zero, 0.1f);
        UIExitButtonSet.transform.position = Vector3.Lerp(UIExitButtonSet.transform.position, Vector3.zero, 0.025f);

        //

        StartCoroutine("UIAnimation");

    }

    public void StartMusic()
    {
        SoundManager.sounds["TitleMusic"].Play();
    }
  
    void Update () {
        
        MainInFrame1.transform.position = Vector3.Lerp(MainInFrame1.transform.position, InFramePos1, 0.05f);
        MainOutFrame1.transform.position = Vector3.Lerp(MainOutFrame1.transform.position, OutFramePos1, 0.05f);

        MainInFrame2.transform.position = Vector3.Lerp(MainInFrame2.transform.position, InFramePos2, 0.05f);
        MainOutFrame2.transform.position = Vector3.Lerp(MainOutFrame2.transform.position, OutFramePos2, 0.05f);

        MainInFrame3.transform.position = Vector3.Lerp(MainInFrame3.transform.position, InFramePos3, 0.05f);
        MainOutFrame3.transform.position = Vector3.Lerp(MainOutFrame3.transform.position, OutFramePos3, 0.05f);

        MainInFrame4.transform.position = Vector3.Lerp(MainInFrame4.transform.position, InFramePos4, 0.05f);
        MainOutFrame4.transform.position = Vector3.Lerp(MainOutFrame4.transform.position, OutFramePos4, 0.05f);

    }

}

