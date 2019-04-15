using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour {

    Animator NPCanimator;
    public bool EndCleaning;
    public bool Turn;
    bool MovingStart;

    void Awake()
    {
        NPCanimator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!Turn)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            Turn = true;
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            Turn = false;
        }
    }

    void Update()
    {
       StartCoroutine("NPCMoveStart");
    }
    IEnumerator NPCMoveStart()
    {
        yield return null;
        
        Vector3 eulerAngles = transform.localEulerAngles;

        if (TutorialManager.isTutorial && !MovingStart)
        {
            NPCanimator.SetBool("TutorialEnd",true);
            MovingStart = true;
        }
        else if(EndCleaning && MovingStart)
        {
            if (Turn)
            {
                Vector3 nextPos = transform.position;
                nextPos.x -= 0.8f * Time.deltaTime;
                transform.position = nextPos;
                StartCoroutine("Moving");
            }
            else
            {
                Vector3 nextPos = transform.position;
                nextPos.x += 0.8f * Time.deltaTime;
                transform.position = nextPos;
                StartCoroutine("Moving");
            }
        }
        else if(!EndCleaning && MovingStart)
        {
            StartCoroutine("Cleaning");
        }
    }

    IEnumerator Moving()
    {
        yield return new WaitForSeconds(8f);

        NPCanimator.SetBool("CleanEnd", false);

        EndCleaning = false;
    }

    IEnumerator Cleaning()
    {
        yield return new WaitForSeconds(8f);

        NPCanimator.SetBool("CleanEnd", true);

        EndCleaning = true;
    }
}

