using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour//물약제조기에서 작동하는 스크립트
{
    FurnitureManager Fur1;

    public GameObject MaterialWater;

    public GameObject MaterialQSilver;

    public GameObject MaterialGas;

    public static int MaterialWaterNumber = 0;

    public static int MaterialQSilverNumber = 0;

    public static int MaterialGasNumber = 0;

    //public FlaskManager flaskManager;

    public bool ReadyToWater;
    public float counter = 0;

   // public int Outwater = 0, OutSilver = 0, OutGas = 0;


    // Use this for initialization
  /*  void Start()
    {
        Fur1 = GameObject.Find("FurnitureManager").GetComponent<FurnitureManager>();
        StartCoroutine("OKWaterButton");
        StartCoroutine("ReadyFlaskButton");
        MaterialButtonUpdate();
    }*/

    public void DataAccess(DataVo dataVo)
    {
        MaterialWaterNumber = int.Parse(dataVo.SaveBlue);
        MaterialQSilverNumber = int.Parse(dataVo.SaveRed);
        MaterialGasNumber = int.Parse(dataVo.SaveGas);
        Fur1 = GameObject.Find("FurnitureManager").GetComponent<FurnitureManager>();
        StartCoroutine("OKWaterButton");
        StartCoroutine("ReadyFlaskButton");
        MaterialButtonUpdate();
    }
    

    public void MaterialButtonUpdate()//원료 버튼상에서 획득한 원료 수를 체크하는 함수
    {
        GameObject Waterbutton = GameObject.Find("Beaker Button_Water");
        GameObject QSilverbutton = GameObject.Find("Beaker Button_Qsilver");
        GameObject Gasbutton = GameObject.Find("Beaker Button_Gas");

        UIButton WaterbuttonScript = Waterbutton.GetComponent<UIButton>();
        UIButton QSilverbuttonScript = QSilverbutton.GetComponent<UIButton>();
        UIButton GasbuttonScript = Gasbutton.GetComponent<UIButton>();

        BoxCollider Watercollider = Waterbutton.GetComponent<BoxCollider>();
        BoxCollider QSilvercollider = QSilverbutton.GetComponent<BoxCollider>();
        BoxCollider Gascollider = Gasbutton.GetComponent<BoxCollider>();


        UILabel WaterLable = GameObject.Find("Water_Num").GetComponent<UILabel>();
        UILabel QSilverLable = GameObject.Find("QuickSilver_Num").GetComponent<UILabel>();
        UILabel GasLable = GameObject.Find("Gas_Num").GetComponent<UILabel>();

        WaterLable.text = string.Format("{0:n0}", MaterialWaterNumber);//화폐단위처럼 출력 1,000,000
        QSilverLable.text = string.Format("{0:n0}", MaterialQSilverNumber);
        GasLable.text = string.Format("{0:n0}", MaterialGasNumber);


        Watercollider.enabled = MaterialWaterNumber > 0;
        WaterbuttonScript.state = (MaterialWaterNumber > 0) ? UIButtonColor.State.Normal : UIButtonColor.State.Disabled;

        QSilvercollider.enabled = MaterialQSilverNumber > 0;
        QSilverbuttonScript.state = (MaterialQSilverNumber > 0) ? UIButtonColor.State.Normal : UIButtonColor.State.Disabled;

        Gascollider.enabled = MaterialGasNumber > 0;
        GasbuttonScript.state = (MaterialGasNumber > 0) ? UIButtonColor.State.Normal : UIButtonColor.State.Disabled;

        
    }


    public void PushWaterButton()
    {
        ReadyToWater = false;

        Transform WaterButton = GameObject.Find("MaterialButton_Water").transform;

        Vector3 nextPos = WaterButton.position;
        nextPos.y -= 6f;
        WaterButton.position = nextPos;
        SoundManager.sounds["Potion Drink (2)"].Play();

        StartCoroutine("OKWaterButton");//물 풍선 재생성 코루틴

        switch (Fur1.Furniture1Level) {
            case 0:
                MaterialWaterNumber += 5;
                break;
            case 1:
                MaterialWaterNumber += 10;
                MaterialQSilverNumber += 1;
                MaterialGasNumber += 1;
                break;
            case 2:
                MaterialWaterNumber += 15;
                MaterialQSilverNumber += 5;
                MaterialGasNumber += 5;
                break;
            case 3:
                MaterialWaterNumber += 10;
                MaterialQSilverNumber += 10;
                MaterialGasNumber += 10;
                break;
        }

        MaterialButtonUpdate();

    }

    IEnumerator OKWaterButton()
    {
        float SpawnWaterTime;

        SpawnWaterTime = 14f /(Fur1.Furniture1Level + 1);
        yield return new WaitForSeconds(SpawnWaterTime);
        if (ReadyToWater != true)
        {
            Transform WaterButton = GameObject.Find("MaterialButton_Water").transform;
            Vector3 nextPos = WaterButton.position;
            nextPos.y += 6f;
            WaterButton.position = nextPos;
            ReadyToWater = true;
            StopCoroutine("OKWaterButton");
        }

    }

    IEnumerator ReadyFlaskButton()
    {
        yield return new WaitForSeconds(0.5f);
        if (counter >= 3)
        {
            AlchemyManager.pushFlask = true;
        }
        else
        {
            AlchemyManager.pushFlask = false;
        }
        StartCoroutine("ReadyFlaskButton");

    }

    public void DropMaterialWater()
    {
        if (counter < 4)
        {

            StartCoroutine("UIdropMaterialWater");
            MaterialWaterNumber--;
            AlchemyManager.Blue += 1;
            counter++;
            MaterialButtonUpdate();
        }
    }


    public void DropMaterialQSilver()
    {
        if (counter < 4)
        {
            StartCoroutine("UIdropMaterialQSilver");
            MaterialQSilverNumber--;
            AlchemyManager.Red += 1;
            counter++;
            MaterialButtonUpdate();
        }
    }

    public void DropMaterialGas()
    {
        if (counter < 4)
        {
            StartCoroutine("UIdropMaterialGas");
            MaterialGasNumber--;
            AlchemyManager.Gas += 1;
            counter++;
            MaterialButtonUpdate();
        }
    }



    IEnumerator UIdropMaterialWater()
    {
        GameObject[] DropPoints = GameObject.FindGameObjectsWithTag("DropPoint");

        for (int index = 0; index < 25; index++)
        {
            SoundManager.sounds["Pop (6)"].Play();
            yield return new WaitForSeconds(0.02f);
            Transform DropPoint = DropPoints[Random.Range(0, 2)].transform;
            Instantiate(MaterialWater, DropPoint);
        }
    }

    IEnumerator UIdropMaterialQSilver()
    {
        GameObject[] DropPoints = GameObject.FindGameObjectsWithTag("DropPoint");

        for (int index = 0; index < 25; index++)
        {
            SoundManager.sounds["Pop (3)"].Play();
            yield return new WaitForSeconds(0.02f);
            Transform DropPoint = DropPoints[Random.Range(0, 2)].transform;
            Instantiate(MaterialQSilver, DropPoint);
        }
    }

    IEnumerator UIdropMaterialGas()
    {
        GameObject[] DropPoints = GameObject.FindGameObjectsWithTag("DropPoint");

        for (int index = 0; index < 25; index++)
        {
            SoundManager.sounds["Pop (6)"].Play();
            yield return new WaitForSeconds(0.02f);
            Transform DropPoint = DropPoints[Random.Range(0, 2)].transform;
            Instantiate(MaterialGas, DropPoint);
        }
    }

    public void OutMaterialButtonClick() // 내보내기 버튼 클릭
    {

        if (counter > 0)
        {

            counter = 0;
            GameObject[] UIMaterials = GameObject.FindGameObjectsWithTag("UIMaterial");
            SoundManager.sounds["Click (6)"].Play();

            for (int index = 0; index < UIMaterials.Length; index++)
            {
                Destroy(UIMaterials[index]);
            }
        }
    }
}
