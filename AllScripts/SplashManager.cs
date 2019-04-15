using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Application.targetFrameRate = 55;
        Screen.SetResolution(Screen.width, Screen.width * 9 / 16, true);
        StartCoroutine("Splashend");
    }

    IEnumerator Splashend()
    {
        yield return new WaitForSeconds(4.2f);
        StartGame();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
