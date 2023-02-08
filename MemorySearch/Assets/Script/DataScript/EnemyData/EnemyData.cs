using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敵の種類　EnemyDataBaseの行番号を格納している
public enum EnemyType : int
{
    Flog    = 2,
    Crab    = 5,
    Cow     = 8,
    Gorilla = 11,
    Fox     = 14,
}

public class EnemyDataBase
{
    public float dropBattery;
    public ParameterForChara baseParam;
    public float moveSpeed;
    public enum ParamRowBase
    {
        Drop_Battery,
        HP,
        Count,
    }

    public EnemyDataBase()
    {
        baseParam = new ParameterForChara();
    }
}

public class FlogData : EnemyDataBase
{
    public float jumpPower;
    public float attackInterval;

    public enum ParamRow
    {
        Defence = EnemyDataBase.ParamRowBase.Count,
        Attack_Power,
        Move_Speed,
        Jump_Power,
        Attack_Interval,
    }
}
public class CowData : EnemyDataBase
{
}
public class CrabData : EnemyDataBase
{
}
public class GorillaData : EnemyDataBase
{
}
public class FoxData : EnemyDataBase
{
}

[System.Serializable]
public class EnemyData : IEnemyData
{
    /*******************************
    * private
    *******************************/
    FlogData flogData;
    CowData cowData;
    CrabData crabData;
    GorillaData gorillaData;
    FoxData foxData;

    /*******************************
    * public 
    *******************************/
    public EnemyData()
    {
        flogData    = new FlogData();
        cowData     = new CowData();
        crabData    = new CrabData();
        gorillaData = new GorillaData();
        foxData     = new FoxData();
    }

    //データの読み込み
    public void Load(TextAsset memoryDataTxt)
    {
        //データをバッファに読み込み
        List<string[]> dataBuffer = MyUtil.TextUtility.ReadCSVData(memoryDataTxt);

        //読み込んだデータを設定
        //Flog
        int enemyType = (int)EnemyType.Flog;
        flogData.dropBattery            = float.Parse(dataBuffer[enemyType][(int)FlogData.ParamRowBase.Drop_Battery]);
        flogData.baseParam.hp           = int.Parse(dataBuffer[enemyType][(int)FlogData.ParamRowBase.HP]);
        flogData.baseParam.defencePower = int.Parse(dataBuffer[enemyType][(int)FlogData.ParamRow.Defence]);
        flogData.moveSpeed              = float.Parse(dataBuffer[enemyType][(int)FlogData.ParamRow.Move_Speed]);
        flogData.jumpPower              = float.Parse(dataBuffer[enemyType][(int)FlogData.ParamRow.Jump_Power]);
        flogData.attackInterval         = float.Parse(dataBuffer[enemyType][(int)FlogData.ParamRow.Attack_Interval]);
        //Cow
        enemyType = (int)EnemyType.Cow;
        cowData.dropBattery = float.Parse(dataBuffer[enemyType][(int)CowData.ParamRowBase.Drop_Battery]);
        cowData.baseParam.hp = int.Parse(dataBuffer[enemyType][(int)CowData.ParamRowBase.HP]);
        //Crab
        enemyType = (int)EnemyType.Crab;
        crabData.dropBattery = float.Parse(dataBuffer[enemyType][(int)CrabData.ParamRowBase.Drop_Battery]);
        crabData.baseParam.hp = int.Parse(dataBuffer[enemyType][(int)CrabData.ParamRowBase.HP]);
        //Gorilla
        enemyType = (int)EnemyType.Gorilla;
        gorillaData.dropBattery = float.Parse(dataBuffer[enemyType][(int)GorillaData.ParamRowBase.Drop_Battery]);
        gorillaData.baseParam.hp = int.Parse(dataBuffer[enemyType][(int)GorillaData.ParamRowBase.HP]);
        //Fox
        enemyType = (int)EnemyType.Fox;
        foxData.dropBattery = float.Parse(dataBuffer[enemyType][(int)FoxData.ParamRowBase.Drop_Battery]);
        foxData.baseParam.hp = int.Parse(dataBuffer[enemyType][(int)FoxData.ParamRowBase.HP]);
    }

    public FlogData GetFlogData()
    {
        return flogData;
    }
    public CowData GetCowData()
    {
        return cowData;
    }
    public CrabData GetCrabData()
    {
        return crabData;
    }
    public GorillaData GetGorillaData()
    {
        return gorillaData;
    }
    public FoxData GetFoxData()
    {
        return foxData;
    }
}
