using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharaBase : MonoBehaviour
{
    /*******************************
    * private
    *******************************/

    //死亡処理
    void Dead()
    {
        transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    //ヒットエフェクトを再生
    void PlayHitEffect(Vector3 attackPos)
    {
        if (attackPos == null) return;

        //レイを攻撃してきた相手から自分に向けて飛ばして、当たった場所にエフェクトを再生させる
        Ray ray = new Ray(attackPos, Vector3.Normalize(transform.position - attackPos));

        foreach(RaycastHit hit in Physics.RaycastAll(ray, 50))
        {
            if(hit.collider.gameObject == this.gameObject)
            {
                HitEffectPlayer.Instance().PlayEffect(hit.point);
                return;
            }
        }

        HitEffectPlayer.Instance().PlayEffect(transform.position);
    }

    /*******************************
    * protected
    *******************************/
    //パラメータ
    protected ParameterForChara charaParam;

    Dictionary<int, AttackInfo> damageInfo;

    protected CharaBase()
    {
        charaParam = new ParameterForChara();
        damageInfo = new Dictionary<int, AttackInfo>();
    }

    protected void CharaBaseInit()
    {
        charaParam.Init();
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
            if (kvp.Value.type == DamageType.Once)
            {
                damageInfo[kvp.Key].situation = AttackSituation.End;
                //ヒットエフェクトを再生
                PlayHitEffect(kvp.Value.attackPos);
            }
        }
    }

    /// <summary>
    /// CharaBaseの更新
    /// 更新を続けるべきではない時にfalseを返す
    /// </summary>
    protected bool UpdateCharaBase()
    {
        UpdateDamage();

        if((CameraManager.CameraType)CameraManager.instance.GetCurrentCameraType() == CameraManager.CameraType.Controller
            || MoveObjectConsoleRange.Instance().IsUseControleCamera())
        {
            return false;
        }
        return true;
    }

    virtual protected bool IsPossibleDamage()
    {
        return true;
    }

    virtual protected void AddDamageProcess(int attackPower)
    {
    }

    /*******************************
    * public
    *******************************/
    [Header("アニメーター")]
    [SerializeField] public Animator animator;

    [Header("アニメーション位置")]
    [SerializeField] public Transform animTransform;

    //ダメージ情報を追加する
    public void AddDamageInfo(int id, AttackInfo attackInfo)
    {
        //死んでいたら終了
        if (IsDead()) return;

        if (!IsPossibleDamage()) return;

        //同じものが登録されていたら終了
        if (damageInfo.ContainsKey(id)) return;
        //Debug.Log("add" + id);

        damageInfo.Add(id, attackInfo);
    }

    //ダメージ情報を除外する
    public void RemoveDamageInfo(int id)
    {
        //idが登録されていなければ終了
        if (!damageInfo.ContainsKey(id)) return;

        damageInfo.Remove(id);
    }

    //ダメージ処理
    public void Damage(int attackPower)
    {
        if (!IsPossibleDamage()) return;

        //ダメージの計算を行う
        int damage = attackPower - charaParam.defencePower;
        damage = damage < 0 ? 0 : damage;

        //ダメージ処理を行う際の追加プロセス
        AddDamageProcess(damage);

        charaParam.hp -= damage;

        if (IsDead())
        {
            Dead();
        }
    }

    /// <summary>
    /// 死んでいるかの確認
    /// 引数には、与えるダメージを入力（初期値は0）
    /// 入力しなければ、現在のhpで確認
    /// 入力すれば、引数のダメージを与えた時に死ぬか確認
    /// </summary>
    /// <param name="damage">与えるダメージを入力</param>
    /// <returns></returns>
    public bool IsDead(int damage = 0)
    {
        return charaParam.hp - damage <= 0 ? true : false;
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
