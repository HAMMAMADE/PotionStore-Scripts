using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour {

    public GameObject UiAlert;

    UILabel MsgText;
    UILabel contents;

    Vector3 MsgNormalPos;
    Vector3 MsgNextPos;

    void Awake()
    {
        MsgNormalPos = UiAlert.transform.localPosition;
        MsgNextPos.y = -500f;
        MsgNextPos.z = 0f;
        MsgText = UiAlert.GetComponentInChildren<UILabel>();
    }

    public void PopUpEventMesg(string Mmsg)
    {

        SoundManager.sounds["Alert"].Play();

        MsgText.text = Mmsg;

        StopCoroutine("MsgPopUpAni");
        StopCoroutine("MsgCloseAni");

        UiAlert.transform.position = MsgNormalPos;

        StartCoroutine("MsgPopUpAni");
    }

    public void PopUpFurnitureMesg()
    {
        SoundManager.sounds["Alert"].Play();

        MsgText.text = "[000000] 시설을 구매하고 강화했습니다. ";

        StopCoroutine("MsgPopUpAni");
        StopCoroutine("MsgCloseAni");

        UiAlert.transform.position = MsgNormalPos;

        StartCoroutine("MsgPopUpAni");
    }


    public void PopUpMaterialMesg(string Mmsg)
    {
        SoundManager.sounds["Alert"].Play();

        MsgText.text = Mmsg + "[000000] 재료 리스트에 등록 되었습니다.";

        StopCoroutine("MsgPopUpAni");
        StopCoroutine("MsgCloseAni");

        UiAlert.transform.position = MsgNormalPos;

        StartCoroutine("MsgPopUpAni");
    }

    public void PopUpMesg(string msg)
    {
        SoundManager.sounds["Alert"].Play();

        MsgText.text = msg + "[000000] 판매 리스트에 등록 되었습니다.";

        UiAlert.transform.position = MsgNormalPos;

        StopCoroutine("MsgPopUpAni");
        StopCoroutine("MsgCloseAni");

        UiAlert.transform.position = MsgNormalPos;

        StartCoroutine("MsgPopUpAni");
    }

    public void PopUpCusMesg()
    {
        SoundManager.sounds["Alert"].Play();

        MsgText.text = "[000000]새로운 손님이 등장합니다.";

        UiAlert.transform.position = MsgNormalPos;

        StopCoroutine("MsgPopUpAni");
        StopCoroutine("MsgCloseAni");

        UiAlert.transform.position = MsgNormalPos;

        StartCoroutine("MsgPopUpAni");
    }

    public void PopUpBossMesg()
    {
        SoundManager.sounds["Alert"].Play();

        MsgText.text = "[000000]특별한 손님이 등장했습니다!";

        UiAlert.transform.position = MsgNormalPos;

        StopCoroutine("MsgPopUpAni");
        StopCoroutine("MsgCloseAni");

        UiAlert.transform.position = MsgNormalPos;

        StartCoroutine("MsgPopUpAni");
    }

    IEnumerator MsgPopUpAni()
    {
        yield return null;

        UiAlert.transform.localPosition = Vector3.Lerp(UiAlert.transform.localPosition, MsgNextPos, 0.2f);

        if (UiAlert.transform.localPosition.y <= MsgNextPos.y + 0.15f)
        {
            UiAlert.transform.localPosition = MsgNextPos;

            yield return new WaitForSeconds(3.5f);

            StartCoroutine("MsgCloseAni");
            StopCoroutine("MsgPopUpAni");
        }
        else
        {
            StartCoroutine("MsgPopUpAni");
        }
    }

    IEnumerator MsgCloseAni()
    {
        yield return null;

        UiAlert.transform.localPosition = Vector3.Lerp(UiAlert.transform.localPosition, MsgNormalPos, 0.2f);

        if (UiAlert.transform.localPosition.y >= MsgNormalPos.y - 0.05f)
        {
            UiAlert.transform.localPosition = MsgNormalPos;
            StopCoroutine("MsgCloseAni");
            yield break;
        }
        else
        {
            StartCoroutine("MsgCloseAni");
        }
    }
}
