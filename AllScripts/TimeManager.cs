using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour {

    public GameObject PauseSet;

    //------------------------------
    public BoxCollider LeftButton;
    public BoxCollider RightButton;

    public Sprite DayMap1;
    public Sprite DayMap2;
    public Sprite DayMap3;
    public Sprite DayMap4;

    public Sprite NightMap1;
    public Sprite NightMap2;
    public Sprite NightMap3;
    public Sprite NightMap4;

    public GameObject MoonObject;
    public GameObject StarObject;
    public GameObject StarFallObject;
    public GameObject CloudObject;

    public SpriteRenderer Map1;
    public SpriteRenderer Map2;
    public SpriteRenderer Map3;
    public SpriteRenderer Map4;

    //-------------------------------
    public Transform ClockPin;

    public UILabel TimeLabel;

    Vector3 CalMovePos;

    public static int Day;
    string Sun;

    public static bool GetOut;
    public static bool nowFlow;

    public bool TimeNight;
    public bool isCalculate;

    public DataCallManager dataCallManager;
    public PlayerControl PlayerStop;
    public CustomerManager CustomerSpawn;

    public MatInventoryManager SaveMatInven;
    public HavePoInvenManager SavePotion;
    public InfluenceManager CheckInfluence;

    public MessageManager Alert;
    StatusManager PlayerStat;
    MarchentManager MarchentTime;
    bool Paused;
    bool isTrueEnd;
    float nextangle;

    public bool looktrueEnd;

   /* void Awake()
    {
        //Day = 19;
        //Day = DataManager.SaveDay;
        //Paused = false;
        //PauseSet.SetActive(false);
        PlayerStat = GameObject.Find("StatusManager").GetComponent<StatusManager>();
        MarchentTime = GameObject.Find("Marchent1Manager").GetComponent<MarchentManager>();
        StartCoroutine("PauseInputWait");
        // StartCoroutine("TimeFlow");   
    }*/
    //-데이터 로드------------------------
    public void DataAccess(DataVo dataVo)
    {
        Day = int.Parse(dataVo.SaveDay);
        Paused = false;
        looktrueEnd = bool.Parse(dataVo.lookedEnd);
        PauseSet.SetActive(false);
        PlayerStat = GameObject.Find("StatusManager").GetComponent<StatusManager>();
        MarchentTime = GameObject.Find("Marchent1Manager").GetComponent<MarchentManager>();
        StartCoroutine("PauseInputWait");
    }
    //--------------------------------------
    void nextDay()
    {
        SoundManager.sounds["BackGroundMusic"].Play();
        MarchentTime.TimeMarchent();

        dataCallManager.SaveDataWrite();

        Day += 1;
        MapChange();

        StatusManager.Mana = 5;
        PlayerStat.UpdateManaState();

        StartCoroutine("ChangeBGM");
        ClockPin.localEulerAngles = new Vector3(0f, 0f, -0.3f);
        nowFlow = true;
        StartCoroutine("TimeFlow");

        if (CustomerManager.SpawnTerm >= 4f)
        {
            CustomerManager.SpawnTerm = 40f;
            CustomerManager.SpawnTerm = CustomerManager.SpawnTerm - (CustomerManager.SpawnTerm * 0.05f * Day + (StatusManager.EvaPoint * 0.001f));//날짜가 지날수록, 
            //DataManager.SaveSpawnTime = CustomerManager.SpawnTerm;
        }
        else
        {
            CustomerManager.SpawnTerm = 4f;
           // DataManager.SaveSpawnTime = CustomerManager.SpawnTerm;
        }

        if(Day == 4)
        {
            Alert.PopUpCusMesg();
            CustomerSpawn.Length += 2;
           // DataManager.SpawnCus = CustomerSpawn.Length;
        }
        else if (Day == 6)
        {
            Alert.PopUpBossMesg();
            CustomerSpawn.BossSpawn = true;
            //DataManager.SaveSpawnBoss = CustomerSpawn.BossSpawn;
            CustomerSpawn.StartCoroutine("SpawnBoss");
        }
        else if(Day == 8)
        {
            Alert.PopUpCusMesg();
            CustomerSpawn.Length += 2;
           // DataManager.SpawnCus = CustomerSpawn.Length;
        }
        else if (Day == 10)
        {
            Alert.PopUpBossMesg();
            CustomerSpawn.BossSpawn = true;
            //DataManager.SaveSpawnBoss = CustomerSpawn.BossSpawn;
            CustomerSpawn.StartCoroutine("SpawnBoss");
        }
        else if(Day == 12)
        {
            Alert.PopUpCusMesg();
            CustomerSpawn.Length += 2;
           // DataManager.SpawnCus = CustomerSpawn.Length;
        }

        else if (Day == 14)
        {
            Alert.PopUpCusMesg();
            CustomerSpawn.Length += 1;
            // DataManager.SpawnCus = CustomerSpawn.Length;
        }
        else CustomerSpawn.BossSpawn = false;

        Sun = "낮";
        GetOut = false;
        TimeLabel.text = Day + "일차 "+ Sun;
    }

    void NighttoDay()
    {
        SoundManager.sounds["NightMusic"].Play();
        CustomerSpawn.EndAlert();
        MarchentTime.TimeMarchent();
        MarchentTime.Listy = 0f;
        MapChange();
        StartCoroutine("ChangeBGM");
        Sun = "밤";
        GetOut = true;
        TimeLabel.text = Day + "일차 " + Sun;
    }

    IEnumerator ChangeBGM()
    {
        yield return new WaitForSeconds(0.1f);
        if (TimeNight)
        {
            if (SoundManager.sounds["BackGroundMusic"].volume > 0f)
            {
                SoundManager.sounds["BackGroundMusic"].volume -= 0.005f;
            }
            else
            {
                SoundManager.sounds["BackGroundMusic"].Stop();
                StartCoroutine("ChangeBGMFadeIn");
                StopCoroutine("ChangeBGM");
            }
        }
        else
        {
            if (SoundManager.sounds["NightMusic"].volume > 0f)
            {
                SoundManager.sounds["NightMusic"].volume -= 0.005f;
            }
            else
            {
                SoundManager.sounds["NightMusic"].Stop();
                StartCoroutine("ChangeBGMFadeIn");
                StopCoroutine("ChangeBGM");
            }
        }

        StartCoroutine("ChangeBGM");

    }

    IEnumerator ChangeBGMFadeIn()
    {
        yield return new WaitForSeconds(0.1f);

        if (TimeNight)
        {
            if (SoundManager.sounds["NightMusic"].volume <= 0.5f)
            {
                SoundManager.sounds["NightMusic"].volume += 0.005f;
            }
            else
            {
                StopCoroutine("ChangeBGMFadeIn");
            }
        }
        else
        {
            if (SoundManager.sounds["BackGroundMusic"].volume <= 0.5f)
            {
                SoundManager.sounds["BackGroundMusic"].volume += 0.005f;
            }
            else
            {
                StopCoroutine("ChangeBGMFadeIn");
            }
        }

        StartCoroutine("ChangeBGMFadeIn");

    }

    IEnumerator TimeFlow()//
    {
        yield return new WaitForSecondsRealtime(0.1f);
        if (ClockPin.localEulerAngles.z >= 177.8 && ClockPin.localEulerAngles.z <= 178)
        {
            CustomerManager customerManager = GameObject.Find("CustomerManager").GetComponent<CustomerManager>();
            customerManager.CustomerAllOutStarter();
            nextangle = -0.3f;
            TimeNight = true;
            NighttoDay();
        }
        else  if (ClockPin.localEulerAngles.z >= -0.05 && ClockPin.localEulerAngles.z <= 0.05)
        {
            TimeNight = false;
            nextangle = -0.2f;
            Calculate();
            yield break;
        }
        Vector3 nextAngle = ClockPin.localEulerAngles;
        nextAngle.z = nextangle;
        ClockPin.Rotate(nextAngle);

        StartCoroutine("TimeFlow");
    }

    public void Calculate()
    {
        StopCoroutine("CalculateSetMove");
        StopCoroutine("TimeFlow");
        nowFlow = false;
        //--------플레이어 이동을 멈춤------------------
        PlayerStop.inputLeft = false;
        PlayerStop.inputRight = false;
        LeftButton.enabled = false;
        RightButton.enabled = false;

        PlayerStat.UpdateReceipt();
        GameObject CalculatePos = GameObject.Find("Calculate Set");
        CalMovePos = CalculatePos.transform.position;
        CalMovePos.y += 9f;
        isCalculate = true;
        StartCoroutine("CalculateSetMove");
    }

    IEnumerator CalculateSetMove()
    {

        Transform calMoveSet = GameObject.Find("Calculate Set").transform;
        yield return null;

        StartCoroutine("CalculateSetMove");

        if (Mathf.Abs(calMoveSet.position.y - CalMovePos.y) < 0.05f)
        {
            StopCoroutine("CalculateSetMove");
            yield break;
        }
        calMoveSet.position = Vector3.Lerp(calMoveSet.position, CalMovePos, 0.3f);
    }

    public void EndCalculate()
    {
        // Debug.Log("정산종료");
        LeftButton.enabled = true;
        RightButton.enabled = true;
        SoundManager.sounds["PaperNext"].Play();
        isCalculate = false;
        StopCoroutine("CalculateSetMove");
        StatusManager.TodayPoint = 0;
        GameObject CalculatePos = GameObject.Find("Calculate Set");
        CalMovePos = CalculatePos.transform.position;
        CalMovePos.y -= 9f;

        StatusManager.LoadPoint = StatusManager.Point;
        StatusManager.LoadEva = StatusManager.EvaPoint;

        //DataManager.HaveMoney = StatusManager.LoadPoint;
       // DataManager.HaveEva = StatusManager.LoadEva;

        //DataManager.Diamonds = StatusManager.Diamonds;

        /*if (Day == 0)
        {
            CustomerSpawn.StartCoroutine("SpawnCustomer");
        }*/

        if(CheckInfluence.isPerson || CheckInfluence.isMonster)
        {
            StartCoroutine("MainScreenFadeOut");
        }

        if (StatusManager.Diamonds <= 0 && Day >= 20 && !looktrueEnd)
        {
            looktrueEnd = true;
            isTrueEnd = true;
            StartCoroutine("MainScreenFadeOut");
        }
        if (StatusManager.Diamonds > 0 && Day >= 20 && !looktrueEnd)
        {
            isTrueEnd = false;
            StartCoroutine("MainScreenFadeOut");
        }
        else {
            nextDay();
            StartCoroutine("CalculateSetMove");
        }
    }
    IEnumerator MainScreenFadeOut()
    {

        UI2DSprite spriteRenderer = GameObject.Find("MainUIFadein").GetComponent<UI2DSprite>();
        Color currentColor = spriteRenderer.color;

        SoundManager.sounds["BackGroundMusic"].volume -= 0.05f;

        if (currentColor.a >= 1)
        {
            if (isTrueEnd)
            {
                // DataManager.isSaved = false;
                ButtonManager.takedNum = 1;
                SceneManager.LoadScene("Ending");
            }
            else if (!isTrueEnd && Day >= 20)
            {
                //  DataManager.isSaved = false;
                ButtonManager.takedNum = 4;
                SceneManager.LoadScene("GameOver3");
            }
            else if (CheckInfluence.isPerson)
            {
                //  DataManager.isSaved = false;
                ButtonManager.takedNum = 2;
                SceneManager.LoadScene("GameOver");
            }
            else if (CheckInfluence.isMonster)
            {
                //  DataManager.isSaved = false;
                ButtonManager.takedNum = 3;
                SceneManager.LoadScene("GameOver2");
            }
            StopCoroutine("MainScreenFadeOut");
            yield break;
        }

        currentColor.a += 0.075f;
        spriteRenderer.color = currentColor;
        yield return new WaitForSeconds(0.1f);


        StartCoroutine("MainScreenFadeOut");

    }

    public void MapChange()
    {
        if (TimeNight)
        {
            Map1.sprite = NightMap1;
            Map2.sprite = NightMap2;
            Map3.sprite = NightMap3;
            Map4.sprite = NightMap4;
            MoonObject.SetActive(true);
            StarObject.SetActive(true);
            StarFallObject.SetActive(true);
            CloudObject.SetActive(false);
        }

        else
        {
            Map1.sprite = DayMap1;
            Map2.sprite = DayMap2;
            Map3.sprite = DayMap3;
            Map4.sprite = DayMap4;
            MoonObject.SetActive(false);
            StarObject.SetActive(false);
            StarFallObject.SetActive(false);
            CloudObject.SetActive(true);
        }

    }

    IEnumerator PauseInputWait()
    {
        yield return null;

        if (Input.GetButtonDown("Pause") && nowFlow)
        {
            SoundManager.sounds["NoMoney"].Play();
            if (!Paused)
            {
                PlayerStop.inputLeft = false;
                PlayerStop.inputRight = false;
                LeftButton.enabled = false;
                RightButton.enabled = false;

                Paused = true;
                PauseSet.SetActive(true);
                nowFlow = false;
                StopCoroutine("TimeFlow");

                StopCoroutine("PauseInputWait");
            }
        }
        StartCoroutine("PauseInputWait");
    }
    public void PushNoPause()//일시정지화면에서 아니오 버튼 클릭
    {
        SoundManager.sounds["NextCatoonSound"].Play();
        PauseSet.SetActive(false);
        Paused = false;
        LeftButton.enabled = true;
        RightButton.enabled = true;
        nowFlow = true;
        StartCoroutine("TimeFlow");
        StartCoroutine("PauseInputWait");
    }

    public void PushOkPause()//일시정지화면에서 네 버튼 클릭
    {
        SoundManager.sounds["NextCatoonSound"].Play();
        StartCoroutine("PauseScreenFadeOut");
    }

    IEnumerator PauseScreenFadeOut()
    {

        UI2DSprite spriteRenderer = GameObject.Find("MainUIFadein").GetComponent<UI2DSprite>();
        Color currentColor = spriteRenderer.color;

       if(!TimeNight) SoundManager.sounds["BackGroundMusic"].volume -= 0.05f;
       else if(TimeNight) SoundManager.sounds["NightMusic"].volume -= 0.05f;

        if (currentColor.a >= 1)
        {
            SceneManager.LoadScene("MainMenu");
            StopCoroutine("PauseScreenFadeOut");
            yield break;
        }

        currentColor.a += 0.075f;
        spriteRenderer.color = currentColor;
        yield return new WaitForSeconds(0.1f);


        StartCoroutine("PauseScreenFadeOut");

    }
}
