using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarchentManager : MonoBehaviour {

    TimeManager TimeCheck;
    public PlayerControl PlayerSad;
    public static int BuyCount;
    //-----------------------------------------
    public GameObject FurnitureMarchent;
    public GameObject NightMarchent;
    public GameObject NightUIButton;
    public Transform ItemOutPos;
    //-----------------------------------------

    public GameObject NightMarchentItem1;
    public GameObject NightMarchentItem2;
    public GameObject NightMarchentItem3;
    public GameObject NightMarchentItem4;
    public GameObject NightMarchentItem5;
    public GameObject NightMarchentItem6;
    public GameObject NightMarchentItem7;
    public GameObject NightMarchentItem8;

    //-----------------------------------------
    public Transform SListPos;
    public GameObject SaleLists1;
    public GameObject SaleLists2;
    public GameObject SaleLists3;
    public GameObject SaleLists4;
    public GameObject SaleLists5;
    public GameObject SaleLists6;
    public GameObject SaleLists7;
    public GameObject SaleLists8;
    //-----------------------------------------
    public GameObject MarchentItem1;
    public GameObject MarchentItem2;
    public GameObject MarchentItem3;
    //-----------------------------------------
    public GameObject[] SList;
    int SaleingListNum;
    public float Listy;
    public bool isNightMarchent;
    Vector3 NightMarchentPos;
    Vector3 DayMarchentPos;
    public int SaleingListIndex;

    public void DataAccess(DataVo dataVo)
    {
        SaleingListIndex = int.Parse(dataVo.SalingNumSave);
        SList = new GameObject[SaleingListIndex];

        Listy = 0f;
        TimeCheck = GameObject.Find("TimeManager").GetComponent<TimeManager>();
    }

    public void TimeMarchent()
    { 

        UIButton NightbuttonScript = NightUIButton.GetComponent<UIButton>();
        BoxCollider Nightcollider = NightUIButton.GetComponent<BoxCollider>();

        if (TimeCheck.TimeNight)
        {
            if (PlayerSad.isFurniture)
            {
                OutFurnitureMarchent();
            }

            SaleingListUpdate();
            FurnitureMarchent.SetActive(true);
            NightMarchent.SetActive(true);
            Nightcollider.enabled = true;
            NightbuttonScript.state = UIButtonColor.State.Normal;
        }
        else
        {
            for (int index = 0; index < SaleingListIndex; index++)
            {
                Destroy(SList[index]);
            }

            if (isNightMarchent)
            {
                OutMapMarchent();
            }
            if (PlayerSad.isFurniture)
            {
                OutFurnitureMarchent();
            }
            NightMarchent.SetActive(false);

            FurnitureMarchent.SetActive(true);
            Nightcollider.enabled = false;
            NightbuttonScript.state = UIButtonColor.State.Disabled;
        }
    }

    public void OutMapMarchent()//상인이 없어진 경우 발동
    {
        if (isNightMarchent)
        {
            isNightMarchent = false;

            GameObject NightMarchentSet = GameObject.Find("Night Marchent Set");
            NightMarchentPos = NightMarchentSet.transform.position;
            NightMarchentPos.y += 8f;
            NightMarchentSet.transform.position = NightMarchentPos;
        }


    }

    public void OutFurnitureMarchent()
    {
         if(PlayerSad.isFurniture)
        {
            PlayerSad.isFurniture = false;
            GameObject DayMarchentSet = GameObject.Find("Furniture Set");
            DayMarchentPos = DayMarchentSet.transform.position;
            DayMarchentPos.y += 9f;
            DayMarchentSet.transform.position = DayMarchentPos;
        }
    }

    public void PushMarchentButton()//야상인 버튼 클릭시 발동
    {
            SoundManager.sounds["BookOpen"].Play();
            if (isNightMarchent == false)
            {
                isNightMarchent = true;

                GameObject NightMarchentSet = GameObject.Find("Night Marchent Set");
                NightMarchentPos = NightMarchentSet.transform.position;
                NightMarchentPos.y -= 8f;
            }
            else
            {
                isNightMarchent = false;

                GameObject NightMarchentSet = GameObject.Find("Night Marchent Set");
                NightMarchentPos = NightMarchentSet.transform.position;
                NightMarchentPos.y += 8f;
            }

            StartCoroutine("PopupNightMarchent");
        
    }

    IEnumerator PopupNightMarchent()//상점창 팝업 코루틴
    {
        yield return null;

        Transform UINightMarchentSet = GameObject.Find("Night Marchent Set").transform;

        StartCoroutine("PopupNightMarchent");

        if (UINightMarchentSet.position == NightMarchentPos)
        {
            StopCoroutine("PopupNightMarchent");
            yield break;
        }
        UINightMarchentSet.position = Vector3.Lerp(UINightMarchentSet.position, NightMarchentPos, 0.5f);
    }

    public void SaleingListUpdate()
    {
        switch (TimeManager.Day)
        {
            case 7:
                SaleingListIndex = 6;
                break;
            case 15:
                SaleingListIndex = 10;
                break;
        }

        SList = new GameObject[SaleingListIndex];

        for (int MarchentIndex = 0; MarchentIndex < SaleingListIndex; MarchentIndex++)
        {
            SaleingListNum = Random.Range(0, 9);
            switch (SaleingListNum)
            {
                case 0:

                    SList[MarchentIndex] = Instantiate(SaleLists1);
                    SList[MarchentIndex].transform.parent = SListPos.transform;

                    SList[MarchentIndex].transform.localPosition = new Vector3(0f, Listy, 0f);
                    SList[MarchentIndex].transform.localScale = Vector3.one;
                    Listy -= 400f;
                    break;

                case 1:
                    SList[MarchentIndex] = Instantiate(SaleLists2);
                    SList[MarchentIndex].transform.parent = SListPos.transform;

                    SList[MarchentIndex].transform.localPosition = new Vector3(0f, Listy, 0f);
                    SList[MarchentIndex].transform.localScale = Vector3.one;
                    Listy -= 400f;
                    break;

                case 2:
                    SList[MarchentIndex] = Instantiate(SaleLists3);
                    SList[MarchentIndex].transform.parent = SListPos.transform;

                    SList[MarchentIndex].transform.localPosition = new Vector3(0f, Listy, 0f);
                    SList[MarchentIndex].transform.localScale = Vector3.one;
                    Listy -= 400f;
                    break;

                case 3:
                    SList[MarchentIndex] = Instantiate(SaleLists4);
                    SList[MarchentIndex].transform.parent = SListPos.transform;

                    SList[MarchentIndex].transform.localPosition = new Vector3(0f, Listy, 0f);
                    SList[MarchentIndex].transform.localScale = Vector3.one;
                    Listy -= 400f;
                    break;

                case 4:
                    SList[MarchentIndex] = Instantiate(SaleLists5);
                    SList[MarchentIndex].transform.parent = SListPos.transform;

                    SList[MarchentIndex].transform.localPosition = new Vector3(0f, Listy, 0f);
                    SList[MarchentIndex].transform.localScale = Vector3.one;
                    Listy -= 400f;
                    break;

                case 5:
                    SList[MarchentIndex] = Instantiate(SaleLists6);
                    SList[MarchentIndex].transform.parent = SListPos.transform;

                    SList[MarchentIndex].transform.localPosition = new Vector3(0f, Listy, 0f);
                    SList[MarchentIndex].transform.localScale = Vector3.one;
                    Listy -= 400f;
                    break;

                case 6:
                    SList[MarchentIndex] = Instantiate(SaleLists7);
                    SList[MarchentIndex].transform.parent = SListPos.transform;

                    SList[MarchentIndex].transform.localPosition = new Vector3(0f, Listy, 0f);
                    SList[MarchentIndex].transform.localScale = Vector3.one;
                    Listy -= 400f;
                    break;

                case 7:
                    SList[MarchentIndex] = Instantiate(SaleLists8);
                    SList[MarchentIndex].transform.parent = SListPos.transform;

                    SList[MarchentIndex].transform.localPosition = new Vector3(0f, Listy, 0f);
                    SList[MarchentIndex].transform.localScale = Vector3.one;
                    Listy -= 400f;
                    break;
                default:
                    break;
            }

        }
    }

    public void MarchentingItem1()
    {
        if (StatusManager.Point >= 500)
        {
            SoundManager.sounds["BuyFromMarchent"].Play();
            StatusManager.Point -= 500;
            StatusManager.TodayPoint -= 500;
            Transform PriceOutPos = GameObject.Find("Price_Position").transform;
            GameObject OuttedPrice = Instantiate(MarchentItem1, PriceOutPos);
            Rigidbody2D Pricerigid = OuttedPrice.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(-4f, Random.Range(5f, 7f));
            Pricerigid.AddForce(outPower, ForceMode2D.Impulse);
            Pricerigid.AddTorque(90f);
            BuyCount += 1;
        }
        else
        {
            SoundManager.sounds["NoMoney"].Play();
            PlayerSad.PopUpBallon();
        }
    }

    public void MarchentingItem2()
    {
        if (StatusManager.Point >= 400)
        {
            SoundManager.sounds["BuyFromMarchent"].Play();
            StatusManager.Point -= 400;
            StatusManager.TodayPoint -= 400;
            Transform PriceOutPos = GameObject.Find("Price_Position").transform;
            GameObject OuttedPrice = Instantiate(MarchentItem2, PriceOutPos);
            Rigidbody2D Pricerigid = OuttedPrice.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(-4f, Random.Range(5f, 7f));
            Pricerigid.AddForce(outPower, ForceMode2D.Impulse);
            Pricerigid.AddTorque(90f);
            BuyCount += 1;
        }
        else
        {
            SoundManager.sounds["NoMoney"].Play();
            PlayerSad.PopUpBallon();
        }
    }

    public void MarchentingItem3()
    {
        if (StatusManager.Point >= 700)
        {
            SoundManager.sounds["BuyFromMarchent"].Play();
            StatusManager.Point -= 700;
            StatusManager.TodayPoint -= 700;
            Transform PriceOutPos = GameObject.Find("Price_Position").transform;
            GameObject OuttedPrice = Instantiate(MarchentItem3, PriceOutPos);
            Rigidbody2D Pricerigid = OuttedPrice.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(-4f, Random.Range(5f, 7f));
            Pricerigid.AddForce(outPower, ForceMode2D.Impulse);
            Pricerigid.AddTorque(90f);
            BuyCount += 1;
        }
        else
        {
            SoundManager.sounds["NoMoney"].Play();
            PlayerSad.PopUpBallon();
        }
    }

    //-------------------------------------------------------------

    public void NightSellItem1()
    {
        if (StatusManager.Point >= 700)//붉은 허브 판매가
        {
            SoundManager.sounds["BuyFromMarchent"].Play();
            StatusManager.Point -= 700;
            StatusManager.TodayPoint -= 700;
            GameObject OuttedItem = Instantiate(NightMarchentItem1, ItemOutPos);
            Rigidbody2D Pricerigid = OuttedItem.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f,1f), Random.Range(5f, 7f));
            Pricerigid.AddForce(outPower, ForceMode2D.Impulse);
            Pricerigid.AddTorque(90f);
            Destroy(UIButton.current.gameObject);//클릭된 버튼을 제거하는 명령어!!!! 
        }
        else
        {
            SoundManager.sounds["NoMoney"].Play();
            PlayerSad.PopUpBallon();
        }
    }

    public void NightSellItem2()
    {
        if (StatusManager.Point >= 1400)//검정색 깃털 판매가
        {
            SoundManager.sounds["BuyFromMarchent"].Play();
            StatusManager.Point -= 1400;
            StatusManager.TodayPoint -= 1400;
            GameObject OuttedItem = Instantiate(NightMarchentItem2, ItemOutPos);
            Rigidbody2D Pricerigid = OuttedItem.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(5f, 7f));
            Pricerigid.AddForce(outPower, ForceMode2D.Impulse);
            Pricerigid.AddTorque(90f);
            Destroy(UIButton.current.gameObject);
        }
        else
        {
            SoundManager.sounds["NoMoney"].Play();
            PlayerSad.PopUpBallon();
        }
    }

    public void NightSellItem3()
    {
        if (StatusManager.Point >= 700)//깃털 판매가
        {
            SoundManager.sounds["BuyFromMarchent"].Play();
            StatusManager.Point -= 700;
            StatusManager.TodayPoint -= 700;
            GameObject OuttedItem = Instantiate(NightMarchentItem3, ItemOutPos);
            Rigidbody2D Pricerigid = OuttedItem.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(5f, 7f));
            Pricerigid.AddForce(outPower, ForceMode2D.Impulse);
            Pricerigid.AddTorque(90f);
            Destroy(UIButton.current.gameObject);
        }
        else
        {
            SoundManager.sounds["NoMoney"].Play();
            PlayerSad.PopUpBallon();
        }
    }

    public void NightSellItem4()
    {
        if (StatusManager.Point >= 400)//나뭇가지 판매가
        {
            SoundManager.sounds["BuyFromMarchent"].Play();
            StatusManager.Point -= 400;
            StatusManager.TodayPoint -= 400;
            GameObject OuttedItem = Instantiate(NightMarchentItem4, ItemOutPos);
            Rigidbody2D Pricerigid = OuttedItem.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(5f, 7f));
            Pricerigid.AddForce(outPower, ForceMode2D.Impulse);
            Pricerigid.AddTorque(90f);
            Destroy(UIButton.current.gameObject);
        }
        else
        {
            SoundManager.sounds["NoMoney"].Play();
            PlayerSad.PopUpBallon();
        }
    }
    public void NightSellItem5()
    {
        if (StatusManager.Point >= 200)//토마토 판매가
        {
            SoundManager.sounds["BuyFromMarchent"].Play();
            StatusManager.Point -= 200;
            StatusManager.TodayPoint -= 200;
            GameObject OuttedItem = Instantiate(NightMarchentItem5, ItemOutPos);
            Rigidbody2D Pricerigid = OuttedItem.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(5f, 7f));
            Pricerigid.AddForce(outPower, ForceMode2D.Impulse);
            Pricerigid.AddTorque(90f);
            Destroy(UIButton.current.gameObject);
        }
        else
        {
            SoundManager.sounds["NoMoney"].Play();
            PlayerSad.PopUpBallon();
        }
    }
    public void NightSellItem6()
    {
        if (StatusManager.Point >= 500)//더 판매가
        {
            SoundManager.sounds["BuyFromMarchent"].Play();
            StatusManager.Point -= 500;
            StatusManager.TodayPoint -= 500;
            GameObject OuttedItem = Instantiate(NightMarchentItem6, ItemOutPos);
            Rigidbody2D Pricerigid = OuttedItem.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(5f, 7f));
            Pricerigid.AddForce(outPower, ForceMode2D.Impulse);
            Pricerigid.AddTorque(90f);
            Destroy(UIButton.current.gameObject);
        }
        else
        {
            SoundManager.sounds["NoMoney"].Play();
            PlayerSad.PopUpBallon();
        }
    }
    public void NightSellItem7()
    {
        if (StatusManager.Point >= 700)//더미 판매가
        {
            SoundManager.sounds["BuyFromMarchent"].Play();
            StatusManager.Point -= 700;
            StatusManager.TodayPoint -= 700;
            GameObject OuttedItem = Instantiate(NightMarchentItem7, ItemOutPos);
            Rigidbody2D Pricerigid = OuttedItem.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(5f, 7f));
            Pricerigid.AddForce(outPower, ForceMode2D.Impulse);
            Pricerigid.AddTorque(90f);
            Destroy(UIButton.current.gameObject);
        }
        else
        {
            SoundManager.sounds["NoMoney"].Play();
            PlayerSad.PopUpBallon();
        }
    }

    public void NightSellItem8()
    {
        if (StatusManager.Point >= 500)//설탕 판매가
        {
            SoundManager.sounds["BuyFromMarchent"].Play();
            StatusManager.Point -= 500;
            StatusManager.TodayPoint -= 500;
            GameObject OuttedItem = Instantiate(NightMarchentItem8, ItemOutPos);
            Rigidbody2D Pricerigid = OuttedItem.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(5f, 7f));
            Pricerigid.AddForce(outPower, ForceMode2D.Impulse);
            Pricerigid.AddTorque(90f);
            Destroy(UIButton.current.gameObject);
        }
        else
        {
            SoundManager.sounds["NoMoney"].Play();
            PlayerSad.PopUpBallon();
        }
    }
}
