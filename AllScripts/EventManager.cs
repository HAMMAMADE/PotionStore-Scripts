using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    public StatusManager PlayerMana;
    public TimeManager CheckTime;//시간 체크
    public InfluenceManager Influence;//벨런스 게이지 
    public CustomerManager CustomerPrice;
    public MessageManager message;

    public Transform Uiset;
    public Animator animator;
    public UILabel EventLabel;

    public BoxCollider RightButton;
    public BoxCollider LeftButton;

    public PlayerControl PlayerMove;

    public GameObject RandomDrop1;
    public GameObject RandomDrop2;

    public Transform DropPoint1;
    public Transform DropPoint2;

    int oldeEvnet;
    int eventIndex;
    int activeRate;
    int DropNum;
    bool isPoison;
    bool isCold;

    string[] EventContents = {
                    "",
                    "던전에 독을 가진 보스급 몬스터가 등장했습니다!",
                    "나라에서 던전 공략을 위해 정예 기사단을 파견했습니다!",
                    "던전이 이상기후로 인해 일시적으로 추워졌습니다!",
                    "물약 샘의 정령으로부터 저주를 받았습니다!",
                    "알수 없는 이유로 던전의 몬스터들이 사나워 졌습니다!",
                    "약국 심사 평가 결과 낮은 점수를 받았습니다!",
                    "빚쟁이들이 나타나 이자를 요구했습니다!",
                    "균형의 수호신이 나타나 손가락을 튕겼습니다!",
                    "약국 주변에 머물고 있는 상인들로부터 팁을 받았습니다!",
                    "욕심이 생겨 잠시동안 물약 값을 올렸습니다!",
                    "약국에 대한 좋은 기사가 신문에 실렸습니다!",
                    "물약 샘의 정령으로부터 축복을 받았습니다!"};
    
    public void Awake()
    {
        StartCoroutine("Activate");
    }

    public void UIVisible(bool visible)
    {
        StopCoroutine("UIEvent");
        StartCoroutine("UIEvent",visible);
    }
	
    IEnumerator UIEvent(bool visible)
    {
        yield return null;

        Vector3 nextPos = Uiset.position;
        nextPos.y = visible ? 0f : 10f;

        if (Mathf.Abs(Uiset.position.y - nextPos.y) < 0.05f)
            yield break;

        Uiset.position = Vector3.Lerp(Uiset.position, nextPos, 0.3f);

        StartCoroutine("UIEvent",visible);
    }
    
    IEnumerator Activate()
    {
        yield return new WaitForSeconds(4f);
       
        if (!isCold && !isPoison && !StatusManager.isEvent&&!SkillSetManager.IsSkillGame&&!CheckTime.isCalculate&&TimeManager.nowFlow)
        {
            activeRate = Random.Range(0, 10000);
            eventIndex = Random.Range(0, EventContents.Length);
            if (250 >= activeRate && eventIndex != 0 && eventIndex != oldeEvnet)
            {
                PlayerMove.inputLeft = false;
                PlayerMove.inputRight = false;
                RightButton.enabled = false;
                LeftButton.enabled = false;
                SoundManager.sounds["Alert"].Play();
                //---------------------랜덤 이벤트 종류 선택
                EventLabel.text = EventContents[eventIndex];
                oldeEvnet = eventIndex;
                Eventeffect(eventIndex);
                //------------------------
                animator.SetTrigger("do" + eventIndex + "Anim");
                CheckTime.StopCoroutine("TimeFlow");
                TimeManager.nowFlow = false;
                StatusManager.isEvent = true;
                UIVisible(true);
            }

            if(9750 <= activeRate)
            {
                DropNum = Random.Range(0, 4);

                switch (DropNum)
                {
                    case 0:
                        break;
                    case 1:
                        Instantiate(RandomDrop1, DropPoint1);
                        break;
                    case 2:
                        Instantiate(RandomDrop2, DropPoint1);
                        break;
                    case 3:
                        Instantiate(RandomDrop2, DropPoint2);
                        break;
                    case 4:
                        Instantiate(RandomDrop1, DropPoint2);
                        break;
                }
            }
        }

        StartCoroutine("Activate");
    }

    public void ClickOKButton()
    {
        switch (eventIndex)
        {
            case 1:
                message.PopUpEventMesg("[000000]일정시간동안 독상태의 손님이 등장합니다.");
                break;
            case 2:
                message.PopUpEventMesg("[000000]모험가 세력이 약간 강해졌습니다.");
                break;
            case 3:
                message.PopUpEventMesg("[000000]일정시간동안 감기상태의 손님이 등장합니다.");
                break;
            case 4:
                message.PopUpEventMesg("[000000]마나가 한개 감소했습니다.");
                break;
            case 5:
                message.PopUpEventMesg("[000000]몬스터 세력이 약간 강해졌습니다.");
                break;
            case 6:
                message.PopUpEventMesg("[000000]평판이 감소했습니다.");
                break;
            case 7:
                message.PopUpEventMesg("[000000]소지금을 강탈당했습니다.");
                break;
            case 8:
                message.PopUpEventMesg("[000000]양측 세력의 균형이 유지되었습니다.");
                break;
            case 9:
                message.PopUpEventMesg("[000000]소지금이 약간 증가했습니다.");
                break;
            case 10:
                message.PopUpEventMesg("[000000]일정 시간동안 포션판매금이 약간 상승했습니다.");
                break;
            case 11:
                message.PopUpEventMesg("[000000]평판이 증가했습니다.");
                break;
            case 12:
                message.PopUpEventMesg("[000000]마나를 모두 회복하였습니다.");
                break;
        }
        RightButton.enabled = true;
        LeftButton.enabled = true;
        animator.SetTrigger("noneAnim");
        SoundManager.sounds["NextCatoonSound"].Play();
        StatusManager.isEvent = false;
        CheckTime.StartCoroutine("TimeFlow");
        TimeManager.nowFlow = true;
        UIVisible(false);
    }

    public void Eventeffect(int index)
    {
        switch (index)
        {
            case 1://보스급 몬스터 등장
                CustomerPrice.StopCoroutine("SpawnCustomer");
                CustomerPrice.StartCoroutine("SpawnPoison");
                StartCoroutine("PoisonTime");
                isPoison = true;
                break;
            case 2://기사 파견
                Influence.MoveInfluenceFunc(15);     
                break;
            case 3://이상 기후
                CustomerPrice.StopCoroutine("SpawnCustomer");
                CustomerPrice.StartCoroutine("SpawnCold");
                StartCoroutine("ColdTime");
                isCold = true;
                break;
            case 4://물약샘정령의 저주
                StatusManager.Mana -= 1;
                PlayerMana.UpdateManaState();
                break;
            case 5://몬스터가 사나워짐
                Influence.MoveInfluenceFunc(-15);
                break;
            case 6://평가도 하락
                StatusManager.EvaPoint = (int)(StatusManager.EvaPoint - StatusManager.EvaPoint * 0.1);
                break;
            case 7://빚쟁이
                StatusManager.Point = (int)(StatusManager.Point - StatusManager.Point * 0.2);
                break;
            case 8://타노스
                Vector3 nextScale = Influence.InfluenceProgress.transform.localScale;
                nextScale.x = 0.5f;
                Influence.InfluenceProgress.transform.localScale = nextScale;
                Influence.Influence = 50;
                Influence.Value = 50;
                break;
            case 9://팁
                StatusManager.Point = (int)(StatusManager.Point + StatusManager.Point * 0.1);
                break;
            case 10://물약값
                CustomerPrice.UpPrice = CustomerPrice.UpPrice + CustomerPrice.UpPrice * 0.15;
                StartCoroutine("UpPriceTime");
                break;
            case 11://신문기사
                StatusManager.EvaPoint = (int)(StatusManager.EvaPoint + StatusManager.EvaPoint * 0.1);
                break;
            case 12://물약샘정령의 축복
                StatusManager.Mana = 5;
                PlayerMana.UpdateManaState();
                break;
        }
    }

    IEnumerator UpPriceTime()
    {
        yield return new WaitForSecondsRealtime(30);

        CustomerPrice.UpPrice = 1;
    }

    IEnumerator PoisonTime()
    {
        yield return new WaitForSecondsRealtime(30);
        isPoison = false;
        CustomerPrice.StopCoroutine("SpawnPoison");
        CustomerPrice.StartCoroutine("SpawnCustomer");
    }

    IEnumerator ColdTime()
    {
        yield return new WaitForSecondsRealtime(30);
        isCold = false;
        CustomerPrice.StopCoroutine("SpawnCold");
        CustomerPrice.StartCoroutine("SpawnCustomer");
    }
}
