using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour//가지고 있는 포션을 관리하는 스크립트
{
    public PlayerControl Player;



    public SpriteRenderer ParSprite;

    public SpriteRenderer Sprite;
    //-----------------------------------

    public int GetFlaskCount;
    public int haveFlaskCount;
    public int haveBlue;
    public int haveRed;
    public int haveGas;

    //----------------------------------

    public bool eatFlask1;
    public bool eatFlask2;

    //----------------------------------

   /* void Awake()
    {
        GetFlaskCount = 0;
        StartCoroutine("GetFlasks");
    }

    IEnumerator GetFlasks()//플라스크 획득모션 및 인식 인터벌
    {

            yield return new WaitForSeconds(1f);
            GetFlaskCount = 0;

            GameObject[] Flasks = GameObject.FindGameObjectsWithTag("potion");
            

            Vector3 PlayerPos = GameObject.Find("Player").transform.position;
            for (int index = 0; index < Flasks.Length; index++)
            {
                if (Flasks[index].transform.position.x >= PlayerPos.x - 0.5f && Flasks[index].transform.position.x <= PlayerPos.x + 0.5f && Flasks[index].transform.position.y <= -2.2f)
                {
                if (haveFlaskCount < 2)
                {
                    if (GetFlaskCount < 1)
                    {

                        Par = Flasks[index].GetComponent<FlaskParmeter>();
                        ParSprite = Flasks[index].GetComponent<SpriteRenderer>();

                        SoundManager.sounds["Pop (3)"].Play();
                        Rigidbody2D rigid = Flasks[index].GetComponent<Rigidbody2D>();
                        Vector2 getMotion = new Vector2(0f, 3f);
                        rigid.AddForce(getMotion, ForceMode2D.Impulse);

                        haveBlue = Par.inBlue;
                        haveRed = Par.inRed;
                        haveGas = Par.inGas;

                        //----------------------------------------------------

                       if (eatFlask1 == false)
                        {
               
                            FlaskinList1.AddList1(haveBlue, haveRed, haveGas);
                            haveFlaskCount += 1;
                           eatFlask1 = true;
                            goto exit;
                       }
                       if(eatFlask2 == false)
                        {
                
                            FlaskinList2.AddList2(haveBlue, haveRed, haveGas);
                            haveFlaskCount += 1;
                           eatFlask2 = true;
                            goto exit;
                        }
                        //------------------------------------------------------

                        exit:
                        GetFlaskCount++;
                        Destroy(Flasks[index].gameObject, 0.5f);

                    }
                }
                }
            }
            StartCoroutine("GetFlasks");
        }*/
}

