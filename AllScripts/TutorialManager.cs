using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    Animator animator;
    Vector3 PaperMovePos;

    public GameObject SkipSet;
    public Transform SkipMovSet;
    Vector3 SkipMovPos;

    //-----------------------------------------
    public SpriteRenderer Pointer;
    public SpriteRenderer Pointer2;
    public SpriteRenderer Pointer3;

    public GameObject TutoNextButton;
    public GameObject TutoBackButton;

    public GameObject TutorialSet;
    public Transform TutorialMovSet;
    Vector3 TutorialMovPos;
    public int TutopageNum;

    public UILabel TutorialTitle;
    public UILabel TutorialText;
    public SpriteRenderer TutorialImg;

    [TextArea]
    public string[] tutorial1Text;
    [TextArea]
    public string[] tutorial2Text;
    [TextArea]
    public string[] tutorial3Text;
    [TextArea]
    public string[] tutorial4Text;
    [TextArea]
    public string[] tutorial5Text;


    public Sprite[] tutorial1Image;
    public Sprite[] tutorial2Image;
    public Sprite[] tutorial3Image;
    public Sprite[] tutorial4Image;
    public Sprite[] tutorial5Image;

    public int PageNum;
    int MaxPage;
    int index;

    //----------------------------------------
    public PlayerControl Player;
    public StoryManager Story;
    public TimeManager StopTime;
    public CommuManager Community;
    public GameObject FirstPerson;
    public GameObject NPCScale;

    public BoxCollider Button1Collider;
    public BoxCollider Button2Collider;
    public BoxCollider2D PlayerBlock;

    public GameObject FirstSpeech;
    public GameObject SecondSpeech;
    public GameObject BossSpeech;

    public Transform NameTag;
    public UILabel NameLabel;
    public UILabel MessageLabel;

    public UISprite PlayerSprite;
    public UISprite NPCSprite;

    public int TutorialPage;
    public string[] CurrentMessage;
    int CurrentMsgIndex;
    List<string[]> MessageList = new List<string[]>();

    public static bool isTutorial;
    public static bool FirstCus;

    void Awake()
    {
        Application.targetFrameRate = 55;
        index = 1;//튜토리얼 페이지 넘버
        TutorialPage = 1;//튜토리얼대화넘버
        BossSpeech.SetActive(false);//대화창끄기
        SecondSpeech.SetActive(false);
        animator = FirstPerson.GetComponent<Animator>();//첫번째 사람의 에니메이터를 가져옴

    }

    public void DataAccess(DataVo dataVo)
    {
        if (bool.Parse(dataVo.isFirst))
        {
            SkipMovPos = new Vector3(0.0f, 0.8f, -11.0f);
            StartCoroutine("SkipSetMovind");
        }
        else//로드했을때
        {
            Community.NowFirstTalking = false;
            Destroy(FirstPerson);
            isTutorial = true;
            Player.Tutorial2 = true;
            Player.Tutorial3 = true;
            Player.Tutorial4 = true;
            Player.FirstMagic = true;
            FirstCus = true;
            PlayerBlock.enabled = false;
            StartCoroutine("InitTuto");
        }
    }

    IEnumerator InitTuto()
    {
        yield return new WaitForSeconds(1f);
        StopTime.StartCoroutine("TimeFlow");
        StartCoroutine("MainScreenFadein");
    }


    IEnumerator SkipSetMovind()
    {
        yield return null;
        SkipMovSet.position = Vector3.Lerp(SkipMovSet.position, SkipMovPos, 0.2f);
        if (SkipMovSet.position == SkipMovPos)
        {
            StopCoroutine("SkipSetMovind");
            yield break;
        }

        StartCoroutine("SkipSetMovind");
    }

    public void PushYesButton()
    {
        SoundManager.sounds["NextCatoonSound"].Play();
        StartCoroutine("InitTuto");
        isTutorial = true;
        Destroy(FirstPerson);
        Player.Tutorial2 = true;
        Player.Tutorial3 = true;
        Player.Tutorial4 = true;
        FirstCus = true;
        PlayerBlock.enabled = false;
        Community.NowFirstTalking = false;
        SkipSetBack();
    }

    public void PushNoButton()
    {
        SoundManager.sounds["NextCatoonSound"].Play();
        StartCoroutine("MainScreenFadein");
        Button1Collider.enabled = false;
        Button2Collider.enabled = false;
        GameObject PaperPos = GameObject.Find("Paper Set");
        PaperMovePos = PaperPos.transform.position;
        PaperMovePos.y += 9f;
        SkipSetBack();
        StartCoroutine("PaperSetMove");
    }

    public void SkipSetBack()
    {
        SkipMovPos = new Vector3(0.0f, 7.8f, -11.0f);
        StartCoroutine("SkipSetMovind");
    }

    IEnumerator PaperSetMove()
    {
        Transform PaperMoveSet = GameObject.Find("Paper Set").transform;
        yield return null;

        StartCoroutine("PaperSetMove");

        if (Mathf.Abs(PaperMoveSet.position.y - PaperMovePos.y) < 0.05f)
        {
            StopCoroutine("PaperSetMove");
            yield break;
        }
        PaperMoveSet.position = Vector3.Lerp(PaperMoveSet.position, PaperMovePos, 0.65f);
    }

    public void TutorialStart()
    {
        SoundManager.sounds["PaperNext"].Play();
        StopCoroutine("PaperSetMove");
        GameObject PaperPos = GameObject.Find("Paper Set");
        PaperMovePos = PaperPos.transform.position;
        PaperMovePos.y -= 9f;
        StartCoroutine("PaperSetMove");

        MessageList = new List<string[]>();
        GenerateTalkList();//리스트 안에 입력된 스토리 삽입

        Community.UIVisible(true);
        LoadTalk(0);

        Talking();
    }

    public void NextTutorial()
    {
        Player.inputLeft = false;
        Player.inputRight = false;

        MessageList = new List<string[]>();
        GenerateTalkList2();
        NPCSprite.spriteName = "NPC_널";
        CurrentMsgIndex = 0;
        TutorialPage = 2;

        Community.UIVisible(true);
        LoadTalk(0);

        Talking();
    }

    public void NextTutorial2()
    {
        Player.inputLeft = false;
        Player.inputRight = false;

        Pointer.color = new Color(255, 0, 142, 0);
        MessageList = new List<string[]>();
        GenerateTalkList3();
        NPCSprite.spriteName = "NPC_널";
        CurrentMsgIndex = 0;
        TutorialPage = 3;

        Community.UIVisible(true);
        LoadTalk(0);

        Talking();
    }

    public void NextTutorial3()
    {
        Player.inputLeft = false;
        Player.inputRight = false;

        Pointer2.color = new Color(255, 0, 142, 0);
        MessageList = new List<string[]>();
        GenerateTalkList4();
        NPCSprite.spriteName = "NPC_널";
        CurrentMsgIndex = 0;
        TutorialPage = 4;

        Community.UIVisible(true);
        LoadTalk(0);

        Talking();
    }

    public void NextTutorial4()
    {
        Player.inputLeft = false;
        Player.inputRight = false;

        Pointer3.color = new Color(255, 0, 142, 0);
        MessageList = new List<string[]>();
        GenerateTalkList5();
        NPCSprite.spriteName = "NPC_널";
        CurrentMsgIndex = 0;
        TutorialPage = 5;

        Community.UIVisible(true);
        LoadTalk(0);

        Talking();
    }

    void GenerateTalkList()
    {
        MessageList.Add(new string[] {
        "라인:힘듬:.......",
        "라임:힘듬:.......",
        "라인:힘듬:우리 이제 어떡해야 되는거야?",
        "라임:우울:20일 만에 언제 다 갚아..",
        "라인:힘듬:.......",
        "라임:힘듬:일단 약국 문은 열어야 되니까......\n 오늘 하루동안 생각해보자 ",
        "라인:힘듬:응..난 청소하고 있을게",
        "라임:힘듬:그래, 난 오늘 팔 약을 만들게",
        "라인:보통:어..손님 왔는데?"
        });
    }

    void GenerateTalkList2()
    {
        MessageList.Add(new string[] {
            "라임:힘듬:재료가 없었는데 마침 받아서 다행이네..",
            "라임:힘듬:그럼 약을 만들기 전에 약국을 한번 둘러볼까"
        });
    }

    void GenerateTalkList3()
    {
        MessageList.Add(new string[] {
            "라임:보통:조제실은 늘 깨끗한거 같네 역시 청소 하나는 잘해"
        });
    }

    void GenerateTalkList4()
    {
        MessageList.Add(new string[] {
            "라임:보통:뒷마당에서 물을 얻을수 있었지",
            "라임:놀람:음? 누군가 있는거 같은데?.."
        });
    }

    void GenerateTalkList5()
    {
        MessageList.Add(new string[] {
            "라임:보통:분명 여기에 상인들이 머물렀었지",
            "라임:보통:필요한게 있다면 상인들에게서 사면 될거 같네"
        });
    }

    public void LoadTalk(int index)
    {
        CurrentMessage = MessageList[index];
        CurrentMsgIndex = 0;
        MessageLabel.text = "";
    }

    public void Talking()
    {
        if (Community.EndMessage&&!isTutorial)//대화 끝의 여부를 확인한다 끝난경우는 아래를 실행
        {
            switch (TutorialPage)
            {
                case 1:
                    if (CurrentMsgIndex < 9)
                    {
                        if (CurrentMsgIndex > 0)
                        {
                            if (MessageLabel.text.Length < CurrentMessage[CurrentMsgIndex - 1].Split(':')[1].Length)
                            {
                                return;
                            }
                        }
                        string target = CurrentMessage[CurrentMsgIndex].Split(':')[0];
                        string emotion = CurrentMessage[CurrentMsgIndex].Split(':')[1];
                        string message = CurrentMessage[CurrentMsgIndex].Split(':')[2];
                        bool isBigFont = CurrentMessage[CurrentMsgIndex].Split(':').Length == 4;


                        if (target == "라임")
                        {
                            Story.StartCoroutine("PlayerPopUpSprite");
                            NameTag.localPosition = new Vector3(-2500, 400, 0);
                            PlayerSprite.spriteName = "Player_라임-" + emotion;
                        }
                        else if (target == "라인")
                        {
                            Story.StartCoroutine("NPCPopUpSprite");
                            NameTag.localPosition = new Vector3(2500, 400, 0);
                            NPCSprite.spriteName = "NPC_라인-" + emotion;
                        }
                        Community.IntroSetting(target, message);
                        Community.EndMessage = false;
                        MessageLabel.fontSize = isBigFont ? 400 : 225;
                        CurrentMsgIndex += 1;
                    }
                    else
                    {
                        StartFirstComing();
                        PageNum = 0;
                        TutorialOpen();
                        Community.UIVisible(false);

                    }
                    break;

                case 2:
                    if (CurrentMsgIndex < 2)
                    {
                        if (CurrentMsgIndex > 0)
                        {
                            if (MessageLabel.text.Length < CurrentMessage[CurrentMsgIndex - 1].Split(':')[1].Length)
                            {
                                return;
                            }
                        }

                        string target = CurrentMessage[CurrentMsgIndex].Split(':')[0];
                        string emotion = CurrentMessage[CurrentMsgIndex].Split(':')[1];
                        string message = CurrentMessage[CurrentMsgIndex].Split(':')[2];
                        bool isBigFont = CurrentMessage[CurrentMsgIndex].Split(':').Length == 4;


                        if (target == "라임")
                        {
                            Story.StartCoroutine("PlayerPopUpSprite");
                            NameTag.localPosition = new Vector3(-2500, 400, 0);
                            PlayerSprite.spriteName = "Player_라임-" + emotion;
                        }
                        else if (target == "라인")
                        {
                            Story.StartCoroutine("NPCPopUpSprite");
                            NameTag.localPosition = new Vector3(2500, 400, 0);
                            NPCSprite.spriteName = "NPC_라인-" + emotion;
                        }
                        Community.IntroSetting(target, message);
                        Community.EndMessage = false;
                        MessageLabel.fontSize = isBigFont ? 400 : 225;
                        CurrentMsgIndex += 1;
                    }
                    else//독백 끝 1.
                    {
                        Community.UIVisible(false);
                        Pointer.color = new Color(255, 0, 142, 255);
                    }
                    break;
                case 3:
                    if (CurrentMsgIndex < 1)
                    {
                        if (CurrentMsgIndex > 0)
                        {
                            if (MessageLabel.text.Length < CurrentMessage[CurrentMsgIndex - 1].Split(':')[1].Length)
                            {
                                return;
                            }
                        }

                        string target = CurrentMessage[CurrentMsgIndex].Split(':')[0];
                        string emotion = CurrentMessage[CurrentMsgIndex].Split(':')[1];
                        string message = CurrentMessage[CurrentMsgIndex].Split(':')[2];
                        bool isBigFont = CurrentMessage[CurrentMsgIndex].Split(':').Length == 4;


                        if (target == "라임")
                        {
                            Story.StartCoroutine("PlayerPopUpSprite");
                            NameTag.localPosition = new Vector3(-2500, 400, 0);
                            PlayerSprite.spriteName = "Player_라임-" + emotion;
                        }
                        else if (target == "라인")
                        {
                            Story.StartCoroutine("NPCPopUpSprite");
                            NameTag.localPosition = new Vector3(2500, 400, 0);
                            NPCSprite.spriteName = "NPC_라인-" + emotion;
                        }
                        Community.IntroSetting(target, message);
                        Community.EndMessage = false;
                        MessageLabel.fontSize = isBigFont ? 400 : 225;
                        CurrentMsgIndex += 1;
                    }
                    else//독백 끝 1.
                    {
                        Community.UIVisible(false);
                        PageNum = 1;
                        TutorialOpen();
                        Pointer2.color = new Color(255, 0, 142, 255);
                    }
                    break;
                case 4://뒷마당에서 독백
                    if (CurrentMsgIndex < 2)
                    {
                        if (CurrentMsgIndex > 0)
                        {
                            if (MessageLabel.text.Length < CurrentMessage[CurrentMsgIndex - 1].Split(':')[1].Length)
                            {
                                return;
                            }
                        }

                        string target = CurrentMessage[CurrentMsgIndex].Split(':')[0];
                        string emotion = CurrentMessage[CurrentMsgIndex].Split(':')[1];
                        string message = CurrentMessage[CurrentMsgIndex].Split(':')[2];
                        bool isBigFont = CurrentMessage[CurrentMsgIndex].Split(':').Length == 4;


                        if (target == "라임")
                        {
                            Story.StartCoroutine("PlayerPopUpSprite");
                            NameTag.localPosition = new Vector3(-2500, 400, 0);
                            PlayerSprite.spriteName = "Player_라임-" + emotion;
                        }
                        else if (target == "라인")
                        {
                            Story.StartCoroutine("NPCPopUpSprite");
                            NameTag.localPosition = new Vector3(2500, 400, 0);
                            NPCSprite.spriteName = "NPC_라인-" + emotion;
                        }
                        Community.IntroSetting(target, message);
                        Community.EndMessage = false;
                        MessageLabel.fontSize = isBigFont ? 400 : 225;
                        CurrentMsgIndex += 1;
                    }
                    else//독백 끝 1.
                    {
                        Community.UIVisible(false);
                        PageNum = 2;
                        TutorialOpen();
                        Pointer3.color = new Color(255, 0, 142, 255);
                    }
                    break;
                case 5://상점에서 독백
                    if (CurrentMsgIndex < 2)
                    {
                        if (CurrentMsgIndex > 0)
                        {
                            if (MessageLabel.text.Length < CurrentMessage[CurrentMsgIndex - 1].Split(':')[1].Length)
                            {
                                return;
                            }
                        }

                        string target = CurrentMessage[CurrentMsgIndex].Split(':')[0];
                        string emotion = CurrentMessage[CurrentMsgIndex].Split(':')[1];
                        string message = CurrentMessage[CurrentMsgIndex].Split(':')[2];
                        bool isBigFont = CurrentMessage[CurrentMsgIndex].Split(':').Length == 4;


                        if (target == "라임")
                        {
                            Story.StartCoroutine("PlayerPopUpSprite");
                            NameTag.localPosition = new Vector3(-2500, 400, 0);
                            PlayerSprite.spriteName = "Player_라임-" + emotion;
                        }
                        else if (target == "라인")
                        {
                            Story.StartCoroutine("NPCPopUpSprite");
                            NameTag.localPosition = new Vector3(2500, 400, 0);
                            NPCSprite.spriteName = "NPC_라인-" + emotion;
                        }
                        Community.IntroSetting(target, message);
                        Community.EndMessage = false;
                        MessageLabel.fontSize = isBigFont ? 400 : 225;
                        CurrentMsgIndex += 1;
                    }
                    else//독백 끝 1.
                    {
                        Community.UIVisible(false);
                        PageNum = 3;
                        Community.NowFirstTalking = false;
                        TutorialOpen();
                    }
                    break;
            }
        }
        else //대화중의 경우 타입이펙트를 끈다. 타입이펙트를 끄면 대화가 끝난것으로 친다.
        {
            Community.typeEffect.Finish();
            Community.EndMessage = true;
            if(!Community.NowFirstTalking) Community.EndMessage = false;
        }
    }

    public void StartFirstComing()
    {
        FirstSpeech.SetActive(false);
        SecondSpeech.SetActive(true);
        Button1Collider.enabled = true;
        Button2Collider.enabled = true;
        Transform NPCTrans = NPCScale.transform;
        NPCTrans.localScale = new Vector3(1f, 1f, 1f);
        StartCoroutine("FirstCustomerComming");
    }

    IEnumerator FirstCustomerComming()
    {
        yield return null;
        animator.SetBool("IsWalking", true);
        Vector3 nextPos = FirstPerson.transform.position;
        if(FirstPerson.transform.position.x <= 2f)
        {
            animator.SetBool("IsWalking", false);
            nextPos.x = 2f;
            StartCoroutine("PlayerCom");
            StopCoroutine("FirstCustomerComming");
            yield break;
        }
        else
        {
            nextPos.x -= 3f * Time.deltaTime;
        }
        FirstPerson.transform.position = nextPos;
        StartCoroutine("FirstCustomerComming");
    }

    public void StartFirstOut()
    {
        Transform NPCTrans = NPCScale.transform;
        Button1Collider.enabled = false;
        Button2Collider.enabled = false;
        StartCoroutine("FirstCustomerOut");
    }

    IEnumerator FirstCustomerOut()
    {
        yield return null;
        animator.SetBool("IsWalking", true);
        FirstPerson.transform.localScale = new Vector3(1f, 1f, 1f);
        Vector3 nextPos = FirstPerson.transform.position;
        if (FirstPerson.transform.position.x >= 10f)
        {
            Button1Collider.enabled = true;
            Button2Collider.enabled = true;

            Story.NewStoryList();

            animator.SetBool("IsWalking", false);
            Destroy(FirstPerson);
            StopCoroutine("FirstCustomerOut");
            yield break;
        }
        else
        {
            nextPos.x += 3f * Time.deltaTime;
        }
        FirstPerson.transform.position = nextPos;
        StartCoroutine("FirstCustomerOut");
    }


    IEnumerator PlayerCom()
    {
        yield return null;
        if (FirstCus)
        {
            PlayerBlock.enabled = false;

            Community.UIVisible(true);

            Story.NewStoryList();
            
            StopCoroutine("PlayerCom");
            yield break;
        }
        else
        {
            StartCoroutine("PlayerCom");
        }
    }

    public void TutorialPageSetting()
    {
        switch (PageNum)
        {
            case 0:
                TutorialTitle.text = "1.계산대";
                TutorialText.text = tutorial1Text[0];
                TutorialImg.sprite = tutorial1Image[0];
                TutorialPageBack();
                MaxPage = 9;
                break;
            case 1:
                TutorialTitle.text = "2.조제실";
                TutorialText.text = tutorial2Text[0];
                TutorialImg.sprite = tutorial2Image[0];
                TutorialPageBack();
                MaxPage = 13;
                break;
            case 2:
                TutorialTitle.text = "3.뒷마당";
                TutorialText.text = tutorial3Text[0];
                TutorialImg.sprite = tutorial3Image[0];
                TutorialPageBack();
                MaxPage = 2;
                break;
            case 3:
                TutorialTitle.text = "4.상점 & 인터페이스";
                TutorialText.text = tutorial4Text[0];
                TutorialImg.sprite = tutorial4Image[0];
                TutorialPageBack();
                MaxPage = 16;
                break;
            case 9:
                TutorialTitle.text = "3.스킬배우기";
                TutorialText.text = tutorial5Text[0];
                TutorialImg.sprite = tutorial5Image[0];
                TutorialPageBack();
                MaxPage = 3;
                break;

        }
    }

    public void TutorialOpen()
    {
        index = 1;

        Player.inputLeft = false;
        Player.inputRight = false;
        Button1Collider.enabled = false;
        Button2Collider.enabled = false;

        TutorialMovPos = new Vector3(0.0f, 0.3f, -11.0f);
        TutorialPageSetting();
        StartCoroutine("TutorialMoving");
    }

    public void TutorialClose()
    {
        SoundManager.sounds["NextCatoonSound"].Play();
        Button1Collider.enabled = true;
        Button2Collider.enabled = true;

        if(PageNum == 3)
        {
            isTutorial = true;
            DataManager.SaveTuto = isTutorial;
            StopTime.StartCoroutine("TimeFlow");
            TimeManager.GetOut = true;
        }

        TutorialMovPos = new Vector3(0.0f, -9.7f, -11.0f);
        StartCoroutine("TutorialMoving");
    }

    public void TutorialPageNext()
    {
        if (index == MaxPage - 2)
        {
            TutoNextButton.SetActive(false);
            TutoBackButton.SetActive(true);
        }
        else
        {
            TutoNextButton.SetActive(true);
            TutoBackButton.SetActive(true);
        }
        if (index < MaxPage-1)
        {
            SoundManager.sounds["NextCatoonSound"].Play();
            switch (PageNum)
            {
                case 0:
                    index += 1;
                    TutorialText.text = tutorial1Text[index];
                    TutorialImg.sprite = tutorial1Image[index];
                    break;
                case 1:
                    index += 1;
                    TutorialText.text = tutorial2Text[index];
                    TutorialImg.sprite = tutorial2Image[index];
                    break;
                case 2:
                    index += 1;
                    TutorialText.text = tutorial3Text[index];
                    TutorialImg.sprite = tutorial3Image[index];
                    break;
                case 3:
                    index += 1;
                    TutorialText.text = tutorial4Text[index];
                    TutorialImg.sprite = tutorial4Image[index];
                    break;
                case 9:
                    index += 1;
                    TutorialText.text = tutorial5Text[index];
                    TutorialImg.sprite = tutorial5Image[index];
                    break;
            }
        }
    }

    public void TutorialPageBack()
    {
        if (index == 1)
        {
            TutoBackButton.SetActive(false);
            TutoNextButton.SetActive(true);
        }
        else
        {
            TutoBackButton.SetActive(true);
            TutoNextButton.SetActive(true);
        }

        if (index >= 1)
        {
            SoundManager.sounds["NextCatoonSound"].Play();
            switch (PageNum)
            {
                case 0:
                    index -= 1;
                    TutorialText.text = tutorial1Text[index];
                    TutorialImg.sprite = tutorial1Image[index];
                    break;
                case 1:
                    index -= 1;
                    TutorialText.text = tutorial2Text[index];
                    TutorialImg.sprite = tutorial2Image[index];
                    break;
                case 2:
                    index -= 1;
                    TutorialText.text = tutorial3Text[index];
                    TutorialImg.sprite = tutorial3Image[index];
                    break;
                case 3:
                    index -= 1;
                    TutorialText.text = tutorial4Text[index];
                    TutorialImg.sprite = tutorial4Image[index];
                    break;
                case 9:
                    index -= 1;
                    TutorialText.text = tutorial5Text[index];
                    TutorialImg.sprite = tutorial5Image[index];
                    break;
            }
        }
    }

    IEnumerator TutorialMoving()
    {
        yield return null;
        TutorialMovSet.position = Vector3.Lerp(TutorialMovSet.position,TutorialMovPos,0.4f);
        if(TutorialMovSet.position == TutorialMovPos)
        {
            StopCoroutine("TutorialMoving");
            yield break;
        }

        StartCoroutine("TutorialMoving");
    }

    IEnumerator MainScreenFadein()
    {

        UI2DSprite spriteRenderer = GameObject.Find("MainUIFadein").GetComponent<UI2DSprite>();
        Color currentColor = spriteRenderer.color;

        if (currentColor.a <= 0)
        {
            StopCoroutine("MainScreenFadein");
            yield break;
        }

        currentColor.a -= 0.075f;
        spriteRenderer.color = currentColor;
        yield return new WaitForSeconds(0.1f);


        StartCoroutine("MainScreenFadein");

    }
}

