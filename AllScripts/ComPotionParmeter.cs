using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComPotionParmeter : MonoBehaviour {
    public static int[] PotionType = new int[3] { 0, 0, 0 };
    public int amount;

    public static void InType(int num)
    {
        for(int index = 0; index <= 2; index++)
        {
            if(PotionType[index] == 0)
            {
                PotionType[index] = num;
                break;
            }
        }
    }
    public static void Reset()
    {
        PotionType = new int[3] { 0, 0, 0 };
    }
}
