using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionListManager : MonoBehaviour {
    public SkillSetManager getSkillLevel;

    public AlchemyManager getParmeter;
    //----------기초재료-----------------
    public GameObject Out_baseMat1;

    public GameObject Out_baseMat2;
    //----------조합재료-----------------
    public GameObject Out_Material1;
    public GameObject Out_Material2;//붉은버섯
    public GameObject Out_Material3;//꽃
    public GameObject Out_Material4;//달빛설탕
    public GameObject Out_Material5;//비타민C
    //-----------------------------------
    public GameObject Enforce_Potion1;
    public GameObject Enforce_Potion2;
    //----------포션---------------------
    public GameObject Out_Potion1;

    public GameObject Out_Potion2;

    public GameObject Out_Potion3;

    public GameObject Out_Potion4;

    public GameObject Out_Potion5;

    public GameObject Out_Potion6;//비타500

    public GameObject Out_Potion7;//박카스

    public GameObject Out_Potion8;//힘

    public GameObject Out_Potion9;//고힘

    public GameObject Out_Potion10;//쌍화탕

    public GameObject Out_Potion11;//스쿠마

    public GameObject Out_Potion12;//성수

    public GameObject Out_Potion13;//멀미약

    public GameObject Out_Potion14;//지능약

    public GameObject Out_Potion15;//고지능약
    //----------재료강화-----------------------
    public GameObject Enforce_Mat1;
    public GameObject Enforce_Mat2;
    public GameObject Enforce_Mat3;//버섯강화
    public GameObject Enforce_Mat4;//설탕강화
    public GameObject Enforce_Mat5;//자색허브
    public GameObject Enforce_Mat6;//허브
    public GameObject Enforce_Mat7;//플루메리아
    //-----------------------------------------

    public GameObject OutEffect;

    public Transform PotionoutPos;

    public static int validitynum;

    public static int FurnitureEffect;

    public bool CompleteSkill2;

    public bool CompleteSkill3;//재료강화스킬

    int PotionCase;

    private void Awake()
    {
        validitynum = 1;
        FurnitureEffect = 0;
    }

    IEnumerator FindCase()
    {
        yield return new WaitForSeconds(0.2f);
        if (AlchemyManager.Blue >= 2 && getParmeter.useItemNum == 2)
        {
            getParmeter.CompleteGuageChange(3,false);
            PotionCase = 0;
        }
        //----------------------------------------------------------------Skill2 사용시
        else if ((AlchemyManager.Blue >= 1 || AlchemyManager.Red >= 1 || AlchemyManager.Gas >= 1) && getParmeter.useItemNum == 1 && CompleteSkill2 && !CompleteSkill3)//허브
        {
            getParmeter.CompleteGuageChange(10, true);
            PotionCase = 47;
        }
        else if ((AlchemyManager.Blue >= 1 || AlchemyManager.Red >= 1 || AlchemyManager.Gas >= 1) && getParmeter.useItemNum == 15 && CompleteSkill2 && !CompleteSkill3)//자색허브
        {
            getParmeter.CompleteGuageChange(10, true);
            PotionCase = 46;
        }
        else if ((AlchemyManager.Blue >= 1 || AlchemyManager.Red >= 1 || AlchemyManager.Gas >= 1) && getParmeter.useItemNum == 5 && CompleteSkill2 && !CompleteSkill3)
        {
            getParmeter.CompleteGuageChange(10,true);
            PotionCase = 50;
        }
        else if ((AlchemyManager.Blue >= 1 || AlchemyManager.Red >= 1 || AlchemyManager.Gas >= 1) && getParmeter.useItemNum == 9 && CompleteSkill2 && !CompleteSkill3)
        {
            getParmeter.CompleteGuageChange(20, true);
            PotionCase = 49;
        }

        else if ((AlchemyManager.Blue >= 1 || AlchemyManager.Red >= 1 || AlchemyManager.Gas >= 1) && getParmeter.useItemNum == 13 && CompleteSkill2 && !CompleteSkill3)
        {
            getParmeter.CompleteGuageChange(20, true);
            PotionCase = 49;
        }

        else if ((AlchemyManager.Blue >= 1 || AlchemyManager.Red >= 1 || AlchemyManager.Gas >= 1) && getParmeter.useItemNum == 14 && CompleteSkill2 && !CompleteSkill3)
        {
            getParmeter.CompleteGuageChange(45, true);
            PotionCase = 45;
        }
        //---------------------------------------------------------------------Skill3 
        else if ((AlchemyManager.Blue>=1 || AlchemyManager.Red >= 1 || AlchemyManager.Gas>=1 ) && getParmeter.useItemNum == 5 && !CompleteSkill2 && CompleteSkill3)
        {
            PotionCase = 97;
        }
        else if ((AlchemyManager.Blue >= 1 || AlchemyManager.Red >= 1 || AlchemyManager.Gas >= 1 ) && getParmeter.useItemNum == 1 && CompleteSkill3)
        {
            int Percen = Random.Range(0, 5);
            if (Percen > 3) PotionCase = 98;
            else PotionCase = 94;
        }
        else if ((AlchemyManager.Blue >= 1 || AlchemyManager.Red >= 1 || AlchemyManager.Gas >= 1) && getParmeter.useItemNum == 15 && CompleteSkill3)
        {
            int Percen = Random.Range(0, 5);
            if (Percen > 3) PotionCase = 98;
            else PotionCase = 93;
        }
        else if ((AlchemyManager.Blue >= 1 || AlchemyManager.Red >= 1 || AlchemyManager.Gas >= 1) && getParmeter.useItemNum == 9 && !CompleteSkill2 && CompleteSkill3)
        {
            PotionCase = 96;
        }
        else if ((AlchemyManager.Blue >= 1 || AlchemyManager.Red >= 1 || AlchemyManager.Gas >= 1) && getParmeter.useItemNum == 10 && !CompleteSkill2 && CompleteSkill3)
        {
            PotionCase = 92;
        }
        else if ((AlchemyManager.Blue >= 1 || AlchemyManager.Red >= 1 || AlchemyManager.Gas >= 1) && getParmeter.useItemNum == 13 && CompleteSkill3)
        {
            PotionCase = 95;
        }
        //------------------------------------------스킬없이 재료조합시-------------------------
        else if (AlchemyManager.Gas > 3 && getParmeter.useItemNum == 5)
        {
            getParmeter.CompleteGuageChange(10, false);
            PotionCase = 4;
        }
        else if (AlchemyManager.Gas > 0 && getParmeter.useItemNum == 2)
        {
            getParmeter.CompleteGuageChange(10, false);
            PotionCase = 104;
        }
        else if (AlchemyManager.Red >= 3 && getParmeter.useItemNum == 9)
        {
            getParmeter.CompleteGuageChange(15, false);
            PotionCase = 101;//붉은버섯 조합식
        }
        //꽃 조합식---------------------------------------------------------
        else if (AlchemyManager.Gas == 1 && getParmeter.useItemNum == 10)
        {
                getParmeter.CompleteGuageChange(35, false);
                PotionCase = 102;//꽃 조합식
        }
        else if (AlchemyManager.Gas > 1 && getParmeter.useItemNum == 10)
        {
            getParmeter.ResetGuage();
            PotionCase = 99;//실패시
        }

        //-----------------------------------------------------------------
        else if (AlchemyManager.Gas >= 3 && getParmeter.useItemNum == 13)
        {
            getParmeter.CompleteGuageChange(15, false);
            PotionCase = 103;//설탕조합식
        }
        //---------------------------------------------------------------------Skill없이 일반 제조시
        else if (AlchemyManager.Blue >= 3 && getParmeter.useItemNum == 1 && !CompleteSkill3)//허브
        {
            getParmeter.CompleteGuageChange(5, false);
            PotionCase = 1;
        }
        else if (AlchemyManager.Blue < 3 && AlchemyManager.Red >= 1 && getParmeter.useItemNum == 1 && !CompleteSkill3)//허브
        {
            getParmeter.CompleteGuageChange(8, false);
            PotionCase = 2;
        }
        else if (AlchemyManager.Blue >= 3 && getParmeter.useItemNum == 15 && !CompleteSkill3)//자색허브
        {
            getParmeter.CompleteGuageChange(8, false);
            PotionCase = 2;
        }
        else if (AlchemyManager.Blue < 3 && AlchemyManager.Red >= 1 && getParmeter.useItemNum == 15 && !CompleteSkill3)//자색허브
        {
            getParmeter.CompleteGuageChange(5, false);
            PotionCase = 1;
        }
        else if (AlchemyManager.Blue >= 2 && getParmeter.useItemNum == 3)
        {
            getParmeter.CompleteGuageChange(10, false);
            PotionCase = 3;
        }
        else if (AlchemyManager.Blue >= 1 && AlchemyManager.Red >= 1 && getParmeter.useItemNum == 4)
        {
            getParmeter.CompleteGuageChange(10, false);
            PotionCase = 5;
        }
        else if (AlchemyManager.Blue >= 2 && getParmeter.useItemNum == 5 && !CompleteSkill3 && !CompleteSkill2)//흰색 깃털 
        {
            getParmeter.CompleteGuageChange(10, false);
            PotionCase = 6;
        }
        else if (AlchemyManager.Blue >= 3 && getParmeter.useItemNum == 6)
        {
            getParmeter.CompleteGuageChange(10, false);
            PotionCase = 7;//민첩포션
        }
        else if (AlchemyManager.Blue >= 2 && AlchemyManager.Red >= 2 && getParmeter.useItemNum == 6)
        {
            getParmeter.CompleteGuageChange(25, false);
            PotionCase = 8;//고효능 민첩포션
        }

        else if (AlchemyManager.Blue >= 2 && getParmeter.useItemNum == 7)
        {
            getParmeter.CompleteGuageChange(10, false);
            PotionCase = 9;//네타500
        }

        else if (AlchemyManager.Blue >= 3 && getParmeter.useItemNum == 8)
        {
            getParmeter.CompleteGuageChange(10, false);
            PotionCase = 10;//박카스
        }

        else if (AlchemyManager.Red >= 3 && getParmeter.useItemNum == 8)
        {
            getParmeter.CompleteGuageChange(20, false);
            PotionCase = 16;//멀미약
        }

        else if (AlchemyManager.Red >= 1 && getParmeter.useItemNum == 9)
        {
            getParmeter.CompleteGuageChange(10, false);
            PotionCase = 1;//버섯 기본
        }

        else if (AlchemyManager.Red >= 2 && getParmeter.useItemNum == 10)
        {
            getParmeter.CompleteGuageChange(20, false);
            PotionCase = 11;//힘포션
        }

        else if (AlchemyManager.Blue >= 1&&AlchemyManager.Red >= 1 && AlchemyManager.Gas >= 1 && getParmeter.useItemNum == 11)
        {
            getParmeter.CompleteGuageChange(50, false);
            PotionCase = 12;//고효능힘포션
        }

        else if (AlchemyManager.Blue >= 4 && getParmeter.useItemNum == 12)
        {
            getParmeter.CompleteGuageChange(10, false);
            PotionCase = 13;//쌍화탕
        }


        else if (AlchemyManager.Blue >= 2 && getParmeter.useItemNum == 13)
        {
            getParmeter.CompleteGuageChange(10, false);
            PotionCase = 15;//설탕 기본
        }

        else if (AlchemyManager.Red >= 3 && getParmeter.useItemNum == 14)
        {
            getParmeter.CompleteGuageChange(25, false);
            PotionCase = 14;
        }

        else if (AlchemyManager.Blue >= 2 && getParmeter.useItemNum == 14)
        {
            getParmeter.CompleteGuageChange(10, false);
            PotionCase = 17;
        }
        //-------------------------------------------------------
        else
        {
            PotionCase = 99;
        }
        StartCoroutine("FindCase");
      //  Debug.Log(PotionCase);
    }

    public void makePotion()
    {
        StopCoroutine("FindCase");
        
        switch (PotionCase)
        {
            case 0:
                if (AlchemyManager.Red == 1)
                {
                    OutbaseMat2();
                }
                else
                {
                    OutbaseMat1();
                }
                break;
            case 1:
                OutPotion2();
                break;
            case 2:
                OutPotion1();
                break;
            case 3:
                if (AlchemyManager.Red == 1)
                {
                    OutPotion1();
                    OutPotion2();
                }
                else
                {
                    OutPotion2();
                    OutPotion2();
                }
                break;
            case 4:
                OutMaterial1();
                break;
            case 5:
                OutPotion3();
                break;
            case 6:
                OutPotion4();
                break;
            case 7:
                OutPotion4();
                OutPotion4();
                break;
            case 8:
                OutPotion5();
                break;
            case 9://비타500
                OutPotion6();
                break;
            case 10://박카스
                OutPotion7();
                break;
            case 11://힘
                OutPotion8();
                break;
            case 12://고힘
                OutPotion9();
                break;
            case 13://쌍화탕
                OutPotion10();
                break;
            case 14://스쿠마
                OutPotion11();
                break;
            case 15://성수
                OutPotion12();
                break;
            case 16://멀미약
                OutPotion13();
                break;
            case 17://지능약
                OutPotion14();
                break;
            case 45:
                EnforcePotion7();
                break;
            case 46:
                EnforcePotion6();
                break;
            case 47:
                EnforcePotion5();
                break;
            case 48:
                EnforcePotion3();
                break;
            case 49:
                EnforcePotion2();
                break;
            case 50:
                EnforcePotion1();
                break;
            case 92:
                EnforeMat7();
                break;
            case 93://허브로 변환
                EnforeMat6();
                break;
            case 94://자색허브로 변환
                EnforeMat5();
                break;
            case 95:
                EnforeMat4();
                break;
            case 96:
                EnforeMat3();
                break;
            case 97:
                EnforeMat2();
                break;
            case 98:
                EnforeMat1();
                break;
            case 99:
                break;
            case 101:
                OutMaterial2();
                break;
            case 102:
                OutMaterial3();
                break;
            case 103:
                OutMaterial4();
                break;
            case 104:
                OutMaterial5();
                break;
            default:
                break;
        }
        getParmeter.ResetGuage();
        getParmeter.PotionCompleted = 0;
        getParmeter.useItemNum = 0;
        validitynum = 1;
        AlchemyManager.Blue = 0;
        AlchemyManager.Red = 0;
        AlchemyManager.Gas = 0;
        StartCoroutine("FindCase");
    }


    public void OutbaseMat1()
    {
        for (int index = 0; index <= 1 * validitynum + FurnitureEffect; index++)
        {
            GameObject potionIns = Instantiate(Out_baseMat1, PotionoutPos);
            Rigidbody2D rigid = potionIns.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 8f));
            GameObject Effect = Instantiate(OutEffect, PotionoutPos);
            rigid.AddForce(outPower, ForceMode2D.Impulse);
            rigid.AddTorque(30f);
            StartCoroutine("DestroyEffect", Effect);
            // Destroy(Effect);
        }
    }

    public void OutbaseMat2()
    {
        for (int index = 0; index <= 1 * validitynum + FurnitureEffect; index++)
        {
            GameObject potionIns = Instantiate(Out_baseMat2, PotionoutPos);
            Rigidbody2D rigid = potionIns.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 8f));
            GameObject Effect = Instantiate(OutEffect, PotionoutPos);
            rigid.AddForce(outPower, ForceMode2D.Impulse);
            rigid.AddTorque(30f);
            StartCoroutine("DestroyEffect", Effect);
            // Destroy(Effect);
        }
    }
    //----------------재료조합식-------------------------------

    public void OutMaterial1()
    {
        for (int index = 0; index <= validitynum + FurnitureEffect; index++)
        {
            GameObject potionIns = Instantiate(Out_Material1, PotionoutPos);
            Rigidbody2D rigid = potionIns.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 8f));
            GameObject Effect = Instantiate(OutEffect, PotionoutPos);
            rigid.AddForce(outPower, ForceMode2D.Impulse);
            rigid.AddTorque(30f);
            StartCoroutine("DestroyEffect", Effect);
            // Destroy(Effect);
        }
    }//재료조합식

    public void OutMaterial2()//붉은버섯 조합식
    {
        for (int index = 0; index <= validitynum + FurnitureEffect; index++)
        {
            GameObject potionIns = Instantiate(Out_Material2, PotionoutPos);
            Rigidbody2D rigid = potionIns.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 8f));
            GameObject Effect = Instantiate(OutEffect, PotionoutPos);
            rigid.AddForce(outPower, ForceMode2D.Impulse);
            rigid.AddTorque(30f);
            StartCoroutine("DestroyEffect", Effect);
            // Destroy(Effect);
        }
    }

    public void OutMaterial3()//꽃 조합식
    {
        for (int index = 0; index <= validitynum + FurnitureEffect; index++)
        {
            GameObject potionIns = Instantiate(Out_Material3, PotionoutPos);
            Rigidbody2D rigid = potionIns.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 8f));
            GameObject Effect = Instantiate(OutEffect, PotionoutPos);
            rigid.AddForce(outPower, ForceMode2D.Impulse);
            rigid.AddTorque(30f);
            StartCoroutine("DestroyEffect", Effect);
            // Destroy(Effect);
        }
    }
    public void OutMaterial4()//달빛설탕 조합식
    {
        for (int index = 0; index <= validitynum + FurnitureEffect; index++)
        {
            GameObject potionIns = Instantiate(Out_Material4, PotionoutPos);
            Rigidbody2D rigid = potionIns.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 8f));
            GameObject Effect = Instantiate(OutEffect, PotionoutPos);
            rigid.AddForce(outPower, ForceMode2D.Impulse);
            rigid.AddTorque(30f);
            StartCoroutine("DestroyEffect", Effect);
            // Destroy(Effect);
        }
    }

    public void OutMaterial5()//비타민 조합식
    {
        for (int index = 0; index <= validitynum + FurnitureEffect; index++)
        {
            GameObject potionIns = Instantiate(Out_Material5, PotionoutPos);
            Rigidbody2D rigid = potionIns.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 8f));
            GameObject Effect = Instantiate(OutEffect, PotionoutPos);
            rigid.AddForce(outPower, ForceMode2D.Impulse);
            rigid.AddTorque(30f);
            StartCoroutine("DestroyEffect", Effect);
            // Destroy(Effect);
        }
    }
    //---------------------------------------------------------
    public void OutPotion1()
    {
        for (int index = 0; index <= validitynum + FurnitureEffect; index++)
        {
            GameObject potionIns = Instantiate(Out_Potion1, PotionoutPos);
            Rigidbody2D rigid = potionIns.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 8f));
            GameObject Effect = Instantiate(OutEffect, PotionoutPos);
            rigid.AddForce(outPower, ForceMode2D.Impulse);
            rigid.AddTorque(30f);
            StartCoroutine("DestroyEffect", Effect);
           // Destroy(Effect);
        }
    }
    public void OutPotion2()
    {
        for (int index = 0; index <= validitynum + FurnitureEffect; index++)
        {
            GameObject potionIns = Instantiate(Out_Potion2, PotionoutPos);
            Rigidbody2D rigid = potionIns.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 8f));
            GameObject Effect = Instantiate(OutEffect, PotionoutPos);
            rigid.AddForce(outPower, ForceMode2D.Impulse);
            rigid.AddTorque(30f);
            StartCoroutine("DestroyEffect", Effect);
            // Destroy(Effect);
        }
    }


    public void OutPotion3()//해독제는 좀더 많이
    {
        for (int index = 0; index <= validitynum + FurnitureEffect + 1; index++)
        {
            GameObject potionIns = Instantiate(Out_Potion3, PotionoutPos);
            Rigidbody2D rigid = potionIns.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 8f));
            GameObject Effect = Instantiate(OutEffect, PotionoutPos);
            rigid.AddForce(outPower, ForceMode2D.Impulse);
            rigid.AddTorque(30f);
            StartCoroutine("DestroyEffect", Effect);
            // Destroy(Effect);
        }
    }

    public void OutPotion4()
    {
        for (int index = 0; index <=  validitynum + FurnitureEffect; index++)
        {
            GameObject potionIns = Instantiate(Out_Potion4, PotionoutPos);
            Rigidbody2D rigid = potionIns.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 8f));
            GameObject Effect = Instantiate(OutEffect, PotionoutPos);
            rigid.AddForce(outPower, ForceMode2D.Impulse);
            rigid.AddTorque(30f);
            StartCoroutine("DestroyEffect", Effect);
            // Destroy(Effect);
        }
    }

    public void OutPotion5()
    {
        for (int index = 0; index <=  validitynum + FurnitureEffect; index++)
        {
            GameObject potionIns = Instantiate(Out_Potion5, PotionoutPos);
            Rigidbody2D rigid = potionIns.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 8f));
            GameObject Effect = Instantiate(OutEffect, PotionoutPos);
            rigid.AddForce(outPower, ForceMode2D.Impulse);
            rigid.AddTorque(30f);
            StartCoroutine("DestroyEffect", Effect);
            // Destroy(Effect);
        }
    }

    public void OutPotion6()
    {
        for (int index = 0; index <= validitynum + FurnitureEffect; index++)
        {
            GameObject potionIns = Instantiate(Out_Potion6, PotionoutPos);
            Rigidbody2D rigid = potionIns.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 8f));
            GameObject Effect = Instantiate(OutEffect, PotionoutPos);
            rigid.AddForce(outPower, ForceMode2D.Impulse);
            rigid.AddTorque(30f);
            StartCoroutine("DestroyEffect", Effect);
            // Destroy(Effect);
        }
    }

    public void OutPotion7()
    {
        for (int index = 0; index <= validitynum + FurnitureEffect; index++)
        {
            GameObject potionIns = Instantiate(Out_Potion7, PotionoutPos);
            Rigidbody2D rigid = potionIns.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 8f));
            GameObject Effect = Instantiate(OutEffect, PotionoutPos);
            rigid.AddForce(outPower, ForceMode2D.Impulse);
            rigid.AddTorque(30f);
            StartCoroutine("DestroyEffect", Effect);
            // Destroy(Effect);
        }
    }

    public void OutPotion8()
    {
        for (int index = 0; index <= validitynum + FurnitureEffect; index++)
        {
            GameObject potionIns = Instantiate(Out_Potion8, PotionoutPos);
            Rigidbody2D rigid = potionIns.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 8f));
            GameObject Effect = Instantiate(OutEffect, PotionoutPos);
            rigid.AddForce(outPower, ForceMode2D.Impulse);
            rigid.AddTorque(30f);
            StartCoroutine("DestroyEffect", Effect);
            // Destroy(Effect);
        }
    }

    public void OutPotion9()
    {
        for (int index = 0; index <= validitynum + FurnitureEffect; index++)
        {
            GameObject potionIns = Instantiate(Out_Potion9, PotionoutPos);
            Rigidbody2D rigid = potionIns.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 8f));
            GameObject Effect = Instantiate(OutEffect, PotionoutPos);
            rigid.AddForce(outPower, ForceMode2D.Impulse);
            rigid.AddTorque(30f);
            StartCoroutine("DestroyEffect", Effect);
            // Destroy(Effect);
        }
    }

    public void OutPotion10()//쌍화탕도 조금 더 많이
    {
        for (int index = 0; index <= validitynum + FurnitureEffect+1; index++)
        {
            GameObject potionIns = Instantiate(Out_Potion10, PotionoutPos);
            Rigidbody2D rigid = potionIns.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 8f));
            GameObject Effect = Instantiate(OutEffect, PotionoutPos);
            rigid.AddForce(outPower, ForceMode2D.Impulse);
            rigid.AddTorque(30f);
            StartCoroutine("DestroyEffect", Effect);
            // Destroy(Effect);
        }
    }

    public void OutPotion11()
    {
        for (int index = 0; index <= validitynum + FurnitureEffect; index++)
        {
            GameObject potionIns = Instantiate(Out_Potion11, PotionoutPos);
            Rigidbody2D rigid = potionIns.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 8f));
            GameObject Effect = Instantiate(OutEffect, PotionoutPos);
            rigid.AddForce(outPower, ForceMode2D.Impulse);
            rigid.AddTorque(30f);
            StartCoroutine("DestroyEffect", Effect);
            // Destroy(Effect);
        }
    }

    public void OutPotion12()
    {
        for (int index = 0; index <= validitynum + FurnitureEffect; index++)
        {
            GameObject potionIns = Instantiate(Out_Potion12, PotionoutPos);
            Rigidbody2D rigid = potionIns.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 8f));
            GameObject Effect = Instantiate(OutEffect, PotionoutPos);
            rigid.AddForce(outPower, ForceMode2D.Impulse);
            rigid.AddTorque(30f);
            StartCoroutine("DestroyEffect", Effect);
            // Destroy(Effect);
        }
    }

    public void OutPotion13()
    {
        for (int index = 0; index <= validitynum + FurnitureEffect; index++)
        {
            GameObject potionIns = Instantiate(Out_Potion13, PotionoutPos);
            Rigidbody2D rigid = potionIns.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 8f));
            GameObject Effect = Instantiate(OutEffect, PotionoutPos);
            rigid.AddForce(outPower, ForceMode2D.Impulse);
            rigid.AddTorque(30f);
            StartCoroutine("DestroyEffect", Effect);
            // Destroy(Effect);
        }
    }

    public void OutPotion14()
    {
        for (int index = 0; index <= validitynum + FurnitureEffect; index++)
        {
            GameObject potionIns = Instantiate(Out_Potion14, PotionoutPos);
            Rigidbody2D rigid = potionIns.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 8f));
            GameObject Effect = Instantiate(OutEffect, PotionoutPos);
            rigid.AddForce(outPower, ForceMode2D.Impulse);
            rigid.AddTorque(30f);
            StartCoroutine("DestroyEffect", Effect);
            // Destroy(Effect);
        }
    }
    //------------------물약강화-스킬------------------------------------
    public void EnforcePotion1()//흰색깃털 물약강화스킬
    {
        for (int index = 0; index < getSkillLevel.Skill2Level; index++)
        {
            GameObject potionIns = Instantiate(Out_Potion5, PotionoutPos);
            Rigidbody2D rigid = potionIns.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 8f));
            GameObject Effect = Instantiate(OutEffect, PotionoutPos);
            rigid.AddForce(outPower, ForceMode2D.Impulse);
            rigid.AddTorque(30f);
            StartCoroutine("DestroyEffect", Effect);
            // Destroy(Effect);
        }
    }

    public void EnforcePotion2()//식용버섯 물약강화스킬
    {
        for (int index = 0; index < getSkillLevel.Skill2Level; index++)
        {
            GameObject potionIns;
            if (getSkillLevel.Skill2Level >= 2)
            {
                potionIns = Instantiate(Out_Potion9, PotionoutPos);
            }
            else potionIns = Instantiate(Out_Potion8, PotionoutPos);

            Rigidbody2D rigid = potionIns.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 8f));
            GameObject Effect = Instantiate(OutEffect, PotionoutPos);
            rigid.AddForce(outPower, ForceMode2D.Impulse);
            rigid.AddTorque(30f);
            StartCoroutine("DestroyEffect", Effect);
            // Destroy(Effect);
        }
    }

    public void EnforcePotion3(){//설탕 물약강화스킬

        for (int index = 0; index < getSkillLevel.Skill2Level; index++)
        {
            GameObject potionIns;
            potionIns = Instantiate(Out_Potion11, PotionoutPos);

            Rigidbody2D rigid = potionIns.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 8f));
            GameObject Effect = Instantiate(OutEffect, PotionoutPos);
            rigid.AddForce(outPower, ForceMode2D.Impulse);
            rigid.AddTorque(30f);
            StartCoroutine("DestroyEffect", Effect);
            // Destroy(Effect);
        }
    }
    public void EnforcePotion4()
    {//붉은버섯 물약강화스킬

        for (int index = 0; index < getSkillLevel.Skill2Level; index++)
        {
            GameObject potionIns;
            potionIns = Instantiate(Out_Potion9, PotionoutPos);

            Rigidbody2D rigid = potionIns.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 8f));
            GameObject Effect = Instantiate(OutEffect, PotionoutPos);
            rigid.AddForce(outPower, ForceMode2D.Impulse);
            rigid.AddTorque(30f);
            StartCoroutine("DestroyEffect", Effect);
            // Destroy(Effect);
        }
    }

    public void EnforcePotion5()
    {//허브 물약강화스킬

        for (int index = 0; index < 1; index++)
        {
            GameObject potionIns;
            potionIns = Instantiate(Enforce_Potion1, PotionoutPos);

            Rigidbody2D rigid = potionIns.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 8f));
            GameObject Effect = Instantiate(OutEffect, PotionoutPos);
            rigid.AddForce(outPower, ForceMode2D.Impulse);
            rigid.AddTorque(30f);
            StartCoroutine("DestroyEffect", Effect);
            // Destroy(Effect);
        }
    }

    public void EnforcePotion6()
    {//자색허브 물약강화스킬

        for (int index = 0; index < 1; index++)
        {
            GameObject potionIns;
            potionIns = Instantiate(Enforce_Potion2, PotionoutPos);

            Rigidbody2D rigid = potionIns.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 8f));
            GameObject Effect = Instantiate(OutEffect, PotionoutPos);
            rigid.AddForce(outPower, ForceMode2D.Impulse);
            rigid.AddTorque(30f);
            StartCoroutine("DestroyEffect", Effect);
            // Destroy(Effect);
        }
    }
    public void EnforcePotion7()
    {//달빛설탕 물약강화스킬

        for (int index = 0; index < 1; index++)
        {
            GameObject potionIns;
            potionIns = Instantiate(Out_Potion15, PotionoutPos);

            Rigidbody2D rigid = potionIns.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 8f));
            GameObject Effect = Instantiate(OutEffect, PotionoutPos);
            rigid.AddForce(outPower, ForceMode2D.Impulse);
            rigid.AddTorque(30f);
            StartCoroutine("DestroyEffect", Effect);
            // Destroy(Effect);
        }
    }
    //------------------재료강화-스킬------------------------------------
    public void EnforeMat1()
    {
        for (int index = 0; index < getSkillLevel.Skill3Level; index++)
        {
            GameObject potionIns = Instantiate(Enforce_Mat1, PotionoutPos);//붉은허브
            Rigidbody2D rigid = potionIns.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 8f));
            GameObject Effect = Instantiate(OutEffect, PotionoutPos);
            rigid.AddForce(outPower, ForceMode2D.Impulse);
            rigid.AddTorque(30f);
            StartCoroutine("DestroyEffect", Effect);
            // Destroy(Effect);
        }
    }

    public void EnforeMat2()
    {
        for (int index = 0; index < getSkillLevel.Skill3Level; index++)
        {
            GameObject potionIns = Instantiate(Enforce_Mat2, PotionoutPos);
            Rigidbody2D rigid = potionIns.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 8f));
            GameObject Effect = Instantiate(OutEffect, PotionoutPos);
            rigid.AddForce(outPower, ForceMode2D.Impulse);
            rigid.AddTorque(30f);
            StartCoroutine("DestroyEffect", Effect);
            // Destroy(Effect);
        }
    }

    public void EnforeMat3()
    {
        for (int index = 0; index < getSkillLevel.Skill3Level; index++)
        {
            GameObject potionIns = Instantiate(Enforce_Mat3, PotionoutPos);
            Rigidbody2D rigid = potionIns.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 8f));
            GameObject Effect = Instantiate(OutEffect, PotionoutPos);
            rigid.AddForce(outPower, ForceMode2D.Impulse);
            rigid.AddTorque(30f);
            StartCoroutine("DestroyEffect", Effect);
            // Destroy(Effect);
        }
    }

    public void EnforeMat4()
    {
        for (int index = 0; index < getSkillLevel.Skill3Level; index++)
        {
            GameObject potionIns = Instantiate(Enforce_Mat4, PotionoutPos);
            Rigidbody2D rigid = potionIns.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 8f));
            GameObject Effect = Instantiate(OutEffect, PotionoutPos);
            rigid.AddForce(outPower, ForceMode2D.Impulse);
            rigid.AddTorque(30f);
            StartCoroutine("DestroyEffect", Effect);
            // Destroy(Effect);
        }
    }

    public void EnforeMat5()
    {
        for (int index = 0; index < getSkillLevel.Skill3Level; index++)
        {
            GameObject potionIns = Instantiate(Enforce_Mat5, PotionoutPos);//자색허브
            Rigidbody2D rigid = potionIns.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 8f));
            GameObject Effect = Instantiate(OutEffect, PotionoutPos);
            rigid.AddForce(outPower, ForceMode2D.Impulse);
            rigid.AddTorque(30f);
            StartCoroutine("DestroyEffect", Effect);
            // Destroy(Effect);
        }
    }

    public void EnforeMat6()
    {
        for (int index = 0; index < getSkillLevel.Skill3Level; index++)
        {
            GameObject potionIns = Instantiate(Enforce_Mat6, PotionoutPos);//허브
            Rigidbody2D rigid = potionIns.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 8f));
            GameObject Effect = Instantiate(OutEffect, PotionoutPos);
            rigid.AddForce(outPower, ForceMode2D.Impulse);
            rigid.AddTorque(30f);
            StartCoroutine("DestroyEffect", Effect);
            // Destroy(Effect);
        }
    }

    public void EnforeMat7()
    {
        for (int index = 0; index < getSkillLevel.Skill3Level; index++)
        {
            GameObject potionIns = Instantiate(Enforce_Mat7, PotionoutPos);//플루메리아
            Rigidbody2D rigid = potionIns.GetComponent<Rigidbody2D>();
            Vector3 outPower = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 8f));
            GameObject Effect = Instantiate(OutEffect, PotionoutPos);
            rigid.AddForce(outPower, ForceMode2D.Impulse);
            rigid.AddTorque(30f);
            StartCoroutine("DestroyEffect", Effect);
            // Destroy(Effect);
        }
    }
    //---------------------------------------------------------------
    IEnumerator DestroyEffect(GameObject Effect)
    {
        yield return new WaitForSeconds(0.8f);

        Destroy(Effect);
    }
}
