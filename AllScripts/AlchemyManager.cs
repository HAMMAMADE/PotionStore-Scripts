using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemyManager : MonoBehaviour {

    public int PotionCompleted;

    float Value;

    public UISprite PotionProgress;

    public MaterialListManager MList;

    //public InventoryManager Inven;

    public MatInventoryManager MatInven;

    public SkillSetManager Skill;

    //--------------------------------------생성될 포션 리스트를 가져올 변수 선언 호롤롤롤롤로
    public PotionListManager PotionList;
    //-------------------------------------아이콘을 넘기는 변수선언
    public UISprite FlaskIconSprite;
    public UISprite MaterialIconSprite;
    //--------------------------------------넘겨받은 물약구성요소 및 재료 변수 선언

    public static int Blue, Red, Gas;

    public int useItemNum;

    public static bool pushMaterial;//완성 전에 모든 재료가 들어갔는지 확인하는 변수

    public static bool pushFlask;

    GameObject OKbutton;

    UIButton OKbuttonScript;

    BoxCollider OKcollider;

    void Awake()
    {
        PotionProgress = GameObject.Find("CompletedGuage").GetComponentsInChildren<UISprite>()[1];
        OKbutton = GameObject.Find("AlchemyButton");
        OKbuttonScript = OKbutton.GetComponent<UIButton>();
        OKcollider = OKbutton.GetComponent<BoxCollider>();
    }

    void Update()
    { 
        OKcollider.enabled = pushFlask == true && pushMaterial == true;
        OKbuttonScript.state = (pushFlask == true && pushMaterial == true) ? UIButtonColor.State.Normal : UIButtonColor.State.Disabled;
    }

    public void ResetGuage()
    {
        StopCoroutine("MoveUpGuage");
        StopCoroutine("MoveDownGuage");
        Vector3 nextScale = PotionProgress.transform.localScale;


        nextScale.y = 0;

        StartCoroutine("ResetColorGuage");
        PotionProgress.transform.localScale = nextScale;
    }

    public void CompleteGuageChange(int num, bool ChangeColor)
    {
        PotionCompleted += num;
        Value = num;
        if (num >= 0)
        {
            StartCoroutine("MoveUpGuage");
        }
        else
        {
            StartCoroutine("MoveDownGuage");
        }

        if (ChangeColor)
        {
            StartCoroutine("MoveColorGuage");
        }
    }
    IEnumerator MoveUpGuage()
    {
        yield return null;
        Vector3 nextScale = PotionProgress.transform.localScale;
        
        if (Value / 100f > nextScale.y)
        {
            if (PotionProgress.transform.localScale.y >= 1f)
            {
                nextScale.y = 1f;
                PotionProgress.transform.localScale = nextScale;
            }
            else
            {
                nextScale.y += 0.1f * 0.05f;
                PotionProgress.transform.localScale = nextScale;
            }
        }
        else
        {
            StopCoroutine("MoveUpGuage");
            yield break;
        }
        StartCoroutine("MoveUpGuage");
    }

    IEnumerator MoveDownGuage()
    {
        yield return null;
        Vector3 nextScale = PotionProgress.transform.localScale;

        if (Value / 100f <= nextScale.y)
        {
            if (PotionProgress.transform.localScale.y <= 0f)
            {
                nextScale.y = 1f;
                PotionProgress.transform.localScale = nextScale;
            }
            else
            {
                nextScale.y -= 0.1f * 0.05f;
                PotionProgress.transform.localScale = nextScale;
            }
        }
        else
        {
            StopCoroutine("MoveDownGuage");
            yield break;
        }
        StartCoroutine("MoveDownGuage");
    }

    IEnumerator MoveColorGuage()
    {
        yield return null;
        Color32 nextRGB = PotionProgress.color;

        if (nextRGB.r >= 1)
        {
            nextRGB.r -= 1;
            PotionProgress.color = nextRGB;
        }
        else
        {
            PotionProgress.color = new Color32(0, 255, 255, 255);
            StopCoroutine("MoveColorGuage");
            yield break;
        }
        StartCoroutine("MoveColorGuage");
    }

    IEnumerator ResetColorGuage()
    {
        yield return null;
        Color32 nextRGB = PotionProgress.color;

        if (nextRGB.r < 255)
        {
            nextRGB.r += 1;
            PotionProgress.color = nextRGB;
        }
        else
        {
            PotionProgress.color = new Color32(255, 255, 255, 255);
            StopCoroutine("ResetColorGuage");
            yield break;
        }
        StartCoroutine("ResetColorGuage");
    }

    public void PushAlchemyButton()//물약 완성하기 버튼 클릭시 발동하는 함수
    {
        
        SoundManager.sounds["DoAlchemy"].Play();
        GameObject MiniMaterial = GameObject.FindGameObjectWithTag("MiniMaterial");
        SkillSetManager.EffectDeleteFunc();
        Destroy(MiniMaterial);
        PotionList.makePotion();
        PotionList.CompleteSkill2 = false;
        PotionList.CompleteSkill3 = false;
        Skill.cantChange = false;
        Skill.cantEnforce = false;
        Skill.cantEnfPotion = false;
        pushMaterial = false;
        pushFlask = false;
        Skill.nowSkill = false;
        ResetGuage();
    }

    public void PushMaterialButton()//재료리스트중 재료버튼 클릭시 발동하는 함수 
    {
        useItemNum = MatInven.Itemnum;

        SoundManager.sounds["DoAlchemy"].Play();

        switch (useItemNum)
        {
            case 1:
                MatInven.haveHarb--;
                MatInven.haveItem--;
                break;

            case 2:
                MatInven.haveTomato--;
                MatInven.haveItem--;
                break;

            case 3:
                MatInven.haveItem3--;
                MatInven.haveItem--;
                break;
        }

    }
   
}
