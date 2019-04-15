using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour {

    CommuManager commuManager;
    public PlayerControl PlayerStop;
    public TimeManager Time;
    public List<GameObject> Customers;
    public Vector3[] CustomersPos;

    //-----------------------------------------------------SpawnList
    public GameObject[] CustomerSet;
    public GameObject[] BossSet;
    public GameObject[] PoisonSet;
    public GameObject[] ColdSet;
    //-----------------------------------------------------
    public GameObject WaitAlertSet;
    public SpriteRenderer WaitIcon;
    public UILabel WaitSpeech;
    //-----------------------------------------------------
    public Transform SpawnPoint;
    public GameObject SpeechBubble;

    public GameObject SpeechMarchentBub;

    UILabel CustomerNameLabel;
    UILabel CustomerOrderLabel;
    public CustomerControl currentCustomer;

    public int SpawnCount;
    public double UpPrice;
    //bool SpeechBubbleBouncing;
    public static float SpawnTerm;
    public static bool AnSaYo;

    Vector3 nextAlertPos;
    Vector3 backAlertPos;

    public bool firstAlert;
    public bool BossSpawn;
    bool FirstBoss;
    int FindCus;
    int lastCus;
    int lastCus2;
    public int Length;

   /* void Awake()
    {
        if (DataManager.isLoaded) SpawnTerm = DataManager.SaveSpawnTime;
        else SpawnTerm = 30f;

        UpPrice = 1;
        commuManager = GameObject.Find("CommuManager").GetComponent<CommuManager>();
        CustomerNameLabel = SpeechBubble.GetComponentsInChildren<UILabel>()[0];
        CustomerOrderLabel = SpeechBubble.GetComponentsInChildren<UILabel>()[1];
        //SpawnPoint = GameObject.Find("CustomerSpawnPoint").transform;

        Customers = new List<GameObject>();
        CustomersPos = new Vector3[4] { new Vector3(2f, 0f, -1f), new Vector3(4f, 0f, -1f), new Vector3(6f, 0f, -1f), new Vector3(8f, 0f, -1f) };

        Length = DataManager.SpawnCus;
        BossSpawn = DataManager.SaveSpawnBoss;

        SpawnCount = 0;
        //StartCoroutine("SpawnCustomer");
    }*/
    //-데이터로드-------------------------------------
    public void DataAccess(DataVo dataVo)
    {
        SpawnTerm = float.Parse(dataVo.SaveSpawnTime);
        Length = int.Parse(dataVo.SpawnCus);
        BossSpawn = bool.Parse(dataVo.SaveSpawnBoss);

        UpPrice = 1;
        lastCus2 = 0;
        commuManager = GameObject.Find("CommuManager").GetComponent<CommuManager>();
        CustomerNameLabel = SpeechBubble.GetComponentsInChildren<UILabel>()[0];
        CustomerOrderLabel = SpeechBubble.GetComponentsInChildren<UILabel>()[1];

        Customers = new List<GameObject>();
        CustomersPos = new Vector3[4] { new Vector3(2f, 0f, -1f), new Vector3(4f, 0f, -1f), new Vector3(6f, 0f, -1f), new Vector3(8f, 0f, -1f) };
        SpawnCount = 0;
        StartCoroutine("SpawnCustomer");
    }

    //------------------------------------------------
    

    IEnumerator SpawnCustomer()
    {
        yield return new WaitForSeconds(SpawnTerm);
        bool Spawn = !TimeManager.GetOut && SpawnCount <= 3;
        if (Spawn && TimeManager.nowFlow)
        {
            bool isSpawn = true;

            if (isSpawn && !BossSpawn)
            {
                do
                {
                    FindCus = Random.Range(0, Length);
                }
                while (lastCus == FindCus);

                lastCus = FindCus;
                Customers.Add(Instantiate(CustomerSet[FindCus], SpawnPoint));
                SpawnCount++;
            }
        }
        StartCoroutine("SpawnCustomer");
    }


    IEnumerator SpawnBoss()
    {
        yield return new WaitForSeconds(1f);
        if (BossSpawn && !FirstBoss)
        {
            FirstBoss = true;
            BossSpawn = false;
           // DataManager.SaveSpawnBoss = BossSpawn;
            if(TimeManager.Day == 6) Customers.Add(Instantiate(BossSet[0], SpawnPoint));
            else if(TimeManager.Day == 10) Customers.Add(Instantiate(BossSet[1], SpawnPoint));
            StopCoroutine("SpawnBoss");
            SpawnCount++;
            yield break;
        }
    }


    IEnumerator SpawnPoison()
    {
        yield return new WaitForSeconds(5f);
        bool PoisonSpawn = !TimeManager.GetOut && SpawnCount <= 3;
        if (PoisonSpawn)
        {
            int FindCus = Random.Range(0, PoisonSet.Length);
            Customers.Add(Instantiate(PoisonSet[FindCus], SpawnPoint));
            SpawnCount++;
        }
        StartCoroutine("SpawnPoison");
    }


    IEnumerator SpawnCold()
    {
        yield return new WaitForSeconds(5f);
        bool ColdSpawn = !TimeManager.GetOut && SpawnCount <= 3;
        if (ColdSpawn)
        {
            int FindCus = Random.Range(0, ColdSet.Length);
            Customers.Add(Instantiate(ColdSet[FindCus], SpawnPoint));
            SpawnCount++;
        }
        StartCoroutine("SpawnCold");
    }


    public void CustomerOrder(CustomerControl customer)
    {
            currentCustomer = customer;
            CustomerNameLabel.text = customer.CustomerName;
            CustomerOrderLabel.text = customer.CustomerOrder;
            switch (customer.CustomerType)
            {
                case "A":
                    SpeechStart(true);
                    break;
                case "B":
                    MarchentStart(true);
                    break;
                case "C":
                    Animator animator = currentCustomer.GetComponent<Animator>();
                    animator.SetBool("isIntro", true);
                    Invoke("BossActionStarter", 1f);
                    break;
            }
        
    }
    public void StartAlert(CustomerControl customer)
    {
        if (!firstAlert)
        {
            firstAlert = true;
            nextAlertPos = WaitAlertSet.transform.position;
            nextAlertPos = new Vector3(7.1f, 1.7f, -11.0f);
            WaitIcon.sprite = customer.CustomerIcon;
            WaitSpeech.text = customer.CustomerWaitOrder;
            StopCoroutine("CustomerWaitAlert");
            StartCoroutine("CustomerWaitAlert", customer);
        }
    }

    IEnumerator CustomerWaitAlert(CustomerControl customer)
    {
        yield return null;
        if (PlayerStop.PlayerPos > 1 && !customer.StopWait && customer.WaitTime >= 30 * customer.CustomerWaitLimit && !Time.TimeNight)
         {
             WaitAlertSet.transform.position = Vector3.Lerp(WaitAlertSet.transform.position, nextAlertPos, 0.1f);
             if (WaitAlertSet.transform.position == nextAlertPos)
             {
                WaitAlertSet.transform.position = nextAlertPos;
                yield return new WaitForSeconds(3f);
                EndAlert();
                StopCoroutine("CustomerWaitAlert");
                yield break;
             }
        }
        else if (PlayerStop.ReadyOrderd)
        {
            EndAlert();
            StopCoroutine("CustomerWaitAlert");
            yield break;
        }
        StartCoroutine("CustomerWaitAlert",customer);
    }

    public void EndAlert()
    {
        firstAlert = false;
        backAlertPos = WaitAlertSet.transform.position;
        backAlertPos = new Vector3(15.1f, 1.7f, -11.0f);
        StopCoroutine("CloseAlert");
        StartCoroutine("CloseAlert");
    }

    IEnumerator CloseAlert()
    {
        yield return null;
        WaitAlertSet.transform.position = Vector3.Lerp(WaitAlertSet.transform.position, backAlertPos, 0.1f);
        if (WaitAlertSet.transform.position == backAlertPos)
        {
            WaitAlertSet.transform.position = backAlertPos;
            StopCoroutine("CloseAlert");
            yield break;
        }
            StartCoroutine("CloseAlert");
    }


    //----------------------------------------------------------------------------

    void MarchentStart(bool visible)
    {
        StopCoroutine("SpeechMarchent");
        StartCoroutine("SpeechMarchent", visible);
    }

    IEnumerator SpeechMarchent(bool visible)
    {
        yield return null;

        Vector3 nextScale = SpeechMarchentBub.transform.localScale;
        nextScale.x = visible ? 1f : 0f;
        nextScale.y = visible ? 1f : 0f;

        if (Mathf.Abs(SpeechMarchentBub.transform.localScale.x - nextScale.y) < 0.05f)
            yield break;

        SpeechMarchentBub.transform.localScale = Vector3.MoveTowards(SpeechMarchentBub.transform.localScale, nextScale, 0.5f);

        StartCoroutine("SpeechMarchent", visible);
    }

    public void MarchentEnd(GameObject customer)
    {
        MarchentStart(false);
        Customers.Remove(customer);
        Invoke("CustomerWaitEnd", 5f);
    }

    //------------------------------------------------------------------------------
    public void SpeechStart(bool visible)
    {
        StopCoroutine("SpeechOrder");
        StartCoroutine("SpeechOrder", visible);
    }

    void EventStart(bool visible)
    {
        commuManager.setTargetCustomer(currentCustomer);
        commuManager.UIVisible(visible);   
    }

    IEnumerator SpeechOrder(bool visible)
    {
        yield return null;
        Vector3 nextScale = SpeechBubble.transform.localScale;
        nextScale.x = visible ? 1f : 0f;
        nextScale.y = visible ? 1f : 0f;

        if (Mathf.Abs(SpeechBubble.transform.localScale.x - nextScale.y) < 0.05f)
            yield break;

        SpeechBubble.transform.localScale = Vector3.MoveTowards(SpeechBubble.transform.localScale, nextScale, 0.5f);

        StartCoroutine("SpeechOrder",visible);
    }

    public void OrderEnd(GameObject customer)
    {
        SpeechStart(false);
        Customers.Remove(customer);
        Invoke("CustomerWaitEnd", 5f);

    }

    public void CustomerWaitEnd()
    {
        if (TimeManager.GetOut)
            return;

        for (int index = 0; index < Customers.Count; index++)
        {
            CustomerControl customerControl = Customers[index].GetComponent<CustomerControl>();
            customerControl.isWaiting = false;
        }

    }

    public void CustomerAllOutStarter()
    {

        StartCoroutine("CustomerAllOut");
    }

    IEnumerator CustomerAllOut()
    {
        if (Customers.Count == 0)
            yield break;

        SpeechOrder(false);

       CustomerControl customerControl = Customers[Customers.Count-1].GetComponent<CustomerControl>();
       customerControl.isGoHome = true;
       customerControl.isWaiting = false;
       customerControl.isMarchent = false;
       customerControl.isOrder = false;
            yield return new WaitForSeconds(0.2f);

        StartCoroutine("CustomerAllOut");

    }

    public void NoParchase()
    {
        SoundManager.sounds["Click (6)"].Play();
        AnSaYo = true;
    }

    public void BossActionStarter()
    {
        PlayerStop.inputLeft = false;
        PlayerStop.inputRight = false;

        // 시간 정지

        TimeManager timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();//전역 할당 예정
        timeManager.StopCoroutine("TimeFlow");
        TimeManager.nowFlow = false;

        // 모든 uI 숨기기
        GameObject sellingSet = GameObject.Find("Selling Set");//전역 public 할당 예정
        UILabel[] sellingLabels = sellingSet.GetComponentsInChildren<UILabel>();
        UISprite[] sellingsprites = sellingSet.GetComponentsInChildren<UISprite>();
        BoxCollider[] sellingCollider = sellingSet.GetComponentsInChildren<BoxCollider>();
        for(int index = 0; index < sellingLabels.Length; index++)
        {
            sellingLabels[index].alpha = 0f;
        }
        for (int index = 0; index < sellingsprites.Length; index++)
        {
            sellingsprites[index].alpha = 0f;
        }
        for (int index = 0; index < sellingCollider.Length; index++)
        {
            sellingCollider[index].enabled = false;
        }


        GameObject UISet2 = GameObject.Find("UI Set 2");//전역 public 할당 예정
        UILabel[] uiLabels = UISet2.GetComponentsInChildren<UILabel>();
        UISprite[] uiSprites = UISet2.GetComponentsInChildren<UISprite>();
        BoxCollider[] UiCollider = UISet2.GetComponentsInChildren<BoxCollider>();
        for (int uiindex=0; uiindex< uiLabels.Length; uiindex++)
        {
            uiLabels[uiindex].alpha = 0f;
        }
        for (int uiindex = 0; uiindex < uiSprites.Length; uiindex++)
        {
            uiSprites[uiindex].alpha = 0f;
        }
        for (int uiindex = 0; uiindex < UiCollider.Length; uiindex++)
        {
            UiCollider[uiindex].enabled = false;
        }
        //나머지 요소 가리기

        SpriteRenderer[] sprites = currentCustomer.GetComponentsInChildren<SpriteRenderer>();
        for(int index = 0; index < sprites.Length; index++)
        {
            sprites[index].sortingLayerName = "SpotCharacter";
        }

        SpriteRenderer hiderSprite = GameObject.Find("HideScreen").GetComponent<SpriteRenderer>();
        hiderSprite.color = new Color(0.5f, 0.5f, 0.5f, 1f);

        //카메라 줌인
        GameObject mainCamera = GameObject.Find("Main Camera");
        StartCoroutine("CameraSpoting",mainCamera);
    }

    IEnumerator CameraSpoting(GameObject mainCamera)
    {
        yield return null;

        if (mainCamera.transform.position.y <-1.28f)//카메라가 멈췄을때
        {
            yield return new WaitForSeconds(0.5f);
            GameObject spotUiset = GameObject.Find("Spot Set");

            UILabel nameLabel = spotUiset.GetComponentsInChildren<UILabel>()[0];
            UILabel descriptionLabel = spotUiset.GetComponentsInChildren<UILabel>()[1];

            nameLabel.text = currentCustomer.CustomerName;
            descriptionLabel.text = currentCustomer.CustomerDecript;

            StartCoroutine(SpotTitle(mainCamera, nameLabel, descriptionLabel));
            yield break;
        }
        Vector3 dictPos = new Vector3(1f,-1.3f,-10f);
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, dictPos, 0.1f);

        Camera cameraScript = mainCamera.GetComponent<Camera>();
        cameraScript.orthographicSize = Mathf.Lerp(cameraScript.orthographicSize, 2f, 0.1f);

        StartCoroutine("CameraSpoting", mainCamera);
    }

    IEnumerator SpotTitle(GameObject mainCamera, UILabel nameLabel, UILabel descriptionLabel)
    {
        yield return null;


        if (descriptionLabel.transform.localPosition.y < -201)
        {
            // 이름 오버레이
            Vector3 distPos1 = new Vector3(-1280, 200, 0f);
            nameLabel.transform.localPosition = Vector3.Lerp(nameLabel.transform.localPosition, distPos1, 0.1f);
            // 설명 오버레이
            Vector3 distPos2 = new Vector3(-1280, -200, 0f);
            descriptionLabel.transform.localPosition = Vector3.Lerp(descriptionLabel.transform.localPosition, distPos2, 0.1f);
        }

        else if (descriptionLabel.transform.localPosition.y == -200)
        {
            yield return new WaitForSeconds(3f);
            Vector3 distPos2 = new Vector3(-1280, -199, 0f);
            descriptionLabel.transform.localPosition = distPos2;
        }

        else if (descriptionLabel.transform.localPosition.y <= 2449)
        {
            Vector3 distPos1 = new Vector3(-1280, 2650, 0f);
            nameLabel.transform.localPosition = Vector3.Lerp(nameLabel.transform.localPosition, distPos1, 0.1f);
            Vector3 distPos2 = new Vector3(-1280, 2450, 0f);
            descriptionLabel.transform.localPosition = Vector3.Lerp(descriptionLabel.transform.localPosition, distPos2, 0.1f);
        }
        else if(descriptionLabel.transform.localPosition.y > 2449)
        {
            Vector3 distPos1 = new Vector3(-1280, -2100, 0f);
            nameLabel.transform.localPosition = distPos1;
            Vector3 distPos2 = new Vector3(-1280, -2300, 0f);
            descriptionLabel.transform.localPosition = distPos2;

            StartCoroutine("CameraReturning", mainCamera);
            yield break;
        }

        StartCoroutine(SpotTitle(mainCamera, nameLabel, descriptionLabel));
    }

    IEnumerator CameraReturning(GameObject mainCamera)
    {
        yield return null;
        Vector3 dictPos = new Vector3(0f, 0f, -10f);
        if (mainCamera.transform.position.y > -0.01f)
        {
            mainCamera.transform.position = dictPos;

           // TimeManager timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
           //timeManager.StartCoroutine("TimeFlow");
           // TimeManager.nowFlow = true;

            // 모든 uI 보이기
            GameObject sellingSet = GameObject.Find("Selling Set");
            UILabel[] sellingLabels = sellingSet.GetComponentsInChildren<UILabel>();
            UISprite[] sellingsprites = sellingSet.GetComponentsInChildren<UISprite>();
            BoxCollider[] sellingCollider = sellingSet.GetComponentsInChildren<BoxCollider>();
           
            for (int index = 0; index < sellingLabels.Length; index++)
            {
                sellingLabels[index].alpha = 1f;
            }
            for (int index = 0; index < sellingsprites.Length; index++)
            {
                sellingsprites[index].alpha = 1f;
            }
            for (int index = 0; index < sellingCollider.Length; index++)
            {
                sellingCollider[index].enabled = true;
            }

            GameObject UISet2 = GameObject.Find("UI Set 2");
            UILabel[] uiLabels = UISet2.GetComponentsInChildren<UILabel>();
            UISprite[] uiSprites = UISet2.GetComponentsInChildren<UISprite>();
            BoxCollider[] UiCollider = UISet2.GetComponentsInChildren<BoxCollider>();

            for (int uiindex = 0; uiindex < uiLabels.Length; uiindex++)
            {
                uiLabels[uiindex].alpha = 1f;
            }
            for (int uiindex = 0; uiindex < uiSprites.Length; uiindex++)
            {
                uiSprites[uiindex].alpha = 1f;
            }
            for (int uiindex = 0; uiindex < UiCollider.Length; uiindex++)
            {
                UiCollider[uiindex].enabled = true;
            }
            //나머지 요소 가리기

            SpriteRenderer[] sprites = currentCustomer.GetComponentsInChildren<SpriteRenderer>();
            for (int index = 0; index < sprites.Length; index++)
            {
                sprites[index].sortingLayerName = "Char";
            }

            SpriteRenderer hiderSprite = GameObject.Find("HideScreen").GetComponent<SpriteRenderer>();
            hiderSprite.color = new Color(0.5f, 0.5f, 0.5f, 0f);
            SpeechTypeC();

            yield break;
        }
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, dictPos, 0.1f);

        Camera cameraScript = mainCamera.GetComponent<Camera>();
        cameraScript.orthographicSize = Mathf.Lerp(cameraScript.orthographicSize, 5f, 0.1f);
        StartCoroutine("CameraReturning", mainCamera);
    }

    public void SpeechTypeC()
    {
        EventStart(true);
        Animator animator = currentCustomer.GetComponent<Animator>();
        animator.SetBool("isIntro", false);
    }
    
    public void BossItemGiv(CustomerControl customer)
    {
        GameObject OuttedPrice;
        Rigidbody2D Pricerigid;
        Vector2 outPower;
        Transform PriceOutPos = GameObject.Find("Price_Position").transform;
        OuttedPrice = Instantiate(customer.CustomerPrice, PriceOutPos);
        Pricerigid = OuttedPrice.GetComponent<Rigidbody2D>();
        outPower = new Vector2(-3f, Random.Range(5f, 6f));
        Pricerigid.AddForce(outPower, ForceMode2D.Impulse);
        Pricerigid.AddTorque(90f);
    }
}
