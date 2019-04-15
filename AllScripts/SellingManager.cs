using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingManager : MonoBehaviour {

    Vector3 MoveDicPos;

    PlayerControl playerMotion;

    public HavePoInvenManager PotionInven;

    public HavePotionListManager PotionList;

    public Transform SellingOutPos;

    //---------------------------------------------

    public GameObject SellPotion1;

    public GameObject SellPotion2;

    public GameObject SellPotion3;

    public GameObject SellPotion4;

    public GameObject SellPotion5;

    public GameObject SellPotion6;

    public GameObject SellPotion7;

    public GameObject SellPotion8;

    public GameObject SellPotion9;

    public GameObject SellPotion10;

    public GameObject SellPotion11;

    public GameObject SellPotion12;

    public GameObject SellPotion13;

    public GameObject SellPotion14;

    public GameObject SellPotion15;

    //---------------------------------------------

    public GameObject Complete_Medicine;
    public Transform Complete_Pos;

    //---------------------------------------------
    public int AllPrice;
    public bool IsComplete;
    public bool Ordering;
    public bool isReadySell;
    //bool BasketReady;
    int SellCount;
    bool showDic;

    void Awake()
    {
        playerMotion = GameObject.Find("Player").GetComponent<PlayerControl>(); ;
    }

    void Update()
    {
        GameObject OKbutton = GameObject.Find("SellingButton");
        UIButton OKbuttonScript = OKbutton.GetComponent<UIButton>();
        BoxCollider OKcollider = OKbutton.GetComponent<BoxCollider>();
        if(SellCount > 0 && Ordering == true)
        {
            OKcollider.enabled = true;
            OKbuttonScript.state = UIButtonColor.State.Normal;
        }
        else
        {
            OKcollider.enabled = false;
            OKbuttonScript.state = UIButtonColor.State.Disabled;
        }
    }

    public void PushDictionary()
    {
        SoundManager.sounds["BookOpen"].Play();
        if (showDic == false)
        {
            showDic = true;

            GameObject DicSet = GameObject.Find("DictionaryMenu");
            MoveDicPos = DicSet.transform.position;
            MoveDicPos.x += 3.9f;
        }
        else
        {
            showDic = false;

            GameObject DicSet = GameObject.Find("DictionaryMenu");
            MoveDicPos = DicSet.transform.position;
            MoveDicPos.x -= 3.9f;
        }

        StartCoroutine("PopUpDic");
    }

    IEnumerator PopUpDic()
    {
        yield return null;

        Transform DicSet = GameObject.Find("DictionaryMenu").transform;

        StartCoroutine("PopUpDic");

        if (DicSet.position == MoveDicPos)
        {
            StopCoroutine("PopUpDic");
            yield break;
        }
        DicSet.position = Vector3.Lerp(DicSet.position, MoveDicPos, 0.8f);
    }

    public void PushSellButton()
    {
        SoundManager.sounds["PotionSell"].Play();
        StartCoroutine("BasketMove");

        playerMotion.SellingMotion();
        Instantiate(Complete_Medicine, Complete_Pos);
        StartCoroutine("MedicineOnDesk");

        SellCount = 0;
    }

    IEnumerator BasketMove()
    {
        yield return null;
        Transform Basket = GameObject.Find("BasketSprite").transform;
        Basket.position = Vector3.Lerp(Basket.position, new Vector3(-1f, Basket.position.y,Basket.position.z), 0.2f);

        if(Basket.position.x >= -1.5f)
        {
            GameObject[] UIPotion = GameObject.FindGameObjectsWithTag("SellPotion");

            for (int index = 0; index < UIPotion.Length; index++)
            {
                Destroy(UIPotion[index]);
            }
            Basket.position = new Vector3(-8f, Basket.position.y, Basket.position.z);
            StartCoroutine("BasketReadyMove");
            StopCoroutine("BasketMove");
            yield break;
        }
        else
        {
            StartCoroutine("BasketMove");
        }
        
    }
    IEnumerator BasketReadyMove()
    {
        yield return null;
        //BasketReady = false;
        Transform Basket = GameObject.Find("BasketSprite").transform;
        Basket.position = Vector3.Lerp(Basket.position, new Vector3(-4.8f, Basket.position.y, Basket.position.z), 0.095f);

        if (Basket.position.x >= -4.85f)
        {
            Basket.position = new Vector3(-4.8f, Basket.position.y, Basket.position.z);
            StopCoroutine("BasketReadyMove");
            yield break;
        }
        else
        {
            StartCoroutine("BasketReadyMove");
        }
    }

    IEnumerator MedicineOnDesk()
    {
        yield return null;
        GameObject Medicine = GameObject.Find("CompleteMedicine(Clone)");
        Transform CompleteMedicine = Medicine.transform;
        CompleteMedicine.position = Vector3.Lerp(CompleteMedicine.position, new Vector3(0.4f, -1.8f, -1f), 0.2f);

        if (CompleteMedicine.position.x >= 0.25f)
        {
            CompleteMedicine.position = new Vector3(0f, -1.8f, -1f);
            IsComplete = true;
            StopCoroutine("MedicineOnDesk");
            yield break;
        }
        else
        {
            StartCoroutine("MedicineOnDesk");
        }

    }

    public void ResetBasket()
    {
        if (SellCount >= 1)
        {
            SoundManager.sounds["PotionSell"].Play();
            for (int index = 0; index < SellCount; index++) {
                switch (ComPotionParmeter.PotionType[index])
                {
                    case 1:
                        PotionInven.Potion1Num += 1;
                        PotionList.Potion1ListUpdate();
                        break;
                    case 2:
                        PotionInven.Potion2Num += 1;
                        PotionList.Potion2ListUpdate();
                        break;
                    case 3:
                        PotionInven.Potion3Num += 1;
                        PotionList.Potion3ListUpdate();
                        break;
                    case 4:
                        PotionInven.Potion4Num += 1;
                        PotionList.Potion4ListUpdate();
                        break;
                    case 5:
                        PotionInven.Potion5Num += 1;
                        PotionList.Potion5ListUpdate();
                        break;
                    case 6:
                        PotionInven.Potion6Num += 1;
                        PotionList.Potion6ListUpdate();
                        break;
                    case 7:
                        PotionInven.Potion7Num += 1;
                        PotionList.Potion7ListUpdate();
                        break;
                    case 8:
                        PotionInven.Potion8Num += 1;
                        PotionList.Potion8ListUpdate();
                        break;
                    case 9:
                        PotionInven.Potion9Num += 1;
                        PotionList.Potion9ListUpdate();
                        break;
                    case 10:
                        PotionInven.Potion10Num += 1;
                        PotionList.Potion10ListUpdate();
                        break;
                    case 11:
                        PotionInven.Potion11Num += 1;
                        PotionList.Potion11ListUpdate();
                        break;
                    case 12:
                        PotionInven.Potion12Num += 1;
                        PotionList.Potion12ListUpdate();
                        break;
                    case 13:
                        PotionInven.Potion13Num += 1;
                        PotionList.Potion13ListUpdate();
                        break;
                    case 14:
                        PotionInven.Potion14Num += 1;
                        PotionList.Potion14ListUpdate();
                        break;
                    case 15:
                        PotionInven.Potion15Num += 1;
                        PotionList.Potion15ListUpdate();
                        break;
                }
            }
            SellCount = 0;
            ComPotionParmeter.Reset();
            GameObject[] UIPotion = GameObject.FindGameObjectsWithTag("SellPotion");
            for (int index = 0; index < UIPotion.Length; index++)
            {
                Destroy(UIPotion[index]);
            }
        }
    }

    public void PushPotion1()
    {
        if (SellCount < 3)
        {
            if (PotionInven.Potion1Num > 0)
            { 
                SoundManager.sounds["Click (6)"].Play();
                PotionInven.Potion1Num -= 1;
                PotionList.Potion1ListUpdate();
                GameObject SellPotion = Instantiate(SellPotion1, SellingOutPos);
                Rigidbody2D rigid = SellPotion.GetComponent<Rigidbody2D>();
                Vector3 outPower = new Vector2(Random.Range(0f, 1f), 0f);
                rigid.AddForce(outPower, ForceMode2D.Impulse);
                rigid.AddTorque(30f);
                ComPotionParmeter.InType(1);
                AllPrice += 500;
                SellCount += 1;
            }
            else
            {
                SoundManager.sounds["NotItem"].Play();
            }
        }
        else
        {
            SoundManager.sounds["NotItem"].Play();
        }
    }

    public void PushPotion2()
    {
        if (SellCount < 3)
        {
            if (PotionInven.Potion2Num > 0)
            {
                SoundManager.sounds["Click (6)"].Play();
                PotionInven.Potion2Num -= 1;
                PotionList.Potion2ListUpdate();
                GameObject SellPotion = Instantiate(SellPotion2, SellingOutPos);
                Rigidbody2D rigid = SellPotion.GetComponent<Rigidbody2D>();
                Vector3 outPower = new Vector2(Random.Range(0f, 2f), 0.5f);
                rigid.AddForce(outPower, ForceMode2D.Impulse);
                rigid.AddTorque(30f);
                AllPrice += 700;
                SellCount += 1;
                ComPotionParmeter.InType(2);
            }
            else
            {
                SoundManager.sounds["NotItem"].Play();
            }
        }
        else
        {
            SoundManager.sounds["NotItem"].Play();
        }
    }

    public void PushPotion3()
    {
        if (SellCount < 3)
        {
            if (PotionInven.Potion3Num > 0)
            {
                SoundManager.sounds["Click (6)"].Play();
                PotionInven.Potion3Num -= 1;
                PotionList.Potion3ListUpdate();
                GameObject SellPotion = Instantiate(SellPotion3, SellingOutPos);
                Rigidbody2D rigid = SellPotion.GetComponent<Rigidbody2D>();
                Vector3 outPower = new Vector2(Random.Range(0f, 2f), 0.5f);
                rigid.AddForce(outPower, ForceMode2D.Impulse);
                rigid.AddTorque(30f);
                AllPrice += 400;
                SellCount += 1;
                ComPotionParmeter.InType(3);
            }
            else
            {
                SoundManager.sounds["NotItem"].Play();
            }
        }
        else
        {
            SoundManager.sounds["NotItem"].Play();
        }
    }

    public void PushPotion4()
    {
        if (SellCount < 3)
        {
            if (PotionInven.Potion4Num > 0)
            {
                SoundManager.sounds["Click (6)"].Play();
                PotionInven.Potion4Num -= 1;
                PotionList.Potion4ListUpdate();
                GameObject SellPotion = Instantiate(SellPotion4, SellingOutPos);
                Rigidbody2D rigid = SellPotion.GetComponent<Rigidbody2D>();
                Vector3 outPower = new Vector2(Random.Range(0f, 2f), 0.5f);
                rigid.AddForce(outPower, ForceMode2D.Impulse);
                rigid.AddTorque(30f);
                AllPrice += 900;
                SellCount += 1;
                ComPotionParmeter.InType(4);
            }
            else
            {
                SoundManager.sounds["NotItem"].Play();
            }
        }
        else
        {
            SoundManager.sounds["NotItem"].Play();
        }
    }

    public void PushPotion5()
    {
        if (SellCount < 3)
        {
            if (PotionInven.Potion5Num > 0)
            {
                SoundManager.sounds["Click (6)"].Play();
                PotionInven.Potion5Num -= 1;
                PotionList.Potion5ListUpdate();
                GameObject SellPotion = Instantiate(SellPotion5, SellingOutPos);
                Rigidbody2D rigid = SellPotion.GetComponent<Rigidbody2D>();
                Vector3 outPower = new Vector2(Random.Range(0f, 2f), 0.5f);
                rigid.AddForce(outPower, ForceMode2D.Impulse);
                rigid.AddTorque(30f);
                AllPrice += 1800;
                SellCount += 1;
                ComPotionParmeter.InType(5);
            }
            else
            {
                SoundManager.sounds["NotItem"].Play();
            }
        }
        else
        {
            SoundManager.sounds["NotItem"].Play();
        }
    }

    public void PushPotion6()
    {
        if (SellCount < 3)
        {
            if (PotionInven.Potion6Num > 0)
            {
                SoundManager.sounds["Click (6)"].Play();
                PotionInven.Potion6Num -= 1;
                PotionList.Potion6ListUpdate();
                GameObject SellPotion = Instantiate(SellPotion6, SellingOutPos);
                Rigidbody2D rigid = SellPotion.GetComponent<Rigidbody2D>();
                Vector3 outPower = new Vector2(Random.Range(0f, 2f), 0.5f);
                rigid.AddForce(outPower, ForceMode2D.Impulse);
                rigid.AddTorque(30f);
                AllPrice += 1000;//포션가격
                SellCount += 1;
                ComPotionParmeter.InType(6);
            }
            else
            {
                SoundManager.sounds["NotItem"].Play();
            }
        }
        else
        {
            SoundManager.sounds["NotItem"].Play();
        }
    }

    public void PushPotion7()
    {
        if (SellCount < 3)
        {
            if (PotionInven.Potion7Num > 0)
            {
                SoundManager.sounds["Click (6)"].Play();
                PotionInven.Potion7Num -= 1;
                PotionList.Potion7ListUpdate();
                GameObject SellPotion = Instantiate(SellPotion7, SellingOutPos);
                Rigidbody2D rigid = SellPotion.GetComponent<Rigidbody2D>();
                Vector3 outPower = new Vector2(Random.Range(0f, 2f), 0.5f);
                rigid.AddForce(outPower, ForceMode2D.Impulse);
                rigid.AddTorque(30f);
                AllPrice += 2500;//포션가격
                SellCount += 1;
                ComPotionParmeter.InType(7);
            }
            else
            {
                SoundManager.sounds["NotItem"].Play();
            }
        }
        else
        {
            SoundManager.sounds["NotItem"].Play();
        }
    }

    public void PushPotion8()
    {
        if (SellCount < 3)
        {
            if (PotionInven.Potion8Num > 0)
            {
                SoundManager.sounds["Click (6)"].Play();
                PotionInven.Potion8Num -= 1;
                PotionList.Potion8ListUpdate();
                GameObject SellPotion = Instantiate(SellPotion8, SellingOutPos);
                Rigidbody2D rigid = SellPotion.GetComponent<Rigidbody2D>();
                Vector3 outPower = new Vector2(Random.Range(0f, 2f), 0.5f);
                rigid.AddForce(outPower, ForceMode2D.Impulse);
                rigid.AddTorque(30f);
                AllPrice += 800;//포션가격
                SellCount += 1;
                ComPotionParmeter.InType(8);
            }
            else
            {
                SoundManager.sounds["NotItem"].Play();
            }
        }
        else
        {
            SoundManager.sounds["NotItem"].Play();
        }
    }

    public void PushPotion9()
    {
        if (SellCount < 3)
        {
            if (PotionInven.Potion9Num > 0)
            {
                SoundManager.sounds["Click (6)"].Play();
                PotionInven.Potion9Num -= 1;
                PotionList.Potion9ListUpdate();
                GameObject SellPotion = Instantiate(SellPotion9, SellingOutPos);
                Rigidbody2D rigid = SellPotion.GetComponent<Rigidbody2D>();
                Vector3 outPower = new Vector2(Random.Range(0f, 2f), 0.5f);
                rigid.AddForce(outPower, ForceMode2D.Impulse);
                rigid.AddTorque(30f);
                AllPrice += 700;//포션가격
                SellCount += 1;
                ComPotionParmeter.InType(9);
            }
            else
            {
                SoundManager.sounds["NotItem"].Play();
            }
        }
        else
        {
            SoundManager.sounds["NotItem"].Play();
        }
    }

    public void PushPotion10()
    {
        if (SellCount < 3)
        {
            if (PotionInven.Potion10Num > 0)
            {
                SoundManager.sounds["Click (6)"].Play();
                PotionInven.Potion10Num -= 1;
                PotionList.Potion10ListUpdate();
                GameObject SellPotion = Instantiate(SellPotion10, SellingOutPos);
                Rigidbody2D rigid = SellPotion.GetComponent<Rigidbody2D>();
                Vector3 outPower = new Vector2(Random.Range(0f, 2f), 0.5f);
                rigid.AddForce(outPower, ForceMode2D.Impulse);
                rigid.AddTorque(30f);
                AllPrice += 800;//포션가격
                SellCount += 1;
                ComPotionParmeter.InType(10);
            }
            else
            {
                SoundManager.sounds["NotItem"].Play();
            }
        }
        else
        {
            SoundManager.sounds["NotItem"].Play();
        }
    }

    public void PushPotion11()
    {
        if (SellCount < 3)
        {
            if (PotionInven.Potion11Num > 0)
            {
                SoundManager.sounds["Click (6)"].Play();
                PotionInven.Potion11Num -= 1;
                PotionList.Potion11ListUpdate();
                GameObject SellPotion = Instantiate(SellPotion11, SellingOutPos);
                Rigidbody2D rigid = SellPotion.GetComponent<Rigidbody2D>();
                Vector3 outPower = new Vector2(Random.Range(0f, 2f), 0.5f);
                rigid.AddForce(outPower, ForceMode2D.Impulse);
                rigid.AddTorque(30f);
                AllPrice += 2000;//포션가격
                SellCount += 1;
                ComPotionParmeter.InType(11);
            }
            else
            {
                SoundManager.sounds["NotItem"].Play();
            }
        }
        else
        {
            SoundManager.sounds["NotItem"].Play();
        }
    }

    public void PushPotion12()
    {
        if (SellCount < 3)
        {
            if (PotionInven.Potion12Num > 0)
            {
                SoundManager.sounds["Click (6)"].Play();
                PotionInven.Potion12Num -= 1;
                PotionList.Potion12ListUpdate();
                GameObject SellPotion = Instantiate(SellPotion12, SellingOutPos);
                Rigidbody2D rigid = SellPotion.GetComponent<Rigidbody2D>();
                Vector3 outPower = new Vector2(Random.Range(0f, 2f), 0.5f);
                rigid.AddForce(outPower, ForceMode2D.Impulse);
                rigid.AddTorque(30f);
                AllPrice += 1000;//포션가격
                SellCount += 1;
                ComPotionParmeter.InType(12);
            }
            else
            {
                SoundManager.sounds["NotItem"].Play();
            }
        }
        else
        {
            SoundManager.sounds["NotItem"].Play();
        }
    }

    public void PushPotion13()
    {
        if (SellCount < 3)
        {
            if (PotionInven.Potion13Num > 0)
            {
                SoundManager.sounds["Click (6)"].Play();
                PotionInven.Potion13Num -= 1;
                PotionList.Potion13ListUpdate();
                GameObject SellPotion = Instantiate(SellPotion13, SellingOutPos);
                Rigidbody2D rigid = SellPotion.GetComponent<Rigidbody2D>();
                Vector3 outPower = new Vector2(Random.Range(0f, 2f), 0.5f);
                rigid.AddForce(outPower, ForceMode2D.Impulse);
                rigid.AddTorque(30f);
                AllPrice += 1000;//포션가격
                SellCount += 1;
                ComPotionParmeter.InType(13);
            }
            else
            {
                SoundManager.sounds["NotItem"].Play();
            }
        }
        else
        {
            SoundManager.sounds["NotItem"].Play();
        }
    }

    public void PushPotion14()
    {
        if (SellCount < 3)
        {
            if (PotionInven.Potion14Num > 0)
            {
                SoundManager.sounds["Click (6)"].Play();
                PotionInven.Potion14Num -= 1;
                PotionList.Potion14ListUpdate();
                GameObject SellPotion = Instantiate(SellPotion14, SellingOutPos);
                Rigidbody2D rigid = SellPotion.GetComponent<Rigidbody2D>();
                Vector3 outPower = new Vector2(Random.Range(0f, 2f), 0.5f);
                rigid.AddForce(outPower, ForceMode2D.Impulse);
                rigid.AddTorque(30f);
                AllPrice += 900;//포션가격
                SellCount += 1;
                ComPotionParmeter.InType(14);
            }
            else
            {
                SoundManager.sounds["NotItem"].Play();
            }
        }
        else
        {
            SoundManager.sounds["NotItem"].Play();
        }
    }

    public void PushPotion15()
    {
        if (SellCount < 3)
        {
            if (PotionInven.Potion15Num > 0)
            {
                SoundManager.sounds["Click (6)"].Play();
                PotionInven.Potion15Num -= 1;
                PotionList.Potion15ListUpdate();
                GameObject SellPotion = Instantiate(SellPotion15, SellingOutPos);
                Rigidbody2D rigid = SellPotion.GetComponent<Rigidbody2D>();
                Vector3 outPower = new Vector2(Random.Range(0f, 2f), 0.5f);
                rigid.AddForce(outPower, ForceMode2D.Impulse);
                rigid.AddTorque(30f);
                AllPrice += 1700;//포션가격
                SellCount += 1;
                ComPotionParmeter.InType(15);
            }
            else
            {
                SoundManager.sounds["NotItem"].Play();
            }
        }
        else
        {
            SoundManager.sounds["NotItem"].Play();
        }
    }
}
