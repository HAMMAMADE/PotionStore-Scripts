using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using LitJson;
using System.Text;
using System.IO;

//인트로 카툰및 메뉴화면에서 작동하는 스크립트 
public class ButtonManager : MonoBehaviour {

    public Animator BrokenEffect;
    public Animator clearEffect;
    Vector3 NextCartoonPos;
    Vector3 NextEndCartoonPos;
    public Collider NextButtonCollider;
    public Collider NextEndButtonCollider;

    public GameObject RealEndButton;

    public GameObject LoadButton;
    public UIButton LoadbuttonScript;
    public BoxCollider Loadcollider;
    public UILabel DayLabel;
    //----------------------------------------
    public DataVo dataVo;
    string fileName = "/resources.json";
    bool isExistFile;
    //----------------------------------------
    Vector3 NextOver1toonPos;
    public Animator Over1Effect;
    public TypewriterEffect Over1Type0;
    public TypewriterEffect Over1Type1;
    public TypewriterEffect Over1Type2;
    public TypewriterEffect Over1Type3;
    //----------------------------------------
    Vector3 NextOver2toonPos;
    public Animator Over2Effect;
    public TypewriterEffect Over2Type0;
    public TypewriterEffect Over2Type1;
    public TypewriterEffect Over2Type2;
    public TypewriterEffect Over2Type3;
    public TypewriterEffect Over2Type4;
    //----------------------------------------
    Vector3 NextOver3toonPos;
    public Animator Over3Effect;
    public TypewriterEffect Over3Type0;
    public TypewriterEffect Over3Type1;
    public TypewriterEffect Over3Type2;
    public TypewriterEffect Over3Type3;
    //----------------------------------------
    public TypewriterEffect OpType1;
    public TypewriterEffect OpType2;
    public TypewriterEffect OpType3;
    public TypewriterEffect OpType4;
    public TypewriterEffect OpType5;
    public TypewriterEffect OpType6;
    //----------------------------------------
    public GameObject TextLabel4;
    public GameObject TextLabel5;
    public GameObject TextLabel6;
    public Animator Ending1;
    public Animator Ending2;
    public Animator Ending3;
    //public UILabel Text4;
    public UILabel Text5;
    public UILabel Text6;
    public TypewriterEffect Type1;
    public TypewriterEffect Type2;
    public TypewriterEffect Type3;
    public TypewriterEffect Type4;
    public TypewriterEffect Type5;
    public TypewriterEffect Type6;

    bool loading;
    bool firstSlide;
    bool firstGame;
    int inputDay;
    int ClickOpenCount;
    int ClickCount;

   public static int takedNum;
    private void Start()
    {
        firstSlide = true;
        StartCoroutine("BackButtonClick");
        Application.targetFrameRate = 55;
       // Screen.SetResolution(Screen.width, Screen.width * 9 / 16, true);
        
        switch (takedNum)
        {
            case 1:
                NextEndButtonClick();
                break;
            case 2:
                GameOver1NextButtonClick();
                break;
            case 3:
                GameOver2NextButtonClick();
                break;
            case 4:
                GameOver3NextButtonClick();
                break;
            default:
                break;
        }
    }

    public void ButtonStart()
    {
        if(File.Exists(Application.dataPath.Replace("/Assets", "") + fileName)){

            StreamReader streamReader = new StreamReader(Application.dataPath.Replace("/Assets", "") + fileName);
            dataVo = JsonMapper.ToObject<DataVo>(streamReader.ReadToEnd());
            streamReader.Close();

            inputDay = int.Parse(dataVo.SaveDay) + 1;
            firstGame = bool.Parse(dataVo.isFirst);
        }

       if(File.Exists(Application.dataPath.Replace("/Assets", "") + fileName)){
            if (!firstGame)
            {
                Loadcollider.enabled = true;
                LoadbuttonScript.state = UIButtonColor.State.Normal;
                DayLabel.text = inputDay.ToString() + "일 차";
            }
            else
            {
                Loadcollider.enabled = false;
                LoadbuttonScript.state = UIButtonColor.State.Disabled;
                DayLabel.text = "";
            }
        }
        else
        {
            Loadcollider.enabled = false;
            LoadbuttonScript.state = UIButtonColor.State.Disabled;
            DayLabel.text = "";
        }
    }
    public void StartButtonClick()
    {
        
        SoundManager.sounds["Click (7)"].Play();
        
        if(File.Exists(Application.dataPath.Replace("/Assets", "") + fileName)) System.IO.File.Delete(Application.dataPath.Replace("/Assets", "") + fileName);

        DataManager.isSaved = false;
        StartCoroutine("ScreenFadeOut");
        StopCoroutine("BackButtonClick");
    }

    IEnumerator ScreenFadeOut()
    {
        UI2DSprite spriteRenderer = GameObject.Find("UIFadeOut").GetComponent<UI2DSprite>();
        Color currentColor = spriteRenderer.color;

        currentColor.a += 0.05f;
        spriteRenderer.color = currentColor;

        SoundManager.sounds["TitleMusic"].volume -= 0.05f;

        if (currentColor.a >= 1)
        {
            if (!loading)
            {
                SceneManager.LoadScene("Intro");
            }
            else
            {
                SceneManager.LoadScene("Main");
            }
            yield break;
        }

        yield return new WaitForSeconds(0.05f);

        StartCoroutine("ScreenFadeOut");
    }

    IEnumerator CartoonScreenFadeOut()
    {
        UI2DSprite spriteRenderer = GameObject.Find("UIFadeOut").GetComponent<UI2DSprite>();
        Color currentColor = spriteRenderer.color;

        if (currentColor.a >= 1)
        {
            SceneManager.LoadScene("Main");
            yield break;
        }

        currentColor.a += 0.05f;
        spriteRenderer.color = currentColor;
        SoundManager.sounds["OpeningMusic"].volume -= 0.05f;
        SoundManager.sounds["Drum Roll (3)"].volume -= 0.05f;
        yield return new WaitForSeconds(0.05f);


        StartCoroutine("CartoonScreenFadeOut");

    }

    public void LoadButtonClick()
    {
        SoundManager.sounds["Click (7)"].Play();
        if (File.Exists(Application.dataPath.Replace("/Assets", "") + fileName))
        {
            StreamReader streamReader = new StreamReader(Application.dataPath.Replace("/Assets", "") + fileName);
            dataVo = JsonMapper.ToObject<DataVo>(streamReader.ReadToEnd());
            streamReader.Close();
            isExistFile = false;
            loading = true;
            StartCoroutine("ScreenFadeOut");
            StopCoroutine("BackButtonClick");
        }
    }
    public void ExitButtonClick()
    {
        Debug.Log("Exit");
        SoundManager.sounds["NoMoney"].Play();
        RealEndButton.SetActive(true);
        //Application.Quit();
    }

    IEnumerator BackButtonClick()
    {
        yield return null;
        if (Input.GetButtonDown("Pause"))
        {
            SoundManager.sounds["NoMoney"].Play();
            RealEndButton.SetActive(true);
            StopCoroutine("BackButtonClick");
            //Application.Quit();
        }
        StartCoroutine("BackButtonClick");
    }

    public void PushNoEnd()//종료버튼에서 아니오 클릭
    {
        SoundManager.sounds["NextCatoonSound"].Play();
        RealEndButton.SetActive(false);
        StartCoroutine("BackButtonClick");
    }

    public void PushOkEnd()// 네 버튼 클릭
    {
        SoundManager.sounds["NextCatoonSound"].Play();
        Application.Quit();
    }


    public void NextCartoonButtonClick()
    {
        ClickOpenCount++;
        GameObject cartoon = GameObject.Find("Cartoon Set");
        SoundManager.sounds["NextCatoonSound"].Play();
        NextButtonCollider.enabled = false;

        NextCartoonPos = cartoon.transform.position;

        NextCartoonPos.y += 10f;
        StartCoroutine("IntroAnim");
        StartCoroutine("CartoonSlide"); 
    }

    IEnumerator CartoonSlide()
    {
        yield return null;

        GameObject cartoon = GameObject.Find("Cartoon Set");
        if (cartoon.transform.position.y >= NextCartoonPos.y - 0.1f)
        {
            cartoon.transform.position = new Vector3(0f,NextCartoonPos.y,0f);
            NextButtonCollider.enabled = true;
            StopCoroutine("CartoonSlide");
            yield break;
        }
        cartoon.transform.position = Vector3.Lerp(cartoon.transform.position,NextCartoonPos,0.1f);
        StartCoroutine("CartoonSlide");
    }

    IEnumerator IntroAnim()
    {
        yield return null;

        GameObject cartoon = GameObject.Find("Cartoon Set");
        switch (ClickOpenCount)
        {
            case 1:
                OpType2.enabled = true;
                break;
            case 2:
                OpType3.enabled = true;
                break;
            case 3:
                OpType4.enabled = true;
                StartAnimation();
                break;
            case 4:
                OpType5.enabled = true;
                break;
            case 5:
                OpType6.enabled = true;
                break;
            case 12:
                StartFadeOut();
                break;
        }
    }

    IEnumerator StartAnim()
    {
        BrokenEffect.SetTrigger("Broken");
        yield return new WaitForSeconds(2f);
    }

    public void StartAnimation()
    {
        SoundManager.sounds["BrokenSound"].Play();
        StartCoroutine("StartAnim");
    }

    public void StartFadeOut()
    {
        SoundManager.sounds["Drum Roll (3)"].Play();
        StartCoroutine("CartoonScreenFadeOut");
    }

    //-----------------엔딩 카툰 슬라이드
    public void NextEndButtonClick()
    {
        ClickCount++;
        GameObject Endcartoon = GameObject.Find("End Cartoon Set");
        if(!firstSlide) SoundManager.sounds["NextCatoonSound"].Play();
        NextButtonCollider.enabled = false;

        NextEndCartoonPos = Endcartoon.transform.position;

        if (ClickCount == 5)
        {
            TextLabel4.SetActive(false);
            TextLabel5.SetActive(true);
            NextEndCartoonPos.y += 0;
        }

        else if (ClickCount == 7)
        {
            NextEndCartoonPos.y += 0;
        }

        else if (ClickCount == 8)
        {
            NextEndCartoonPos.y += 0;
        }

        else { NextEndCartoonPos.y += 9.5f; }

        StartCoroutine("EndCartoonSlide");
    }

    IEnumerator EndCartoonSlide()
    {
        yield return null;
        GameObject Endcartoon = GameObject.Find("End Cartoon Set");

        if (Endcartoon.transform.position.y >= NextEndCartoonPos.y - 0.1f)
        {
            Endcartoon.transform.position = new Vector3(0f, NextEndCartoonPos.y, 0f);
            NextButtonCollider.enabled = true;
            if(ClickCount == 5 || ClickCount == 7 || ClickCount == 8) StartCoroutine("EndAnim");
            StopCoroutine("EndCartoonSlide");
            yield break;
        }
        Endcartoon.transform.position = Vector3.Lerp(Endcartoon.transform.position, NextEndCartoonPos, 0.1f);
        StartCoroutine("EndAnim");
       
        StartCoroutine("EndCartoonSlide");
    }
    IEnumerator EndAnim()
    {
        yield return null;
        switch (ClickCount)
        {
            case 1:
                Type1.enabled = true;
                firstSlide = false;
                break;
            case 2:
                Ending1.SetTrigger("StartCartoon1");
                Type2.enabled = true;
                break;
            case 3:
                Ending2.SetTrigger("StartCartoon2");
                Type3.enabled = true;
                break;
            case 4:
                Type4.enabled = true;
                break;
            case 5:
                Text5.text = "[앞으로는 얼굴 볼일 없게 합시다잉]";
                Type5.enabled = true;
                Type5.ResetToBeginning();
                break;
            case 7:
                Text6.text = "라임은 빚을 모두 갚았습니다!...fin";
                Type6.enabled = true;
                Type6.ResetToBeginning();
                Ending3.SetTrigger("StartCartoon3");

                break;
            case 8:
                takedNum = 0;
                StartCoroutine("EndScreenFadeOut");
                break;
        }
    }

    IEnumerator EndScreenFadeOut()
    {
        UI2DSprite spriteRenderer = GameObject.Find("UIFadeOut").GetComponent<UI2DSprite>();
        Color currentColor = spriteRenderer.color;

        if (currentColor.a >= 1)
        {
            ClickCount = 0;
            SceneManager.LoadScene("Credit");
            yield break;
        }

        currentColor.a += 0.05f;
        spriteRenderer.color = currentColor;
        yield return new WaitForSeconds(0.1f);


        StartCoroutine("EndScreenFadeOut");

    }
    //------게임오버1 카툰슬라이드----------------------

    public void GameOver1NextButtonClick()
    {
        ClickCount++;
        GameObject GameOver1toon = GameObject.Find("Over Cartoon Set");
        if (!firstSlide) SoundManager.sounds["NextCatoonSound"].Play();
        NextButtonCollider.enabled = false;

        NextOver1toonPos = GameOver1toon.transform.position;

        if (ClickCount < 6)
        {
            NextOver1toonPos.y += 9.75f;
            StartCoroutine("Over1Slide");

        }
        else
        {
            NextOver1toonPos.y += 0f;
            StartCoroutine("Over1Anim");
        }
    }

    IEnumerator Over1Slide()
    {
        yield return null;

        GameObject Over1cartoon = GameObject.Find("Over Cartoon Set");

        if (Over1cartoon.transform.position.y >= NextOver1toonPos.y - 0.1f)
        {
            Over1cartoon.transform.position = new Vector3(0f, NextOver1toonPos.y, 0f);
            NextButtonCollider.enabled = true;
            
            StopCoroutine("Over1Slide");
            yield break;
        }
        Over1cartoon.transform.position = Vector3.Lerp(Over1cartoon.transform.position, NextOver1toonPos, 0.1f);
        StartCoroutine("Over1Anim");
        StartCoroutine("Over1Slide");
    }
    
    IEnumerator Over1Anim()
    {
        yield return null;
        switch (ClickCount)
        {
            case 1:
                firstSlide = false;
                Over1Type0.enabled = true;
                break;
            case 2:
                Over1Type1.enabled = true;
                break;
            case 3:
                Over1Type2.enabled = true;
                break;
            case 4:
                Over1Type3.enabled = true;
                break;
            case 6:
                Over1Effect.SetTrigger("NextButtonClick");
                takedNum = 0;
                yield return new WaitForSeconds(2.5f);
                StartCoroutine("EndScreenFadeOut");
                break;
        }
    }

    //--------게임오버2 카툰슬라이드--------------------------

    public void GameOver2NextButtonClick()
    {
        ClickCount++;
        GameObject GameOver2toon = GameObject.Find("Over Cartoon2 Set");
       if(!firstSlide) SoundManager.sounds["NextCatoonSound"].Play();
        NextButtonCollider.enabled = false;

        NextOver2toonPos = GameOver2toon.transform.position;

        if (ClickCount < 7)
        {
            NextOver2toonPos.y += 9.75f;
            StartCoroutine("Over2Slide");

        }
        else
        {
            NextOver2toonPos.y += 0f;
            StartCoroutine("Over2Anim");
        }
    }

    IEnumerator Over2Slide()
    {
        yield return null;

        GameObject Over2cartoon = GameObject.Find("Over Cartoon2 Set");

        if (Over2cartoon.transform.position.y >= NextOver2toonPos.y - 0.1f)
        {
            Over2cartoon.transform.position = new Vector3(0f, NextOver2toonPos.y, 0f);
            NextButtonCollider.enabled = true;

            StopCoroutine("Over2Slide");
            yield break;
        }
        Over2cartoon.transform.position = Vector3.Lerp(Over2cartoon.transform.position, NextOver2toonPos, 0.1f);
        StartCoroutine("Over2Anim");
        StartCoroutine("Over2Slide");
    }
    IEnumerator Over2Anim()
    {
        yield return null;
        switch (ClickCount)
        {
            case 1:
                firstSlide = false;
                Over2Type0.enabled = true;
                break;
            case 2:
                Over2Type1.enabled = true;
                break;
            case 3:
                Over2Type2.enabled = true;
                break;
            case 4:
                Over2Type3.enabled = true;
                break;
            case 5:
                Over2Type4.enabled = true;
                break;
            case 7:
                Over2Effect.SetTrigger("NextButtonClick");
                takedNum = 0;
                yield return new WaitForSeconds(2.5f);
                StartCoroutine("EndScreenFadeOut");
                break;
        }
    }
    //-----------게임오버3 카툰슬라이드-------------------------
    public void GameOver3NextButtonClick()
    {
        ClickCount++;
        GameObject GameOver3toon = GameObject.Find("Over Cartoon3 Set");
        if(!firstSlide) SoundManager.sounds["NextCatoonSound"].Play();
        NextButtonCollider.enabled = false;

        NextOver3toonPos = GameOver3toon.transform.position;

        if (ClickCount < 6)
        {
            NextOver3toonPos.y += 9.75f;
            StartCoroutine("Over3Slide");

        }
        else
        {
            NextOver3toonPos.y += 0f;
            StartCoroutine("Over3Anim");
        }
    }

    IEnumerator Over3Slide()
    {
        yield return null;

        GameObject Over3cartoon = GameObject.Find("Over Cartoon3 Set");

        if (Over3cartoon.transform.position.y >= NextOver3toonPos.y - 0.1f)
        {
            Over3cartoon.transform.position = new Vector3(0f, NextOver3toonPos.y, 0f);
            NextButtonCollider.enabled = true;

            StopCoroutine("Over3Slide");
            yield break;
        }
        Over3cartoon.transform.position = Vector3.Lerp(Over3cartoon.transform.position, NextOver3toonPos, 0.1f);
        StartCoroutine("Over3Anim");
        StartCoroutine("Over3Slide");
    }

    IEnumerator Over3Anim()
    {
        yield return null;
        switch (ClickCount)
        {
            case 1:
                firstSlide = false;
                Over3Type0.enabled = true;
                break;
            case 2:
                Over3Type1.enabled = true;
                break;
            case 3:
                Over3Type2.enabled = true;
                break;
            case 4:
                Over3Type3.enabled = true;
                break;
            case 6:
                Over3Effect.SetTrigger("NextButtonClick");
                takedNum = 0;
                yield return new WaitForSeconds(2.5f);
                StartCoroutine("EndScreenFadeOut");
                break;
        }
    }
}


