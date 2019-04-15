using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommuManager : MonoBehaviour {

    public CustomerManager CustomerSpeech;
    CustomerControl targetCustomer;
    TimeManager StopTime;

    public GameObject TutorialSpeech;
    public GameObject StorySpeech;
    public GameObject BossSpeech;

    public Transform uiSet;
    public UISprite PlayerSprite;
    public UISprite NPCSprite;
    public UILabel NPCNameLabel;
    public UILabel SpeechLabel;
    public TypewriterEffect typeEffect;

    public Transform BossPop;

    public bool EndMessage;
    public bool NowFirstTalking;
    public int CommuCurrentSpeech;

    void Awake()
    {
        StopTime = GameObject.Find("TimeManager").GetComponent<TimeManager>();    
    }

    public void setTargetCustomer(CustomerControl customer)
    {
        targetCustomer = customer;
        UISetting();
    }

    public void NextSpeech()
    {
        if (CommuCurrentSpeech < targetCustomer.CommuMax)
        {
            if (EndMessage)
            {
                CommuCurrentSpeech++;
                UISetting();
                NowFirstTalking = true;
                typeEffect.ResetToBeginning();
                StartCoroutine("BossPopUpSprite");
                EndMessage = false;
                targetCustomer.CommuCurrentIndex++;
            }
            else
            {
                typeEffect.Finish();
                EndMessage = true;
            }
        }
        else
        {
            if (EndMessage)
            {
                CustomerSpeech.SpeechStart(true);

                CustomerSpeech.BossItemGiv(targetCustomer);
                CustomerSpeech.BossItemGiv(targetCustomer);

                StopTime.StartCoroutine("TimeFlow");
                TimeManager.nowFlow = true;
                UIVisible(false);
                CommuCurrentSpeech = 0;
                NowFirstTalking = false;
                EndMessage = false;

            }
            else
            {
                typeEffect.Finish();
                EndMessage = true;
            }
        }
    }

    public void UISetting()
    {
       
        TutorialSpeech.SetActive(false);
        StorySpeech.SetActive(false);
        BossSpeech.SetActive(true);

        string emotion = targetCustomer.CommuSpeechs[CommuCurrentSpeech].Split(':')[0];
        string message = targetCustomer.CommuSpeechs[CommuCurrentSpeech].Split(':')[1];

        NPCSprite.width = 2700;
        NPCSprite.height = 3500;

        PlayerSprite.spriteName = "Player_라임-힘듬";
        NPCSprite.spriteName = "NPC_" +targetCustomer.CustomerName+"-"+emotion;
        NPCNameLabel.text = targetCustomer.CustomerName;
        SpeechLabel.text = message;

        EndMessage = false;
        typeEffect.enabled = true;
        typeEffect.ResetToBeginning();
    }

    public void IntroSetting(string name, string message)
    {
        NPCNameLabel.text = name;
        SpeechLabel.text = message;
        typeEffect.enabled = true;
        typeEffect.ResetToBeginning();
    }

    public void UIVisible(bool visible)
    {
        StopCoroutine("UICommu");
        StartCoroutine("UICommu", visible);
        if (!visible)
            StartCoroutine("UICommu", visible);
    }

    IEnumerator UICommu(bool visible)
    {
        yield return null;

        Vector3 nextPos = uiSet.position;
        nextPos.y = visible ? -3.5f : -15f;

        if (Mathf.Abs(uiSet.position.y - nextPos.y) < 0.05f)
            yield break;

        uiSet.position = Vector3.Lerp(uiSet.position, nextPos, 0.3f);

        StartCoroutine("UICommu", visible);
    }

    public void DoingMsg()
    {
        EndMessage = false;
    }

    IEnumerator BossPopUpSprite()
    {
        yield return null;

        Vector3 PopupScale = new Vector3(1.1f, 0.9f, 1f);

        if (BossPop.localScale == PopupScale)
        {
            BossPop.localScale = new Vector3(1.1f, 0.9f, 1f);
            StartCoroutine("BossPopDownSprite");
            StopCoroutine("BossPopUpSprite");
        }
        else
        {
            BossPop.localScale = Vector3.Lerp(BossPop.localScale, PopupScale, 0.9f);
            StartCoroutine("BossPopUpSprite");
        }
    }

    IEnumerator BossPopDownSprite()
    {
        yield return null;

        Vector3 PopdownScale = new Vector3(1f, 1f, 1f);

        if (BossPop.localScale == PopdownScale)
        {
            BossPop.localScale = new Vector3(1f, 1f, 1f);
            StopCoroutine("BossPopDownSprite");
        }
        else
        {
            BossPop.localScale = Vector3.Lerp(BossPop.localScale, PopdownScale, 0.9f);
            StartCoroutine("BossPopDownSprite");
        }
    }
}
