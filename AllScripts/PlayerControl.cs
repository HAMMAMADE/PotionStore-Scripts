using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour//플레이어를 움직이게 하고 각종 UI가 나타나게 하는 스크립트
{
    Animator animator;

    public int PlayerLevel = 1;

    public int PlayerPos;

    public Transform mainCamera;

    public Transform MoneyBallon;

    Transform UISet;

    Vector3 CamnextPos;

    Vector3 UInextPos;

    Vector3 UI1MovePos;

    Vector3 UI2MovePos;

    Vector3 UI3MovePos;

    Vector3 UI4MovePos;

    Vector3 UI5MovePos;

    Vector3 UI6MovePos;

    Rigidbody2D rigid;

    public MarchentManager MeetMarchent;
    public FurnitureManager FurnitureSet;
    public PotionListManager FindPotionCase;
    public TutorialManager isTutorial;
    public StoryManager story;

    SellingManager isReady;
    SkillSetManager PushSkillButton;

    public bool ReadyOrderd;

    bool useButton = false;
    bool FirstMet;
    public bool FirstMagic;
    //------------------------------------------

    public bool inputLeft = false;
    public bool inputRight = false;

    public bool isFurniture;

    //------------------------------------------
    bool InUi3;
    //------------------------------------------
    public bool Tutorial2;
    public bool Tutorial3;
    public bool Tutorial4;

    public void LeftButtonClick()
    {
        inputRight = false;
        inputLeft = true;
    }

    public void LeftButtonRelease()
    {
        inputLeft = false;
    }

    public void RightButtonClick()
    {
        inputLeft = false;
        inputRight = true;
    }

    public void RightButtonRelease()
    {
        inputRight = false;
    }

    void Awake()
    {
        PlayerPos = 1;

        isReady = GameObject.Find("SellingManager").GetComponent<SellingManager>();

        animator = GetComponent<Animator>();

        UISet = GameObject.Find("UI Set 1").transform;

        rigid = GetComponent<Rigidbody2D>();

        CamnextPos = Vector3.zero;
        CamnextPos.z = -10;
        UInextPos = Vector3.zero;
        UInextPos.z = -10;

    }

    void Update()
    {
        Vector3 eulerAngles = transform.localEulerAngles;

        if ((!inputLeft && !inputRight))
        {
            animator.SetBool("isMoving", false);
        }
        else if (inputLeft)
        {
            eulerAngles.y = 180f;
            transform.localRotation = Quaternion.Euler(eulerAngles);
            animator.SetBool("isMoving", true);
        }
        else if (inputRight)
        {
            eulerAngles.y = 0f;
            transform.localRotation = Quaternion.Euler(eulerAngles);
            animator.SetBool("isMoving", true);
        }

    }

    public void SellingMotion()
    {
        StartCoroutine("Selling");
    }

    IEnumerator Selling()
    {
        animator.SetTrigger("SellingItem");
        yield return new WaitForSeconds(0.55f);
        animator.SetTrigger("SellingItem");
    }


    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Floor")
        {

            PlayerPos = rigid.velocity.x > 0 ? PlayerPos -= 1 : PlayerPos += 1;
            if(rigid.velocity.x > 0)
            {
                inputLeft = false;
            }
            else
            {
                inputRight = false;
            }
           // CamnextPos.x = rigid.velocity.x > 0 ? CamnextPos.x + 18f : CamnextPos.x - 18f;//x<0 왼쪽, x>0 오른쪽

            if(PlayerPos == 1 && rigid.velocity.x < 0)//오른쪽이동
            {
                CamnextPos = new Vector3(0.0f, 0.0f, -10.0f);
                UInextPos = new Vector3(0.0f, 0.0f, -10.0f);
            }
            else if(PlayerPos == 1 && rigid.velocity.x > 0)//2->1
            {
                CamnextPos = new Vector3(0.0f, 0.0f, -10.0f);
                UInextPos = new Vector3(0.0f, 0.0f, -10.0f);
            }
            else if(PlayerPos == 2 && rigid.velocity.x < 0)//1->2
            {
                CamnextPos = new Vector3(-18.0f, 0.0f, -10.0f);
                UInextPos = new Vector3(18.0f, 0.0f, -10.0f);
            }
            else if (PlayerPos == 2 && rigid.velocity.x > 0)//3->2
            {
                CamnextPos = new Vector3(-18.0f, 0.0f, -10.0f);
                UInextPos = new Vector3(18.0f, 0.0f, -10.0f);
            }
            else if (PlayerPos == 3 && rigid.velocity.x < 0)//2->3
            {
                CamnextPos = new Vector3(-36.0f, 0.0f, -10.0f);
                UInextPos = new Vector3(36.0f, 0.0f, -10.0f);
            }
            else if (PlayerPos == 3 && rigid.velocity.x > 0)//4->3
            {
                CamnextPos = new Vector3(-36.0f, 0.0f, -10.0f);
                UInextPos = new Vector3(36.0f, 0.0f, -10.0f);
            }
            else if (PlayerPos == 4 && rigid.velocity.x < 0)//3->4
            {
                CamnextPos = new Vector3(-54.0f, 0.0f, -10.0f);
                UInextPos = new Vector3(54.0f, 0.0f, -10.0f);
            }
            else if (PlayerPos == 4 && rigid.velocity.x > 0)//4error
            {
                CamnextPos = new Vector3(-54.0f, 0.0f, -10.0f);
                UInextPos = new Vector3(54.0f, 0.0f, -10.0f);
            }

            //UInextPos.x = rigid.velocity.x > 0 ? UInextPos.x - 18f : UInextPos.x + 18f;
            Vector3 nextPos = transform.position;
            nextPos.x = rigid.velocity.x > 0 ? nextPos.x + 2.7f : nextPos.x - 2.7f;
            transform.position = nextPos;


            StartCoroutine("CameraAnimation");
           
            if (!Tutorial2 && PlayerPos == 2)
            {
                isTutorial.NextTutorial2();
                Tutorial2 = true;
            }
            else if (!Tutorial3 && PlayerPos == 3)
            {
                isTutorial.NextTutorial3();
                Tutorial3 = true;
            }
            else if (!Tutorial4 && PlayerPos == 4)
            {
                isTutorial.NextTutorial4();
                Tutorial4 = true;
            }

        }

        else if (collision.gameObject.name == "DeskSample")
        {
            if (!TutorialManager.FirstCus)
            {
                TutorialManager.FirstCus = true;
                StopCoroutine("UI1Move");
                isReady.isReadySell = true;
                GameObject UI1Pos = GameObject.Find("Selling Set");
                UI1MovePos = UI1Pos.transform.position;
                ReadyOrderd = true;
                FirstMet = true;
                StartCoroutine("UI1Move");
            }
            else
            {
                StopCoroutine("UI1Move");
                isReady.isReadySell = true;
                GameObject UI1Pos = GameObject.Find("Selling Set");
                UI1MovePos = new Vector3(-7.9f, 1.0f, -11.0f);
                ReadyOrderd = true;
                StartCoroutine("UI1Move");
            }
        }

        else if (collision.gameObject.name == "Gama")
        {
            StopCoroutine("UI3Move");
            FindPotionCase.StartCoroutine("FindCase");
            GameObject UI3Pos = GameObject.Find("Alchemy Set");
            UI3MovePos = new Vector3(-10.6f, 0.7f, -10.0f);
            InUi3 = true;
            StartCoroutine("UI3Move");

        }

        else if (collision.gameObject.name == "Gama2")
        {
            StopCoroutine("UI4Move");
            GameObject UI4Pos = GameObject.Find("Skill Set");
            UI4MovePos = UI4Pos.transform.position;
            StartCoroutine("UI4Move");
        }

        else if (collision.gameObject.name == "MarchentChar")
        {
            StopCoroutine("UI5Move");
            isFurniture = true;
            GameObject UI5Pos = GameObject.Find("Furniture Set");
            if(MeetMarchent.isNightMarchent) MeetMarchent.PushMarchentButton();
            if(!FurnitureSet.PushedBuyButton) UI5MovePos = new Vector3(-0.2f, 0.7f, -10.0f);
            else UI5MovePos = new Vector3(6.3f, 0.7f, -10.0f);
            StartCoroutine("UI5Move");
        }

        else if (collision.gameObject.name == "SkillMaster")
        {
            StopCoroutine("UI6Move");
            if (!FirstMagic && !TutorialManager.isTutorial)
            {
                FirstMagic = true;
                inputLeft = false;
                inputRight = false;
                story.StoryPage = 97;
                story.NewStoryList();
                GameObject UI6Pos = GameObject.Find("SkillUp Set");
                UI6MovePos = new Vector3(-5.7f, 9.0f, -10.0f);
            }
            else
            {
                GameObject UI6Pos = GameObject.Find("SkillUp Set");
                UI6MovePos = new Vector3(-5.7f, 1.75f, -10.0f);
            }

            StartCoroutine("UI6Move");
        }
    }


    IEnumerator UI1Move()
    {
        Transform UIMoveSet1 = GameObject.Find("Selling Set").transform;

        yield return null;
        StartCoroutine("UI1Move");

        if (UIMoveSet1.position == UI1MovePos)
        {
            StopCoroutine("UI1Move");
            yield break;
        }
        UIMoveSet1.position = Vector3.Lerp(UIMoveSet1.position, UI1MovePos, 0.65f);

    }

    IEnumerator UI3Move()
    {
        Transform UIMoveSet3 = GameObject.Find("Alchemy Set").transform;
        yield return null;

        StartCoroutine("UI3Move");

        if (InUi3 == true)
        {
            if (UIMoveSet3.position == UI3MovePos)
            {
                UIMoveSet3.position = UI3MovePos;
                StopCoroutine("UI3Move");
                yield break;
            }
        }
        else
        {
            if (UIMoveSet3.position == UI3MovePos)
            {
                UIMoveSet3.position = UI3MovePos;
                StopCoroutine("UI3Move");
                yield break;
            }
        }
        UIMoveSet3.position = Vector3.Lerp(UIMoveSet3.position, UI3MovePos, 0.65f);
     }

    IEnumerator UI4Move()
    {

        Transform UIMoveSet4 = GameObject.Find("Skill Set").transform;

        yield return null;

        StartCoroutine("UI4Move");

        if (UIMoveSet4.position == UI4MovePos)
        {
            StopCoroutine("UI4Move");
            yield break;
        }
       
        UIMoveSet4.position = Vector3.Lerp(UIMoveSet4.position, UI4MovePos, 0.65f);
    }

    IEnumerator UI5Move()
    {

        Transform UIMoveSet5 = GameObject.Find("Furniture Set").transform;
        yield return null;

        StartCoroutine("UI5Move");

        if (UIMoveSet5.position == UI5MovePos)
        {
            StopCoroutine("UI5Move");
            yield break;
        }
        UIMoveSet5.position = Vector3.Lerp(UIMoveSet5.position, UI5MovePos, 0.65f);

    }

    IEnumerator UI6Move()
    {

        Transform UIMoveSet6 = GameObject.Find("SkillUp Set").transform;
        yield return null;

        StartCoroutine("UI6Move");

        if (UIMoveSet6.position == UI6MovePos)
        {
            StopCoroutine("UI6Move");
            yield break;
        }
        UIMoveSet6.position = Vector3.Lerp(UIMoveSet6.position, UI6MovePos, 0.4f);

    }

    IEnumerator CameraAnimation()//카메라의 움직임
    {
        mainCamera.position = Vector3.Lerp(mainCamera.transform.position, CamnextPos, 0.2f);
        UISet.position = Vector3.Lerp(UISet.transform.position, UInextPos, 0.2f);

        if (UISet.position.x > UInextPos.x && UISet.position.x < UInextPos.x + 0.01f)
        {
            mainCamera.position = CamnextPos;
            UISet.position = UInextPos;
            StopCoroutine("CameraAnimation");
            yield break;
        }

        else if (UISet.position.x < UInextPos.x && UISet.position.x > UInextPos.x - 0.01f)
        {
            mainCamera.position = CamnextPos;
            UISet.position = UInextPos;
            StopCoroutine("CameraAnimation");
            yield break;
        }

        yield return null;
        StartCoroutine("CameraAnimation");
    }

    IEnumerator TalkBallonNoMoney()
    {
        yield return null;
        Vector3 nextScale = MoneyBallon.transform.localScale;
        if (nextScale.x < 0.9f)
        {
            nextScale.x += 0.12f;
            MoneyBallon.transform.localScale = nextScale;
        }
        else
        {
            nextScale.x = 1f;
            MoneyBallon.transform.localScale = nextScale;
            yield return new WaitForSeconds(1f);
            StartCoroutine("TalkBallonNoMoneyExit");
            StopCoroutine("TalkBallonNoMoney");
            yield break;
        }
        StartCoroutine("TalkBallonNoMoney");
    }

    IEnumerator TalkBallonNoMoneyExit()
    {
        yield return null;
        Vector3 nextScale = MoneyBallon.transform.localScale;
        if (nextScale.x > 0f)
        {
            nextScale.x -= 0.12f;
            MoneyBallon.transform.localScale = nextScale;
        }
        else
        {
            nextScale.x = 0f;
            MoneyBallon.transform.localScale = nextScale;
            StopCoroutine("TalkBallonNoMoneyExit");
            yield break;
        }
        StartCoroutine("TalkBallonNoMoneyExit");
    }

    public void PopUpBallon()
    {
        StartCoroutine("TalkBallonNoMoney");
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "DeskSample")
        {
            if (FirstMet)
            {
                StopCoroutine("UI1Move");
                isReady.isReadySell = false;
                ReadyOrderd = false;
                GameObject UI1Pos = GameObject.Find("Selling Set");
                UI1MovePos = UI1Pos.transform.position;
                FirstMet = false;
                StartCoroutine("UI1Move");
            }
            else
            {
                StopCoroutine("UI1Move");
                isReady.isReadySell = false;
                ReadyOrderd = false;
                GameObject UI1Pos = GameObject.Find("Selling Set");
                UI1MovePos = new Vector3(-7.9f, 8.5f, -11.0f);
                StartCoroutine("UI1Move");
            }
        }

        else if (collision.gameObject.name == "Potion Desk")
        {
            StopCoroutine("UI2Move");
            GameObject UI2Pos = GameObject.Find("Making Set");
            UI2MovePos = UI2Pos.transform.position;
            //InUi2 = false;
            UI2MovePos.y += 10.5f;
             StartCoroutine("UI2Move");
        }
        else if (collision.gameObject.name == "Gama")
        {
            StopCoroutine("UI3Move");
            FindPotionCase.StopCoroutine("FindCase");
            InUi3 = false;
            GameObject UI3Pos = GameObject.Find("Alchemy Set");
            UI3MovePos = new Vector3(-10.6f, 10.7f, -10.0f);
            StartCoroutine("UI3Move");
      
        }

        else if (collision.gameObject.name == "Gama2")
        {
            StopCoroutine("UI4Move");
            GameObject useSkill = GameObject.Find("SkilllManager");
            PushSkillButton = useSkill.GetComponent<SkillSetManager>();

            useButton = PushSkillButton.useSkillButton;

            GameObject UI4Pos = GameObject.Find("Skill Set");
            UI4MovePos = UI4Pos.transform.position;

            if (useButton == true)
            {
                UI4MovePos.y -= 8f;
                PushSkillButton.useSkillButton = false;
            }

            StartCoroutine("UI4Move");
        }

        else if (collision.gameObject.name == "MarchentChar")
        {
            isFurniture = false;
            StopCoroutine("UI5Move");

            GameObject UI5Pos = GameObject.Find("Furniture Set");
            if (!FurnitureSet.PushedBuyButton) UI5MovePos = new Vector3(-0.2f, 9.8f, -10.0f);
            else UI5MovePos = new Vector3(6.3f, 9.8f, -10.0f);
            StartCoroutine("UI5Move");

        }

        else if (collision.gameObject.name == "SkillMaster")
        {
            StopCoroutine("UI6Move");
            GameObject UI6Pos = GameObject.Find("SkillUp Set");
            UI6MovePos = new Vector3(-5.7f, 9.0f, -10.0f);
            StartCoroutine("UI6Move");
        }
    }
}