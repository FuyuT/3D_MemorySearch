using UnityEngine;
public class ParameterForChara
{
    /*******************************
    * public
    *******************************/
    public int uniqueID;
    public int hp;
    public AttackInfo attackInfo;

    public ParameterForChara()
    {
        attackInfo = new AttackInfo();
        Init();
    }

    public void Init()
    {
        hp = 0;
        attackInfo.Init();
    }
}
