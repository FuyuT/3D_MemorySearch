using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敵の種類　EnemyDataBaseの行番号を格納している
enum EnemyType : int
{
    Flog    = 3,
    Crab    = 6,
    Cow     = 9,
    Gorilla = 12,
}

public class EnemyDataBase
{
    public float DropBattery;
    public ParameterForChara baseParam;
    public float moveSpeed;
}

public class FlogData : EnemyDataBase
{
    public float jumpPower;
    public float attackInterval;

    //パラメータの並び
    public enum ParamRow
    {
        Drop_Battery,
        HP,
        Defence,
        Attack_Power,
        Move_Speed,
        Jump_Power,
        Attack_Interval,
    }
}

[System.Serializable]
public class EnemyData
{
    /*******************************
    * private
    *******************************/
    FlogData flogData;

    /*******************************
    * public 
    *******************************/
    public EnemyData()
    {
        flogData = new FlogData();
    }

    //データの読み込み
    public void Load(TextAsset memoryDataTxt)
    {
        //データをバッファに読み込み
        List<string[]> dataBuffer = MyUtil.TextUtility.ReadCSVData(memoryDataTxt);

        //読み込んだデータを設定
        int enemyType = (int)EnemyType.Flog;
        flogData.DropBattery            = float.Parse(dataBuffer[enemyType][(int)FlogData.ParamRow.Drop_Battery]);
        flogData.baseParam.hp           = int.Parse(dataBuffer[enemyType][(int)FlogData.ParamRow.HP]);
        flogData.baseParam.defencePower = int.Parse(dataBuffer[enemyType][(int)FlogData.ParamRow.Defence]);
        flogData.moveSpeed              = float.Parse(dataBuffer[enemyType][(int)FlogData.ParamRow.Move_Speed]);
        flogData.jumpPower              = float.Parse(dataBuffer[enemyType][(int)FlogData.ParamRow.Jump_Power]);
        flogData.attackInterval         = float.Parse(dataBuffer[enemyType][(int)FlogData.ParamRow.Attack_Interval]);
    }
}
