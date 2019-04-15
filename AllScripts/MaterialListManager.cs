using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialListManager : MonoBehaviour {//재료를 관리하는 리스트 매니저 스크립트

    MessageManager Alert;

    public PlayerControl playerLevel;//플레이어의 레벨을 체크하고 레벨에 맞는 재료를 해금할수 있도록 하기 위한 변수

    public MatInventoryManager MatInven;//가지고 있는 재료를 확인하기 위한 변수

    public AlchemyManager usedAlchemyMat;//재료를 클릭했을때 AlchemyManager에게 넘기기 위한 변수

    public SkillSetManager IsCanSkill;
 
    public Transform DestinSprite;//해당 재료 아이콘이 들어갈 목적지 변수

    public Transform EffectDestination;

    public GameObject Effect;

    //----------------------------------------//아이템 오브젝트 
    public GameObject Itembutton1;

    public GameObject Itembutton2;

    public GameObject Itembutton3;

    public GameObject Itembutton4;

    public GameObject Itembutton5;

    public GameObject Itembutton6;

    public GameObject Itembutton7;

    public GameObject Itembutton8;

    public GameObject Itembutton9;

    public GameObject Itembutton10;

    public GameObject Itembutton11;

    public GameObject Itembutton12;

    public GameObject Itembutton13;

    public GameObject Itembutton14;

    public GameObject Itembutton15;
    //----------------------------------------//아이템 오브젝트의 아이콘
    public GameObject MiniItem1;

    public GameObject MiniItem2;

    public GameObject MiniItem3;

    public GameObject MiniItem4;

    public GameObject MiniItem5;

    public GameObject MiniItem6;

    public GameObject MiniItem7;

    public GameObject MiniItem8;

    public GameObject MiniItem9;

    public GameObject MiniItem10;

    public GameObject MiniItem11;

    public GameObject MiniItem12;

    public GameObject MiniItem13;

    public GameObject MiniItem14;

    public GameObject MiniItem15;
    //---------------------------------------
    public Transform MListPos;

    public GameObject HarbList;

    public GameObject TomatoList;

    public GameObject Item3List;

    public GameObject Item4List;

    public GameObject Item5List;

    public GameObject Item6List;

    public GameObject Item7List;

    public GameObject Item8List;

    public GameObject Item9List;

    public GameObject Item10List;

    public GameObject Item11List;

    public GameObject Item12List;

    public GameObject Item13List;

    public GameObject Item14List;

    public GameObject Item15List;
    //--------------------------------------
    bool Item1Used;

    bool Item2Used;

    bool Item3Used;

    bool Item4Used;

    bool Item5Used;

    bool Item6Used;

    bool Item7Used;

    bool Item8Used;

    bool Item9Used;

    bool Item10Used;

    bool Item11Used;

    bool Item12Used;

    bool Item13Used;

    bool Item14Used;

    bool Item15Used;
    //--------------------------------------
    public bool first1ItemHaved;

    bool first2ItemHaved;

    bool first3ItemHaved;

    bool first4ItemHaved;

    bool first5ItemHaved;

    bool first6ItemHaved;

    bool first7ItemHaved;

    bool first8ItemHaved;

    bool first9ItemHaved;

    bool first10ItemHaved;

    bool first11ItemHaved;

    bool first12ItemHaved;

    bool first13ItemHaved;

    bool first14ItemHaved;

    bool first15ItemHaved;
    //--------------------------------------

    public Dictionary<string, int> ItemListNum = new Dictionary<string, int>();
    public List<int> MatTypeNum = new List<int>();
    public int MatCount;
    //public int ItemTypeNum;

    //--------------------------------------
    int y = 0;

    //-----------데이터 로드------------------------------------
    private void Awake()
    {
        Alert = GameObject.Find("MessageManager").GetComponent<MessageManager>();
    }
    public void DataAccess(DataVo dataVo)
    {
        MatCount = int.Parse(dataVo.SaveNum);
        for (int index = 0; index < MatCount; index++)
        {
            LoadMatList(int.Parse(dataVo.SaveItems[index]));
        }

    }

    public void LoadMatList(int TypeNum)
    {
        switch (TypeNum)
        {
            case 1:
                HarbListUpdate();
                if(!ItemListNum.ContainsKey("허브")) ItemListNum.Add("허브", 1);
                break;
            case 2:
                TomatoListUpdate();
                if (!ItemListNum.ContainsKey("토마토")) ItemListNum.Add("토마토", 2);
                break;
            case 3:
                Item3ListUpdate();
                if (!ItemListNum.ContainsKey("붉은허브")) ItemListNum.Add("붉은허브", 3);
                break;
            case 4:
                Item4ListUpdate();
                if (!ItemListNum.ContainsKey("헛계나무")) ItemListNum.Add("헛계나무", 4);
                break;
            case 5:
                Item5ListUpdate();
                if (!ItemListNum.ContainsKey("흰색깃털")) ItemListNum.Add("흰색깃털", 5);
                break;
            case 6:
                Item6ListUpdate();
                if (!ItemListNum.ContainsKey("검정색깃털")) ItemListNum.Add("검정색깃털", 6);
                break;
            case 7:
                Item7ListUpdate();
                if (!ItemListNum.ContainsKey("비타민")) ItemListNum.Add("비타민", 7);
                break;
            case 8:
                Item8ListUpdate();
                if (!ItemListNum.ContainsKey("커피콩")) ItemListNum.Add("커피콩", 8);
                break;
            case 9:
                Item9ListUpdate();
                if (!ItemListNum.ContainsKey("식용버섯")) ItemListNum.Add("식용버섯", 9);
                break;
            case 10:
                Item10ListUpdate();
                if (!ItemListNum.ContainsKey("붉은버섯")) ItemListNum.Add("붉은버섯", 10);
                break;
            case 11:
                Item11ListUpdate();
                if (!ItemListNum.ContainsKey("플루메")) ItemListNum.Add("플루메", 11);
                break;
            case 12:
                Item12ListUpdate();
                if (!ItemListNum.ContainsKey("감초")) ItemListNum.Add("감초", 12);
                break;
            case 13:
                Item13ListUpdate();
                if (!ItemListNum.ContainsKey("설탕")) ItemListNum.Add("설탕", 13);
                break;
            case 14:
                Item14ListUpdate();
                if (!ItemListNum.ContainsKey("문슈가")) ItemListNum.Add("문슈가", 14);
                break;
            case 15:
                Item15ListUpdate();
                if (!ItemListNum.ContainsKey("자색허브")) ItemListNum.Add("자색허브", 15);
                break;
            case 0:
                break;
        }
    }

    //--------------------------------------------------
    IEnumerator DeleteEffect()
    {
        yield return new WaitForSeconds(0.3f);
        GameObject OutEffect = GameObject.FindGameObjectWithTag("Effect");
        Destroy(OutEffect);
        StopCoroutine("DeleteEffect");
    }

    public void HarbListUpdate()//재료 리스트
    {
        GameObject MList = GameObject.FindGameObjectWithTag("ReadyedMaterial");
        if (Item1Used == false && first1ItemHaved == false)
        {
            if (!MatInven.FirstItem1)
            {
                Alert.PopUpMaterialMesg("[59F100]허브[000000]가");
                ItemListNum.Add("허브", 1);
                MatCount += 1;
                MatInven.FirstItem1 = true;
            }

            MList = Instantiate(HarbList);
            MList.transform.parent = MListPos.transform;

            MList.transform.localPosition = new Vector3(0f, y, 0f);
            MList.transform.localScale = Vector3.one;
            Item1Used = true;
            y -= 550;
            first1ItemHaved = true;
            HarbNumUpdate();
        }
        else if (MatInven.haveHarb == 1 && Item1Used == true)
        {
            HarbNumUpdate();
        }
        else if(MatInven.haveHarb >= 1)
        {
            HarbNumUpdate();
        }
        else if (MatInven.haveHarb == 0)
        {
            HarbNumUpdate();
        }
        else if(MatInven.haveHarb <= 0 && Item1Used == true)
        { 
            HarbNumUpdate();
            Item1Used = false;
        }
    }

    public void TomatoListUpdate()
    {
        GameObject MList = GameObject.FindGameObjectWithTag("ReadyedMaterial");

        if (Item2Used == false && first2ItemHaved == false)
        {
            if (!MatInven.FirstItem2)
            {
                Alert.PopUpMaterialMesg("[FF6500]토마토[000000]가");
                ItemListNum.Add("토마토", 2);
                MatCount += 1;
                MatInven.FirstItem2 = true;
            }

            MList = Instantiate(TomatoList);
            MList.transform.parent = MListPos.transform;

            MList.transform.localPosition = new Vector3(0f, y, 0f);
            MList.transform.localScale = Vector3.one;
            Item2Used = true;
            y -= 550;
            first2ItemHaved = true;
            TomatoNumUpdate();
        }
        else if (MatInven.haveTomato == 1 && Item2Used == true)
        {
            TomatoNumUpdate();
        }
        else if (MatInven.haveTomato >= 1)
        {
            TomatoNumUpdate();
        }
        else if (MatInven.haveTomato == 0)
        {
            TomatoNumUpdate();
        }
        else if (MatInven.haveTomato <= 0 && Item2Used == true)
        {
            TomatoNumUpdate();
            Item2Used = false;
        }
        
    }

    public void Item3ListUpdate()
    {
        GameObject MList = GameObject.FindGameObjectWithTag("ReadyedMaterial");

        if (Item3Used == false && first3ItemHaved == false)
        {
            if (!MatInven.FirstItem3)
            {
                Alert.PopUpMaterialMesg("[FF007E]붉은허브[000000]가");
                ItemListNum.Add("붉은허브", 3);
                MatCount += 1;
                MatInven.FirstItem3 = true;
            }
            MList = Instantiate(Item3List);
            MList.transform.parent = MListPos.transform;

            MList.transform.localPosition = new Vector3(0f, y, 0f);
            MList.transform.localScale = Vector3.one;
            Item3Used = true;
            y -= 550;
            first3ItemHaved = true;
            Item3Update();
        }
        else if (MatInven.haveItem3 == 1 && Item3Used == true)
        {
            Item3Update();
        }
        else if (MatInven.haveItem3 >= 1)
        {
            Item3Update();
        }
        else if (MatInven.haveItem3 == 0)
        {
            Item3Update();
        }
        else if (MatInven.haveItem3 <= 0 && Item3Used == true)
        {
            Item3Update();
            Item3Used = false;
        }

    }

    public void Item4ListUpdate()
    {
        GameObject MList = GameObject.FindGameObjectWithTag("ReadyedMaterial");

        if ( Item4Used == false && first4ItemHaved == false)
        {
            if (!MatInven.FirstItem4)
            {
                Alert.PopUpMaterialMesg("[BEA700]헛계나뭇가지[000000]가");
                ItemListNum.Add("헛계나무", 4);
                MatCount += 1;
                MatInven.FirstItem4 = true;
            }

            MList = Instantiate(Item4List);
            MList.transform.parent = MListPos.transform;

            MList.transform.localPosition = new Vector3(0f, y, 0f);
            MList.transform.localScale = Vector3.one;
            Item4Used = true;
            y -= 550;
            first4ItemHaved = true;
            Item4Update();
        }
        else if (MatInven.haveItem4 == 1 && Item4Used == true)
        {
            Item4Update();
        }
        else if (MatInven.haveItem4 >= 1)
        {
            Item4Update();
        }
        else if (MatInven.haveItem4 == 0)
        {
            Item4Update();
        }
        else if (MatInven.haveItem4 <= 0 && Item4Used == true)
        {
            Item4Update();
            Item4Used = false;
        }

    }

    public void Item5ListUpdate()
    {
        GameObject MList = GameObject.FindGameObjectWithTag("ReadyedMaterial");

        if ( Item5Used == false && first5ItemHaved == false)
        {
            if (!MatInven.FirstItem5)
            {
                Alert.PopUpMaterialMesg("[7D7D7D]흰색깃털[000000]이");
                ItemListNum.Add("흰색깃털", 5);
                MatCount += 1;
                MatInven.FirstItem5 = true;
            }
            MList = Instantiate(Item5List);
            MList.transform.parent = MListPos.transform;

            MList.transform.localPosition = new Vector3(0f, y, 0f);
            MList.transform.localScale = Vector3.one;
            Item5Used = true;
            y -= 550;
            first5ItemHaved = true;
            Item5Update();
        }
        else if (MatInven.haveItem5 == 1 && Item5Used == true)
        {
            Item5Update();
        }
        else if (MatInven.haveItem5 >= 1)
        {
            Item5Update();
        }
        else if (MatInven.haveItem5 == 0)
        {
            Item5Update();
        }
        else if (MatInven.haveItem5 <= 0 && Item5Used == true)
        {
            Item5Update();
            Item5Used = false;
        }

    }

    public void Item6ListUpdate()
    {
        GameObject MList = GameObject.FindGameObjectWithTag("ReadyedMaterial");

        if (Item6Used == false && first6ItemHaved == false)
        {
            if (!MatInven.FirstItem6)
            {
                Alert.PopUpMaterialMesg("[3C3C3C]검정색깃털[000000]이");
                ItemListNum.Add("검정색깃털", 6);
                MatCount += 1;
                MatInven.FirstItem6 = true;
            }
            MList = Instantiate(Item6List);
            MList.transform.parent = MListPos.transform;

            MList.transform.localPosition = new Vector3(0f, y, 0f);
            MList.transform.localScale = Vector3.one;
            Item6Used = true;
            y -= 550;
            first6ItemHaved = true;
            Item6Update();
        }
        else if (MatInven.haveItem6 == 1 && Item6Used == true)
        {
            Item6Update();
        }
        else if (MatInven.haveItem6 >= 1)
        {
            Item6Update();
        }
        else if (MatInven.haveItem6 == 0)
        {
            Item6Update();
        }
        else if (MatInven.haveItem6 <= 0 && Item6Used == true)
        {
            Item6Update();
            Item6Used = false;
        }

    }

    public void Item7ListUpdate()
    {
        GameObject MList = GameObject.FindGameObjectWithTag("ReadyedMaterial");

        if ( Item7Used == false && first7ItemHaved == false)
        {
            if (!MatInven.FirstItem7)
            {
                Alert.PopUpMaterialMesg("[FF5700]네타민C[000000]가");
                ItemListNum.Add("비타민", 7);
                MatCount += 1;
                MatInven.FirstItem7 = true;
            }
            MList = Instantiate(Item7List);
            MList.transform.parent = MListPos.transform;

            MList.transform.localPosition = new Vector3(0f, y, 0f);
            MList.transform.localScale = Vector3.one;
            Item7Used = true;
            y -= 550;
            first7ItemHaved = true;
            Item7Update();
        }
        else if (MatInven.haveItem7 == 1 && Item7Used == true)
        {
            Item7Update();
        }
        else if (MatInven.haveItem7 >= 1)
        {
            Item7Update();
        }
        else if (MatInven.haveItem7 == 0)
        {
            Item7Update();
        }
        else if (MatInven.haveItem7 <= 0 && Item7Used == true)
        {
            Item7Update();
            Item7Used = false;
        }

    }

    public void Item8ListUpdate()
    {
        GameObject MList = GameObject.FindGameObjectWithTag("ReadyedMaterial");

        if ( Item8Used == false && first8ItemHaved == false)
        {
            if (!MatInven.FirstItem8)
            {
                Alert.PopUpMaterialMesg("[3C3C3C]카페인 콩[000000]이");
                ItemListNum.Add("커피콩", 8);
                MatCount += 1;
                MatInven.FirstItem8 = true;
            }
            MList = Instantiate(Item8List);
            MList.transform.parent = MListPos.transform;

            MList.transform.localPosition = new Vector3(0f, y, 0f);
            MList.transform.localScale = Vector3.one;
            Item8Used = true;
            y -= 550;
            first8ItemHaved = true;
            Item8Update();
        }
        else if (MatInven.haveItem8 == 1 && Item8Used == true)
        {
            Item8Update();
        }
        else if (MatInven.haveItem8 >= 1)
        {
            Item8Update();
        }
        else if (MatInven.haveItem8 == 0)
        {
            Item8Update();
        }
        else if (MatInven.haveItem8 <= 0 && Item8Used == true)
        {
            Item8Update();
            Item8Used = false;
        }

    }

    public void Item9ListUpdate()
    {
        GameObject MList = GameObject.FindGameObjectWithTag("ReadyedMaterial");

        if (Item9Used == false && first9ItemHaved == false)
        {
            if (!MatInven.FirstItem9)
            {
                Alert.PopUpMaterialMesg("[7B4935]식용버섯[000000]이");
                ItemListNum.Add("식용버섯", 9);
                MatCount += 1;
                MatInven.FirstItem9 = true;
            }
            MList = Instantiate(Item9List);
            MList.transform.parent = MListPos.transform;

            MList.transform.localPosition = new Vector3(0f, y, 0f);
            MList.transform.localScale = Vector3.one;
            Item9Used = true;
            y -= 550;
            first9ItemHaved = true;
            Item9Update();
        }
        else if (MatInven.haveItem9 == 1 && Item9Used == true)
        {
            Item9Update();
        }
        else if (MatInven.haveItem9 >= 1)
        {
            Item9Update();
        }
        else if (MatInven.haveItem9 == 0)
        {
            Item9Update();
        }
        else if (MatInven.haveItem9 <= 0 && Item9Used == true)
        {
            Item9Update();
            Item9Used = false;
        }

    }

    public void Item10ListUpdate()
    {
        GameObject MList = GameObject.FindGameObjectWithTag("ReadyedMaterial");

        if (Item10Used == false && first10ItemHaved == false)
        {
            if (!MatInven.FirstItem10)
            {
                Alert.PopUpMaterialMesg("[F50000]붉은버섯[000000]이");
                ItemListNum.Add("붉은버섯", 10);
                MatCount += 1;
                MatInven.FirstItem10 = true;
            }

            MList = Instantiate(Item10List);
            MList.transform.parent = MListPos.transform;

            MList.transform.localPosition = new Vector3(0f, y, 0f);
            MList.transform.localScale = Vector3.one;
            Item10Used = true;
            y -= 550;
            first10ItemHaved = true;
            Item10Update();
        }
        else if (MatInven.haveItem10 == 1 && Item10Used == true)
        {
            Item10Update();
        }
        else if (MatInven.haveItem10 >= 1)
        {
            Item10Update();
        }
        else if (MatInven.haveItem10 == 0)
        {
            Item10Update();
        }
        else if (MatInven.haveItem10 <= 0 && Item10Used == true)
        {
            Item10Update();
            Item10Used = false;
        }

    }

    public void Item11ListUpdate()
    {
        GameObject MList = GameObject.FindGameObjectWithTag("ReadyedMaterial");

        if (Item11Used == false && first11ItemHaved == false)
        {
            if (!MatInven.FirstItem11)
            {
                Alert.PopUpMaterialMesg("[FF00EA]빛나는플루메리아[000000]가");
                ItemListNum.Add("플루메", 11);
                MatCount += 1;
                MatInven.FirstItem11 = true;
            }

            MList = Instantiate(Item11List);
            MList.transform.parent = MListPos.transform;

            MList.transform.localPosition = new Vector3(0f, y, 0f);
            MList.transform.localScale = Vector3.one;
            Item11Used = true;
            y -= 550;
            first11ItemHaved = true;
            Item11Update();
        }
        else if (MatInven.haveItem11 == 1 && Item11Used == true)
        {
            Item11Update();
        }
        else if (MatInven.haveItem11 >= 1)
        {
            Item11Update();
        }
        else if (MatInven.haveItem11 == 0)
        {
            Item11Update();
        }
        else if (MatInven.haveItem11 <= 0 && Item11Used == true)
        {
            Item11Update();
            Item11Used = false;
        }

    }

    public void Item12ListUpdate()
    {
        GameObject MList = GameObject.FindGameObjectWithTag("ReadyedMaterial");

        if (Item12Used == false && first12ItemHaved == false)
        {
            if (!MatInven.FirstItem12)
            {
                Alert.PopUpMaterialMesg("[B76000]약방감초[000000]가");
                ItemListNum.Add("감초", 12);
                MatCount += 1;
                MatInven.FirstItem12 = true;
            }
            MList = Instantiate(Item12List);
            MList.transform.parent = MListPos.transform;

            MList.transform.localPosition = new Vector3(0f, y, 0f);
            MList.transform.localScale = Vector3.one;
            Item12Used = true;
            y -= 550;
            first12ItemHaved = true;
            Item12Update();
        }
        else if (MatInven.haveItem12 == 1 && Item12Used == true)
        {
            Item12Update();
        }
        else if (MatInven.haveItem12 >= 1)
        {
            Item12Update();
        }
        else if (MatInven.haveItem12 == 0)
        {
            Item12Update();
        }
        else if (MatInven.haveItem12 <= 0 && Item12Used == true)
        {
            Item12Update();
            Item12Used = false;
        }

    }

    public void Item13ListUpdate()
    {
        GameObject MList = GameObject.FindGameObjectWithTag("ReadyedMaterial");

        if ( Item13Used == false && first13ItemHaved == false)
        {
            if (!MatInven.FirstItem13)
            {
                Alert.PopUpMaterialMesg("[3C3C3C]각진설탕[000000]이");
                ItemListNum.Add("설탕", 13);
                MatCount += 1;
                MatInven.FirstItem13 = true;
            }
            MList = Instantiate(Item13List);
            MList.transform.parent = MListPos.transform;

            MList.transform.localPosition = new Vector3(0f, y, 0f);
            MList.transform.localScale = Vector3.one;
            Item13Used = true;
            y -= 550;
            first13ItemHaved = true;
            Item13Update();
        }
        else if (MatInven.haveItem13 == 1 && Item13Used == true)
        {
            Item13Update();
        }
        else if (MatInven.haveItem13 >= 1)
        {
            Item13Update();
        }
        else if (MatInven.haveItem13 == 0)
        {
            Item13Update();
        }
        else if (MatInven.haveItem13 <= 0 && Item13Used == true)
        {
            Item13Update();
            Item13Used = false;
        }

    }

    public void Item14ListUpdate()
    {
        GameObject MList = GameObject.FindGameObjectWithTag("ReadyedMaterial");

        if (Item14Used == false && first14ItemHaved == false)
        {
            if (!MatInven.FirstItem14)
            {
                Alert.PopUpMaterialMesg("[5C00FF]달빛설탕[000000]이");
                ItemListNum.Add("문슈가", 14);
                MatCount += 1;
                MatInven.FirstItem14 = true;
            }

            MList = Instantiate(Item14List);
            MList.transform.parent = MListPos.transform;

            MList.transform.localPosition = new Vector3(0f, y, 0f);
            MList.transform.localScale = Vector3.one;
            Item14Used = true;
            y -= 550;
            first14ItemHaved = true;
            Item14Update();
        }
        else if (MatInven.haveItem14 == 1 && Item14Used == true)
        {
            Item14Update();
        }
        else if (MatInven.haveItem14 >= 1)
        {
            Item14Update();
        }
        else if (MatInven.haveItem14 == 0)
        {
            Item14Update();
        }
        else if (MatInven.haveItem14 <= 0 && Item14Used == true)
        {
            Item14Update();
            Item14Used = false;
        }

    }

    public void Item15ListUpdate()
    {
        GameObject MList = GameObject.FindGameObjectWithTag("ReadyedMaterial");

        if (Item15Used == false && first15ItemHaved == false)
        {
            if (!MatInven.FirstItem15)
            {
                Alert.PopUpMaterialMesg("[2A0098]자색허브[000000]가");
                ItemListNum.Add("자색허브", 15);
                MatCount += 1;
                MatInven.FirstItem15 = true;
            }

            MList = Instantiate(Item15List);
            MList.transform.parent = MListPos.transform;

            MList.transform.localPosition = new Vector3(0f, y, 0f);
            MList.transform.localScale = Vector3.one;
            Item15Used = true;
            y -= 550;
            first15ItemHaved = true;
            Item15Update();
        }
        else if (MatInven.haveItem15 == 1 && Item15Used == true)
        {
            Item15Update();
        }
        else if (MatInven.haveItem15 >= 1)
        {
            Item15Update();
        }
        else if (MatInven.haveItem15 == 0)
        {
            Item15Update();
        }
        else if (MatInven.haveItem15 <= 0 && Item15Used == true)
        {
            Item15Update();
            Item15Used = false;
        }

    }
    //--------------------------------------------------------------------------------

    public void HarbNumUpdate()
    {
        UILabel Item1Lable = GameObject.Find("Item1_Num").GetComponent<UILabel>();
        Item1Lable.text = string.Format("{0:n0}", MatInven.haveHarb);
    }

    public void TomatoNumUpdate()
    {
        UILabel Item2Lable = GameObject.Find("Item2_Num").GetComponent<UILabel>();
        Item2Lable.text = string.Format("{0:n0}", MatInven.haveTomato);
    }

    public void Item3Update()
    {
        UILabel Item3Lable = GameObject.Find("Item3_Num").GetComponent<UILabel>();
        Item3Lable.text = string.Format("{0:n0}", MatInven.haveItem3);
    }

    public void Item4Update()
    {
        UILabel Item4Lable = GameObject.Find("Item4_Num").GetComponent<UILabel>();
        Item4Lable.text = string.Format("{0:n0}", MatInven.haveItem4);
    }

    public void Item5Update()
    {
        UILabel Item5Lable = GameObject.Find("Item5_Num").GetComponent<UILabel>();
        Item5Lable.text = string.Format("{0:n0}", MatInven.haveItem5);
    }

    public void Item6Update()
    {
        UILabel Item6Lable = GameObject.Find("Item6_Num").GetComponent<UILabel>();
        Item6Lable.text = string.Format("{0:n0}", MatInven.haveItem6);
    }

    public void Item7Update()
    {
        UILabel Item7Lable = GameObject.Find("Item7_Num").GetComponent<UILabel>();
        Item7Lable.text = string.Format("{0:n0}", MatInven.haveItem7);
    }

    public void Item8Update()
    {
        UILabel Item8Lable = GameObject.Find("Item8_Num").GetComponent<UILabel>();
        Item8Lable.text = string.Format("{0:n0}", MatInven.haveItem8);
    }

    public void Item9Update()
    {
        UILabel Item9Lable = GameObject.Find("Item9_Num").GetComponent<UILabel>();
        Item9Lable.text = string.Format("{0:n0}", MatInven.haveItem9);
    }

    public void Item10Update()
    {
        UILabel Item10Lable = GameObject.Find("Item10_Num").GetComponent<UILabel>();
        Item10Lable.text = string.Format("{0:n0}", MatInven.haveItem10);
    }

    public void Item11Update()
    {
        UILabel Item11Lable = GameObject.Find("Item11_Num").GetComponent<UILabel>();
        Item11Lable.text = string.Format("{0:n0}", MatInven.haveItem11);
    }

    public void Item12Update()
    {
        UILabel Item12Lable = GameObject.Find("Item12_Num").GetComponent<UILabel>();
        Item12Lable.text = string.Format("{0:n0}", MatInven.haveItem12);
    }

    public void Item13Update()
    {
        UILabel Item13Lable = GameObject.Find("Item13_Num").GetComponent<UILabel>();
        Item13Lable.text = string.Format("{0:n0}", MatInven.haveItem13);
    }

    public void Item14Update()
    {
        UILabel Item14Lable = GameObject.Find("Item14_Num").GetComponent<UILabel>();
        Item14Lable.text = string.Format("{0:n0}", MatInven.haveItem14);
    }

    public void Item15Update()
    {
        UILabel Item15Lable = GameObject.Find("Item15_Num").GetComponent<UILabel>();
        Item15Lable.text = string.Format("{0:n0}", MatInven.haveItem15);
    }

    //---------------------------------------------------------------------------------


    public void PushMaterial1Button()//재료 버튼 클릭시 발동하는 함수
    {
        if (AlchemyManager.pushMaterial != true) {
            if (MatInven.haveHarb > 0)
            {
                Instantiate(Effect, EffectDestination);
                SoundManager.sounds["UseMaterial"].Play();
                MatInven.haveHarb--;
                HarbListUpdate();
                usedAlchemyMat.useItemNum = 1;
                AlchemyManager.pushMaterial = true;

                if (IsCanSkill.Skill2Level < 2) IsCanSkill.cantEnfPotion = true;
                else IsCanSkill.cantEnfPotion = false;

                Instantiate(MiniItem1, DestinSprite);
                StartCoroutine("DeleteEffect");
            }
            else SoundManager.sounds["NotItem"].Play();
        }
        else SoundManager.sounds["NotItem"].Play();
    }

    public void PushMaterial2Button()
    {
        if (AlchemyManager.pushMaterial != true)
        {
            if (MatInven.haveTomato > 0)
            {
                Instantiate(Effect, EffectDestination);
                SoundManager.sounds["UseMaterial"].Play();
                MatInven.haveTomato--;
                TomatoListUpdate();
                usedAlchemyMat.useItemNum = 2;
                AlchemyManager.pushMaterial = true;

                IsCanSkill.cantChange = true;
                IsCanSkill.cantEnfPotion = true; 

                Instantiate(MiniItem2, DestinSprite);
                StartCoroutine("DeleteEffect");
            }
            else SoundManager.sounds["NotItem"].Play();
        }
        else SoundManager.sounds["NotItem"].Play();
    }

    public void PushMaterial3Button()//붉은허브
    {
        if (AlchemyManager.pushMaterial != true)
        {
            if (MatInven.haveItem3 > 0)
            {
                Instantiate(Effect, EffectDestination);
                SoundManager.sounds["UseMaterial"].Play();
                MatInven.haveItem3--;
                Item3ListUpdate();
                usedAlchemyMat.useItemNum = 3;
                AlchemyManager.pushMaterial = true;

                IsCanSkill.cantChange = true;
                IsCanSkill.cantEnfPotion = true;

                Instantiate(MiniItem3, DestinSprite);
                StartCoroutine("DeleteEffect");
            }
            else SoundManager.sounds["NotItem"].Play();
        }
        else SoundManager.sounds["NotItem"].Play();
    }

    public void PushMaterial4Button()//헛계
    {
        if (AlchemyManager.pushMaterial != true)
        {
            if (MatInven.haveItem4 > 0)
            {
                Instantiate(Effect, EffectDestination);
                SoundManager.sounds["UseMaterial"].Play();
                MatInven.haveItem4--;
                Item4ListUpdate();
                usedAlchemyMat.useItemNum = 4;
                AlchemyManager.pushMaterial = true;

                IsCanSkill.cantChange = true;
                IsCanSkill.cantEnfPotion = true;

                Instantiate(MiniItem4, DestinSprite);
                StartCoroutine("DeleteEffect");
            }
            else SoundManager.sounds["NotItem"].Play();
        }
        else SoundManager.sounds["NotItem"].Play();
    }

    public void PushMaterial5Button()//흰깃털
    {
        if (AlchemyManager.pushMaterial != true)
        {
            if (MatInven.haveItem5 > 0)
            {
                Instantiate(Effect, EffectDestination);
                SoundManager.sounds["UseMaterial"].Play();
                MatInven.haveItem5--;
                Item5ListUpdate();
                usedAlchemyMat.useItemNum = 5;
                AlchemyManager.pushMaterial = true;
                Instantiate(MiniItem5, DestinSprite);
                StartCoroutine("DeleteEffect");
            }
            else SoundManager.sounds["NotItem"].Play();
        }
        else SoundManager.sounds["NotItem"].Play();
    }

    public void PushMaterial6Button()//검정깃털
    {
        if (AlchemyManager.pushMaterial != true)
        {
            if (MatInven.haveItem6 > 0)
            {
                Instantiate(Effect, EffectDestination);
                SoundManager.sounds["UseMaterial"].Play();
                MatInven.haveItem6--;
                Item6ListUpdate();
                usedAlchemyMat.useItemNum = 6;

                IsCanSkill.cantChange = true;
                IsCanSkill.cantEnfPotion = true;

                AlchemyManager.pushMaterial = true;
                Instantiate(MiniItem6, DestinSprite);
                StartCoroutine("DeleteEffect");
            }
            else SoundManager.sounds["NotItem"].Play();
        }
        else SoundManager.sounds["NotItem"].Play();
    }

    public void PushMaterial7Button()//비타민씨
    {
        if (AlchemyManager.pushMaterial != true)
        {
            if (MatInven.haveItem7 > 0)
            {
                Instantiate(Effect, EffectDestination);
                SoundManager.sounds["UseMaterial"].Play();
                MatInven.haveItem7--;
                Item7ListUpdate();
                usedAlchemyMat.useItemNum = 7;
                AlchemyManager.pushMaterial = true;

                IsCanSkill.cantChange = true;
                IsCanSkill.cantEnfPotion = true;

                Instantiate(MiniItem7, DestinSprite);
                StartCoroutine("DeleteEffect");
            }
            else SoundManager.sounds["NotItem"].Play();
        }
        else SoundManager.sounds["NotItem"].Play();
    }

    public void PushMaterial8Button()//커피콩
    {
        if (AlchemyManager.pushMaterial != true)
        {
            if (MatInven.haveItem8 > 0)
            {
                Instantiate(Effect, EffectDestination);
                SoundManager.sounds["UseMaterial"].Play();
                MatInven.haveItem8--;
                Item8ListUpdate();
                usedAlchemyMat.useItemNum = 8;
                AlchemyManager.pushMaterial = true;

                IsCanSkill.cantChange = true;
                IsCanSkill.cantEnfPotion = true;

                Instantiate(MiniItem8, DestinSprite);
                StartCoroutine("DeleteEffect");
            }
            else SoundManager.sounds["NotItem"].Play();
        }
        else SoundManager.sounds["NotItem"].Play();
    }

    public void PushMaterial9Button()//버섯
    {
        if (AlchemyManager.pushMaterial != true)
        {
            if (MatInven.haveItem9 > 0)
            {
                Instantiate(Effect, EffectDestination);
                SoundManager.sounds["UseMaterial"].Play();
                MatInven.haveItem9--;
                Item9ListUpdate();
                usedAlchemyMat.useItemNum = 9;
                AlchemyManager.pushMaterial = true;


                Instantiate(MiniItem9, DestinSprite);
                StartCoroutine("DeleteEffect");
            }
            else SoundManager.sounds["NotItem"].Play();
        }
        else SoundManager.sounds["NotItem"].Play();
    }

    public void PushMaterial10Button()//빨간버섯
    {
        if (AlchemyManager.pushMaterial != true)
        {
            if (MatInven.haveItem10 > 0)
            {
                Instantiate(Effect, EffectDestination);
                SoundManager.sounds["UseMaterial"].Play();
                MatInven.haveItem10--;
                Item10ListUpdate();
                usedAlchemyMat.useItemNum = 10;
                AlchemyManager.pushMaterial = true;

                if (IsCanSkill.Skill3Level < 2) IsCanSkill.cantEnforce = true;
                IsCanSkill.cantEnfPotion = true;

                Instantiate(MiniItem10, DestinSprite);
                StartCoroutine("DeleteEffect");
            }
            else SoundManager.sounds["NotItem"].Play();
        }
        else SoundManager.sounds["NotItem"].Play();
    }

    public void PushMaterial11Button()//플루메리아
    {
        if (AlchemyManager.pushMaterial != true)
        {
            if (MatInven.haveItem11 > 0)
            {
                Instantiate(Effect, EffectDestination);
                SoundManager.sounds["UseMaterial"].Play();
                MatInven.haveItem11--;
                Item11ListUpdate();
                usedAlchemyMat.useItemNum = 11;
                AlchemyManager.pushMaterial = true;

                IsCanSkill.cantChange = true;
                IsCanSkill.cantEnfPotion = true;

                Instantiate(MiniItem11, DestinSprite);
                StartCoroutine("DeleteEffect");
            }
            else SoundManager.sounds["NotItem"].Play();
        }
        else SoundManager.sounds["NotItem"].Play();
    }

    public void PushMaterial12Button()//약재
    {
        if (AlchemyManager.pushMaterial != true)
        {
            if (MatInven.haveItem12 > 0)
            {
                Instantiate(Effect, EffectDestination);
                SoundManager.sounds["UseMaterial"].Play();
                MatInven.haveItem12--;
                Item12ListUpdate();
                usedAlchemyMat.useItemNum = 12;
                AlchemyManager.pushMaterial = true;

                IsCanSkill.cantChange = true;
                IsCanSkill.cantEnfPotion = true;

                Instantiate(MiniItem12, DestinSprite);
                StartCoroutine("DeleteEffect");
            }
            else SoundManager.sounds["NotItem"].Play();
        }
        else SoundManager.sounds["NotItem"].Play();
    }

    public void PushMaterial13Button()//설탕
    {
        if (AlchemyManager.pushMaterial != true)
        {
            if (MatInven.haveItem13 > 0)
            {
                Instantiate(Effect, EffectDestination);
                SoundManager.sounds["UseMaterial"].Play();
                MatInven.haveItem13--;
                Item13ListUpdate();
                usedAlchemyMat.useItemNum = 13;
                AlchemyManager.pushMaterial = true;

                //IsCanSkill.cantChange = true;
                if (IsCanSkill.Skill3Level < 2) IsCanSkill.cantEnforce = true;
                IsCanSkill.cantEnfPotion = true;

                Instantiate(MiniItem13, DestinSprite);
                StartCoroutine("DeleteEffect");
            }
            else SoundManager.sounds["NotItem"].Play();
        }
        else SoundManager.sounds["NotItem"].Play();
    }

    public void PushMaterial14Button()//문슈가
    {
        if (AlchemyManager.pushMaterial != true)
        {
            if (MatInven.haveItem14 > 0)
            {
                Instantiate(Effect, EffectDestination);
                SoundManager.sounds["UseMaterial"].Play();
                MatInven.haveItem14--;
                Item14ListUpdate();
                usedAlchemyMat.useItemNum = 14;
                AlchemyManager.pushMaterial = true;

                IsCanSkill.cantChange = true;

                Instantiate(MiniItem14, DestinSprite);
                StartCoroutine("DeleteEffect");
            }
            else SoundManager.sounds["NotItem"].Play();
        }
        else SoundManager.sounds["NotItem"].Play();
    }

    public void PushMaterial15Button()//자색허브
    {
        if (AlchemyManager.pushMaterial != true)
        {
            if (MatInven.haveItem15 > 0)
            {
                Instantiate(Effect, EffectDestination);
                SoundManager.sounds["UseMaterial"].Play();
                MatInven.haveItem15--;
                Item15ListUpdate();
                usedAlchemyMat.useItemNum = 15;
                AlchemyManager.pushMaterial = true;

                if (IsCanSkill.Skill2Level < 2) IsCanSkill.cantEnfPotion = true;
                else IsCanSkill.cantEnfPotion = false;

                Instantiate(MiniItem15, DestinSprite);
                StartCoroutine("DeleteEffect");
            }
            else SoundManager.sounds["NotItem"].Play();
        }
        else SoundManager.sounds["NotItem"].Play();
    }
}

