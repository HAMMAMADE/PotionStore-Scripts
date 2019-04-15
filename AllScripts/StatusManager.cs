using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour {

    //public static bool isTutorial;
    public static int LoadPoint;
    public static int LoadEva;

    public static int Point;
    public static int EvaPoint;
    public static int TodayPoint;
    public static float Diamonds;
    public static int Mana;
    public static bool isEvent;

    UILabel PointLabel;
    UILabel EvaPointLabel;

    public UISprite ManaStone1;
    public UISprite ManaStone2;
    public UISprite ManaStone3;
    public UISprite ManaStone4;
    public UISprite ManaStone5;

    public UILabel ReceiptDia;
    public UILabel TodayGetPoint;
    public UILabel UsePoint;
    public UILabel ReceiptResult;

    Vector3 GUINextPos;
    public Transform GUIMoveSet;
    public UIPanel GUInum;
    public UILabel GUIPoint;
    public UILabel GUIEva;

    float calculate;

	void Awake () {
        GUINextPos = GUIMoveSet.position;
        TodayPoint = 0;
        Mana = 5;
        PointLabel = GameObject.Find("Point Label").GetComponentInChildren<UILabel>();
        EvaPointLabel = GameObject.Find("Eva Label").GetComponentInChildren<UILabel>();
        StartCoroutine("UiUpdate");
	}

    //-------데이터 로드--------------------------

    public void DataAccess(DataVo dataVo)
    {
        Diamonds = float.Parse(dataVo.Diamonds);
        Point = int.Parse(dataVo.HaveMoney);
        EvaPoint = int.Parse(dataVo.HaveEva);
    }

    //--------------------------------------------
	IEnumerator UiUpdate()
    {
        yield return new WaitForSeconds(0.1f);
        PointLabel.text = string.Format("{0:n0}", Point);
        EvaPointLabel.text = string.Format("{0:n0}", EvaPoint);
        StartCoroutine("UiUpdate");
    }

    public void UpdateManaState()
    {
        switch (Mana)
        {
            case 0:
                ManaStone1.color = new Color32(255, 255, 255, 0);
                ManaStone2.color = new Color32(255, 255, 255, 0);
                ManaStone3.color = new Color32(255, 255, 255, 0);
                ManaStone4.color = new Color32(255, 255, 255, 0);
                ManaStone5.color = new Color32(255, 255, 255, 0);
                break;
            case 1:
                ManaStone1.color = new Color32(255, 255, 255, 255);
                ManaStone2.color = new Color32(255, 255, 255, 0);
                ManaStone3.color = new Color32(255, 255, 255, 0);
                ManaStone4.color = new Color32(255, 255, 255, 0);
                ManaStone5.color = new Color32(255, 255, 255, 0);
                break;
            case 2:
                ManaStone1.color = new Color32(255, 255, 255, 255);
                ManaStone2.color = new Color32(255, 255, 255, 255);
                ManaStone3.color = new Color32(255, 255, 255, 0);
                ManaStone4.color = new Color32(255, 255, 255, 0);
                ManaStone5.color = new Color32(255, 255, 255, 0);
                break;
            case 3:
                ManaStone1.color = new Color32(255, 255, 255, 255);
                ManaStone2.color = new Color32(255, 255, 255, 255);
                ManaStone3.color = new Color32(255, 255, 255, 255);
                ManaStone4.color = new Color32(255, 255, 255, 0);
                ManaStone5.color = new Color32(255, 255, 255, 0);
                break;
            case 4:
                ManaStone1.color = new Color32(255, 255, 255, 255);
                ManaStone2.color = new Color32(255, 255, 255, 255);
                ManaStone3.color = new Color32(255, 255, 255, 255);
                ManaStone4.color = new Color32(255, 255, 255, 255);
                ManaStone5.color = new Color32(255, 255, 255, 0);
                break;
            case 5:
                ManaStone1.color = new Color32(255, 255, 255, 255);
                ManaStone2.color = new Color32(255, 255, 255, 255);
                ManaStone3.color = new Color32(255, 255, 255, 255);
                ManaStone4.color = new Color32(255, 255, 255, 255);
                ManaStone5.color = new Color32(255, 255, 255, 255);
                break;
        }
    }

    public void UpdateReceipt()
    {
        if (TodayPoint < 0)
        {
            calculate = Diamonds;
            TodayPoint = 0;
        }
        else
        {
            if (TimeManager.Day >= 20 && Diamonds > 0)
            {
                TodayPoint = Point;
                calculate = Diamonds - (Point * 0.5f / 100f);
                if (calculate < 0)
                {
                    Point = (int)calculate * -200;
                    calculate = 0;
                }
                else
                {
                    Point = 0;
                }
            }
            else
            {
                calculate = Diamonds - (TodayPoint * 0.5f / 100f);
                Point = Point - (TodayPoint / 2);
            }
        }

        calculate = Mathf.FloorToInt(calculate);


        ReceiptDia.text = string.Format("{0}", Diamonds);
        TodayGetPoint.text = string.Format("{0:n0}", TodayPoint);
        UsePoint.text = string.Format("{0:n0}", TodayPoint / 2);
        ReceiptResult.text = string.Format("{0}", calculate);

        if (calculate >= 0) Diamonds = calculate;
        else calculate = 0;

    }

    public void GUIPopUp(int Money, int Eva)
    {
        GUINextPos.y = 0f;
        GUIMoveSet.position = GUINextPos;
        StopCoroutine("ShowGui");
        StartCoroutine("ShowGui");
        GUIPoint.text = string.Format("{0:n0}", Money);
        GUIEva.text = Eva.ToString();
    }

    IEnumerator ShowGui()
    {
        
        GUInum.alpha = 0.9f;
        yield return new WaitForSeconds(0.2f);
        StartCoroutine("EndShow");
    }

    IEnumerator EndShow()
    {
        GUINextPos = GUIMoveSet.position;
        
        GUINextPos.y = GUINextPos.y + 0.15f;

        yield return null;
        if (GUInum.alpha <= 0)
        {
            StopCoroutine("EndShow");
            yield break;
        }
        else
        {
            GUIMoveSet.position = Vector3.Lerp(GUIMoveSet.position, GUINextPos, 0.1f);
            GUInum.alpha -= 0.02f;
        }
        StartCoroutine("EndShow");
    }
}
