using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureManager : MonoBehaviour {


    MessageManager Alert;
    Vector3 MoveFurnBuyPos;

    public int Furniture1Level;
    public int Furniture2Level;
    public int Furniture3Level;
    //-----------------------------------------------------------
    public SpriteRenderer Furniture1;
    public SpriteRenderer Furniture2;
    public SpriteRenderer Furniture3;

    public Sprite nextFurniture1_1;
    public Sprite nextFurniture1_2;
    public Sprite nextFurniture1_3;
    public Sprite nextFurniture2_1;
    public Sprite nextFurniture2_2;
    public Sprite nextFurniture2_3;
    public Sprite nextFurniture3_1;
    public Sprite nextFurniture3_2;
    public Sprite nextFurniture3_3;
    //-----------------------------------------------------------

    public UILabel Furniture1Info;
    public UILabel Furniture2Info;
    public UILabel Furniture3Info;

    public UILabel Furniture1MatInfo;
    public UILabel Furniture2MatInfo;
    public UILabel Furniture3MatInfo;

    public UISprite Furniture1icon;
    public UISprite Furniture2icon;
    public UISprite Furniture3icon;

    public UISprite BuyFurniture1icon;
    public UISprite BuyFurniture2icon;
    public UISprite BuyFurniture3icon;

    public UISprite UpFurniture1icon;
    public UISprite UpFurniture2icon;
    public UISprite UpFurniture3icon;

    public GameObject Slot1Furniture;
    public GameObject Slot2Furniture;
    public GameObject Slot3Furniture;

    int Slot1Num;
    public bool PushedBuyButton;
    public int Furniture3Effect;
    public float Furniture3_3Effect;

    //----------데이터로드--------------------------
    public void DataAccess(DataVo dataVo)
    {
        Furniture1Level = int.Parse(dataVo.SaveFur1Lev);
        Furniture2Level = int.Parse(dataVo.SaveFur2Lev);
        Furniture3Level = int.Parse(dataVo.SaveFur3Lev);

        Alert = GameObject.Find("MessageManager").GetComponent<MessageManager>();
        UpdateFurniture();
    }

    //-----------------------------------------------

    public void PushBuyFurniture()
    {
        SoundManager.sounds["NextCatoonSound"].Play();
        PushedBuyButton = true;

        Slot1Furniture.SetActive(true);
        Slot2Furniture.SetActive(false);
        Slot3Furniture.SetActive(false);

        GameObject FurnBuyPos = GameObject.Find("Furniture Set");
        MoveFurnBuyPos = FurnBuyPos.transform.position;
        MoveFurnBuyPos.x += 6.5f;

        GameObject Buy1button = GameObject.Find("BuySlot1Button");
        UIButton Buy1buttonScript = Buy1button.GetComponent<UIButton>();
        BoxCollider Buy1collider = Buy1button.GetComponent<BoxCollider>();

        GameObject Buy2button = GameObject.Find("BuySlot2Button");
        UIButton Buy2buttonScript = Buy2button.GetComponent<UIButton>();
        BoxCollider Buy2collider = Buy2button.GetComponent<BoxCollider>();

        GameObject Buy3button = GameObject.Find("BuySlot3Button");
        UIButton Buy3buttonScript = Buy3button.GetComponent<UIButton>();
        BoxCollider Buy3collider = Buy3button.GetComponent<BoxCollider>();

        Buy1collider.enabled = false;
        Buy1buttonScript.state = UIButtonColor.State.Disabled;

        Buy2collider.enabled = false;
        Buy2buttonScript.state = UIButtonColor.State.Disabled;

        Buy3collider.enabled = false;
        Buy3buttonScript.state = UIButtonColor.State.Disabled;

        StartCoroutine("MoveBuy");
    }

    public void PushBuyFurniture2()
    {
        SoundManager.sounds["NextCatoonSound"].Play();
        PushedBuyButton = true;
        Slot1Furniture.SetActive(false);
        Slot2Furniture.SetActive(true);
        Slot3Furniture.SetActive(false);

        GameObject FurnBuyPos = GameObject.Find("Furniture Set");
        MoveFurnBuyPos = FurnBuyPos.transform.position;
        MoveFurnBuyPos.x += 6.5f;

        GameObject Buy1button = GameObject.Find("BuySlot1Button");
        UIButton Buy1buttonScript = Buy1button.GetComponent<UIButton>();
        BoxCollider Buy1collider = Buy1button.GetComponent<BoxCollider>();

        GameObject Buy2button = GameObject.Find("BuySlot2Button");
        UIButton Buy2buttonScript = Buy2button.GetComponent<UIButton>();
        BoxCollider Buy2collider = Buy2button.GetComponent<BoxCollider>();

        GameObject Buy3button = GameObject.Find("BuySlot3Button");
        UIButton Buy3buttonScript = Buy3button.GetComponent<UIButton>();
        BoxCollider Buy3collider = Buy3button.GetComponent<BoxCollider>();

        Buy1collider.enabled = false;
        Buy1buttonScript.state = UIButtonColor.State.Disabled;

        Buy2collider.enabled = false;
        Buy2buttonScript.state = UIButtonColor.State.Disabled;

        Buy3collider.enabled = false;
        Buy3buttonScript.state = UIButtonColor.State.Disabled;
        StartCoroutine("MoveBuy");
    }

    public void PushBuyFurniture3()
    {
        SoundManager.sounds["NextCatoonSound"].Play();
        PushedBuyButton = true;
        Slot1Furniture.SetActive(false);
        Slot2Furniture.SetActive(false);
        Slot3Furniture.SetActive(true);

        GameObject FurnBuyPos = GameObject.Find("Furniture Set");
        MoveFurnBuyPos = FurnBuyPos.transform.position;
        MoveFurnBuyPos.x += 6.5f;

        GameObject Buy1button = GameObject.Find("BuySlot1Button");
        UIButton Buy1buttonScript = Buy1button.GetComponent<UIButton>();
        BoxCollider Buy1collider = Buy1button.GetComponent<BoxCollider>();

        GameObject Buy2button = GameObject.Find("BuySlot2Button");
        UIButton Buy2buttonScript = Buy2button.GetComponent<UIButton>();
        BoxCollider Buy2collider = Buy2button.GetComponent<BoxCollider>();

        GameObject Buy3button = GameObject.Find("BuySlot3Button");
        UIButton Buy3buttonScript = Buy3button.GetComponent<UIButton>();
        BoxCollider Buy3collider = Buy3button.GetComponent<BoxCollider>();

        Buy1collider.enabled = false;
        Buy1buttonScript.state = UIButtonColor.State.Disabled;

        Buy2collider.enabled = false;
        Buy2buttonScript.state = UIButtonColor.State.Disabled;

        Buy3collider.enabled = false;
        Buy3buttonScript.state = UIButtonColor.State.Disabled;
        StartCoroutine("MoveBuy");
    }


    IEnumerator MoveBuy()
    {
        Transform BuyMoveSet = GameObject.Find("Furniture Set").transform;

        yield return null;
        StartCoroutine("MoveBuy");

        if (BuyMoveSet.position == MoveFurnBuyPos)
        {
            StopCoroutine("MoveBuy");
            yield break;
        }
        BuyMoveSet.position = Vector3.Lerp(BuyMoveSet.position, MoveFurnBuyPos, 0.65f);

    }

    public void BackBuyFurniture()
    {
        SoundManager.sounds["NextCatoonSound"].Play();
        PushedBuyButton = false;
        GameObject FurnBuyPos = GameObject.Find("Furniture Set");
        MoveFurnBuyPos = FurnBuyPos.transform.position;
        MoveFurnBuyPos.x -= 6.5f;
        GameObject Buy1button = GameObject.Find("BuySlot1Button");
        UIButton Buy1buttonScript = Buy1button.GetComponent<UIButton>();
        BoxCollider Buy1collider = Buy1button.GetComponent<BoxCollider>();

        GameObject Buy2button = GameObject.Find("BuySlot2Button");
        UIButton Buy2buttonScript = Buy2button.GetComponent<UIButton>();
        BoxCollider Buy2collider = Buy2button.GetComponent<BoxCollider>();

        GameObject Buy3button = GameObject.Find("BuySlot3Button");
        UIButton Buy3buttonScript = Buy3button.GetComponent<UIButton>();
        BoxCollider Buy3collider = Buy3button.GetComponent<BoxCollider>();

        Buy1collider.enabled = true;
        Buy1buttonScript.state = UIButtonColor.State.Normal;

        Buy2collider.enabled = true;
        Buy2buttonScript.state = UIButtonColor.State.Normal;

        Buy3collider.enabled = true;
        Buy3buttonScript.state = UIButtonColor.State.Normal;

        StartCoroutine("MoveBuy");
    }

    public void UpdateFurniture()
    {
        switch (Furniture1Level) {
            case 0:

                Furniture1icon.spriteName = "WaterZonIcon0";
                BuyFurniture1icon.spriteName = "WaterZonIcon0";
                UpFurniture1icon.spriteName = "WaterZonIcon1";
                Furniture1Info.text = "파란 물 획득 +5";
                Furniture1MatInfo.text = "업그레이드 : 5000 골드 필요";
                break;
            case 1:
                Furniture1.sprite = nextFurniture1_1;
                Furniture1icon.spriteName = "WaterZonIcon1";
                BuyFurniture1icon.spriteName = "WaterZonIcon1";
                UpFurniture1icon.spriteName = "WaterZonIcon2";
                Furniture1Info.text = "파란 물 획득 +10 빨간 물 +1 연금가스 +1";
                Furniture1MatInfo.text = "업그레이드 : 15000 골드 필요";
                break;
            case 2:
                Furniture1.sprite = nextFurniture1_2;
                Furniture1icon.spriteName = "WaterZonIcon2";
                BuyFurniture1icon.spriteName = "WaterZonIcon2";
                UpFurniture1icon.spriteName = "WaterZonIcon3";
                Furniture1Info.text = "파란 물 획득 +15 빨간 물 +5 연금가스 +5";
                Furniture1MatInfo.text = "업그레이드 : 40000 골드 필요";
                break;
            case 3:
                Furniture1.sprite = nextFurniture1_3;
                Furniture1icon.spriteName = "WaterZonIcon3";
                BuyFurniture1icon.spriteName = "WaterZonIcon3";
                UpFurniture1icon.spriteName = "WaterZonIconM";
                Furniture1Info.text = "파란 물 +10 빨간 물 +10 연금가스 +10";
                Furniture1MatInfo.text = "최종 단계";
                break;
        }

        switch (Furniture2Level)
        {
            case 0:
                PotionListManager.FurnitureEffect = 0;
                Furniture2icon.spriteName = "BoxIcon0";
                BuyFurniture2icon.spriteName = "BoxIcon0";
                UpFurniture2icon.spriteName = "BoxIcon1";
                Furniture2Info.text = "포션 생성 갯수 + 0";
                Furniture2MatInfo.text = "업그레이드 : 10000 골드 필요";
                break;
            case 1:
                PotionListManager.FurnitureEffect = 1;
                Furniture2.sprite = nextFurniture2_1;
                Furniture2icon.spriteName = "BoxIcon1";
                BuyFurniture2icon.spriteName = "BoxIcon1";
                UpFurniture2icon.spriteName = "BoxIcon2";
                Furniture2Info.text = "포션 생성 갯수 + 1";
                Furniture2MatInfo.text = "업그레이드 : 35000 골드 필요";
                break;
            case 2:
                Furniture2.sprite = nextFurniture2_2;
                PotionListManager.FurnitureEffect = 2;
                Furniture2icon.spriteName = "BoxIcon2";
                BuyFurniture2icon.spriteName = "BoxIcon2";
                UpFurniture2icon.spriteName = "BoxIcon3";
                Furniture2Info.text = "포션 생성 갯수 + 2";
                Furniture2MatInfo.text = "업그레이드 : 55000 골드";
                break;
            case 3:
                Furniture2.sprite = nextFurniture2_3;
                PotionListManager.FurnitureEffect = 4;
                Furniture2icon.spriteName = "BoxIcon3";
                BuyFurniture2icon.spriteName = "BoxIcon3";
                UpFurniture2icon.spriteName = "BoxIconM";
                Furniture2Info.text = "포션 생성 갯수 + 3";
                Furniture2MatInfo.text = "최종 단계";
                break;
        }

        switch (Furniture3Level)
        {
            case 0:
                Furniture3Effect = 0;
                Furniture3_3Effect = 0f;
                Furniture3icon.spriteName = "CleaningMob1";
                BuyFurniture3icon.spriteName = "CleaningMob1";
                UpFurniture3icon.spriteName = "CleaningMob2";
                Furniture3Info.text = "손님 대기 시간 + 0";
                Furniture3MatInfo.text = "업그레이드 : 5000 골드 필요";
                break;
            case 1:
                Furniture3.sprite = nextFurniture3_1;
                Furniture3Effect = 10;
                Furniture3icon.spriteName = "CleaningMob2";
                BuyFurniture3icon.spriteName = "CleaningMob2";
                UpFurniture3icon.spriteName = "CleaningMob3";
                Furniture3Info.text = "손님 대기 시간 + 1";
                Furniture3MatInfo.text = "업그레이드 : 15000 골드 필요";
                break;
            case 2:
                Furniture3.sprite = nextFurniture3_2;
                Furniture3Effect = 20;
                Furniture3icon.spriteName = "CleaningMob3";
                BuyFurniture3icon.spriteName = "CleaningMob3";
                UpFurniture3icon.spriteName = "CleaningMob4";
                Furniture3Info.text = "손님 대기 시간 + 2";
                Furniture3MatInfo.text = "업그레이드 : 95000 골드 필요";
                break;
            case 3:
                Furniture3.sprite = nextFurniture3_3;
                Furniture3Effect = 25;
                Furniture3_3Effect = 0.1f;
                Furniture3icon.spriteName = "CleaningMob4";
                BuyFurniture3icon.spriteName = "CleaningMob4";
                UpFurniture3icon.spriteName = "CleaningMobM";
                Furniture3Info.text = "손님 대기 시간 + 3, 아이템 획득 속도 +";
                Furniture3MatInfo.text = "최종 단계";
                break;
        }

    }

    public void EnforceFur1()
    {
        switch (Furniture1Level)
        {
            case 0:
                if (StatusManager.Point >= 5000)
                {
                    SoundManager.sounds["PurchaseFurniture"].Play();
                    Alert.PopUpFurnitureMesg();
                    StatusManager.Point -= 5000;
                    StatusManager.TodayPoint -= 5000;
                    Furniture1Level += 1;
                    Furniture1.sprite = nextFurniture1_1;
                    UpdateFurniture();
                }
                else
                {
                    SoundManager.sounds["CantBuyFurniture"].Play();
                }
                break;
            case 1:
                if(StatusManager.Point >= 15000)
                {
                    SoundManager.sounds["PurchaseFurniture"].Play();
                    Alert.PopUpFurnitureMesg();
                    StatusManager.Point -= 15000;
                    StatusManager.TodayPoint -= 15000;
                    Furniture1Level += 1;
                    Furniture1.sprite = nextFurniture1_2;
                    UpdateFurniture();
                }
                else
                {
                    SoundManager.sounds["CantBuyFurniture"].Play();
                }
                break;
            case 2:
                if (StatusManager.Point >= 45000)
                {
                    SoundManager.sounds["PurchaseFurniture"].Play();
                    Alert.PopUpFurnitureMesg();
                    StatusManager.Point -= 45000;
                    StatusManager.TodayPoint -= 45000;
                    Furniture1Level += 1;
                    Furniture1.sprite = nextFurniture1_3;
                    UpdateFurniture();
                }
                else
                {
                    SoundManager.sounds["CantBuyFurniture"].Play();
                }
                break;
        }
        DataManager.SaveFur1Lev = Furniture1Level;
    }

    public void EnforceFur2()
    {
        switch (Furniture2Level)
        {
            case 0:
                if (StatusManager.Point >= 10000)
                {
                    SoundManager.sounds["PurchaseFurniture"].Play();
                    Alert.PopUpFurnitureMesg();
                    StatusManager.Point -= 10000;
                    StatusManager.TodayPoint -= 10000;
                    Furniture2Level += 1;
                    Furniture2.sprite = nextFurniture2_1;
                    UpdateFurniture();
                }
                else
                {
                    SoundManager.sounds["CantBuyFurniture"].Play();
                }
                break;
            case 1:
                if (StatusManager.Point >= 35000)
                {
                    SoundManager.sounds["PurchaseFurniture"].Play();
                    Alert.PopUpFurnitureMesg();
                    StatusManager.Point -= 35000;
                    StatusManager.TodayPoint -= 35000;
                    Furniture2Level += 1;
                    Furniture2.sprite = nextFurniture2_2;
                    UpdateFurniture();
                }
                else
                {
                    SoundManager.sounds["CantBuyFurniture"].Play();
                }
                break;
            case 2:
                if (StatusManager.Point >= 55000)
                {
                    SoundManager.sounds["PurchaseFurniture"].Play();
                    Alert.PopUpFurnitureMesg();
                    StatusManager.Point -= 55000;
                    StatusManager.TodayPoint -= 55000;
                    Furniture2Level += 1;
                    Furniture2.sprite = nextFurniture2_3;
                    UpdateFurniture();
                }
                else
                {
                    SoundManager.sounds["CantBuyFurniture"].Play();
                }
                break;
        }
        DataManager.SaveFur2Lev = Furniture2Level;
    }

    public void EnforceFur3()
    {
        switch (Furniture3Level)
        {
            case 0:
                if (StatusManager.Point >= 5000)
                {
                    SoundManager.sounds["PurchaseFurniture"].Play();
                    Alert.PopUpFurnitureMesg();
                    StatusManager.Point -= 5000;
                    StatusManager.TodayPoint -= 5000;
                    Furniture3Level += 1;
                    Furniture3.sprite = nextFurniture3_1;
                    UpdateFurniture();
                }
                else
                {
                    SoundManager.sounds["CantBuyFurniture"].Play();
                }
                break;
            case 1:
                if (StatusManager.Point >= 15000)
                {
                    SoundManager.sounds["PurchaseFurniture"].Play();
                    Alert.PopUpFurnitureMesg();
                    StatusManager.Point -= 15000;
                    StatusManager.TodayPoint -= 15000;
                    Furniture3Level += 1;
                    Furniture3.sprite = nextFurniture3_2;
                    UpdateFurniture();
                }
                else
                {
                    SoundManager.sounds["CantBuyFurniture"].Play();
                }
                break;
            case 2:
                if (StatusManager.Point >= 95000)
                {
                    SoundManager.sounds["PurchaseFurniture"].Play();
                    Alert.PopUpFurnitureMesg();
                    StatusManager.Point -= 95000;
                    StatusManager.TodayPoint -= 95000;
                    Furniture3Level += 1;
                    Furniture3.sprite = nextFurniture3_3;
                    UpdateFurniture();
                }
                else
                {
                    SoundManager.sounds["CantBuyFurniture"].Play();
                }
                break;
        }
        DataManager.SaveFur3Lev = Furniture3Level;
    }
}
