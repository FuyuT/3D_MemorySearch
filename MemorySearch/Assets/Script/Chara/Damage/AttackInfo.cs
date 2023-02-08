using UnityEngine;
public enum DamageType
{
    Once,
    Continue,
}
public enum AttackSituation
{
    Possible,
    End,
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
    public GameObject attacker;
    public Vector3 attackPos;

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
