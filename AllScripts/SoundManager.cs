using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static AudioSource[] audioSource;
    public static Dictionary<string, AudioSource> sounds;


    private void Awake()
    {
        audioSource = GetComponents<AudioSource>();
        sounds = new Dictionary<string, AudioSource>();

        sounds.Add("Click (7)", audioSource[0]);
        sounds.Add("TitleMusic", audioSource[1]);
        sounds.Add("Drum Roll (3)", audioSource[2]);
        sounds.Add("CartoonBGM", audioSource[3]);
        sounds.Add("Click (6)", audioSource[4]);
        sounds.Add("Pop (6)", audioSource[5]);
        sounds.Add("Pop (3)", audioSource[6]);
        sounds.Add("Potion Drink (2)", audioSource[7]);
        sounds.Add("DoAlchemy", audioSource[8]);
        sounds.Add("NotItem", audioSource[9]);
        sounds.Add("BookOpen", audioSource[10]);
        sounds.Add("UseFlask", audioSource[11]);
        sounds.Add("UseMaterial", audioSource[12]);
        sounds.Add("PotionDrop", audioSource[13]);
        sounds.Add("PotionSell", audioSource[14]);
        sounds.Add("BuyFromMarchent", audioSource[15]);
        sounds.Add("NoMoney", audioSource[16]);
        sounds.Add("SkillNextPage", audioSource[17]);
        sounds.Add("CantuseSkill", audioSource[18]);
        sounds.Add("Alert", audioSource[19]);
        sounds.Add("BackGroundMusic", audioSource[20]);
        sounds.Add("PaperNext", audioSource[21]);
        sounds.Add("NextCatoonSound", audioSource[22]);
        sounds.Add("BrokenSound", audioSource[23]);
        sounds.Add("MagicSound", audioSource[24]);
        sounds.Add("MagicComplete", audioSource[25]);
        sounds.Add("PurchaseFurniture", audioSource[26]);
        sounds.Add("CantBuyFurniture", audioSource[27]);
        sounds.Add("NightMusic", audioSource[28]);
        sounds.Add("OpeningMusic", audioSource[29]);

    }
}

//SoundManager.sounds["Alert"].Play();
