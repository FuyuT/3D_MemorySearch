using UnityEngine;
public enum DamageType
{
    Once,
    Continue,
}

[System.Serializable]
public class AttackInfo
{
    /*******************************
    * public
    *******************************/
    public int power;
    public DamageType type;

    public AttackInfo()
    {
        Init();
    }

    public void Init()
    {
        power = 0;
        type = DamageType.Once;
    }
}

//todo:ダメージ判定のところに移動
public enum AttackSituation
{
    Possible,
    End,
}