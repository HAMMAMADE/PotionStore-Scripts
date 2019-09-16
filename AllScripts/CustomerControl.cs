using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerControl : MonoBehaviour {

    Animator animator;

    public GameObject CustomerPrice;//손님이 플레이어에게 주는 보상

    public GameObject CustomerPrice2;

    public GameObject CustomerPrice3;

    public GameObject Emotion_Good;
    public GameObject Emotion_SoSo;
    public GameObject Emotion_Bad;

    public string CustomerName;
    //public string CustomerGrade;//손님의 등급 (삭제예정)
    public string CustomerType;//손님의 타입, 일반손님, 잡상인, 보스손님
    public Sprite CustomerIcon;//손님아이콘
    [TextArea]
    public string CustomerOrder;//손님의 주문
    [TextArea]
    public string CustomerDecript;//손님 설명
    [TextArea]
    public string CustomerWaitOrder;//대기 손님시 부름 대사

    public int EvaUpPoint;
    public int EvaDownPoint;
    public int AddInfluNum;
    public int MarchentLevel;//잡상인 레벨, 레벨에 따라 다른 물건을 판매함
    public int CustomerWaitLimit;//손님 대기 제한 시간
    public int WaitTime;
    public bool isOrder;//손님이 주문 했는지 확인
    public bool isMarchent;//도착한 손님이 상인인지 확인
    public bool isWaiting;//주문한 손님이 아닌 경우 기다리는 손님
    public bool isBack;//손님이 돌아가는 경우
    public bool isGoHome;//밤 시간에 모든 손님이 돌아가는 경우
    public bool isOk;//주문에 맞는 포션을 받은 경우
    public bool StopWait;
    public bool isDebuff;

    public int CommuMax;//보스 손님의 경우는 대사 이벤트가 있음 그거와 관련된 변수
    public int CommuCurrentIndex;
    public int[] CommuNum;
    
    [TextArea]
    public string[] CommuSpeechs;

    public int[] WantPotionType = new int[3];//원하는 포션의 종류를 확인하는 변수

    FurnitureManager FurnitureEffect;// 
    CustomerManager customerManager;
    SellingManager Orderd;
    InfluenceManager AddInFlu;
    StatusManager State;

    public Transform GrapPoint;

    bool isBuy;
 

    int AllEvaPoint;
    float EmotionCount;

    void Awake()
    {
        StopWait = false;
        EmotionCount = 0;
        animator = GetComponent<Animator>();

        FurnitureEffect = GameObject.Find("FurnitureManager").GetComponent<FurnitureManager>();
        AddInFlu = GameObject.Find("InfluenceManager").GetComponent<InfluenceManager>();
        Orderd = GameObject.Find("SellingManager").GetComponent<SellingManager>();
        customerManager = GameObject.Find("CustomerManager").GetComponent<CustomerManager>();
        State = GameObject.Find("StatusManager").GetComponent<StatusManager>();
    }	

	void Update () {
        if (isOrder)//주문하고 물건을 받을때 isOrder는 true가 된다.
        {
            if (customerManager.firstAlert) customerManager.EndAlert();
            Orderd.Ordering = true;
            GameObject CompletedMedicine = GameObject.Find("CompleteMedicine(Clone)");
            if (CompletedMedicine != null)
            {
                if (Orderd.IsComplete)
                {
                    isOrder = false;
                    isWaiting = false;
                    Orderd.IsComplete = false;

                    CompletedMedicine.name = "CompleteMedicine(Clone)(Get)";
                    Transform CompletedMedicineTrans = CompletedMedicine.transform;
                    CompletedMedicineTrans.SetParent(GrapPoint);
                    CompletedMedicineTrans.localPosition = Vector3.zero;

                    Vector3 backPos = transform.position;
                    backPos.z = -2;
                    transform.position = backPos;

                    for (int index = 0; index <= 2; index++)
                    {
                        int checkNum =  Array.IndexOf<int>(WantPotionType, ComPotionParmeter.PotionType[index]);
                        if(checkNum >= 0)
                        {
                            EmotionCount += 1f;
                            StatusManager.EvaPoint += EvaUpPoint;
                            AllEvaPoint += EvaUpPoint;
                            isOk = true;
                        }
                        else
                        {
                            EmotionCount -= 1f;
                            StatusManager.EvaPoint -= EvaDownPoint;
                            AllEvaPoint -= EvaDownPoint;
                        }
                    }
                    if(EmotionCount > 1f)
                    {
                        StartCoroutine("GoodEmotion");
                    }
                    else if(EmotionCount <= 1f && EmotionCount >= -1f)
                    {
                        StartCoroutine("SosoEmotion");
                    }
                    else if (EmotionCount < 1f)
                    {
                        StartCoroutine("BadEmotion");
                    }
                    if (isOk)//원하는 포션을 받았을 경우 
                    {
                        AddInFlu.MoveInfluenceFunc(AddInfluNum);
                        //int PricePersent = Random.Range(0, PriceMaxNum);
                        //if (PricePersent >= PriceMinNum)
                        //{
                            GameObject OuttedPrice;//일정 확률로 재료를 보상으로 준다.
                            Rigidbody2D Pricerigid;
                            Vector3 outPower;
                            Transform PriceOutPos = GameObject.Find("Price_Position").transform;
                            int PriceDice = UnityEngine.Random.Range(0, 2);
                        if (!isDebuff)
                        {
                            switch (PriceDice)
                            {
                                case 0:
                                    OuttedPrice = Instantiate(CustomerPrice, PriceOutPos);
                                    Pricerigid = OuttedPrice.GetComponent<Rigidbody2D>();
                                    outPower = new Vector2(-3f, UnityEngine.Random.Range(5f, 6f));
                                    Pricerigid.AddForce(outPower, ForceMode2D.Impulse);
                                    Pricerigid.AddTorque(90f);
                                    break;
                                case 1:
                                    OuttedPrice = Instantiate(CustomerPrice2, PriceOutPos);
                                    Pricerigid = OuttedPrice.GetComponent<Rigidbody2D>();
                                    outPower = new Vector2(-3f, UnityEngine.Random.Range(5f, 6f));
                                    Pricerigid.AddForce(outPower, ForceMode2D.Impulse);
                                    Pricerigid.AddTorque(90f);
                                    break;
                                case 2:
                                    OuttedPrice = Instantiate(CustomerPrice3, PriceOutPos);
                                    Pricerigid = OuttedPrice.GetComponent<Rigidbody2D>();
                                    outPower = new Vector2(-3f, UnityEngine.Random.Range(5f, 6f));
                                    Pricerigid.AddForce(outPower, ForceMode2D.Impulse);
                                    Pricerigid.AddTorque(90f);
                                    break;

                            }
                        }
                       // }
                    }
                    else
                    {
                        AddInFlu.MoveInfluenceFunc(-AddInfluNum);
                    }
                    StatusManager.Point += (int)(Orderd.AllPrice * customerManager.UpPrice);
                    StatusManager.TodayPoint += (int)(Orderd.AllPrice * customerManager.UpPrice);

                    if (isDebuff)
                    {
                        StatusManager.Point += 1000;
                        StatusManager.TodayPoint += 1000;
                    }

                    State.GUIPopUp((int)(Orderd.AllPrice * customerManager.UpPrice), AllEvaPoint);
                    Orderd.AllPrice = 0;
                    customerManager.OrderEnd(gameObject);
                    isBuy = true;
                    isBack = true;
                    StartCoroutine("TurnBack");
                    ComPotionParmeter.Reset();
                }
            }
            if (TimeManager.nowFlow) WaitTime++;
            else WaitTime += 0;
        }
        else if(isMarchent)//상인이 온 경우
        {
            Orderd.Ordering = false;
            if (MarchentManager.BuyCount > 2 || CustomerManager.AnSaYo == true)
            {
                isOrder = false;
                isMarchent = false;
                isWaiting = false;
                customerManager.MarchentEnd(gameObject);
                MarchentManager.BuyCount = 0;

                isBack = true;
                Vector3 backPos = transform.position;
                backPos.z = -2;
                transform.position = backPos;
                CustomerManager.AnSaYo = false;
                StartCoroutine("TurnBack");
            }
            if (TimeManager.nowFlow) WaitTime++;
            else WaitTime += 0;
        }
        else if (isWaiting)//단순 기다리는 경우
        {
            return;
        }
        else if (isGoHome)//밤 시간이 되어 집으로 가라는 경우
        {
            if(customerManager.firstAlert) customerManager.EndAlert();
            if (!isOrder)
            {
               // customerManager.EndAlert();
                Orderd.Ordering = false;
                isOrder = false;
                isWaiting = false;
                isBack = true;
                customerManager.OrderEnd(gameObject);
                customerManager.MarchentEnd(gameObject);
                MarchentManager.BuyCount = 0;

                StartCoroutine("TurnBack");
                isGoHome = false;
                WaitTime = 0;
            }
        }

        else if (isBack)//포션을 받고 돌아가는 경우
        {
            if (customerManager.firstAlert) customerManager.EndAlert();
            StopWait = true;
            if (isBuy)
            {
                animator.SetBool("CantBuy", true);
            }
            else
            {
                animator.SetBool("NoItemBuy", true);
            }
            Orderd.Ordering = false;
            isOrder = false;
            isWaiting = false;
            if (transform.position.x > 10f)
            {
                customerManager.SpawnCount--;
                Destroy(gameObject);
                isBack = false;
            }
            Vector3 nextPos = transform.position;
            nextPos.x += 3f * Time.deltaTime;
            transform.position = nextPos;
        }
        else if(customerManager.CustomersPos[customerManager.Customers.IndexOf(gameObject)].x < transform.position.x)//손님의 위치에 따라서 움직이는지 안 움직이는지 여부를 확인함
        {
            animator.SetBool("IsWalking", true);
            Vector3 nextPos = transform.position;
            nextPos.x -= 3f * Time.deltaTime;
            transform.position = nextPos;
        }
        else if(customerManager.CustomersPos[customerManager.Customers.IndexOf(gameObject)].x >= transform.position.x)//카운터 앞으로 이동한 경우
        {
            animator.SetBool("IsWalking", false);
           // Orderd.Ordering = true;
            if (customerManager.Customers.IndexOf(gameObject) == 0)
            {
                if (Orderd.isReadySell)
                {
                    switch (CustomerType)
                    {
                        case "A":
                            isWaiting = false;
                            isOrder = true;
                            isMarchent = false;
                            customerManager.CustomerOrder(this);
                            break;
                        case "B":
                            isWaiting = false;
                            isOrder = false;
                            isMarchent = true;
                            Orderd.Ordering = false;
                            customerManager.CustomerOrder(this);
                            break;
                        case "C":
                            isWaiting = false;
                            isOrder = true;
                            isMarchent = false;
                            //Orderd.Ordering = false;
                            customerManager.CustomerOrder(this);
                            break;
                    }
                }
                else
                {
                    customerManager.StartAlert(this);
                }
                
             if (TimeManager.nowFlow) WaitTime++;
            else WaitTime += 0;
            }
        }
        if (WaitTime  >= 60 * (CustomerWaitLimit + FurnitureEffect.Furniture3Effect))//대기 시간이 모두 되었을때 발동
        {
            StartCoroutine("BadEmotion");
            if (customerManager.firstAlert) customerManager.EndAlert();
            Vector3 backPos = transform.position;
            backPos.z = -2;
            transform.position = backPos;
            if (isMarchent)
            {
                customerManager.MarchentEnd(gameObject);
                Orderd.Ordering = false;
                isMarchent = false;
            }
            else
            {
                AddInFlu.MoveInfluenceFunc(-10);
                Orderd.Ordering = false;
                StatusManager.EvaPoint -= EvaDownPoint; ;
                customerManager.OrderEnd(gameObject);
            }
            isOrder = false;
            isWaiting = false;
            isBack = true;
            StartCoroutine("TurnBack");
            WaitTime = 0;
        }

    }

    IEnumerator TurnBack()//손님이 물건을 받고 뒤도는 코루틴
    {
        yield return null;
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(transform.localScale.x+2f, 1f, 1f), 0.1f);
        
        if(transform.localScale.x > 0.98f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - 3f);
            transform.localScale = new Vector3(1f, 1f, 1f);
            StopCoroutine("TurnBack");
            yield break;
        }
        
        StartCoroutine("TurnBack");
    }

    IEnumerator GoodEmotion()//손님의 만족도를 표시
    {
        yield return null;
        Vector3 nextScale = Emotion_Good.transform.localScale;
        if (nextScale.x < 0.9f)
        {
            nextScale.x += 0.12f;
            Emotion_Good.transform.localScale = nextScale;
        }
        else
        {
            nextScale.x = 1f;
            Emotion_Good.transform.localScale = nextScale;
            yield return new WaitForSeconds(1f);
            StopCoroutine("GoodEmotion");
            yield break;
        }
        StartCoroutine("GoodEmotion");
    }

    IEnumerator SosoEmotion()//손님의 만족도를 표시
    {
        yield return null;
        Vector3 nextScale = Emotion_SoSo.transform.localScale;
        if (nextScale.x < 0.9f)
        {
            nextScale.x += 0.12f;
            Emotion_SoSo.transform.localScale = nextScale;
        }
        else
        {
            nextScale.x = 1f;
            Emotion_SoSo.transform.localScale = nextScale;
            yield return new WaitForSeconds(1f);
            StopCoroutine("SosoEmotion");
            yield break;
        }
        StartCoroutine("SosoEmotion");
    }

    IEnumerator BadEmotion()//손님의 만족도를 표시
    {
        yield return null;
        Vector3 nextScale = Emotion_Bad.transform.localScale;
        if (nextScale.x < 0.9f)
        {
            nextScale.x += 0.12f;
            Emotion_Bad.transform.localScale = nextScale;
        }
        else
        {
            nextScale.x = 1f;
            Emotion_Bad.transform.localScale = nextScale;
            yield return new WaitForSeconds(1f);
            StopCoroutine("BadEmotion");
            yield break;
        }
        StartCoroutine("BadEmotion");
    }

}
