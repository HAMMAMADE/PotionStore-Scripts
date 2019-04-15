using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSetManager : MonoBehaviour
{

    public GameObject SkillUpSet;

    Vector3 SkillUpPos;
    Vector3 SkillInPos;

    public BoxCollider Skill1UPbox;
    public BoxCollider Skill2UPbox;
    public BoxCollider Skill3UPbox;

    public UIButton Skill1Upbtn;
    public UIButton Skill2Upbtn;
    public UIButton Skill3Upbtn;

    public GameObject InSkill1UP;
    public GameObject InSkill2UP;
    public GameObject InSkill3UP;
    public GameObject ProfProgress1;
    public GameObject ProfProgress2;
    public GameObject ProfProgress3;

    public int SkillPoints;
    bool PushSkillUp;
    float UpNum;
    float UpNum2;
    float UpNum3;
    int UpCase;

    bool CanUpSkill1;
    bool CanUpSkill2;
    bool CanUpSkill3;

    public Animator SkillAnim1;
    public Animator SkillAnim2;
    public Animator SkillAnim3;

    public float Proficiency1;
    public float Proficiency2;
    public float Proficiency3;

    public bool cantEnfPotion;

    public bool cantEnforce;
    public bool cantChange;

    public GameObject UISkillSet;

    public bool useSkillButton = false;

    Vector3 UIMoveSkillPos;

    float MagicTime;

    public MessageManager message;
    public StatusManager ManaState;
    TimeManager SkillTime;
    AlchemyManager Guage;

    BoxCollider AlchemyButton;

    public Transform EffectPoint;
    //----------------------------------
    public PotionListManager TargetPotion;
    //----------------------------------

    public UILabel Skill1Lable;
    public UILabel Skill2Lable;
    public UILabel Skill3Lable;

    public UILabel UpSkill1Lable;
    public UILabel UpSkill2Lable;
    public UILabel UpSkill3Lable;

    public UILabel SkillPointLable;
    //-----------------------------------

    public BoxCollider Skill1;
    public BoxCollider Skill2;
    public BoxCollider Skill3;

    public UIButton Skill1btn;
    public UIButton Skill2btn;
    public UIButton Skill3btn;

    //----------------------------------Skill 1

    Vector3 UsingSkill1Pos;

    public Transform UsingSkillSet1;

    public UISprite MagicBar;

    public UISprite UnMagicBar;

    bool Skill1isOk;

    //----------------------------------Skill2
    Vector3 UsingSkill2Pos;

    Vector3 PinNextPos;

    public Transform UsingSkillSet2;

    public UISprite StopPin;

    public UISprite BackPin;

    bool PlusPos;

    float RandomPos, PinRange, PinSpeed;

    //-----------------------------------Skill3

    public GameObject Level1Skill3;
    public GameObject Level2Skill3;
    public GameObject Level3Skill3;

    BoxCollider ArrowButtonCol0;
    BoxCollider ArrowButtonCol1;
    BoxCollider ArrowButtonCol2;
    BoxCollider ArrowButtonCol3;
    BoxCollider ArrowButtonCol4;

    Vector3 UsingSkill3Pos;

    public Transform UsingSkillSet3;

    public UISprite[] InputArrows;
    int[] TargetArrows;
    int[] GetArrows;
    int ArrowIndex;
    int countNum;
    int CheckNum;

    bool Skill3isOk;

    //-----------------------------------

    public GameObject Skill1Effect;

    public GameObject Skill2Effect;

    public GameObject Skill3Effect;

    //-----------------------------------
    public GameObject SkillPage1;

    public GameObject SkillPage2;

    public GameObject SkillPage3;

    public GameObject SkillPage4;
    //-----------------------------------
    //-----------------------------------
    public bool learnedSkill;

    public int Skill1Level;

    public int Skill2Level;

    public int Skill3Level;

    //-----------------------------------
    int PageNum;
    public bool nowSkill;
    public static bool IsSkillGame;

    //---데이터로드-------------------------------

    public void DataAccess(DataVo dataVo)
    {
        SkillPoints = int.Parse(dataVo.SaveSkillPoint);
        UpdataSkillPoints();

        Skill1Level = int.Parse(dataVo.SaveSkill1Lev);
        Skill2Level = int.Parse(dataVo.SaveSkill2Lev);
        Skill3Level = int.Parse(dataVo.SaveSkill3Lev);

        Proficiency1 = float.Parse(dataVo.SaveSkill1Prof);
        Proficiency2 = float.Parse(dataVo.SaveSkill2Prof);
        Proficiency3 = float.Parse(dataVo.SaveSkill3Prof);

        ProfLoad();

        SkillAnim1.enabled = false;
        SkillAnim2.enabled = false;
        SkillAnim3.enabled = false;
        UpdateSkillLevel();
        UpdateSkillBtn();
        SkillTime = GameObject.Find("TimeManager").GetComponent<TimeManager>();
        Guage = GameObject.Find("AlchemyManager").GetComponent<AlchemyManager>();
        AlchemyButton = GameObject.Find("AlchemyButton").GetComponent<BoxCollider>();

        ArrowButtonCol0 = GameObject.Find("ArrowButton0").GetComponent<BoxCollider>();
        ArrowButtonCol1 = GameObject.Find("ArrowButton1").GetComponent<BoxCollider>();
        ArrowButtonCol2 = GameObject.Find("ArrowButton2").GetComponent<BoxCollider>();
        ArrowButtonCol3 = GameObject.Find("ArrowButton3").GetComponent<BoxCollider>();
        ArrowButtonCol4 = GameObject.Find("ArrowButton4").GetComponent<BoxCollider>();
    }

    //--------------------------------------------

    public void UpdateSkillBtn()
    {
        if (Skill1Level == 0)
        {
            Skill1.enabled = false;
            Skill1btn.state = UIButtonColor.State.Disabled;
        }
        else
        {
            Skill1.enabled = true;
            Skill1btn.state = UIButtonColor.State.Normal;
        }

        if (Skill2Level == 0)
        {
            Skill2.enabled = false;
            Skill2btn.state = UIButtonColor.State.Disabled;
        }
        else
        {
            Skill2.enabled = true;
            Skill2btn.state = UIButtonColor.State.Normal;
        }

        if (Skill3Level == 0)
        {
            Skill3.enabled = false;
            Skill3btn.state = UIButtonColor.State.Disabled;
        }
        else
        {
            Skill3.enabled = true;
            Skill3btn.state = UIButtonColor.State.Normal;
        }
    }

    public void PushSkillButton()//스킬 사용하기 버튼 클릭시 발동하는 함수
    {
        if (learnedSkill && !nowSkill)
        {
            SoundManager.sounds["BookOpen"].Play();
            if (useSkillButton == false)
            {
                useSkillButton = true;

                UIMoveSkillPos = UISkillSet.transform.position;
                UIMoveSkillPos.y += 8f;
            }
            else
            {
                useSkillButton = false;

                UIMoveSkillPos = UISkillSet.transform.position;
                UIMoveSkillPos.y -= 8f;
            }

            StartCoroutine("PopupSkill");
        }
        else
        {
            SoundManager.sounds["CantuseSkill"].Play();
        }
    }

    IEnumerator PopupSkill()//스킬창 팝업코루틴
    {
        yield return null;

        Transform UISkillSet = GameObject.Find("Skill Set").transform;

        StartCoroutine("PopupSkill");

        if (UISkillSet.position == UIMoveSkillPos)
        {
            StopCoroutine("PopupSkill");
            yield break;
        }
        UISkillSet.position = Vector3.Lerp(UISkillSet.position, UIMoveSkillPos, 0.5f);
    }

    public void UpdataSkillPoints()
    {
        SkillPointLable.text = string.Format("{0:n0}", SkillPoints);
    }

    public void UpdateSkillLevel()
    {
        Skill1Lable.text = string.Format("{0:n0}", Skill1Level);
        Skill2Lable.text = string.Format("{0:n0}", Skill2Level);
        Skill3Lable.text = string.Format("{0:n0}", Skill3Level);

        UpSkill1Lable.text = string.Format("{0:n0}", Skill1Level);
        UpSkill2Lable.text = string.Format("{0:n0}", Skill2Level);
        UpSkill3Lable.text = string.Format("{0:n0}", Skill3Level);
    }

    public void PushNextButton()
    {
        switch (PageNum)
        {
            case 0:
                SoundManager.sounds["SkillNextPage"].Play();
                SkillPage1.SetActive(false);
                SkillPage2.SetActive(true);
                PageNum += 1;
                break;
            case 1:
                SoundManager.sounds["SkillNextPage"].Play();
                SkillPage2.SetActive(false);
                SkillPage3.SetActive(true);
                PageNum += 1;
                break;
            case 2:
                SoundManager.sounds["SkillNextPage"].Play();
                SkillPage3.SetActive(false);
                SkillPage4.SetActive(true);
                PageNum += 1;
                break;
            case 3:
                SoundManager.sounds["SkillNextPage"].Play();
                break;
        }

    }

    public void PushBackButton()
    {
        switch (PageNum)
        {
            case 3:
                SoundManager.sounds["SkillNextPage"].Play();
                SkillPage4.SetActive(false);
                SkillPage3.SetActive(true);
                PageNum -= 1;
                break;
            case 2:
                SoundManager.sounds["SkillNextPage"].Play();
                SkillPage3.SetActive(false);
                SkillPage2.SetActive(true);
                PageNum -= 1;
                break;
            case 1:
                SoundManager.sounds["SkillNextPage"].Play();
                SkillPage2.SetActive(false);
                SkillPage1.SetActive(true);
                PageNum -= 1;
                break;
            case 0:
                SoundManager.sounds["SkillNextPage"].Play();
                break;
        }

    }

    public static void EffectDeleteFunc()
    {
        GameObject Destination = GameObject.FindGameObjectWithTag("SkillEffect");

        Destroy(Destination);
    }

    public void Skill1Push()//스킬 아이콘 클릭시 미니게임창 을 띄우는 함수
    {
        if (AlchemyManager.pushMaterial == true && AlchemyManager.pushFlask == true && StatusManager.Mana != 0)
        {
            SkillAnim1.enabled = true;
            IsSkillGame = true;
            SkillPoints += 1;
            UpdataSkillPoints();
            StatusManager.Mana -= 1;
            ManaState.UpdateManaState();
            SkillTime.StopCoroutine("TimeFlow");
            TimeManager.nowFlow = false;
            AlchemyButton.enabled = false;
            UsingSkill1Pos = UsingSkillSet1.position;
            UsingSkill1Pos.y += 10f;
            MagicTime = 0.5f;
            StartCoroutine("UsingSkill1");
            PushSkillButton();
        }
        else
        {
            SoundManager.sounds["CantuseSkill"].Play();
        }
    }

    IEnumerator UsingSkill1()
    {
        yield return null;

        StartCoroutine("UsingSkill1");

        if (UsingSkillSet1.position == UsingSkill1Pos)
        {
            StopCoroutine("UsingSkill1");
            StartCoroutine("MagicFlow");
            yield break;
        }
        UsingSkillSet1.position = Vector3.Lerp(UsingSkillSet1.position, UsingSkill1Pos, 0.5f);
    }

    public void EndSkill1()
    {
        SkillAnim1.enabled = false;
        IsSkillGame = false;
        AlchemyButton.enabled = true;
        SkillTime.StartCoroutine("TimeFlow");
        TimeManager.nowFlow = true;
        UsingSkill1Pos = UsingSkillSet1.position;
        UsingSkill1Pos.y -= 10f;
        StartCoroutine("EndUsingSkill1");

        if (Skill1isOk)
        {
            nowSkill = true;
            ProfUpdate1();
            SoundManager.sounds["MagicComplete"].Play();
            message.PopUpEventMesg("[000000]스킬이 발동되었습니다");
            switch (Skill1Level)
            {
                case 1:
                    PotionListManager.validitynum = 2;
                    Guage.CompleteGuageChange(5, false);
                    Instantiate(Skill1Effect, EffectPoint);
                    break;
                case 2:
                    PotionListManager.validitynum = 3;
                    Guage.CompleteGuageChange(10, false);
                    Instantiate(Skill1Effect, EffectPoint);
                    break;
                case 3:
                    PotionListManager.validitynum = 4;
                    Guage.CompleteGuageChange(15, false);
                    Instantiate(Skill1Effect, EffectPoint);
                    break;
            }
        }
        else
        {
            SoundManager.sounds["BrokenSound"].Play();
            message.PopUpEventMesg("[000000]스킬발동에 [F10000]실패[000000]했습니다.");
        }

    }

    IEnumerator EndUsingSkill1()
    {
        yield return null;

        StartCoroutine("EndUsingSkill1");

        if (UsingSkillSet1.position == UsingSkill1Pos)
        {
            StopCoroutine("EndUsingSkill1");
            yield break;
        }
        UsingSkillSet1.position = Vector3.Lerp(UsingSkillSet1.position, UsingSkill1Pos, 0.5f);
    }

    public void PushMagicButton()
    {
        SoundManager.sounds["MagicSound"].Play();
        MagicTime += (0.03f + Skill1Level * 0.0065f);//성공하는 힘
    }

    IEnumerator MagicFlow()
    {
        yield return new WaitForSeconds(0.01f);
        if (MagicTime <= 0f)
        {
            MagicTime = 0f;
            Skill1isOk = false;
            EndSkill1();
            yield break;
        }

        if (MagicTime >= 1f)
        {
            Skill1isOk = true;
            EndSkill1();
            yield break;
        }

        MagicTime -= Skill1Level * 0.001f;//실패하는 힘 

        MagicBar.transform.localScale = new Vector3(MagicTime, 1f, 1f);
        UnMagicBar.transform.localScale = new Vector3(1f - MagicTime, 1f, 1f);
        StartCoroutine("MagicFlow");
    }
    //-----------------------------------------------------------------------------------------------------

    public void Skill2Push()//스킬 아이콘 클릭시 미니게임창 을 띄우는 함수
    {
        if (AlchemyManager.pushMaterial == true && AlchemyManager.pushFlask == true && StatusManager.Mana != 0 && !cantEnfPotion)
        {
            SkillAnim2.enabled = true;
            SkillPoints += 1;
            UpdataSkillPoints();
            StatusManager.Mana -= 1;
            ManaState.UpdateManaState();
            IsSkillGame = true;
            AlchemyButton.enabled = false;
            SkillTime.StopCoroutine("TimeFlow");
            TimeManager.nowFlow = false;
            UsingSkill2Pos = UsingSkillSet2.position;
            UsingSkill2Pos.y += 10f;
            RandomPos = Random.Range(930f, -800f);
            switch (Skill2Level)
            {
                case 1:
                    BackPin.transform.localScale = new Vector3(2.5f, 1f, 1f);
                    PinSpeed = 0.13f;
                    PinRange = 150f;
                    break;
                case 2:
                    BackPin.transform.localScale = new Vector3(1.5f, 1f, 1f);
                    PinSpeed = 0.078f;
                    PinRange = 80f;
                    break;
                case 3:
                    BackPin.transform.localScale = new Vector3(1f, 1f, 1f);
                    PinSpeed = 0.045f;
                    PinRange = 40f;
                    break;
            }
            StartCoroutine("UsingSkill2");
            PushSkillButton();
        }
        else
        {
            SoundManager.sounds["CantuseSkill"].Play();
            if (cantEnfPotion) message.PopUpEventMesg("[F10000]강화할 수 없는 재료 업니다.");
        }
    }

    IEnumerator UsingSkill2()
    {
        yield return null;
        StopPin.transform.localPosition = new Vector3(1f, 0f, 0f);
        BackPin.transform.localPosition = new Vector3(RandomPos, 0f, 0f);
        StartCoroutine("UsingSkill2");

        if (UsingSkillSet2.position == UsingSkill2Pos)
        {
            StopCoroutine("UsingSkill2");
            StartCoroutine("MagicStopFlow");
            yield break;
        }
        UsingSkillSet2.position = Vector3.Lerp(UsingSkillSet2.position, UsingSkill2Pos, 0.5f);
    }


    IEnumerator MagicStopFlow()
    {
        yield return new WaitForSeconds(0.001f);

        PinNextPos = StopPin.transform.position;
        if (!PlusPos)
        {
            PinNextPos.x += PinSpeed;
            if (PinNextPos.x >= 2.95f)
            {
                PlusPos = true;
            }
        }
        else
        {
            PinNextPos.x -= PinSpeed;
            if (PinNextPos.x <= -2.35f)
            {
                PlusPos = false;
            }
        }
        StopPin.transform.position = PinNextPos;

        StartCoroutine("MagicStopFlow");
    }

    public void PushStopButton()
    {
        if (StopPin.transform.localPosition.x >= BackPin.transform.localPosition.x - PinRange && StopPin.transform.localPosition.x <= BackPin.transform.localPosition.x + PinRange)
        {
            nowSkill = true;
            ProfUpdate2();
            Instantiate(Skill2Effect, EffectPoint);
            TargetPotion.CompleteSkill2 = true;
            SoundManager.sounds["MagicSound"].Play();
            message.PopUpEventMesg("[000000]스킬이 발동되었습니다");
        }
        else
        {
            SoundManager.sounds["BrokenSound"].Play();
            message.PopUpEventMesg("[000000]스킬발동에 [F10000]실패[000000]했습니다.");
        }
        StopCoroutine("MagicStopFlow");

        EndSkill2();
    }

    public void EndSkill2()
    {
        SkillAnim2.enabled = false;
        IsSkillGame = false;
        AlchemyButton.enabled = true;
        SkillTime.StartCoroutine("TimeFlow");
        TimeManager.nowFlow = true;
        UsingSkill2Pos = UsingSkillSet2.position;
        UsingSkill2Pos.y -= 10f;
        StartCoroutine("EndUsingSkill2");
    }

    IEnumerator EndUsingSkill2()
    {
        yield return null;

        StartCoroutine("EndUsingSkill2");

        if (UsingSkillSet2.position == UsingSkill2Pos)
        {
            StopCoroutine("EndUsingSkill2");
            yield break;
        }
        UsingSkillSet2.position = Vector3.Lerp(UsingSkillSet2.position, UsingSkill2Pos, 0.5f);
    }

    //----------------------------------------

    public void Skill3Push()//스킬 아이콘 클릭시 미니게임창 을 띄우는 함수
    {
        //▼스킬이 발동하기 위한 조건들 cantEnfPotion은 스킬을 쓸수 있는 포션인지 확인하는 변수
        if (AlchemyManager.pushMaterial == true && AlchemyManager.pushFlask == true && StatusManager.Mana != 0 && !cantChange)
        {
            SkillAnim3.enabled = true;//미니게임 화면상의 캐릭터 애니메이션이 작동하도록 한다.

            SkillPoints += 1;
            UpdataSkillPoints();
            StatusManager.Mana -= 1;
            ManaState.UpdateManaState();

            IsSkillGame = true;//미니게임 중 이면 True 아니라면 False
            AlchemyButton.enabled = false;//미니게임중 에는 포션완성 버튼을 누를수 없다

            SkillTime.StopCoroutine("TimeFlow");//미니게임 중에는 게임상 시간이 흐르지 않는다
            TimeManager.nowFlow = false;
            UsingSkill3Pos = UsingSkillSet3.position;
            UsingSkill3Pos.y += 10f;
            MagicTime = 0.5f;
            StartCoroutine("UsingSkill3");//미니게임 화면을 이동시키는 UsingSkill3 코루틴
            PushSkillButton();
        }
        else
        {
            SoundManager.sounds["CantuseSkill"].Play();
            if (cantChange) message.PopUpEventMesg("[F10000]변환할 수 없는 재료 업니다.");
        }
    }


    IEnumerator UsingSkill3()
    {
        yield return null;

        StartCoroutine("UsingSkill3");//조건이 달성 될때까지 코루틴은 반복됨

        if (UsingSkillSet3.position == UsingSkill3Pos)
        {
            //미니게임 화면이 나타나면 UsingSkill3 코루틴을 끝내고 게임을 시작하도록 한다
            StopCoroutine("UsingSkill3");
            StartSkill3();//게임시작 함수
            yield break;
        }
        UsingSkillSet3.position = Vector3.Lerp(UsingSkillSet3.position, UsingSkill3Pos, 0.5f);
    }

    public void StartSkill3()
    {
        CheckNum = 0;//버튼입력이 처음부터 시작하도록 초기화
        ArrowButtonCol0.enabled = false;
        ArrowButtonCol1.enabled = false;
        ArrowButtonCol2.enabled = false;
        ArrowButtonCol3.enabled = false;
        ArrowButtonCol4.enabled = false;
        //입력해야 할 방향이 나타나는동안은 버튼을 누를수 없도록 한다
        switch (Skill3Level)
        {//스킬 레벨에 따라 미니게임의 난이도를 조정한다
            case 1:
                countNum = 4;//4개의 방향을 입력해야 한다
                //레벨1의 난이도가 선택되었으므로 나머지 난이도는 보여지지 않도록 함
                Level1Skill3.SetActive(true);
                Level2Skill3.SetActive(false);
                Level3Skill3.SetActive(false);
                //버튼을 입력받아야하므로 초기화 
                InputArrows = new UISprite[countNum];
                TargetArrows = new int[countNum];
                GetArrows = new int[countNum];

                for (int index = 0; index < countNum; index++)
                {
                    GetArrows[index] = 5;
                    InputArrows[index] = GameObject.Find("Arrow" + index.ToString()).GetComponent<UISprite>();
                    TargetArrows[index] = Random.Range(0, 4);//무작위로 방향을 정한다
                    InputArrows[index].spriteName = "ArrowButton" + TargetArrows[index].ToString();
                }
                break;

            case 2:
                countNum = 5;
                Level1Skill3.SetActive(false);
                Level2Skill3.SetActive(true);
                Level3Skill3.SetActive(false);
                InputArrows = new UISprite[countNum];
                TargetArrows = new int[countNum];
                GetArrows = new int[countNum];
                for (int index = 0; index < countNum; index++)
                {
                    GetArrows[index] = 5;
                    InputArrows[index] = GameObject.Find("Arrow" + index.ToString()).GetComponent<UISprite>();
                    TargetArrows[index] = Random.Range(0, 4);
                    InputArrows[index].spriteName = "ArrowButton" + TargetArrows[index].ToString();
                }
                break;
            case 3:
                countNum = 6;
                Level1Skill3.SetActive(false);
                Level2Skill3.SetActive(false);
                Level3Skill3.SetActive(true);
                InputArrows = new UISprite[countNum];
                TargetArrows = new int[countNum];
                GetArrows = new int[countNum];
                for (int index = 0; index < countNum; index++)
                {
                    GetArrows[index] = 5;
                    InputArrows[index] = GameObject.Find("Arrow" + index.ToString()).GetComponent<UISprite>();
                    TargetArrows[index] = Random.Range(0, 4);
                    InputArrows[index].spriteName = "ArrowButton" + TargetArrows[index].ToString();
                }
                break;
        }
        StartCoroutine("CommendReset");
    }
    IEnumerator CommendReset()
    {
        //미니게임 시작을 위해 나타난 방향들을 가린다
        yield return new WaitForSeconds(2f + Skill3Level - 1f);

        for (int index = 0; index < countNum; index++)
        {
            InputArrows[index].spriteName = "NothingButton";
        }

        ArrowButtonCol0.enabled = true;
        ArrowButtonCol1.enabled = true;
        ArrowButtonCol2.enabled = true;
        ArrowButtonCol3.enabled = true;
        ArrowButtonCol4.enabled = true;
    }

    public void CheckCommend()//버튼이 눌러지면 눌린 버튼과 눌러야할 버튼이 같은지 검사한다
    {
        if (TargetArrows[CheckNum - 1] == GetArrows[CheckNum - 1])
        {
            if (CheckNum - 1 == countNum - 1)
            {
                Skill3isOk = true;
                EndSkill3();
            }
        }
        else
        {
            EndSkill3();
            SoundManager.sounds["BrokenSound"].Play();
            StopCoroutine("CommendCheck");
            message.PopUpEventMesg("[000000]스킬발동에 [F10000]실패[000000]했습니다.");
        }
    }
    //▼버튼의 OnClik 에 할당되는 함수들 버튼을 누르면 방향이 입력되며 각 방향은 숫자로 구분함
    public void PushArrow0()
    {
        SoundManager.sounds["MagicSound"].Play();
        GetArrows[CheckNum] = 0;
        InputArrows[CheckNum].spriteName = "ArrowButton0";
        CheckNum++;
        CheckCommend();
    }
    public void PushArrow1()
    {
        SoundManager.sounds["MagicSound"].Play();
        GetArrows[CheckNum] = 1;
        InputArrows[CheckNum].spriteName = "ArrowButton1";
        CheckNum++;
        CheckCommend();
    }
    public void PushArrow2()
    {
        SoundManager.sounds["MagicSound"].Play();
        GetArrows[CheckNum] = 2;
        InputArrows[CheckNum].spriteName = "ArrowButton2";
        CheckNum++;
        CheckCommend();
    }
    public void PushArrow3()
    {
        SoundManager.sounds["MagicSound"].Play();
        GetArrows[CheckNum] = 3;
        InputArrows[CheckNum].spriteName = "ArrowButton3";
        CheckNum++;
        CheckCommend();
    }
    public void PushArrow4()
    {
        SoundManager.sounds["MagicSound"].Play();
        GetArrows[CheckNum] = 4;
        InputArrows[CheckNum].spriteName = "ArrowButton4";
        CheckNum++;
        CheckCommend();
    }

    public void EndSkill3()//게임이 끝나면 이 함수가 시작된다
    {
        SkillAnim3.enabled = false;
        IsSkillGame = false;
        AlchemyButton.enabled = true;
        SkillTime.StartCoroutine("TimeFlow");
        TimeManager.nowFlow = true;
        if (Skill3isOk)
        {
            SoundManager.sounds["MagicComplete"].Play();
            if (!cantEnforce)
            {
                nowSkill = true;
                ProfUpdate3();
                Skill3isOk = false;
                TargetPotion.CompleteSkill3 = true;
                Instantiate(Skill3Effect, EffectPoint);
                message.PopUpEventMesg("[000000]스킬이 발동되었습니다");
            }
            else if (cantEnforce)
            {
                StatusManager.Mana += 1;
                message.PopUpEventMesg("[F10000]스킬 레벨이 부족[000000]해 강화할수 없습니다.");
            }

        }
        UsingSkill3Pos = UsingSkillSet3.position;
        UsingSkill3Pos.y -= 10f;
        StartCoroutine("EndUsingSkill3");

    }

    IEnumerator EndUsingSkill3()
    {
        yield return null;

        StartCoroutine("EndUsingSkill3");

        if (UsingSkillSet3.position == UsingSkill3Pos)
        {
            StopCoroutine("EndUsingSkill3");
            yield break;
        }
        UsingSkillSet3.position = Vector3.Lerp(UsingSkillSet3.position, UsingSkill3Pos, 0.5f);
    }
    //------------------------------------------------------------------
    public void PushSKillUpButton()// 버튼 클릭시 발동
    {
        SoundManager.sounds["BookOpen"].Play();
        if (PushSkillUp == false)
        {
            PushSkillUp = true;

            SkillUpPos = SkillUpSet.transform.position;
            SkillUpPos = new Vector3(-5.7f, 1.75f, -10.0f);
        }
        else
        {
            PushSkillUp = false;
            SkillUpPos = SkillUpSet.transform.position;
            SkillUpPos = new Vector3(-5.7f, 9.0f, -10.0f);
        }

        StartCoroutine("PopupSkillUp");

    }

    IEnumerator PopupSkillUp()//팝업 코루틴
    {
        yield return null;

        StartCoroutine("PopupSkillUp");

        if (SkillUpSet.transform.position == SkillUpPos)
        {
            StopCoroutine("PopupSkillUp");
            yield break;
        }
        SkillUpSet.transform.position = Vector3.Lerp(SkillUpSet.transform.position, SkillUpPos, 0.5f);
    }

    public void PushInBack()
    {
        SoundManager.sounds["BookOpen"].Play();

        Skill1UPbox.enabled = true;
        Skill2UPbox.enabled = true;
        Skill3UPbox.enabled = true;

        Skill1Upbtn.state = UIButtonColor.State.Normal;
        Skill2Upbtn.state = UIButtonColor.State.Normal;
        Skill3Upbtn.state = UIButtonColor.State.Normal;

        switch (UpCase)
        {
            case 1:
                SkillInPos = InSkill1UP.transform.position;
                break;
            case 2:
                SkillInPos = InSkill2UP.transform.position;
                break;
            case 3:
                SkillInPos = InSkill3UP.transform.position;
                break;
        }

        SkillInPos = new Vector3(-5.7f, 0.6f, -10f);
        StartCoroutine("PopupInUp");

    }
    public void PushSkill1Up()
    {
        SoundManager.sounds["BookOpen"].Play();

        Skill1UPbox.enabled = false;
        Skill2UPbox.enabled = false;
        Skill3UPbox.enabled = false;

        Skill1Upbtn.state = UIButtonColor.State.Disabled;
        Skill2Upbtn.state = UIButtonColor.State.Disabled;
        Skill3Upbtn.state = UIButtonColor.State.Disabled;

        SkillInPos = InSkill1UP.transform.position;
        SkillInPos = new Vector3(-0.6f, 0.6f, -10f);
        UpCase = 1;
        // isOpenInUp = true;
        StartCoroutine("PopupInUp");
    }

    public void PushSkill2Up()
    {
        SoundManager.sounds["BookOpen"].Play();

        Skill1UPbox.enabled = false;
        Skill2UPbox.enabled = false;
        Skill3UPbox.enabled = false;

        Skill1Upbtn.state = UIButtonColor.State.Disabled;
        Skill2Upbtn.state = UIButtonColor.State.Disabled;
        Skill3Upbtn.state = UIButtonColor.State.Disabled;

        SkillInPos = InSkill2UP.transform.position;
        SkillInPos = new Vector3(-0.6f, 0.6f, -10f);
        UpCase = 2;
        // isOpenInUp = true;
        StartCoroutine("PopupInUp");
    }

    public void PushSkill3Up()
    {
        SoundManager.sounds["BookOpen"].Play();

        Skill1UPbox.enabled = false;
        Skill2UPbox.enabled = false;
        Skill3UPbox.enabled = false;

        Skill1Upbtn.state = UIButtonColor.State.Disabled;
        Skill2Upbtn.state = UIButtonColor.State.Disabled;
        Skill3Upbtn.state = UIButtonColor.State.Disabled;

        SkillInPos = InSkill3UP.transform.position;
        SkillInPos = new Vector3(-0.6f, 0.6f, -10f);
        UpCase = 3;
        // isOpenInUp = true;
        StartCoroutine("PopupInUp");
    }

    IEnumerator PopupInUp()
    {
        yield return null;

        StartCoroutine("PopupInUp");
        switch (UpCase)
        {
            case 1:
                if (InSkill1UP.transform.position == SkillInPos)
                {
                    StopCoroutine("PopupInUp");
                    yield break;
                }

                InSkill1UP.transform.position = Vector3.Lerp(InSkill1UP.transform.position, SkillInPos, 0.5f);
                break;
            case 2:
                if (InSkill2UP.transform.position == SkillInPos)
                {
                    StopCoroutine("PopupInUp");
                    yield break;
                }

                InSkill2UP.transform.position = Vector3.Lerp(InSkill2UP.transform.position, SkillInPos, 0.5f);
                break;
            case 3:
                if (InSkill3UP.transform.position == SkillInPos)
                {
                    StopCoroutine("PopupInUp");
                    yield break;
                }

                InSkill3UP.transform.position = Vector3.Lerp(InSkill3UP.transform.position, SkillInPos, 0.5f);
                break;
        }

    }

    public void ProfLoad()
    {
        Vector3 ProfScale1 = ProfProgress1.transform.localScale;
        Vector3 ProfScale2 = ProfProgress1.transform.localScale;
        Vector3 ProfScale3 = ProfProgress1.transform.localScale;

        ProfScale1.y += Proficiency1;
        ProfScale2.y += Proficiency2;
        ProfScale3.y += Proficiency3;

        ProfProgress1.transform.localScale = ProfScale1;
        ProfProgress2.transform.localScale = ProfScale2;
        ProfProgress3.transform.localScale = ProfScale3;
    }

    public void ProfUpdate1()
    {
        Vector3 ProfScale = ProfProgress1.transform.localScale;
        UISprite ProfSprite = ProfProgress1.GetComponent<UISprite>();
        switch (Skill1Level)
        {
            case 1:
                UpNum = 0.25f;//4
                break;
            case 2:
                UpNum = 0.125f;//8
                break;
            case 3:
                UpNum = 0.1f;//10
                break;
        }

        if (ProfScale.y < 0.99)
        {
            ProfSprite.color = new Color32(255, 255, 255, 255);
            ProfScale.y += UpNum;
            Proficiency1 += UpNum;
            if (ProfScale.y == 1)
            {
                ProfSprite.color = new Color32(0, 255, 255, 255);
                CanUpSkill1 = true;
            }
        }
        else
        {
            ProfScale.y = 1;
            Proficiency1 = 1;
            CanUpSkill1 = true;
        }

        ProfProgress1.transform.localScale = ProfScale;
    }
    public void ProfUpdate2()
    {
        Vector3 ProfScale = ProfProgress2.transform.localScale;
        UISprite ProfSprite = ProfProgress2.GetComponent<UISprite>();
        switch (Skill2Level)
        {
            case 1:
                UpNum2 = 0.25f;//4
                break;
            case 2:
                UpNum2 = 0.125f;//8
                break;
            case 3:
                UpNum2 = 0.1f;//10
                break;
        }

        if (ProfScale.y < 0.99)
        {
            ProfSprite.color = new Color32(255, 255, 255, 255);
            ProfScale.y += UpNum2;
            Proficiency2 += UpNum2;

            if (ProfScale.y == 1)
            {
                ProfSprite.color = new Color32(0, 255, 255, 255);
                CanUpSkill2 = true;
            }
        }
        else
        {
            ProfScale.y = 1;
            Proficiency2 = 1;
            CanUpSkill2 = true;
        }

        ProfProgress2.transform.localScale = ProfScale;
    }
    public void ProfUpdate3()
    {
        Vector3 ProfScale = ProfProgress3.transform.localScale;
        UISprite ProfSprite = ProfProgress3.GetComponent<UISprite>();
        switch (Skill3Level)
        {
            case 1:
                UpNum3 = 0.25f;//4
                break;
            case 2:
                UpNum3 = 0.125f;//8
                break;
            case 3:
                UpNum3 = 0.1f;//10
                break;
        }

        if (ProfScale.y < 0.99)
        {
            ProfSprite.color = new Color32(255, 255, 255, 255);
            ProfScale.y += UpNum3;
            Proficiency3 += UpNum3;

            if (ProfScale.y == 1)
            {
                ProfSprite.color = new Color32(0,255,255,255);
                CanUpSkill3 = true;
            }
        }
        else
        {
            ProfScale.y = 1;
            Proficiency3 = 1;
            CanUpSkill3 = true;
        }

        ProfProgress3.transform.localScale = ProfScale;
    }
    
    public void PushUpSkill1()
    {
        if (Skill1Level == 0 && SkillPoints >= 7)
        {
            SoundManager.sounds["PurchaseFurniture"].Play();
            message.PopUpEventMesg("[000000]스킬 레벨업");
            Skill1Level += 1;
            SkillPoints -= 7;
            UpdateSkillLevel();
            UpdataSkillPoints();
            CanUpSkill1 = false;

            Vector3 ProfScale = ProfProgress1.transform.localScale;
            Proficiency1 = 0;
            ProfScale.y = 0;
            ProfProgress1.transform.localScale = ProfScale;

            UpdateSkillBtn();

        }
        else if (CanUpSkill1&& SkillPoints >= 7 && Skill1Level != 3 && Skill1Level != 0)
        {
            SoundManager.sounds["PurchaseFurniture"].Play();
            message.PopUpEventMesg("[000000]스킬 레벨업");
            Skill1Level += 1;
            SkillPoints -= 7;
            UpdateSkillLevel();
            UpdataSkillPoints();
            CanUpSkill1 = false;

            Vector3 ProfScale = ProfProgress1.transform.localScale;
            Proficiency1 = 0;
            ProfScale.y = 0;
            ProfProgress1.transform.localScale = ProfScale;

            UpdateSkillBtn();
        }
        else if (Skill1Level == 3)
        {
            message.PopUpEventMesg("[000000]더이상 스킬 레벨업을 할수 없습니다.");
        }
        else
        {
            SoundManager.sounds["CantBuyFurniture"].Play();
            message.PopUpEventMesg("[F10000]스킬 레벨업 조건을 달성하지 않았습니다.");
        }
    }

    public void PushUpSkill2()
    {
        if (SkillPoints >= 5 && Skill2Level == 0)
        {
            SoundManager.sounds["PurchaseFurniture"].Play();
            message.PopUpEventMesg("[000000]스킬 레벨업");
            Skill2Level += 1;
            SkillPoints -= 5;
            UpdateSkillLevel();
            UpdataSkillPoints();
            CanUpSkill2 = false;

            Vector3 ProfScale = ProfProgress2.transform.localScale;
            Proficiency2 = 0;
            ProfScale.y = 0;
            ProfProgress2.transform.localScale = ProfScale;

            UpdateSkillBtn();
        }
        else if (CanUpSkill2 && SkillPoints >= 5 && Skill2Level != 3 && Skill2Level != 0)
        {
            SoundManager.sounds["PurchaseFurniture"].Play();
            message.PopUpEventMesg("[000000]스킬 레벨업");
            Skill2Level += 1;
            SkillPoints -= 5;
            UpdateSkillLevel();
            UpdataSkillPoints();
            CanUpSkill2 = false;

            Vector3 ProfScale = ProfProgress2.transform.localScale;
            Proficiency2 = 0;
            ProfScale.y = 0;
            ProfProgress2.transform.localScale = ProfScale;

            UpdateSkillBtn();
        }
        else if (Skill2Level == 3)
        {
            message.PopUpEventMesg("[000000]더이상 스킬 레벨업을 할수 없습니다.");
        }
        else
        {
            SoundManager.sounds["CantBuyFurniture"].Play();
            message.PopUpEventMesg("[F10000]스킬 레벨업 조건을 달성하지 않았습니다.");
        }
    }

    public void PushUpSkill3()
    {
        if (SkillPoints >= 5 && Skill3Level == 0)
        {
            SoundManager.sounds["PurchaseFurniture"].Play();
            message.PopUpEventMesg("[000000]스킬 레벨업");
            Skill3Level += 1;
            SkillPoints -= 5;
            UpdateSkillLevel();
            UpdataSkillPoints();

            CanUpSkill3 = false;

            Vector3 ProfScale = ProfProgress3.transform.localScale;
            Proficiency3 = 0;
            ProfScale.y = 0;
            ProfProgress3.transform.localScale = ProfScale;

            UpdateSkillBtn();
        }
        else if (CanUpSkill3 && SkillPoints >= 5 && Skill3Level != 3 && Skill3Level != 0)
        {
            SoundManager.sounds["PurchaseFurniture"].Play();
            message.PopUpEventMesg("[000000]스킬 레벨업");
            Skill3Level += 1;
            SkillPoints -= 5;
            UpdateSkillLevel();
            UpdataSkillPoints();

            CanUpSkill3 = false;

            Vector3 ProfScale = ProfProgress3.transform.localScale;
            Proficiency3 = 0;
            ProfScale.y = 0;
            ProfProgress3.transform.localScale = ProfScale;

            UpdateSkillBtn();
        }
        else if(Skill3Level == 3)
        {
            message.PopUpEventMesg("[000000]더이상 스킬 레벨업을 할수 없습니다.");
        }
        else
        {
            SoundManager.sounds["CantBuyFurniture"].Play();
            message.PopUpEventMesg("[F10000]스킬 레벨업 조건을 달성하지 않았습니다.");
        }
    }
}