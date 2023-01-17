using UnityEngine;
public class ParameterForChara
{
    /*******************************
    * public
    *******************************/
    public int uniqueID;
    public int hp;
    public int defencePower;
    public AttackInfo attackInfo;

    public ParameterForChara()
    {
        attackInfo = new AttackInfo();
        Init();
    }

    public void Init()
    {
        uniqueID = UniqueIDSetter.Instance().GetUniqueID();
        hp = 0;
        defencePower = 0;
        attackInfo.Init();
    }
}
