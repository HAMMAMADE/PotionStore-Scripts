using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfluenceManager : MonoBehaviour {

    public int Influence;

    public bool isPerson;
    public bool isMonster;

    public UISprite InfluenceProgress;

    public float Value;
    public bool isMoving;

    //------------데이터로드
    public void DataAccess(DataVo dataVo)
    {
        InfluenceProgress = GameObject.Find("Influence").GetComponentsInChildren<UISprite>()[1];

        Influence = int.Parse(dataVo.SaveInflu);

        Vector3 nextScale = InfluenceProgress.transform.localScale;

        nextScale.x = Influence / 100f;

        InfluenceProgress.transform.localScale = nextScale;

        StartCoroutine("MoveInfluenceNormal");
        StartCoroutine("CheckInfluence");
    }

    //-----------------------------------

    IEnumerator CheckInfluence()
    {
        yield return new WaitForSeconds(0.2f);
        if(InfluenceProgress.transform.localScale.x >= 0.99f)
        {
            isMonster = false;
            isPerson = true;
        }
        else if (InfluenceProgress.transform.localScale.x <= 0.01f)
        {
            isPerson = false;
            isMonster = true;
        }
        else
        {
            isPerson = false;
            isMonster = false;
        }
        StartCoroutine("CheckInfluence");
    }


    IEnumerator MoveInfluenceNormal()
    {
        yield return new WaitForSeconds(0.1f);
        if (isMoving == false)
        {
            Vector3 nextScale = InfluenceProgress.transform.localScale;

            nextScale.x += 0.1f * 0.05f;
            InfluenceProgress.transform.localScale = nextScale;

            StartCoroutine("MoveInfluenceNormalM");
        }
        else
        {
            StartCoroutine("MoveInfluenceNormalM");
        }
    }

    IEnumerator MoveInfluenceNormalM()
    {
        yield return new WaitForSeconds(0.1f);
        if (isMoving == false)
        {
            Vector3 nextScale = InfluenceProgress.transform.localScale;

            nextScale.x -= 0.1f * 0.05f;
            InfluenceProgress.transform.localScale = nextScale;

            StartCoroutine("MoveInfluenceNormal");
        }
        else
        {
            StartCoroutine("MoveInfluenceNormal");
        }
    }

    public void MoveInfluenceFunc(int value)
    {
        isMoving = true;
        if (value > 0)
        {
            Influence += value;
            Value = Influence;
            DataManager.SaveInflu = Influence;
            StartCoroutine("MoveInfluence");
            isMoving = false;
        }
        else if(value <= 0)
        {
            Influence += value;
            Value = Influence;
            DataManager.SaveInflu = Influence;
            StartCoroutine("MoveInfluenceMinus");
            isMoving = false;
        }
    }

    IEnumerator MoveInfluence()
    {
        yield return null;
        Vector3 nextScale = InfluenceProgress.transform.localScale;
        
        if (Value / 100f > nextScale.x)
        {
            if (InfluenceProgress.transform.localScale.x >= 1f)
            {
                nextScale.x = 1f;
                InfluenceProgress.transform.localScale = nextScale;
            }
            else
            {
                nextScale.x += 0.3f * 0.001f;
                InfluenceProgress.transform.localScale = nextScale;
            }
        }
        else
        {
            StopCoroutine("MoveInfluence");
            yield break;
        }
        StartCoroutine("MoveInfluence");

    }

    IEnumerator MoveInfluenceMinus()
    {
        yield return null;
        Vector3 nextScale = InfluenceProgress.transform.localScale;

        if (Value / 100f < nextScale.x)
        {
            if (InfluenceProgress.transform.localScale.x <= 0f)
            {
                nextScale.x = 0f;
                InfluenceProgress.transform.localScale = nextScale;
            }
            else
            {
                nextScale.x -= 0.3f * 0.001f;//nextScale.x = 0.1이 되어야함.
                InfluenceProgress.transform.localScale = nextScale;
            }
        }
        else
        {
            StopCoroutine("MoveInfluenceMinus");
            yield break;
        }
        StartCoroutine("MoveInfluenceMinus");

    }
}
