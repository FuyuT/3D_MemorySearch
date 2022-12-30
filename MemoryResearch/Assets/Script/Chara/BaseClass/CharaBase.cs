using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaBase : MonoBehaviour
{
    /*******************************
    * private
    *******************************/

    //死亡処理
    void Dead()
    {
        Debug.Log("死亡しました。");
        transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    /*******************************
    * protected
    *******************************/

    //パラメータ
    protected ParameterForChara charaParam;

    protected Rigidbody rigidbody;

    protected CharaBase()
    {
        charaParam = new ParameterForChara();
    }

    protected void CharaBaseInit()
    {
        charaParam.Init();
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    /*******************************
    * public
    *******************************/

    //ダメージ処理
    public void Damage(int attackPower)
    {
        Debug.Log(attackPower + "ダメージ");

        charaParam.hp -= attackPower;
        if(IsDead())
        {
            Dead();
        }
    }

    //死んでいるかの確認
    public bool IsDead()
    {
        return charaParam.hp <= 0 ? true : false;
    }

    //HPの取得
    public int GetHP()
    {
        return charaParam.hp;
    }

}
