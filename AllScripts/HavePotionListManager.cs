using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HavePotionListManager : MonoBehaviour {

    MessageManager PopMsg;

    public HavePoInvenManager Poinven;

    public Transform PoDicPos;

    public Transform PoListPos;
    //-------------------------------------
    public GameObject Potion1Dic;

    public GameObject Potion2Dic;

    public GameObject Potion3Dic;

    public GameObject Potion4Dic;

    public GameObject Potion5Dic;

    public GameObject Potion6Dic;

    public GameObject Potion7Dic;

    public GameObject Potion8Dic;

    public GameObject Potion9Dic;

    public GameObject Potion10Dic;

    public GameObject Potion11Dic;

    public GameObject Potion12Dic;

    public GameObject Potion13Dic;
    public GameObject Potion14Dic;
    public GameObject Potion15Dic;
    //-------------------------------------
    public GameObject Potion1List;

    public GameObject Potion2List;

    public GameObject Potion3List;

    public GameObject Potion4List;

    public GameObject Potion5List;

    public GameObject Potion6List;

    public GameObject Potion7List;

    public GameObject Potion8List;

    public GameObject Potion9List;

    public GameObject Potion10List;

    public GameObject Potion11List;

    public GameObject Potion12List;

    public GameObject Potion13List;
    public GameObject Potion14List;
    public GameObject Potion15List;
    //-------------------------------------

    bool Potion1Updated, Potion2Updated, Potion3Updated, Potion4Updated, Potion5Updated, Potion6Updated, Potion7Updated, Potion8Updated, Potion9Updated, Potion10Updated, Potion11Updated, Potion12Updated;

    bool Potion13Updated, Potion14Updated, Potion15Updated;

    bool Potion1Outted, Potion2Outted, Potion3Outted, Potion4Outted, Potion5Outted, Potion6Outted, Potion7Outted, Potion8Outted, Potion9Outted, Potion10Outted, Potion11Outted, Potion12Outted;

    bool Potion13Outted, Potion14Outted, Potion15Outted;

    //-------------------------------------
    int  y, y2;
    //-------------------------------------
    public Dictionary<string, int> PotionListNum = new Dictionary<string, int>();
    public List<int> PoTypeNum = new List<int>();
    public int PoCount;
    //-----------------------------------------
    private void Awake()
    {
        PopMsg = GameObject.Find("MessageManager").GetComponent<MessageManager>();
    }
    //------------포션리스트 로드-------------

    public void DataAccess(DataVo dataVo)
    {
        PoCount = int.Parse(dataVo.SavePotionNum);
        for (int index = 0; index < PoCount; index++)
        {
            LoadPoList(int.Parse(dataVo.SavePotions[index]));
        }
    }

    public void LoadPoList(int TypeNum)
    {
        switch (TypeNum)
        {
            case 1:
                Potion1ListUpdate();
                if (!PotionListNum.ContainsKey("푸른포션")) PotionListNum.Add("푸른포션", 1);
                break;
            case 2:
                Potion2ListUpdate();
                if (!PotionListNum.ContainsKey("붉은포션")) PotionListNum.Add("붉은포션", 2);
                break;
            case 3:
                Potion3ListUpdate();
                if (!PotionListNum.ContainsKey("해독제")) PotionListNum.Add("해독제", 3);
                break;
            case 4:
                Potion4ListUpdate();
                if (!PotionListNum.ContainsKey("민첩포션")) PotionListNum.Add("민첩포션", 4);
                break;
            case 5:
                Potion5ListUpdate();
                if (!PotionListNum.ContainsKey("고효능민첩포션")) PotionListNum.Add("고효능민첩포션", 5);
                break;
            case 6:
                Potion6ListUpdate();
                if (!PotionListNum.ContainsKey("힘포션")) PotionListNum.Add("힘포션", 6);
                break;
            case 7:
                Potion7ListUpdate();
                if (!PotionListNum.ContainsKey("고효능힘포션")) PotionListNum.Add("고효능힘포션", 7);
                break;
            case 8:
                Potion8ListUpdate();
                if (!PotionListNum.ContainsKey("비타오백")) PotionListNum.Add("비타오백", 8);
                break;
            case 9:
                Potion9ListUpdate();
                if (!PotionListNum.ContainsKey("박카스")) PotionListNum.Add("박카스", 9);
                break;
            case 10:
                Potion10ListUpdate();
                if (!PotionListNum.ContainsKey("쌍화탕")) PotionListNum.Add("쌍화탕", 10);
                break;
            case 11:
                Potion11ListUpdate();
                if (!PotionListNum.ContainsKey("마약")) PotionListNum.Add("마약", 11);
                break;
            case 12:
                Potion12ListUpdate();
                if (!PotionListNum.ContainsKey("성수")) PotionListNum.Add("성수", 12);
                break;
            case 13:
                Potion13ListUpdate();
                if (!PotionListNum.ContainsKey("멀미약")) PotionListNum.Add("멀미약", 13);
                break;
            case 14:
                Potion14ListUpdate();
                if (!PotionListNum.ContainsKey("지능포션")) PotionListNum.Add("지능포션", 14);
                break;
            case 15:
                Potion15ListUpdate();
                if (!PotionListNum.ContainsKey("고지능포션")) PotionListNum.Add("고지능포션", 15);
                break;
            case 0:
                break;
        }
    }
    //-----------------------------------------

    public void Potion1ListUpdate()//포션 리스트
    {
        GameObject PoList = GameObject.FindGameObjectWithTag("MakedPotion");

        GameObject PoDic = GameObject.FindGameObjectWithTag("MakedPotion");

        if (Potion1Outted == false && Potion1Updated == false)
        {
            if (!Poinven.FirstPotion1)
            {
                PopMsg.PopUpMesg("[00E5FF]푸른 포션");
                PotionListNum.Add("푸른포션", 1);
                PoCount += 1;
                Poinven.FirstPotion1 = true;
            }
            PoList = Instantiate(Potion1List);
            PoList.transform.parent = PoListPos.transform;

            PoDic = Instantiate(Potion1Dic);
            PoDic.transform.parent = PoDicPos.transform;

            PoList.transform.localPosition = new Vector3(0f, y, 0f);
            PoList.transform.localScale = Vector3.one;

            PoDic.transform.localPosition = new Vector3(0f, y2, 0f);
            PoDic.transform.localScale = Vector3.one;

            Potion1Outted = true;
            y -= 650;
            y2 -= 410;
            Potion1Updated = true;
            Potion1NumUpdate();
        }
        else if (Poinven.Potion1Num == 1 && Potion1Outted == true)
        {
            Potion1NumUpdate();
        }
        else if (Poinven.Potion1Num >= 1)
        {
            Potion1NumUpdate();
        }
        else if(Poinven.Potion1Num == 0)
        {
            Potion1NumUpdate();
        }
        else if (Poinven.Potion1Num <= 0 && Potion1Outted == true)
        {
            Potion1NumUpdate();
            Potion1Outted = false;
        }
    }

    public void Potion2ListUpdate()//포션 리스트
    {
        GameObject PoList = GameObject.FindGameObjectWithTag("MakedPotion");

        GameObject PoDic = GameObject.FindGameObjectWithTag("MakedPotion");

        if (Potion2Outted == false && Potion2Updated == false)
        {
            if (!Poinven.FirstPotion2)
            {
                PopMsg.PopUpMesg("[FF0000]붉은 포션");
                PotionListNum.Add("붉은포션", 2);
                PoCount += 1;
                Poinven.FirstPotion2 = true;
            }
            PoList = Instantiate(Potion2List);
            PoList.transform.parent = PoListPos.transform;

            PoDic = Instantiate(Potion2Dic);
            PoDic.transform.parent = PoDicPos.transform;

            PoList.transform.localPosition = new Vector3(0f, y, 0f);
            PoList.transform.localScale = Vector3.one;

            PoDic.transform.localPosition = new Vector3(0f, y2, 0f);
            PoDic.transform.localScale = Vector3.one;

            Potion2Outted = true;
            y -= 650;
            y2 -= 410;
            Potion2Updated = true;
            Potion2NumUpdate();
        }
        else if (Poinven.Potion2Num == 1 && Potion2Outted == true)
        {
            Potion2NumUpdate();
        }
        else if (Poinven.Potion2Num >= 1)
        {
            Potion2NumUpdate();
        }
        else if (Poinven.Potion2Num == 0)
        {
            Potion2NumUpdate();
        }
        else if (Poinven.Potion2Num <= 0 && Potion2Outted == true)
        {
            Potion2NumUpdate();
            Potion2Outted = false;
        }
    }

    public void Potion3ListUpdate()//포션 리스트
    {
        GameObject PoList = GameObject.FindGameObjectWithTag("MakedPotion");

        GameObject PoDic = GameObject.FindGameObjectWithTag("MakedPotion");

        if ( Potion3Outted == false && Potion3Updated == false)
        {
            if (!Poinven.FirstPotion3)
            {
                PopMsg.PopUpMesg("[629F00]해독제");
                PotionListNum.Add("해독제", 3);
                PoCount += 1;
                Poinven.FirstPotion3 = true;
            }
            PoList = Instantiate(Potion3List);
            PoList.transform.parent = PoListPos.transform;

            PoDic = Instantiate(Potion3Dic);
            PoDic.transform.parent = PoDicPos.transform;

            PoList.transform.localPosition = new Vector3(0f, y, 0f);
            PoList.transform.localScale = Vector3.one;

            PoDic.transform.localPosition = new Vector3(0f, y2, 0f);
            PoDic.transform.localScale = Vector3.one;

            Potion3Outted = true;
            y -= 650;
            y2 -= 410;
            Potion3Updated = true;
            Potion3NumUpdate();
        }
        else if (Poinven.Potion3Num == 1 && Potion3Outted == true)
        {
            Potion3NumUpdate();
        }
        else if (Poinven.Potion3Num >= 1)
        {
            Potion3NumUpdate();
        }
        else if (Poinven.Potion3Num == 0)
        {
            Potion3NumUpdate();
        }
        else if (Poinven.Potion3Num <= 0 && Potion3Outted == true)
        {
            Potion3NumUpdate();
            Potion3Outted = false;
        }
    }

    public void Potion4ListUpdate()//포션 리스트
    {
        GameObject PoList = GameObject.FindGameObjectWithTag("MakedPotion");

        GameObject PoDic = GameObject.FindGameObjectWithTag("MakedPotion");

        if ( Potion4Outted == false && Potion4Updated == false)
        {
            if (!Poinven.FirstPotion4)
            {
                PopMsg.PopUpMesg("[E5D61E]민첩포션");
                PotionListNum.Add("민첩포션", 4);
                PoCount += 1;
                Poinven.FirstPotion4 = true;
            }
            PoList = Instantiate(Potion4List);
            PoList.transform.parent = PoListPos.transform;

            PoDic = Instantiate(Potion4Dic);
            PoDic.transform.parent = PoDicPos.transform;

            PoList.transform.localPosition = new Vector3(0f, y, 0f);
            PoList.transform.localScale = Vector3.one;

            PoDic.transform.localPosition = new Vector3(0f, y2, 0f);
            PoDic.transform.localScale = Vector3.one;

            Potion4Outted = true;
            y -= 650;
            y2 -= 410;
            Potion4Updated = true;
            Potion4NumUpdate();
        }
        else if (Poinven.Potion4Num == 1 && Potion4Outted == true)
        {
            Potion4NumUpdate();
        }
        else if (Poinven.Potion4Num >= 1)
        {
            Potion4NumUpdate();
        }
        else if (Poinven.Potion4Num == 0)
        {
            Potion4NumUpdate();
        }
        else if (Poinven.Potion4Num <= 0 && Potion4Outted == true)
        {
            Potion4NumUpdate();
            Potion4Outted = false;
        }
    }

    public void Potion5ListUpdate()//포션 리스트
    {
        GameObject PoList = GameObject.FindGameObjectWithTag("MakedPotion");

        GameObject PoDic = GameObject.FindGameObjectWithTag("MakedPotion");

        if ( Potion5Outted == false && Potion5Updated == false)
        {
            if (!Poinven.FirstPotion5)
            {
                PopMsg.PopUpMesg("[BD00FF]고효능 민첩포션");
                PotionListNum.Add("고효능민첩포션", 5);
                PoCount += 1;
                Poinven.FirstPotion5 = true;
            }
            PoList = Instantiate(Potion5List);
            PoList.transform.parent = PoListPos.transform;

            PoDic = Instantiate(Potion5Dic);
            PoDic.transform.parent = PoDicPos.transform;

            PoList.transform.localPosition = new Vector3(0f, y, 0f);
            PoList.transform.localScale = Vector3.one;

            PoDic.transform.localPosition = new Vector3(0f, y2, 0f);
            PoDic.transform.localScale = Vector3.one;

            Potion5Outted = true;
            y -= 650;
            y2 -= 410;
            Potion5Updated = true;
            Potion5NumUpdate();
        }
        else if (Poinven.Potion5Num == 1 && Potion5Outted == true)
        {
            Potion5NumUpdate();
        }
        else if (Poinven.Potion5Num >= 1)
        {
            Potion5NumUpdate();
        }
        else if (Poinven.Potion5Num == 0)
        {
            Potion5NumUpdate();
        }
        else if (Poinven.Potion5Num <= 0 && Potion5Outted == true)
        {
            Potion5NumUpdate();
            Potion5Outted = false;
        }
    }

    public void Potion6ListUpdate()//포션 리스트
    {
        GameObject PoList = GameObject.FindGameObjectWithTag("MakedPotion");

        GameObject PoDic = GameObject.FindGameObjectWithTag("MakedPotion");

        if ( Potion6Outted == false && Potion6Updated == false)
        {
            if (!Poinven.FirstPotion6)
            {
                PopMsg.PopUpMesg("[FF4500]힘 포션");
                PotionListNum.Add("힘포션", 6);
                PoCount += 1;
                Poinven.FirstPotion6 = true;
            }
            PoList = Instantiate(Potion6List);
            PoList.transform.parent = PoListPos.transform;

            PoDic = Instantiate(Potion6Dic);
            PoDic.transform.parent = PoDicPos.transform;

            PoList.transform.localPosition = new Vector3(0f, y, 0f);
            PoList.transform.localScale = Vector3.one;

            PoDic.transform.localPosition = new Vector3(0f, y2, 0f);
            PoDic.transform.localScale = Vector3.one;

            Potion6Outted = true;
            y -= 650;
            y2 -= 410;
            Potion6Updated = true;
            Potion6NumUpdate();
        }
        else if (Poinven.Potion6Num == 1 && Potion6Outted == true)
        {
            Potion6NumUpdate();
        }
        else if (Poinven.Potion6Num >= 1)
        {
            Potion6NumUpdate();
        }
        else if (Poinven.Potion6Num == 0)
        {
            Potion6NumUpdate();
        }
        else if (Poinven.Potion6Num <= 0 && Potion6Outted == true)
        {
            Potion6NumUpdate();
            Potion6Outted = false;
        }
    }

    public void Potion7ListUpdate()//포션 리스트
    {
        GameObject PoList = GameObject.FindGameObjectWithTag("MakedPotion");

        GameObject PoDic = GameObject.FindGameObjectWithTag("MakedPotion");

        if (Potion7Outted == false && Potion7Updated == false)
        {
            if (!Poinven.FirstPotion7)
            {
                PopMsg.PopUpMesg("[FF001C]고효능 힘 포션");
                PotionListNum.Add("고효능힘포션", 7);
                PoCount += 1;
                Poinven.FirstPotion7 = true;
            }
            PoList = Instantiate(Potion7List);
            PoList.transform.parent = PoListPos.transform;

            PoDic = Instantiate(Potion7Dic);
            PoDic.transform.parent = PoDicPos.transform;

            PoList.transform.localPosition = new Vector3(0f, y, 0f);
            PoList.transform.localScale = Vector3.one;

            PoDic.transform.localPosition = new Vector3(0f, y2, 0f);
            PoDic.transform.localScale = Vector3.one;

            Potion7Outted = true;
            y -= 650;
            y2 -= 410;
            Potion7Updated = true;
            Potion7NumUpdate();
        }
        else if (Poinven.Potion7Num == 1 && Potion7Outted == true)
        {
            Potion7NumUpdate();
        }
        else if (Poinven.Potion7Num >= 1)
        {
            Potion7NumUpdate();
        }
        else if (Poinven.Potion7Num == 0)
        {
            Potion7NumUpdate();
        }
        else if (Poinven.Potion7Num <= 0 && Potion7Outted == true)
        {
            Potion7NumUpdate();
            Potion7Outted = false;
        }
    }

    public void Potion8ListUpdate()//포션 리스트
    {
        GameObject PoList = GameObject.FindGameObjectWithTag("MakedPotion");

        GameObject PoDic = GameObject.FindGameObjectWithTag("MakedPotion");

        if ( Potion8Outted == false && Potion8Updated == false)
        {
            if (!Poinven.FirstPotion8)
            {
                PopMsg.PopUpMesg("[F37D00]네타500");
                PotionListNum.Add("비타오백", 8);
                PoCount += 1;
                Poinven.FirstPotion8 = true;
            }
            PoList = Instantiate(Potion8List);
            PoList.transform.parent = PoListPos.transform;

            PoDic = Instantiate(Potion8Dic);
            PoDic.transform.parent = PoDicPos.transform;

            PoList.transform.localPosition = new Vector3(0f, y, 0f);
            PoList.transform.localScale = Vector3.one;

            PoDic.transform.localPosition = new Vector3(0f, y2, 0f);
            PoDic.transform.localScale = Vector3.one;

            Potion8Outted = true;
            y -= 650;
            y2 -= 410;
            Potion8Updated = true;
            Potion8NumUpdate();
        }
        else if (Poinven.Potion8Num == 1 && Potion8Outted == true)
        {
            Potion8NumUpdate();
        }
        else if (Poinven.Potion8Num >= 1)
        {
            Potion8NumUpdate();
        }
        else if (Poinven.Potion8Num == 0)
        {
            Potion8NumUpdate();
        }
        else if (Poinven.Potion8Num <= 0 && Potion8Outted == true)
        {
            Potion8NumUpdate();
            Potion8Outted = false;
        }
    }

    public void Potion9ListUpdate()//포션 리스트
    {
        GameObject PoList = GameObject.FindGameObjectWithTag("MakedPotion");

        GameObject PoDic = GameObject.FindGameObjectWithTag("MakedPotion");

        if (Potion9Outted == false && Potion9Updated == false)
        {
            if (!Poinven.FirstPotion9)
            {
                PopMsg.PopUpMesg("[36429C]직장인의 눈물");
                PotionListNum.Add("박카스", 9);
                PoCount += 1;
                Poinven.FirstPotion9 = true;
            }
            PoList = Instantiate(Potion9List);
            PoList.transform.parent = PoListPos.transform;

            PoDic = Instantiate(Potion9Dic);
            PoDic.transform.parent = PoDicPos.transform;

            PoList.transform.localPosition = new Vector3(0f, y, 0f);
            PoList.transform.localScale = Vector3.one;

            PoDic.transform.localPosition = new Vector3(0f, y2, 0f);
            PoDic.transform.localScale = Vector3.one;

            Potion9Outted = true;
            y -= 650;
            y2 -= 410;
            Potion9Updated = true;
            Potion9NumUpdate();
        }
        else if (Poinven.Potion9Num == 1 && Potion9Outted == true)
        {
            Potion9NumUpdate();
        }
        else if (Poinven.Potion9Num >= 1)
        {
            Potion9NumUpdate();
        }
        else if (Poinven.Potion9Num == 0)
        {
            Potion9NumUpdate();
        }
        else if (Poinven.Potion9Num <= 0 && Potion9Outted == true)
        {
            Potion9NumUpdate();
            Potion9Outted = false;
        }
    }

    public void Potion10ListUpdate()//포션 리스트
    {
        GameObject PoList = GameObject.FindGameObjectWithTag("MakedPotion");

        GameObject PoDic = GameObject.FindGameObjectWithTag("MakedPotion");

        if ( Potion10Outted == false && Potion10Updated == false)
        {
            if (!Poinven.FirstPotion10)
            {
                PopMsg.PopUpMesg("[7B0000]와-탕");
                PotionListNum.Add("쌍화탕", 10);
                PoCount += 1;
                Poinven.FirstPotion10 = true;
            }
            PoList = Instantiate(Potion10List);
            PoList.transform.parent = PoListPos.transform;

            PoDic = Instantiate(Potion10Dic);
            PoDic.transform.parent = PoDicPos.transform;

            PoList.transform.localPosition = new Vector3(0f, y, 0f);
            PoList.transform.localScale = Vector3.one;

            PoDic.transform.localPosition = new Vector3(0f, y2, 0f);
            PoDic.transform.localScale = Vector3.one;

            Potion10Outted = true;
            y -= 650;
            y2 -= 410;
            Potion10Updated = true;
            Potion10NumUpdate();
        }
        else if (Poinven.Potion10Num == 1 && Potion10Outted == true)
        {
            Potion10NumUpdate();
        }
        else if (Poinven.Potion10Num >= 1)
        {
            Potion10NumUpdate();
        }
        else if (Poinven.Potion10Num == 0)
        {
            Potion10NumUpdate();
        }
        else if (Poinven.Potion10Num <= 0 && Potion10Outted == true)
        {
            Potion10NumUpdate();
            Potion10Outted = false;
        }
    }

    public void Potion11ListUpdate()//포션 리스트
    {
        GameObject PoList = GameObject.FindGameObjectWithTag("MakedPotion");

        GameObject PoDic = GameObject.FindGameObjectWithTag("MakedPotion");

        if (Potion11Outted == false && Potion11Updated == false)
        {
            if (!Poinven.FirstPotion11)
            {
                PopMsg.PopUpMesg("[7B0071]스구마");
                PotionListNum.Add("마약", 11);
                PoCount += 1;
                Poinven.FirstPotion11 = true;
            }
            PoList = Instantiate(Potion11List);
            PoList.transform.parent = PoListPos.transform;

            PoDic = Instantiate(Potion11Dic);
            PoDic.transform.parent = PoDicPos.transform;

            PoList.transform.localPosition = new Vector3(0f, y, 0f);
            PoList.transform.localScale = Vector3.one;

            PoDic.transform.localPosition = new Vector3(0f, y2, 0f);
            PoDic.transform.localScale = Vector3.one;

            Potion11Outted = true;
            y -= 650;
            y2 -= 410;
            Potion11Updated = true;
            Potion11NumUpdate();
        }
        else if (Poinven.Potion11Num == 1 && Potion11Outted == true)
        {
            Potion11NumUpdate();
        }
        else if (Poinven.Potion11Num >= 1)
        {
            Potion11NumUpdate();
        }
        else if (Poinven.Potion11Num == 0)
        {
            Potion11NumUpdate();
        }
        else if (Poinven.Potion11Num <= 0 && Potion11Outted == true)
        {
            Potion11NumUpdate();
            Potion11Outted = false;
        }
    }

    public void Potion12ListUpdate()//포션 리스트
    {
        GameObject PoList = GameObject.FindGameObjectWithTag("MakedPotion");

        GameObject PoDic = GameObject.FindGameObjectWithTag("MakedPotion");

        if (Potion12Outted == false && Potion12Updated == false)
        {
            if (!Poinven.FirstPotion12)
            {
                PopMsg.PopUpMesg("[606060]성수");
                PotionListNum.Add("성수", 12);
                PoCount += 1;
                Poinven.FirstPotion12 = true;
            }
            PoList = Instantiate(Potion12List);
            PoList.transform.parent = PoListPos.transform;

            PoDic = Instantiate(Potion12Dic);
            PoDic.transform.parent = PoDicPos.transform;

            PoList.transform.localPosition = new Vector3(0f, y, 0f);
            PoList.transform.localScale = Vector3.one;

            PoDic.transform.localPosition = new Vector3(0f, y2, 0f);
            PoDic.transform.localScale = Vector3.one;

            Potion12Outted = true;
            y -= 650;
            y2 -= 410;
            Potion12Updated = true;
            Potion12NumUpdate();
        }
        else if (Poinven.Potion12Num == 1 && Potion12Outted == true)
        {
            Potion12NumUpdate();
        }
        else if (Poinven.Potion12Num >= 1)
        {
            Potion12NumUpdate();
        }
        else if (Poinven.Potion12Num == 0)
        {
            Potion12NumUpdate();
        }
        else if (Poinven.Potion12Num <= 0 && Potion12Outted == true)
        {
            Potion12NumUpdate();
            Potion12Outted = false;
        }
    }
        public void Potion13ListUpdate()//포션 리스트
        {
            GameObject PoList = GameObject.FindGameObjectWithTag("MakedPotion");

            GameObject PoDic = GameObject.FindGameObjectWithTag("MakedPotion");

            if (Potion13Outted == false && Potion13Updated == false)
            {
                if (!Poinven.FirstPotion13)
                {
                    PopMsg.PopUpMesg("[A94B1B]멀미약");
                    PotionListNum.Add("멀미약", 13);
                    PoCount += 1;
                    Poinven.FirstPotion13 = true;
                }
                PoList = Instantiate(Potion13List);
                PoList.transform.parent = PoListPos.transform;

                PoDic = Instantiate(Potion13Dic);
                PoDic.transform.parent = PoDicPos.transform;

                PoList.transform.localPosition = new Vector3(0f, y, 0f);
                PoList.transform.localScale = Vector3.one;

                PoDic.transform.localPosition = new Vector3(0f, y2, 0f);
                PoDic.transform.localScale = Vector3.one;

                Potion13Outted = true;
                y -= 650;
                y2 -= 410;
                Potion13Updated = true;
                Potion13NumUpdate();
            }
            else if (Poinven.Potion13Num == 1 && Potion13Outted == true)
            {
                Potion13NumUpdate();
            }
            else if (Poinven.Potion13Num >= 1)
            {
                Potion13NumUpdate();
            }
            else if (Poinven.Potion13Num == 0)
            {
                Potion13NumUpdate();
            }
            else if (Poinven.Potion13Num <= 0 && Potion13Outted == true)
            {
                Potion13NumUpdate();
                Potion13Outted = false;
            }
        }

    public void Potion14ListUpdate()//포션 리스트
    {
        GameObject PoList = GameObject.FindGameObjectWithTag("MakedPotion");

        GameObject PoDic = GameObject.FindGameObjectWithTag("MakedPotion");

        if (Potion14Outted == false && Potion14Updated == false)
        {
            if (!Poinven.FirstPotion14)
            {
                PopMsg.PopUpMesg("[AD72BC]지능포션");
                PotionListNum.Add("지능포션", 14);
                PoCount += 1;
                Poinven.FirstPotion14 = true;
            }
            PoList = Instantiate(Potion14List);
            PoList.transform.parent = PoListPos.transform;

            PoDic = Instantiate(Potion14Dic);
            PoDic.transform.parent = PoDicPos.transform;

            PoList.transform.localPosition = new Vector3(0f, y, 0f);
            PoList.transform.localScale = Vector3.one;

            PoDic.transform.localPosition = new Vector3(0f, y2, 0f);
            PoDic.transform.localScale = Vector3.one;

            Potion14Outted = true;
            y -= 650;
            y2 -= 410;
            Potion14Updated = true;
            Potion14NumUpdate();
        }
        else if (Poinven.Potion14Num == 1 && Potion14Outted == true)
        {
            Potion14NumUpdate();
        }
        else if (Poinven.Potion14Num >= 1)
        {
            Potion14NumUpdate();
        }
        else if (Poinven.Potion14Num == 0)
        {
            Potion14NumUpdate();
        }
        else if (Poinven.Potion14Num <= 0 && Potion14Outted == true)
        {
            Potion14NumUpdate();
            Potion14Outted = false;
        }
    }

    public void Potion15ListUpdate()//포션 리스트
    {
        GameObject PoList = GameObject.FindGameObjectWithTag("MakedPotion");

        GameObject PoDic = GameObject.FindGameObjectWithTag("MakedPotion");

        if (Potion15Outted == false && Potion15Updated == false)
        {
            if (!Poinven.FirstPotion15)
            {
                PopMsg.PopUpMesg("[D800FF]고효능 지능 포션");
                PotionListNum.Add("고지능포션", 15);
                PoCount += 1;
                Poinven.FirstPotion15 = true;
            }
            PoList = Instantiate(Potion15List);
            PoList.transform.parent = PoListPos.transform;

            PoDic = Instantiate(Potion15Dic);
            PoDic.transform.parent = PoDicPos.transform;

            PoList.transform.localPosition = new Vector3(0f, y, 0f);
            PoList.transform.localScale = Vector3.one;

            PoDic.transform.localPosition = new Vector3(0f, y2, 0f);
            PoDic.transform.localScale = Vector3.one;

            Potion15Outted = true;
            y -= 650;
            y2 -= 410;
            Potion15Updated = true;
            Potion15NumUpdate();
        }
        else if (Poinven.Potion15Num == 1 && Potion15Outted == true)
        {
            Potion15NumUpdate();
        }
        else if (Poinven.Potion15Num >= 1)
        {
            Potion15NumUpdate();
        }
        else if (Poinven.Potion15Num == 0)
        {
            Potion15NumUpdate();
        }
        else if (Poinven.Potion15Num <= 0 && Potion15Outted == true)
        {
            Potion15NumUpdate();
            Potion15Outted = false;
        }
    }
    //------------------------------------------------------------------------

    public void Potion1NumUpdate()
    {
        UILabel Potion1Lable = GameObject.Find("Potion1_Num").GetComponent<UILabel>();
        Potion1Lable.text = string.Format("{0:n0}", Poinven.Potion1Num);
    }
    public void Potion2NumUpdate()
    {
        UILabel Potion2Lable = GameObject.Find("Potion2_Num").GetComponent<UILabel>();
        Potion2Lable.text = string.Format("{0:n0}", Poinven.Potion2Num);
    }

    public void Potion3NumUpdate()
    {
        UILabel Potion3Lable = GameObject.Find("Potion3_Num").GetComponent<UILabel>();
        Potion3Lable.text = string.Format("{0:n0}", Poinven.Potion3Num);
    }

    public void Potion4NumUpdate()
    {
        UILabel Potion4Lable = GameObject.Find("Potion4_Num").GetComponent<UILabel>();
        Potion4Lable.text = string.Format("{0:n0}", Poinven.Potion4Num);
    }

    public void Potion5NumUpdate()
    {
        UILabel Potion5Lable = GameObject.Find("Potion5_Num").GetComponent<UILabel>();
        Potion5Lable.text = string.Format("{0:n0}", Poinven.Potion5Num);
    }

    public void Potion6NumUpdate()
    {
        UILabel Potion6Lable = GameObject.Find("Potion6_Num").GetComponent<UILabel>();
        Potion6Lable.text = string.Format("{0:n0}", Poinven.Potion6Num);
    }

    public void Potion7NumUpdate()
    {
        UILabel Potion7Lable = GameObject.Find("Potion7_Num").GetComponent<UILabel>();
        Potion7Lable.text = string.Format("{0:n0}", Poinven.Potion7Num);
    }

    public void Potion8NumUpdate()
    {
        UILabel Potion8Lable = GameObject.Find("Potion8_Num").GetComponent<UILabel>();
        Potion8Lable.text = string.Format("{0:n0}", Poinven.Potion8Num);
    }

    public void Potion9NumUpdate()
    {
        UILabel Potion9Lable = GameObject.Find("Potion9_Num").GetComponent<UILabel>();
        Potion9Lable.text = string.Format("{0:n0}", Poinven.Potion9Num);
    }

    public void Potion10NumUpdate()
    {
        UILabel Potion10Lable = GameObject.Find("Potion10_Num").GetComponent<UILabel>();
        Potion10Lable.text = string.Format("{0:n0}", Poinven.Potion10Num);
    }

    public void Potion11NumUpdate()
    {
        UILabel Potion11Lable = GameObject.Find("Potion11_Num").GetComponent<UILabel>();
        Potion11Lable.text = string.Format("{0:n0}", Poinven.Potion11Num);
    }

    public void Potion12NumUpdate()
    {
        UILabel Potion12Lable = GameObject.Find("Potion12_Num").GetComponent<UILabel>();
        Potion12Lable.text = string.Format("{0:n0}", Poinven.Potion12Num);
    }

    public void Potion13NumUpdate()
    {
        UILabel Potion13Lable = GameObject.Find("Potion13_Num").GetComponent<UILabel>();
        Potion13Lable.text = string.Format("{0:n0}", Poinven.Potion13Num);
    }

    public void Potion14NumUpdate()
    {
        UILabel Potion14Lable = GameObject.Find("Potion14_Num").GetComponent<UILabel>();
        Potion14Lable.text = string.Format("{0:n0}", Poinven.Potion14Num);
    }

    public void Potion15NumUpdate()
    {
        UILabel Potion15Lable = GameObject.Find("Potion15_Num").GetComponent<UILabel>();
        Potion15Lable.text = string.Format("{0:n0}", Poinven.Potion15Num);
    }
    //------------------------------------------------------------------------
}
