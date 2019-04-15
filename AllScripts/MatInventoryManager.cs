using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatInventoryManager : MonoBehaviour
{

    public FurnitureManager FurnitureEffect;

    public MaterialListManager matList;

    public PlayerControl Player;

    public MaterialParmeter MatPar;

    public SpriteRenderer MatParSprite;

    public SpriteRenderer MatSprite;

    public int Itemnum;

    public int haveItem;

    //--------------------------------------종류별 보유 아이템 수

    public int haveHarb;

    public int haveTomato;

    public int haveItem3;

    public int haveItem4;

    public int haveItem5;

    public int haveItem6;

    public int haveItem7;

    public int haveItem8;

    public int haveItem9;

    public int haveItem10;

    public int haveItem11;

    public int haveItem12;

    public int haveItem13;

    public int haveItem14;

    public int haveItem15;

    //------------------------------------------------------------
    public bool FirstItem1;
    public bool FirstItem2;
    public bool FirstItem3;
    public bool FirstItem4;
    public bool FirstItem5;
    public bool FirstItem6;
    public bool FirstItem7;
    public bool FirstItem8;
    public bool FirstItem9;
    public bool FirstItem10;
    public bool FirstItem11;
    public bool FirstItem12;
    public bool FirstItem13;
    public bool FirstItem14;
    public bool FirstItem15;
    /*  void Awake()
      {
          StartCoroutine("GetMaterial");

          haveHarb = DataManager.SaveItem1;
          haveTomato = DataManager.SaveItem2;
          haveItem3 = DataManager.SaveItem3;
          haveItem4 = DataManager.SaveItem4;
          haveItem5 = DataManager.SaveItem5;
          haveItem6 = DataManager.SaveItem6;
          haveItem7 = DataManager.SaveItem7;
          haveItem8 = DataManager.SaveItem8;
          haveItem9 = DataManager.SaveItem9;
          haveItem10 = DataManager.SaveItem10;
          haveItem11 = DataManager.SaveItem11;
          haveItem12 = DataManager.SaveItem12;
          haveItem13 = DataManager.SaveItem13;
          haveItem14 = DataManager.SaveItem14;
      }*/
    //---------------데이터로드----------------
    public void DataAccess(DataVo dataVo)
    {
        StartCoroutine("GetMaterial");

        haveHarb = int.Parse(dataVo.SaveItem1);
        haveTomato = int.Parse(dataVo.SaveItem2);
        haveItem3 = int.Parse(dataVo.SaveItem3);
        haveItem4 = int.Parse(dataVo.SaveItem4);
        haveItem5 = int.Parse(dataVo.SaveItem5);
        haveItem6 = int.Parse(dataVo.SaveItem6);
        haveItem7 = int.Parse(dataVo.SaveItem7);
        haveItem8 = int.Parse(dataVo.SaveItem8);
        haveItem9 = int.Parse(dataVo.SaveItem9);
        haveItem10 = int.Parse(dataVo.SaveItem10);
        haveItem11 = int.Parse(dataVo.SaveItem11);
        haveItem12 = int.Parse(dataVo.SaveItem12);
        haveItem13 = int.Parse(dataVo.SaveItem13);
        haveItem14 = int.Parse(dataVo.SaveItem14);
        haveItem15 = int.Parse(dataVo.SaveItem15);

        FirstItem1 = bool.Parse(dataVo.FirstItem1);
        FirstItem2 = bool.Parse(dataVo.FirstItem2);
        FirstItem3 = bool.Parse(dataVo.FirstItem3);
        FirstItem4 = bool.Parse(dataVo.FirstItem4);
        FirstItem5 = bool.Parse(dataVo.FirstItem5);
        FirstItem6 = bool.Parse(dataVo.FirstItem6);
        FirstItem7 = bool.Parse(dataVo.FirstItem7);
        FirstItem8 = bool.Parse(dataVo.FirstItem8);
        FirstItem9 = bool.Parse(dataVo.FirstItem9);
        FirstItem10 = bool.Parse(dataVo.FirstItem10);
        FirstItem11 = bool.Parse(dataVo.FirstItem11);
        FirstItem12 = bool.Parse(dataVo.FirstItem12);
        FirstItem13 = bool.Parse(dataVo.FirstItem13);
        FirstItem14 = bool.Parse(dataVo.FirstItem14);
        FirstItem15 = bool.Parse(dataVo.FirstItem15);

        matList.DataAccess(dataVo);
    }
    //-----------------------------------------


   /* public void MaterialSaved()
    {
        DataManager.SaveItem1 = haveHarb;
        DataManager.SaveItem2 = haveTomato;
        DataManager.SaveItem3 = haveItem3;
        DataManager.SaveItem4 = haveItem4;
        DataManager.SaveItem5 = haveItem5;
        DataManager.SaveItem6 = haveItem6;
        DataManager.SaveItem7 = haveItem7;
        DataManager.SaveItem8 = haveItem8;
        DataManager.SaveItem9 = haveItem9;
        DataManager.SaveItem10 = haveItem10;
        DataManager.SaveItem11 = haveItem11;
        DataManager.SaveItem12 = haveItem12;
        DataManager.SaveItem13 = haveItem13;
        DataManager.SaveItem14 = haveItem14;
    }
    */
    IEnumerator GetMaterial()//떨어진 재료아이탬을 획득하고 해당 재료를 재료 칸에 띄우는 코루틴
    {
        yield return new WaitForSeconds(0.5f- (FurnitureEffect.Furniture3_3Effect));

        GameObject[] Materials = GameObject.FindGameObjectsWithTag("Material");

        Vector3 PlayerPos = GameObject.Find("Player").transform.position;
        Vector3 NPCPos = GameObject.Find("Rain").transform.position;
        for (int index = 0; index < Materials.Length; index++)
        {
            if (Materials[index].transform.position.x >= PlayerPos.x - 0.5f && Materials[index].transform.position.x <= PlayerPos.x + 0.5f && Materials[index].transform.position.y <= -2.2f)
            {

                MatPar = Materials[index].GetComponent<MaterialParmeter>();
                MatParSprite = Materials[index].GetComponent<SpriteRenderer>();

                SoundManager.sounds["Pop (3)"].Play();
                Rigidbody2D rigid = Materials[index].GetComponent<Rigidbody2D>();
                Vector2 getMotion = new Vector2(0f, 4f);
                rigid.AddForce(getMotion, ForceMode2D.Impulse);
                Itemnum = MatPar.ItemNum;

                switch (Itemnum)
                {
                    case 1:
                        if (!FirstItem1)
                        {
                            DataCallManager.SaveItemType(1);
                        }
                        haveHarb += MatPar.ItemCount;
                        haveItem++;
                        matList.HarbListUpdate();
                        break;

                    case 2:
                        if (!FirstItem2)
                        {
                            DataCallManager.SaveItemType(2);
                        }
                        haveTomato += MatPar.ItemCount;   
                        haveItem++;
                        matList.TomatoListUpdate();
                        break;

                    case 3:
                        if (!FirstItem3)
                        {
                            DataCallManager.SaveItemType(3);
                        }
                        haveItem3 += MatPar.ItemCount;
                        haveItem++;
                        matList.Item3ListUpdate();
                        break;
                    case 4:
                        if (!FirstItem4)
                        {
                            DataCallManager.SaveItemType(4);
                        }
                        haveItem4 += MatPar.ItemCount;
                        haveItem++;
                        matList.Item4ListUpdate();
                        break;
                    case 5:
                        if (!FirstItem5)
                        {
                            DataCallManager.SaveItemType(5);
                        }
                        haveItem5 += MatPar.ItemCount;
                        haveItem++;
                        matList.Item5ListUpdate();
                        break;
                    case 6:
                        if (!FirstItem6)
                        {
                            DataCallManager.SaveItemType(6);
                        }
                        haveItem6 += MatPar.ItemCount;
                        haveItem++;
                        matList.Item6ListUpdate();
                        break;
                    case 7:
                        if (!FirstItem7)
                        {
                            DataCallManager.SaveItemType(7);
                        }
                        haveItem7 += MatPar.ItemCount;
                        haveItem++;
                        matList.Item7ListUpdate();
                        break;
                    case 8:
                        if (!FirstItem8)
                        {
                            DataCallManager.SaveItemType(8);
                        }
                        haveItem8 += MatPar.ItemCount;
                        haveItem++;
                        matList.Item8ListUpdate();
                        break;
                    case 9:
                        if (!FirstItem9)
                        {
                            DataCallManager.SaveItemType(9);
                        }
                        haveItem9 += MatPar.ItemCount;
                        haveItem++;
                        matList.Item9ListUpdate();
                        break;

                    case 10:
                        if (!FirstItem10)
                        {
                            DataCallManager.SaveItemType(10);
                        }
                        haveItem10 += MatPar.ItemCount;
                        haveItem++;
                        matList.Item10ListUpdate();
                        break;
                    case 11:
                        if (!FirstItem11)
                        {
                            DataCallManager.SaveItemType(11);
                        }
                        haveItem11 += MatPar.ItemCount;
                        haveItem++;
                        matList.Item11ListUpdate();
                        break;
                    case 12:
                        if (!FirstItem12)
                        {
                            DataCallManager.SaveItemType(12);
                        }
                        haveItem12 += MatPar.ItemCount;
                        haveItem++;
                        matList.Item12ListUpdate();
                        break;
                    case 13:
                        if (!FirstItem13)
                        {
                            DataCallManager.SaveItemType(13);
                        }
                        haveItem13 += MatPar.ItemCount;
                        haveItem++;
                        matList.Item13ListUpdate();
                        break;
                    case 14:
                        if (!FirstItem14)
                        {
                            DataCallManager.SaveItemType(14);
                        }
                        haveItem14 += MatPar.ItemCount;
                        haveItem++;
                        matList.Item14ListUpdate();
                        break;
                    case 15:
                        if (!FirstItem15)
                        {
                            DataCallManager.SaveItemType(15);
                        }
                        haveItem15 += MatPar.ItemCount;
                        haveItem++;
                        matList.Item15ListUpdate();
                        break;
                }

                Destroy(Materials[index].gameObject, 0.4f);
            }

            else if (Materials[index].transform.position.x >= NPCPos.x - 0.5f && Materials[index].transform.position.x <= NPCPos.x + 0.5f && Materials[index].transform.position.y <= -2.2f)
            {

                MatPar = Materials[index].GetComponent<MaterialParmeter>();
                MatParSprite = Materials[index].GetComponent<SpriteRenderer>();

                SoundManager.sounds["Pop (3)"].Play();
                Rigidbody2D rigid = Materials[index].GetComponent<Rigidbody2D>();
                Vector2 getMotion = new Vector2(0f, 4f);
                rigid.AddForce(getMotion, ForceMode2D.Impulse);
                Itemnum = MatPar.ItemNum;

                switch (Itemnum)
                {
                    case 1:
                        if (!FirstItem1)
                        {
                            DataCallManager.SaveItemType(1);
                        }
                        haveHarb += MatPar.ItemCount;
                        haveItem++;
                        matList.HarbListUpdate();
                        break;

                    case 2:
                        if (!FirstItem2)
                        {
                            DataCallManager.SaveItemType(2);
                        }
                        haveTomato += MatPar.ItemCount;
                        haveItem++;
                        matList.TomatoListUpdate();
                        break;

                    case 3:
                        if (!FirstItem3)
                        {
                            DataCallManager.SaveItemType(3);
                        }
                        haveItem3 += MatPar.ItemCount;
                        haveItem++;
                        matList.Item3ListUpdate();
                        break;
                    case 4:
                        if (!FirstItem4)
                        {
                            DataCallManager.SaveItemType(4);
                        }
                        haveItem4 += MatPar.ItemCount;
                        haveItem++;
                        matList.Item4ListUpdate();
                        break;
                    case 5:
                        if (!FirstItem5)
                        {
                            DataCallManager.SaveItemType(5);
                        }
                        haveItem5 += MatPar.ItemCount;
                        haveItem++;
                        matList.Item5ListUpdate();
                        break;
                    case 6:
                        if (!FirstItem6)
                        {
                            DataCallManager.SaveItemType(6);
                        }
                        haveItem6 += MatPar.ItemCount;
                        haveItem++;
                        matList.Item6ListUpdate();
                        break;
                    case 7:
                        if (!FirstItem7)
                        {
                            DataCallManager.SaveItemType(7);
                        }
                        haveItem7 += MatPar.ItemCount;
                        haveItem++;
                        matList.Item7ListUpdate();
                        break;
                    case 8:
                        if (!FirstItem8)
                        {
                            DataCallManager.SaveItemType(8);
                        }
                        haveItem8 += MatPar.ItemCount;
                        haveItem++;
                        matList.Item8ListUpdate();
                        break;
                    case 9:
                        if (!FirstItem9)
                        {
                            DataCallManager.SaveItemType(9);
                        }
                        haveItem9 += MatPar.ItemCount;
                        haveItem++;
                        matList.Item9ListUpdate();
                        break;

                    case 10:
                        if (!FirstItem10)
                        {
                            DataCallManager.SaveItemType(10);
                        }
                        haveItem10 += MatPar.ItemCount;
                        haveItem++;
                        matList.Item10ListUpdate();
                        break;
                    case 11:
                        if (!FirstItem11)
                        {
                            DataCallManager.SaveItemType(11);
                        }
                        haveItem11 += MatPar.ItemCount;
                        haveItem++;
                        matList.Item11ListUpdate();
                        break;
                    case 12:
                        if (!FirstItem12)
                        {
                            DataCallManager.SaveItemType(12);
                        }
                        haveItem12 += MatPar.ItemCount;
                        haveItem++;
                        matList.Item12ListUpdate();
                        break;
                    case 13:
                        if (!FirstItem13)
                        {
                            DataCallManager.SaveItemType(13);
                        }
                        haveItem13 += MatPar.ItemCount;
                        haveItem++;
                        matList.Item13ListUpdate();
                        break;
                    case 14:
                        if (!FirstItem14)
                        {
                            DataCallManager.SaveItemType(14);
                        }
                        haveItem14 += MatPar.ItemCount;
                        haveItem++;
                        matList.Item14ListUpdate();
                        break;
                    case 15:
                        if (!FirstItem15)
                        {
                            DataCallManager.SaveItemType(15);
                        }
                        haveItem15 += MatPar.ItemCount;
                        haveItem++;
                        matList.Item15ListUpdate();
                        break;
                }

                Destroy(Materials[index].gameObject, 0.4f);
            }
        }
        StartCoroutine("GetMaterial");
    }
}

