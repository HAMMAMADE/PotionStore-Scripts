using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionParmeter : MonoBehaviour {

    public string PotionName;
    public int PotionCaseNum;
    public int PotionNum;
    public bool isGasMat;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SoundManager.sounds["PotionDrop"].Play();
    }
}
