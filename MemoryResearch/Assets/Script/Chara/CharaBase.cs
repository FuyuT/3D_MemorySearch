using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaBase : ObjectBase
{
    //////////////////////////////
    /// private

    /// <summary>
    /// 死亡処理
    /// hpが0ならオブジェクトを非アクティブにする
    /// </summary>
    void Dead()
    {
        if (param.Get<int>((int)Enemy.ParamKey.Hp) <= 0)
        {
            transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    //////////////////////////////
    /// protected
    protected CharaBase()
    {
        param = new AnyParameterMap();
        CharaBaseInit();
    }

    protected void CharaBaseInit()
    {
        currentState = 0;
    }

    //////////////////////////////
    /// public
    public enum ParamKey
    {
        Hp,
        PossesionMemory,
        AttackPower,
        Attack_Info,
    }

    public AnyParameterMap param;

    public int currentState;

    /// <summary>
    /// ダメージ処理
    /// </summary>
    /// <param name="attackPower">攻撃力</param>
    public void Damage(int attackPower)
    {
       // Debug.Log(attackPower + "ダメージ");

        int hp = param.Get<int>((int)Enemy.ParamKey.Hp) - attackPower;
        param.Set((int)Enemy.ParamKey.Hp, hp);

        Dead();
    }

}
