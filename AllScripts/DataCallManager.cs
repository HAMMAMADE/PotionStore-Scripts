using LitJson;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = UnityEngine.JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return UnityEngine.JsonUtility.ToJson(wrapper);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }

}*/

public class DataCallManager : MonoBehaviour {

    public TutorialManager tutorialManager;
    public MaterialListManager materialListManager;
    public HavePotionListManager havePotionListManager;
    public StatusManager statusManager;
    public MarchentManager marchentManager;
    public TimeManager timeManager;
    public InfluenceManager influenceManager;
    public CustomerManager customerManager;
    public FurnitureManager furnitureManager;
    public SkillSetManager skillSetManager;
    public MatInventoryManager matInventoryManager;
    public HavePoInvenManager havePoInvenManager;
    public MaterialManager materialManager;

    public GameObject DropMat;
    public GameObject DropMat2;
    public Transform DropPos;

    public DataVo dataVo;
    string fileName = "/resources.json";
    bool isExistFile;

    public static bool FirstToTItle;
    public static bool StartToTitle;

    public static string[] ChashItemType { get; set; }
    public static int chashItemNum;

    public static string[] ChashPotionType { get; set; }
    public static int chashPotionNum;

    void Start () {
       // Screen.SetResolution(Screen.width, Screen.width * 9/16, true);
        Application.targetFrameRate = 55;
        FirstToTItle = true;
        chashItemNum = 0;
        chashPotionNum = 0;
        ChashItemType = new string[] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };//15개
        ChashPotionType = new string[] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0","0","0","0"};//13개

        if (!File.Exists(Application.persistentDataPath.Replace("/Andriod/data/kr.Hamma.pharmacy/files", "") + fileName))
        {
            dataVo = new DataVo();
            DataLoad();
            isExistFile = true;
        }
        else
        {
            StreamReader streamReader = new StreamReader(Application.persistentDataPath.Replace("/Andriod/data/kr.Hamma.pharmacy/files", "") + fileName);
            dataVo = JsonMapper.ToObject<DataVo>(streamReader.ReadToEnd());
            streamReader.Close();
            DataLoad(dataVo);
            isExistFile = false;

        }
		
	}
	
    public void DataLoad(DataVo data = null)//초기화
    {
        if (data == null)
        {
            StartToTitle = true;
            data = new DataVo();
            data.lookedEnd = "false";
            data.isFirst = "true";
            data.HaveMoney = "10000";
            data.HaveEva = "0";
            data.Diamonds = "2000";

            data.SalingNumSave = "3";

            data.SaveBlue = "0";
            data.SaveRed = "0";
            data.SaveGas = "0";

            data.SaveDay = "0";
            data.SaveInflu = "50";
            data.SpawnCus = "2";

            data.SaveFur1Lev = "0";
            data.SaveFur2Lev = "0";
            data.SaveFur3Lev = "0";

            data.SaveSkillPoint = "10";

            data.SaveSkill1Lev = "0";
            data.SaveSkill2Lev = "0";
            data.SaveSkill3Lev = "0";

            data.SaveSkill1Prof = "0";
            data.SaveSkill2Prof = "0";
            data.SaveSkill3Prof = "0";

            data.SaveSpawnTime = "40";
            data.SaveTuto = "false";
            data.SaveSpawnBoss = "false";

            data.FirstItem1 = "false";
            data.FirstItem2 = "false";
            data.FirstItem3 = "false";
            data.FirstItem4 = "false";
            data.FirstItem5 = "false";
            data.FirstItem6 = "false";
            data.FirstItem7 = "false";
            data.FirstItem8 = "false";
            data.FirstItem9 = "false";
            data.FirstItem10 = "false";
            data.FirstItem11 = "false";
            data.FirstItem12 = "false";
            data.FirstItem13 = "false";
            data.FirstItem14 = "false";
            data.FirstItem15 = "false";

            data.SaveNum = "0";

            data.SaveItem1 = "0";
            data.SaveItem2 = "0";
            data.SaveItem3 = "0";
            data.SaveItem4 = "0";
            data.SaveItem5 = "0";
            data.SaveItem6 = "0";
            data.SaveItem7 = "0";
            data.SaveItem8 = "0";
            data.SaveItem9 = "0";
            data.SaveItem10 = "0";
            data.SaveItem11 = "0";
            data.SaveItem12 = "0";
            data.SaveItem13 = "0";
            data.SaveItem14 = "0";
            data.SaveItem15 = "0";

            data.SavePotionNum = "0";

            data.FirstPotion1 = "false";
            data.FirstPotion2 = "false";
            data.FirstPotion3 = "false";
            data.FirstPotion4 = "false";
            data.FirstPotion5 = "false";
            data.FirstPotion6 = "false";
            data.FirstPotion7 = "false";
            data.FirstPotion8 = "false";
            data.FirstPotion9 = "false";
            data.FirstPotion10 = "false";
            data.FirstPotion11 = "false";
            data.FirstPotion12 = "false";
            data.FirstPotion13 = "false";
            data.FirstPotion14 = "false";
            data.FirstPotion15 = "false";

            data.SavePotion1 = "0";
            data.SavePotion2 = "0";
            data.SavePotion3 = "0";
            data.SavePotion4 = "0";
            data.SavePotion5 = "0";
            data.SavePotion6 = "0";
            data.SavePotion7 = "0";
            data.SavePotion8 = "0";
            data.SavePotion9 = "0";
            data.SavePotion10 = "0";
            data.SavePotion11 = "0";
            data.SavePotion12 = "0";
            data.SavePotion13 = "0";
            data.SavePotion14 = "0";
            data.SavePotion15 = "0";

            data.SaveItems = new string[] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0","0"};

            //---------------------------------------
            data.SavePotions = new string[] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0","0","0","0"};

            Instantiate(DropMat, DropPos);
            Instantiate(DropMat, DropPos);
            Instantiate(DropMat2, DropPos);
            Instantiate(DropMat2, DropPos);
        }
 
        StringBuilder sb = new System.Text.StringBuilder();
        JsonWriter writer = new JsonWriter(sb);
        writer.PrettyPrint = true;

        JsonMapper.ToJson(data, writer);
        JsonData saveData = sb.ToString();

        if (!isExistFile)
        {
            var sr = File.CreateText(Application.persistentDataPath.Replace("/Andriod/data/kr.Hamma.pharmacy/files", "") + fileName);
            sr.WriteLine(saveData.ToString());
            sr.Close();
        }
        else
        {   
            File.WriteAllText(Application.persistentDataPath.Replace("/Andriod/data/kr.Hamma.pharmacy/files", "") + fileName, saveData.ToString());
        }

        //---------데이터 연결(매니저들을 호출하고 매니저의 엑세스 메소드를 부른다.)
        tutorialManager.DataAccess(data);
        matInventoryManager.DataAccess(data);
        havePoInvenManager.DataAccess(data);
        statusManager.DataAccess(data);
        marchentManager.DataAccess(data);
        timeManager.DataAccess(data);
        influenceManager.DataAccess(data);
        customerManager.DataAccess(data);
        furnitureManager.DataAccess(data);
        skillSetManager.DataAccess(data);
        materialManager.DataAccess(data);

    }

    public static void SaveItemType(int typeNum)
    {
        for (int index = 0; index <= 14; index++)
        {
            if (ChashItemType[index] == 0.ToString())
            {
                ChashItemType[index] = typeNum.ToString();
                chashItemNum += 1;
                break;
            }
        }
    }

    public static void SavePotionType(int typeNum)
    {
        for (int index = 0; index <= 14; index++)
        {
            if (ChashPotionType[index] == 0.ToString())
            {
                ChashPotionType[index] = typeNum.ToString();
                chashPotionNum += 1;
                break;
            }
        }
    }

    public void SaveDataWrite()
    {
        dataVo.isFirst = "false";
        dataVo.lookedEnd = timeManager.looktrueEnd.ToString();

        int[] MatArr = (new List<int>(materialListManager.ItemListNum.Values)).ToArray();
        string[] strArr = new string[MatArr.Length];
        for (int index= 0; index < MatArr.Length; index++)
        {
            strArr[index] = MatArr[index].ToString();
        }
        dataVo.SaveItems = strArr;

        dataVo.SaveNum = materialListManager.MatCount.ToString();

        dataVo.FirstItem1 = matInventoryManager.FirstItem1.ToString();
        dataVo.FirstItem2 = matInventoryManager.FirstItem2.ToString();
        dataVo.FirstItem3 = matInventoryManager.FirstItem3.ToString();
        dataVo.FirstItem4 = matInventoryManager.FirstItem4.ToString();
        dataVo.FirstItem5 = matInventoryManager.FirstItem5.ToString();
        dataVo.FirstItem6 = matInventoryManager.FirstItem6.ToString();
        dataVo.FirstItem7 = matInventoryManager.FirstItem7.ToString();
        dataVo.FirstItem8 = matInventoryManager.FirstItem8.ToString();
        dataVo.FirstItem9 = matInventoryManager.FirstItem9.ToString();
        dataVo.FirstItem10 = matInventoryManager.FirstItem10.ToString();
        dataVo.FirstItem11 = matInventoryManager.FirstItem11.ToString();
        dataVo.FirstItem12 = matInventoryManager.FirstItem12.ToString();
        dataVo.FirstItem13 = matInventoryManager.FirstItem13.ToString();
        dataVo.FirstItem14 = matInventoryManager.FirstItem14.ToString();
        dataVo.FirstItem15 = matInventoryManager.FirstItem15.ToString();

        dataVo.SaveItem1 = matInventoryManager.haveHarb.ToString();
        dataVo.SaveItem2 = matInventoryManager.haveTomato.ToString();
        dataVo.SaveItem3 = matInventoryManager.haveItem3.ToString();
        dataVo.SaveItem4 = matInventoryManager.haveItem4.ToString();
        dataVo.SaveItem5 = matInventoryManager.haveItem5.ToString();
        dataVo.SaveItem6 = matInventoryManager.haveItem6.ToString();
        dataVo.SaveItem7 = matInventoryManager.haveItem7.ToString();
        dataVo.SaveItem8 = matInventoryManager.haveItem8.ToString();
        dataVo.SaveItem9 = matInventoryManager.haveItem9.ToString();
        dataVo.SaveItem10 = matInventoryManager.haveItem10.ToString();
        dataVo.SaveItem11 = matInventoryManager.haveItem11.ToString();
        dataVo.SaveItem12 = matInventoryManager.haveItem12.ToString();
        dataVo.SaveItem13 = matInventoryManager.haveItem13.ToString();
        dataVo.SaveItem14 = matInventoryManager.haveItem14.ToString();
        dataVo.SaveItem15 = matInventoryManager.haveItem15.ToString();



        int[] PoArr = (new List<int>(havePotionListManager.PotionListNum.Values)).ToArray();
        string[] postrArr = new string[PoArr.Length];
        for (int index = 0; index < PoArr.Length; index++)
        {
            postrArr[index] = PoArr[index].ToString();
        }

        dataVo.SavePotions = postrArr;

        dataVo.SavePotionNum = havePotionListManager.PoCount.ToString();

        dataVo.FirstPotion1 = havePoInvenManager.FirstPotion1.ToString();
        dataVo.FirstPotion2 = havePoInvenManager.FirstPotion2.ToString();
        dataVo.FirstPotion3 = havePoInvenManager.FirstPotion3.ToString();
        dataVo.FirstPotion4 = havePoInvenManager.FirstPotion4.ToString();
        dataVo.FirstPotion5 = havePoInvenManager.FirstPotion5.ToString();
        dataVo.FirstPotion6 = havePoInvenManager.FirstPotion6.ToString();
        dataVo.FirstPotion7 = havePoInvenManager.FirstPotion7.ToString();
        dataVo.FirstPotion8 = havePoInvenManager.FirstPotion8.ToString();
        dataVo.FirstPotion9 = havePoInvenManager.FirstPotion9.ToString();
        dataVo.FirstPotion10 = havePoInvenManager.FirstPotion10.ToString();
        dataVo.FirstPotion11 = havePoInvenManager.FirstPotion11.ToString();
        dataVo.FirstPotion12 = havePoInvenManager.FirstPotion12.ToString();
        dataVo.FirstPotion13 = havePoInvenManager.FirstPotion13.ToString();
        dataVo.FirstPotion14 = havePoInvenManager.FirstPotion14.ToString();
        dataVo.FirstPotion15 = havePoInvenManager.FirstPotion15.ToString();

        dataVo.SavePotion1 = havePoInvenManager.Potion1Num.ToString();
        dataVo.SavePotion2 = havePoInvenManager.Potion2Num.ToString();
        dataVo.SavePotion3 = havePoInvenManager.Potion3Num.ToString();
        dataVo.SavePotion4 = havePoInvenManager.Potion4Num.ToString();
        dataVo.SavePotion5 = havePoInvenManager.Potion5Num.ToString();
        dataVo.SavePotion6 = havePoInvenManager.Potion6Num.ToString();
        dataVo.SavePotion7 = havePoInvenManager.Potion7Num.ToString();
        dataVo.SavePotion8 = havePoInvenManager.Potion8Num.ToString();
        dataVo.SavePotion9 = havePoInvenManager.Potion9Num.ToString();
        dataVo.SavePotion10 = havePoInvenManager.Potion10Num.ToString();
        dataVo.SavePotion11 = havePoInvenManager.Potion11Num.ToString();
        dataVo.SavePotion12 = havePoInvenManager.Potion12Num.ToString();
        dataVo.SavePotion13 = havePoInvenManager.Potion13Num.ToString();
        dataVo.SavePotion14 = havePoInvenManager.Potion14Num.ToString();
        dataVo.SavePotion15 = havePoInvenManager.Potion15Num.ToString();

        dataVo.SaveSpawnTime = CustomerManager.SpawnTerm.ToString();
        dataVo.SaveTuto = TutorialManager.isTutorial.ToString();
        dataVo.SaveSpawnBoss = customerManager.BossSpawn.ToString();

        dataVo.SaveSkillPoint = skillSetManager.SkillPoints.ToString();

        dataVo.SaveSkill1Lev = skillSetManager.Skill1Level.ToString();
        dataVo.SaveSkill2Lev = skillSetManager.Skill2Level.ToString();
        dataVo.SaveSkill3Lev = skillSetManager.Skill3Level.ToString();
        dataVo.SaveSkill1Prof = skillSetManager.Proficiency1.ToString();
        dataVo.SaveSkill2Prof = skillSetManager.Proficiency2.ToString();
        dataVo.SaveSkill3Prof = skillSetManager.Proficiency3.ToString();
        dataVo.SaveFur1Lev = furnitureManager.Furniture1Level.ToString();
        dataVo.SaveFur2Lev = furnitureManager.Furniture2Level.ToString();
        dataVo.SaveFur3Lev = furnitureManager.Furniture3Level.ToString();

        dataVo.SaveDay = TimeManager.Day.ToString();
        dataVo.SaveInflu = influenceManager.Influence.ToString();
        dataVo.SpawnCus = customerManager.Length.ToString();

        dataVo.SaveBlue = MaterialManager.MaterialWaterNumber.ToString();
        dataVo.SaveRed = MaterialManager.MaterialQSilverNumber.ToString();
        dataVo.SaveGas = MaterialManager.MaterialGasNumber.ToString();

        dataVo.HaveMoney = StatusManager.Point.ToString();
        dataVo.HaveEva = StatusManager.EvaPoint.ToString();
        dataVo.Diamonds = StatusManager.Diamonds.ToString();

        dataVo.SalingNumSave = marchentManager.SaleingListIndex.ToString();

        //매니저들로부터 데이터 회수---------------------------

        StringBuilder sb = new System.Text.StringBuilder();
        JsonWriter writer = new JsonWriter(sb);
        writer.PrettyPrint = true;

        JsonMapper.ToJson(dataVo, writer);
        JsonData saveData = sb.ToString();

        File.WriteAllText(Application.persistentDataPath.Replace("/Andriod/data/kr.Hamma.pharmacy/files", "") + fileName, saveData.ToString());
    }
}
