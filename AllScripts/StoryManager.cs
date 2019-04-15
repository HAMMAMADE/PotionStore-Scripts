 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour {

    TimeManager StopTime;
    TutorialManager Tuto;
    CommuManager Community;

    public GameObject firstItem;
    public GameObject TutorialSpeech;
    public GameObject StorySpeech;
    public GameObject BossSpeech;

    public Transform NameTag;
    public UILabel NameLabel;
    public UILabel MessageLabel;

    public UISprite PlayerSprite;
    public UISprite NPCSprite;
    public Transform PlayerPop;
    public Transform NPCPop;

    public string[] CurrentMessage;
    int CurrentStoryIndex;
    List<string[]> MessageList;

    public int StoryPage;
    public int EventNum;

    void Awake()
    {
        StoryPage = 1;
        StopTime = GameObject.Find("TimeManager").GetComponent<TimeManager>();
        Community = GameObject.Find("CommuManager").GetComponent<CommuManager>();
        Tuto = GameObject.Find("TutorialManager").GetComponent<TutorialManager>();
    }

    void GenerateTalkList1()//대화문 작성 및 대화문 데이터 삽입
    {
        MessageList.Add(new string[] {
        "???:보통:여기가 던전 옆에 있는 유일한 약국입니까?",
        "라임:보통:네 왜 그러시죠?",
        "전산양:증명:저는 국내 사냥터 관리 부서에서 나온 사냥터지기 '전산양' 이라고 합니다만 안내문 받으셨습니까?",
        "라임:놀람:네? 어떤 안내문을 말씀하시는거죠?",
        "전산양:설명:그렇다면 말로 설명드려야겠군요",
        "라임:보통:......",
        "전산양:설명:얼마전부터 약국 옆 던전에 몬스터들이 다시 나타나기 시작한 것은 알고 계시나요?",
        "라임:놀람:네? 던전은 2년전에 공략되어서 몬스터가 없는걸로 알았는데요?",
        "전산양:보통:아뇨 다시 나타나기 시작했습니다. \n 그래서 사람들이 이 던전을 다시 공략하기 위해 이곳으로 돌아올거 같습니다.",
        "라임:힘듬:음 그거하고 이 약국이 상관이 있나요?",
        "전산양:설명: 던전 옆에 있는 유일한 약국이잖아요? 앞으로 사람이 많이 올 것 같으니 약을 많이 준비하는게 좋을것 같다고 전해드리러 왔습니다.",
        "전산양:설명: 이건 나라에서 나온 물건들과 준비금 입니다. 당장은 이것으로 대비해 주세요",
        "라임:보통: 네..",
        "전산양:보통: 그럼이만"
        });
    }

    void GenerateTalkList2()//대화문 작성 및 대화문 데이터 삽입
    {
        MessageList.Add(new string[] {
        "라인:보통:누구야?",
        "라임:힘듬:던전 사냥터지기 라던데",
        "라인:보통:뭐라고 하고 갔어?",
        "라임:보통:우리 옆에 있던 던전에 다시 몬스터가 나타나서 사람들이 이쪽으로 올거래",
        "라임:보통:그래서 약을 많이 준비하는게 좋을거 같다고 말하고 갔어",
        "라인:보통:그럼 2년전 처럼 손님들이 많아 질수 있을까?",
        "라임:힘듬:그건 잘 모르겠지만..",
        "라인:웃음:아무튼 좋은 소식이네",
        "라임:웃음:응.. 잘됐어 이 기회에 20일 안에 빚을 모두 갚아보자",
        "라인:힘듬:어..그건 많이 힘들어보이는데..;;"
        });
    }

    void MarhentTalkList()//대화문 작성 및 대화문 데이터 삽입-미사용 데이터
    {
        MessageList.Add(new string[] {
        "라임:놀람:어 아저씨 돌아 오셨네요?",
        "목수아저씨:보통:음 라임이구나",
        "라임:보통:아저씨도 몬스터가 다시 나타난다고 해서 돌아 오신건가요?",
        "목수아저씨:보통:물론 그렇지 분명 내가 해야할 일이 있을거다",
        "목수아저씨:보통:그래서 말인데 당분간 이곳에 있어도 괜찮지?",
        "라임:웃음:그럼요 예전부터 항상 여기 계셨었잖아요",
        "목수아저씨:보통:고맙구나 너도 필요한게 있으면 나한테 말하렴",
        "목수아저씨:보통:약국 시설이 예전만 못하는거 같은데 내가 싼 가격에 도와줄수 있을거 같구나",
        "라임:웃음: 좋아요!"
        });//9
    }

    void MarhentTalkList2()//대화문 작성 및 대화문 데이터 삽입-미사용 데이터
    {
        MessageList.Add(new string[] {
        "라임:놀람: 너 스리즈 아니니?",
        "스리즈:보통:라임 잘 지냈어?",
        "라임:웃음:오랫만이네? 이 시간에 여기까지 무슨일이야?",
        "스리즈:보통: 여기 던전에 몬스터가 다시 나타났다고 해서 사냥하려고 왔지",
        "스리즈:보통: 나..밤에는 여기서 있어도 괜찮을까? 여관이 없어서..",
        "라임:힘듬:아..맞아..수인 전용 여관은 여기 근처에는 없었지..",
        "라임:웃음:당연히 여기 있어도 괜찮지",
        "스리즈:보통:고마워! 아 맞아 필요한게 있으면 나한테 말해줘 던전에서 얻은 것들을 팔고 있으니까!",
        "라임:힘듬:어..공짜 아니야?",
        "스리즈:보통:그럴리가..나도 먹고는 살아야지",
        "라임:힘듬:....."
        });//11
    }

    void MarhentTalkList3()//대화문 작성 및 대화문 데이터 삽입
    {
        MessageList.Add(new string[] {
        "선생님:친절: 안녕? 오랜만이네",
        "라임:놀람: 앗 선생님!",
        "선생님:친절: 약국은 잘 되고 있어?",
        "라임:힘듬: 어..그게..",
        "라임:웃음: 이제 잘 될거에요!",
        "선생님:보통: 알려준 마법은 기억하고 있는거야?",
        "라임:힘듬: 안쓴지 오..오래되서..",
        "선생님:보통: .......",
        "선생님:친절: 다시 배우면 되지! 한동안 여기서 지내니까 필요하면 찾아와",
        "라임:힘듬: 네..네에.."
        });
    }

    public void NewStoryList()
    {
        MessageList = new List<string[]>();
        CurrentStoryIndex = 0;
        switch (StoryPage)
        {
            case 1:
                GenerateTalkList1();
                Community.UIVisible(true);
                LoadStory(0);
                Storying();
                break;

            case 2:
                GenerateTalkList2();
                Community.UIVisible(true);
                LoadStory(0);
                Storying();
                break;
            case 97:
                MarhentTalkList3();
                Community.UIVisible(true);
                LoadStory(0);
                Storying();
                break;
        }

    }

    public void LoadStory(int index)
    {
        CurrentMessage = MessageList[index];
        CurrentStoryIndex = 0;
        MessageLabel.text = "";
    }

    public void Storying()//호출하면 대화 이벤트를 시작한다.
    {
        TutorialSpeech.SetActive(false);
        StorySpeech.SetActive(true);
        BossSpeech.SetActive(false);
        if (Community.EndMessage)//대화 끝의 여부를 확인한다 끝난경우는 아래를 실행
        {
            switch (StoryPage)
            {
                case 1:
                    //------1번스토리의 경우
                    if (CurrentStoryIndex < 14)
                    {
                        if (CurrentStoryIndex > 0)
                        {
                            if (MessageLabel.text.Length < CurrentMessage[CurrentStoryIndex - 1].Split(':')[1].Length)
                            {
                                return;
                            }
                        }

                        string target = CurrentMessage[CurrentStoryIndex].Split(':')[0];
                        string emotion = CurrentMessage[CurrentStoryIndex].Split(':')[1];
                        string message = CurrentMessage[CurrentStoryIndex].Split(':')[2];
                        bool isBigFont = CurrentMessage[CurrentStoryIndex].Split(':').Length == 4;


                        if (target == "라임")
                        {
                            StartCoroutine("PlayerPopUpSprite");
                            NameTag.localPosition = new Vector3(-2500, 400, 0);
                            PlayerSprite.spriteName = "Player_라임-" + emotion;
                        }
                        else if (target == "전산양")
                        {
                            StartCoroutine("NPCPopUpSprite");
                            NPCSprite.width = 2700;
                            NPCSprite.height = 3500;
                            NameTag.localPosition = new Vector3(2500, 400, 0);
                            NPCSprite.spriteName = "NPC_전산양-" + emotion;
                        }
                        else if (target == "???")
                        {
                            StartCoroutine("NPCPopUpSprite");
                            NPCSprite.width = 2700;
                            NPCSprite.height = 3500;
                            NameTag.localPosition = new Vector3(2400, 400, 0);
                            NPCSprite.spriteName = "NPC_사냥터지기-" + emotion;
                        }
                        Community.IntroSetting(target, message);
                        Community.EndMessage = false;
                        MessageLabel.fontSize = isBigFont ? 400 : 225;
                        CurrentStoryIndex += 1;
                    }
                    else
                    {
                        FirstItemGive();
                        FirstItemGive();
                        FirstItemGive();
                        Community.typeEffect.Finish();
                        Community.UIVisible(false);
                        StoryPage += 1;
                        Tuto.StartFirstOut();
                    }
                    break;
                case 2://2번스토리의 경우
                    if (CurrentStoryIndex < 10)
                    {
                        if (CurrentStoryIndex > 0)
                        {
                            if (MessageLabel.text.Length < CurrentMessage[CurrentStoryIndex - 1].Split(':')[1].Length)
                            {
                                return;
                            }
                        }

                        string target = CurrentMessage[CurrentStoryIndex].Split(':')[0];
                        string emotion = CurrentMessage[CurrentStoryIndex].Split(':')[1];
                        string message = CurrentMessage[CurrentStoryIndex].Split(':')[2];
                        bool isBigFont = CurrentMessage[CurrentStoryIndex].Split(':').Length == 4;


                        if (target == "라임")
                        {
                            StartCoroutine("PlayerPopUpSprite");
                            NameTag.localPosition = new Vector3(-2500, 400, 0);
                            PlayerSprite.spriteName = "Player_라임-" + emotion;
                        }
                        else if (target == "라인")
                        {
                            StartCoroutine("NPCPopUpSprite");
                            NPCSprite.width = 2300;
                            NPCSprite.height = 3000;
                            NameTag.localPosition = new Vector3(2500, 400, 0);
                            NPCSprite.spriteName = "NPC_라인-" + emotion;
                        }
                        Community.IntroSetting(target, message);
                        Community.EndMessage = false;
                        MessageLabel.fontSize = isBigFont ? 400 : 225;
                        CurrentStoryIndex += 1;
                    }
                    else
                    {
                        TutorialSpeech.SetActive(true);
                        StorySpeech.SetActive(false);
                        BossSpeech.SetActive(false);

                        Tuto.NPCScale.transform.localScale = new Vector3(-1f, 1f, 1f);
                        Community.UIVisible(false);

                        Tuto.NextTutorial();
                    }
                    break;
                case 99://가구상인
                    break;
                case 98://야간행상인
                    break;
                case 97://스킬마스터
                    if (CurrentStoryIndex < 10)
                    {
                        if (CurrentStoryIndex > 0)
                        {
                            if (MessageLabel.text.Length < CurrentMessage[CurrentStoryIndex - 1].Split(':')[1].Length)
                            {
                                return;
                            }
                        }

                        string target = CurrentMessage[CurrentStoryIndex].Split(':')[0];
                        string emotion = CurrentMessage[CurrentStoryIndex].Split(':')[1];
                        string message = CurrentMessage[CurrentStoryIndex].Split(':')[2];
                        bool isBigFont = CurrentMessage[CurrentStoryIndex].Split(':').Length == 4;


                        if (target == "라임")
                        {
                            StartCoroutine("PlayerPopUpSprite");
                            NameTag.localPosition = new Vector3(-2500, 400, 0);
                            PlayerSprite.spriteName = "Player_라임-" + emotion;
                        }
                        else if (target == "선생님")
                        {
                            StartCoroutine("NPCPopUpSprite");
                            NPCSprite.width = 2100;
                            NPCSprite.height = 2850;
                            NameTag.localPosition = new Vector3(2500, 400, 0);
                            NPCSprite.spriteName = "NPC_마법선생-" + emotion;
                        }
                        Community.IntroSetting(target, message);
                        Community.EndMessage = false;
                        MessageLabel.fontSize = isBigFont ? 400 : 225;
                        CurrentStoryIndex += 1;
                    }
                    else
                    {
                        TutorialSpeech.SetActive(true);
                        StorySpeech.SetActive(false);
                        BossSpeech.SetActive(false);

                        Tuto.NPCScale.transform.localScale = new Vector3(-1f, 1f, 1f);
                        Community.UIVisible(false);
                        Tuto.PageNum = 9;
                        Tuto.TutorialOpen();

                        //Tuto.NextTutorial();
                    }
                    break;
            }
            //------------------2번스토리의 경우
        }
        else//대화중의 경우 타입이펙트를 끈다. 타입이펙트를 끄면 대화가 끝난것으로 친다.
        {
            Community.typeEffect.Finish();
            Community.EndMessage = true;
        }
    }

    //--대화 이벤트 함수에 특정 손님과 닿은 경우의 케이스를 작성한다.

    IEnumerator PlayerPopUpSprite()
    {
        yield return null;
        Vector3 PopupScale = new Vector3(1.1f, 0.9f, 1f);

        if (PlayerPop.localScale == PopupScale)
        {
            PlayerPop.localScale = new Vector3(1.1f, 0.9f, 1f);
            StartCoroutine("PlayerPopDownSprite");
            StopCoroutine("PlayerPopUpSprite");
        }
        else
        {
            PlayerPop.localScale = Vector3.Lerp(PlayerPop.localScale, PopupScale, 0.9f);
            StartCoroutine("PlayerPopUpSprite");
        }
    }

    IEnumerator PlayerPopDownSprite()
    {
        yield return null;

        Vector3 PopdownScale = new Vector3(1f, 1f, 1f);

        if (PlayerPop.localScale == PopdownScale)
        {
            PlayerPop.localScale = new Vector3(1f, 1f, 1f);
            StopCoroutine("PlayerPopDownSprite");
        }
        else
        {
            PlayerPop.localScale = Vector3.Lerp(PlayerPop.localScale, PopdownScale, 0.9f);
            StartCoroutine("PlayerPopDownSprite");
        }
    }

    IEnumerator NPCPopUpSprite()
    {
        yield return null;

        Vector3 PopupScale = new Vector3(1.1f, 0.9f, 1f);

        if (NPCPop.localScale == PopupScale)
        {
            NPCPop.localScale = new Vector3(1.1f, 0.9f, 1f);
            StartCoroutine("NPCPopDownSprite");
            StopCoroutine("NPCPopUpSprite");
        }
        else
        {
            NPCPop.localScale = Vector3.Lerp(NPCPop.localScale, PopupScale, 0.9f);
            StartCoroutine("NPCPopUpSprite");
        }
    }

    IEnumerator NPCPopDownSprite()
    {
        yield return null;

        Vector3 PopdownScale = new Vector3(1f, 1f, 1f);

        if (NPCPop.localScale == PopdownScale)
        {
            NPCPop.localScale = new Vector3(1f, 1f, 1f);
            StopCoroutine("NPCPopDownSprite");
        }
        else
        {
            NPCPop.localScale = Vector3.Lerp(NPCPop.localScale, PopdownScale, 0.9f);
            StartCoroutine("NPCPopDownSprite");
        }
    }

    public void FirstItemGive()
    {
        StatusManager.Point += 3000;

        GameObject OuttedItem;
        Rigidbody2D Itemrigid;
        Vector3 outPower;
        Transform PriceOutPos = GameObject.Find("Price_Position").transform;
        OuttedItem = Instantiate(firstItem, PriceOutPos);
        Itemrigid = OuttedItem.GetComponent<Rigidbody2D>();
        outPower = new Vector2(-3f, Random.Range(5f, 6f));
        Itemrigid.AddForce(outPower, ForceMode2D.Impulse);
        Itemrigid.AddTorque(90f);
    }
}
