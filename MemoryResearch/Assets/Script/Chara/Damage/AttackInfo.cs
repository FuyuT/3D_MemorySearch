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
    public int id;
    public int power;
    public DamageType type;
    public AttackSituation situation;

    public AttackInfo()
    {
        Init();
    }

    public void Init()
    {
        id = UniqueIDSetter.Instance().GetUniqueID();
        power = 0;
        type = DamageType.Once;
        situation = AttackSituation.Possible;
    }
}

//todo:ダメージ判定のところに移動
public enum AttackSituation
{
    Possible,
    End,
}