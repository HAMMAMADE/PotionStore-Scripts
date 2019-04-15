using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HavePoInvenManager : MonoBehaviour {

    public PlayerControl Player;

    public PotionParmeter PoParmeter;

    public HavePotionListManager PoList;

    public FurnitureManager FurnitureEffect;

    MaterialManager getBase;

    public int BluePotion, RedPotion;

    int GetPotionCount;

    int PotionCase;
    //--------------종류별 보유 포션 수

    public int Potion1Num;

    public int Potion2Num;

    public int Potion3Num;

    public int Potion4Num;

    public int Potion5Num;

    public int Potion6Num;

    public int Potion7Num;

    public int Potion8Num;

    public int Potion9Num;

    public int Potion10Num;

    public int Potion11Num;

    public int Potion12Num;

    public int Potion13Num;

    public int Potion14Num;

    public int Potion15Num;
    //---------------------------------
    public bool FirstPotion1;
    public bool FirstPotion2;
    public bool FirstPotion3;
    public bool FirstPotion4;
    public bool FirstPotion5;
    public bool FirstPotion6;
    public bool FirstPotion7;
    public bool FirstPotion8;
    public bool FirstPotion9;
    public bool FirstPotion10;
    public bool FirstPotion11;
    public bool FirstPotion12;
    public bool FirstPotion13;
    public bool FirstPotion14;
    public bool FirstPotion15;

    //---------------------------------
    // float speed = 5000;



    /* void Awake()
     {
         getBase = GameObject.Find("MaterialManager").GetComponent<MaterialManager>();
         StartCoroutine("GetPotion");

         Potion1Num = DataManager.SavePotion1;
         Potion2Num = DataManager.SavePotion2;
         Potion3Num = DataManager.SavePotion3;
         Potion4Num = DataManager.SavePotion4;
         Potion5Num = DataManager.SavePotion5;
         Potion6Num = DataManager.SavePotion6;
         Potion7Num = DataManager.SavePotion7;
         Potion8Num = DataManager.SavePotion8;
         Potion9Num = DataManager.SavePotion9;
         Potion10Num = DataManager.SavePotion10;
         Potion11Num = DataManager.SavePotion11;

     }*/
    //----데이터로드---------------------------------
    public void DataAccess(DataVo dataVo)
    {
        getBase = GameObject.Find("MaterialManager").GetComponent<MaterialManager>();
        StartCoroutine("GetPotion");

        Potion1Num = int.Parse(dataVo.SavePotion1);
        Potion2Num = int.Parse(dataVo.SavePotion2);
        Potion3Num = int.Parse(dataVo.SavePotion3);
        Potion4Num = int.Parse(dataVo.SavePotion4);
        Potion5Num = int.Parse(dataVo.SavePotion5);
        Potion6Num = int.Parse(dataVo.SavePotion6);
        Potion7Num = int.Parse(dataVo.SavePotion7);
        Potion8Num = int.Parse(dataVo.SavePotion8);
        Potion9Num = int.Parse(dataVo.SavePotion9);
        Potion10Num = int.Parse(dataVo.SavePotion10);
        Potion11Num = int.Parse(dataVo.SavePotion11);
        Potion12Num = int.Parse(dataVo.SavePotion12);
        Potion13Num = int.Parse(dataVo.SavePotion13);
        Potion14Num = int.Parse(dataVo.SavePotion14);
        Potion15Num = int.Parse(dataVo.SavePotion15);

        FirstPotion1 = bool.Parse(dataVo.FirstPotion1);
        FirstPotion2 = bool.Parse(dataVo.FirstPotion2);
        FirstPotion3 = bool.Parse(dataVo.FirstPotion3);
        FirstPotion4 = bool.Parse(dataVo.FirstPotion4);
        FirstPotion5 = bool.Parse(dataVo.FirstPotion5);
        FirstPotion6 = bool.Parse(dataVo.FirstPotion6);
        FirstPotion7 = bool.Parse(dataVo.FirstPotion7);
        FirstPotion8 = bool.Parse(dataVo.FirstPotion8);
        FirstPotion9 = bool.Parse(dataVo.FirstPotion9);
        FirstPotion10 = bool.Parse(dataVo.FirstPotion10);
        FirstPotion11 = bool.Parse(dataVo.FirstPotion11);
        FirstPotion12 = bool.Parse(dataVo.FirstPotion12);
        FirstPotion13 = bool.Parse(dataVo.FirstPotion13);
        FirstPotion14 = bool.Parse(dataVo.FirstPotion14);
        FirstPotion15 = bool.Parse(dataVo.FirstPotion15);

        PoList.DataAccess(dataVo);
    }

    //-----------------------------------------------

   /* public void PotionSaved()
    {
        DataManager.SavePotion1 = Potion1Num;
        DataManager.SavePotion2 = Potion2Num;
        DataManager.SavePotion3 = Potion3Num;
        DataManager.SavePotion4 = Potion4Num;
        DataManager.SavePotion5 = Potion5Num;
        DataManager.SavePotion6 = Potion6Num;
        DataManager.SavePotion7 = Potion7Num;
        DataManager.SavePotion8 = Potion8Num;
        DataManager.SavePotion9 = Potion9Num;
        DataManager.SavePotion10 = Potion10Num;
        DataManager.SavePotion11 = Potion11Num;
    }*/

        IEnumerator GetPotion()
    {
        yield return new WaitForSeconds(0.5f - (FurnitureEffect.Furniture3_3Effect));

        GetPotionCount = 0;

        GameObject[] Potion = GameObject.FindGameObjectsWithTag("ComPotion");

        Vector3 PlayerPos = GameObject.Find("Player").transform.position;
        Vector3 NPCPos = GameObject.Find("Rain").transform.position;

        for (int index = 0; index < Potion.Length; index++)
        {
            if (Potion[index].transform.position.x >= PlayerPos.x - 0.5f && Potion[index].transform.position.x <= PlayerPos.x + 0.5f && Potion[index].transform.position.y <= -2.2f)
            {
                if (GetPotionCount < 1)
                {
                    
                    PoParmeter = Potion[index].GetComponent<PotionParmeter>();
                    SoundManager.sounds["Pop (3)"].Play();
                    Rigidbody2D rigid = Potion[index].GetComponent<Rigidbody2D>();
                    Vector2 getMotion = new Vector2(0f, 8f);


                    rigid.AddForce(getMotion, ForceMode2D.Impulse);
                    rigid.AddTorque(90f);

                    GetPotionCount++;
                    PotionCase = PoParmeter.PotionCaseNum;
                    switch(PotionCase)
                    {
                        case 0:
                            if (PoParmeter.isGasMat)
                                MaterialManager.MaterialGasNumber += 2;
                            else
                                MaterialManager.MaterialQSilverNumber += 2;

                            getBase.MaterialButtonUpdate();
                            break;
                        case 1:
                            if (!FirstPotion1)
                            {
                                DataCallManager.SavePotionType(1);
                            }
                            Potion1Num += PoParmeter.PotionNum;
                            PoList.Potion1ListUpdate();
                            break;
                        case 2:
                            if (!FirstPotion2)
                            {
                                DataCallManager.SavePotionType(2);
                            }
                            Potion2Num += PoParmeter.PotionNum;
                            PoList.Potion2ListUpdate();
                            break;
                        case 3:
                            if (!FirstPotion3)
                            {
                                DataCallManager.SavePotionType(3);
                            }
                            Potion3Num += PoParmeter.PotionNum;
                            PoList.Potion3ListUpdate();
                            break;
                        case 4:
                            if (!FirstPotion4)
                            {
                                DataCallManager.SavePotionType(4);
                            }
                            Potion4Num += PoParmeter.PotionNum;
                            PoList.Potion4ListUpdate();
                            break;
                        case 5:
                            if (!FirstPotion5)
                            {
                                DataCallManager.SavePotionType(5);
                            }
                            Potion5Num += PoParmeter.PotionNum;
                            PoList.Potion5ListUpdate();
                            break;
                        case 6:
                            if (!FirstPotion6)
                            {
                                DataCallManager.SavePotionType(6);
                            }
                            Potion6Num += PoParmeter.PotionNum;
                            PoList.Potion6ListUpdate();
                            break;
                        case 7:
                            if (!FirstPotion7)
                            {
                                DataCallManager.SavePotionType(7);
                            }
                            Potion7Num += PoParmeter.PotionNum;
                            PoList.Potion7ListUpdate();
                            break;
                        case 8:
                            if (!FirstPotion8)
                            {
                                DataCallManager.SavePotionType(8);
                            }
                            Potion8Num += PoParmeter.PotionNum;
                            PoList.Potion8ListUpdate();
                            break;
                        case 9:
                            if (!FirstPotion9)
                            {
                                DataCallManager.SavePotionType(9);
                            }
                            Potion9Num += PoParmeter.PotionNum;
                            PoList.Potion9ListUpdate();
                            break;
                        case 10:
                            if (!FirstPotion10)
                            {
                                DataCallManager.SavePotionType(10);
                            }
                            Potion10Num += PoParmeter.PotionNum;
                            PoList.Potion10ListUpdate();
                            break;
                        case 11:
                            if (!FirstPotion11)
                            {
                                DataCallManager.SavePotionType(11);
                            }
                            Potion11Num += PoParmeter.PotionNum;
                            PoList.Potion11ListUpdate();
                            break;
                        case 12:
                            if (!FirstPotion12)
                            {
                                DataCallManager.SavePotionType(12);
                            }
                            Potion12Num += PoParmeter.PotionNum;
                            PoList.Potion12ListUpdate();
                            break;
                        case 13:
                            if (!FirstPotion13)
                            {
                                DataCallManager.SavePotionType(13);
                            }
                            Potion13Num += PoParmeter.PotionNum;
                            PoList.Potion13ListUpdate();
                            break;
                        case 14:
                            if (!FirstPotion14)
                            {
                                DataCallManager.SavePotionType(14);
                            }
                            Potion14Num += PoParmeter.PotionNum;
                            PoList.Potion14ListUpdate();
                            break;
                        case 15:
                            if (!FirstPotion15)
                            {
                                DataCallManager.SavePotionType(15);
                            }
                            Potion15Num += PoParmeter.PotionNum;
                            PoList.Potion15ListUpdate();
                            break;
                    }
                    Destroy(Potion[index].gameObject, 0.5f);
                }
            }

            else if (Potion[index].transform.position.x >= NPCPos.x - 0.5f && Potion[index].transform.position.x <= NPCPos.x + 0.5f && Potion[index].transform.position.y <= -2.2f)
            {
                if (GetPotionCount < 1)
                {

                    PoParmeter = Potion[index].GetComponent<PotionParmeter>();
                    SoundManager.sounds["Pop (3)"].Play();
                    Rigidbody2D rigid = Potion[index].GetComponent<Rigidbody2D>();
                    Vector2 getMotion = new Vector2(0f, 8f);


                    rigid.AddForce(getMotion, ForceMode2D.Impulse);
                    rigid.AddTorque(90f);

                    GetPotionCount++;
                    PotionCase = PoParmeter.PotionCaseNum;
                    switch (PotionCase)
                    {
                        case 0:
                            if (PoParmeter.isGasMat)
                                MaterialManager.MaterialGasNumber += 2;
                            else
                                MaterialManager.MaterialQSilverNumber += 2;

                            getBase.MaterialButtonUpdate();
                            break;
                        case 1:
                            if (!FirstPotion1)
                            {
                                DataCallManager.SavePotionType(1);
                            }
                            Potion1Num += PoParmeter.PotionNum;
                            PoList.Potion1ListUpdate();
                            break;
                        case 2:
                            if (!FirstPotion2)
                            {
                                DataCallManager.SavePotionType(2);
                            }
                            Potion2Num += PoParmeter.PotionNum;
                            PoList.Potion2ListUpdate();
                            break;
                        case 3:
                            if (!FirstPotion3)
                            {
                                DataCallManager.SavePotionType(3);
                            }
                            Potion3Num += PoParmeter.PotionNum;
                            PoList.Potion3ListUpdate();
                            break;
                        case 4:
                            if (!FirstPotion4)
                            {
                                DataCallManager.SavePotionType(4);
                            }
                            Potion4Num += PoParmeter.PotionNum;
                            PoList.Potion4ListUpdate();
                            break;
                        case 5:
                            if (!FirstPotion5)
                            {
                                DataCallManager.SavePotionType(5);
                            }
                            Potion5Num += PoParmeter.PotionNum;
                            PoList.Potion5ListUpdate();
                            break;
                        case 6:
                            if (!FirstPotion6)
                            {
                                DataCallManager.SavePotionType(6);
                            }
                            Potion6Num += PoParmeter.PotionNum;
                            PoList.Potion6ListUpdate();
                            break;
                        case 7:
                            if (!FirstPotion7)
                            {
                                DataCallManager.SavePotionType(7);
                            }
                            Potion7Num += PoParmeter.PotionNum;
                            PoList.Potion7ListUpdate();
                            break;
                        case 8:
                            if (!FirstPotion8)
                            {
                                DataCallManager.SavePotionType(8);
                            }
                            Potion8Num += PoParmeter.PotionNum;
                            PoList.Potion8ListUpdate();
                            break;
                        case 9:
                            if (!FirstPotion9)
                            {
                                DataCallManager.SavePotionType(9);
                            }
                            Potion9Num += PoParmeter.PotionNum;
                            PoList.Potion9ListUpdate();
                            break;
                        case 10:
                            if (!FirstPotion10)
                            {
                                DataCallManager.SavePotionType(10);
                            }
                            Potion10Num += PoParmeter.PotionNum;
                            PoList.Potion10ListUpdate();
                            break;
                        case 11:
                            if (!FirstPotion11)
                            {
                                DataCallManager.SavePotionType(11);
                            }
                            Potion11Num += PoParmeter.PotionNum;
                            PoList.Potion11ListUpdate();
                            break;
                        case 12:
                            if (!FirstPotion12)
                            {
                                DataCallManager.SavePotionType(12);
                            }
                            Potion12Num += PoParmeter.PotionNum;
                            PoList.Potion12ListUpdate();
                            break;
                        case 13:
                            if (!FirstPotion13)
                            {
                                DataCallManager.SavePotionType(13);
                            }
                            Potion13Num += PoParmeter.PotionNum;
                            PoList.Potion13ListUpdate();
                            break;
                        case 14:
                            if (!FirstPotion14)
                            {
                                DataCallManager.SavePotionType(14);
                            }
                            Potion14Num += PoParmeter.PotionNum;
                            PoList.Potion14ListUpdate();
                            break;
                        case 15:
                            if (!FirstPotion15)
                            {
                                DataCallManager.SavePotionType(15);
                            }
                            Potion15Num += PoParmeter.PotionNum;
                            PoList.Potion15ListUpdate();
                            break;
                    }
                    Destroy(Potion[index].gameObject, 0.5f);
                }
            }
        }
        StartCoroutine("GetPotion");
    }
}
