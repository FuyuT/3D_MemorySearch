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

        //合成コストを増加させる
        DataManager.instance.IPlayerData().AddPossesionCombineCost(1);
    }

    /*******************************
    * protected
    *******************************/

    //パラメータ
    protected ParameterForChara charaParam;

    protected Rigidbody rigidbody;

    Dictionary<int, AttackInfo> damageInfo;

    protected CharaBase()
    {
        charaParam = new ParameterForChara();
        damageInfo = new Dictionary<int, AttackInfo>();
    }

    protected void CharaBaseInit()
    {
        charaParam.Init();
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    protected void UpdateDamage()
    {
        //当たっている攻撃のダメージ処理を行う
        foreach(KeyValuePair<int, AttackInfo> kvp in damageInfo)
        {
            //ダメージ判定が終了していれば次の値へ
            if (kvp.Value.situation == AttackSituation.End) continue;
            Damage(kvp.Value.power);

            //一度だけ判定する攻撃なら、攻撃状態を終了に変更
            if(kvp.Value.type == DamageType.Once)
            {
                damageInfo[kvp.Key].situation = AttackSituation.End;
            }
        }
    }

    /// <summary>
    /// ダメージの更新を行う
    /// </summary>
    protected void CharaUpdate()
    {
        UpdateDamage();
    }

    /*******************************
    * public
    *******************************/
    [Header("アニメーター")]
    [SerializeField] public Animator animator;

    //ダメージ情報を追加する
    public void AddDamageInfo(int id, AttackInfo attackInfo)
    {
        //死んでいたら終了
        if (IsDead()) return;

        //同じものが登録されていたら終了
        if (damageInfo.ContainsKey(id)) return;
        Debug.Log("add" + id);

        damageInfo.Add(id, attackInfo);
    }

    //ダメージ情報を除外する
    public void RemoveDamageInfo(int id)
    {
        //idが登録されていなければ終了
        if (!damageInfo.ContainsKey(id)) return;
        Debug.Log("remove" + id);

        damageInfo.Remove(id);
    }

    //ダメージ処理
    public void Damage(int attackPower)
    {
        int damage = attackPower - charaParam.defencePower < 0 ? 0 : attackPower - charaParam.defencePower;
        
        Debug.Log(damage + "ダメージ");

        charaParam.hp -= damage;
        if (IsDead())
        {
            Dead();
        }
    }

    //死んでいるかの確認
    public bool IsDead()
    {
        return charaParam.hp <= 0 ? true : false;
    }
    public bool IsEndDeadMotion()
    {
        if (BehaviorAnimation.IsPlayEnd(ref animator, "Damage_Dead"))
        {
            return true;
        }
        return false;
    }


    //getter
    //IDの取得
    public int GetID()
    {
        return charaParam.uniqueID;
    }

    //HPの取得
    public int GetHP()
    {
        return charaParam.hp;
    }

    //攻撃情報の取得
    public AttackInfo GetAttackInfo()
    {
        return charaParam.attackInfo;
    }


    //setter
    //攻撃力の設定
    public void InitAttackPower()
    {
        charaParam.attackInfo.power = 0;
    }

    public void SetAttackPower(int power)
    {
        charaParam.attackInfo.power = power;
    }

    //防御力の設定
    public void InitDefencePower()
    {
        charaParam.defencePower = 0;
    }

    public void SetDefencePower(int defence)
    {
        charaParam.defencePower = defence;
    }

}
