using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour {

    public Image ProgressBar;
    // Use this for initialization
    //public SpriteRenderer LoadingImage; 
	void Start () {
        Screen.SetResolution(Screen.width, Screen.width * 9 / 16, true);
        Application.targetFrameRate = 55;
        StartCoroutine(LoadScene());
	}
	


    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation oper = SceneManager.LoadSceneAsync("Splash");

        oper.allowSceneActivation = false;

        float timer = 0.0f;
        Debug.Log(oper.isDone);
        while (!oper.isDone)
        {
            yield return null;

            timer += Time.deltaTime;

            if(oper.progress >= 0.9f)
            {
                ProgressBar.fillAmount = Mathf.Lerp(ProgressBar.fillAmount, 1f, timer);
                
                if (ProgressBar.fillAmount == 1.0f)
                {
                    oper.allowSceneActivation = true;
                    Debug.Log(oper.isDone);
                }
            }
            else
            {
                ProgressBar.fillAmount = Mathf.Lerp(ProgressBar.fillAmount, oper.progress, timer);

                if(ProgressBar.fillAmount >= oper.progress)
                {
                    timer = 0f;
                }
            }
        }
    }
}
