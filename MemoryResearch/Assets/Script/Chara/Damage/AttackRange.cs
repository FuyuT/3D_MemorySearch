using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    //[SerializeField] AttackInfo[] OutsideAttackInfo;
    //public AttackSituation attackSituation { get; private set; }

    //System.Collections.Generic.Dictionary<int, DamageType> attackInfo;


    //string enemyTag;
    //CharaBase chara;

    ////////////////////////////////
    ///// setter
    ////////////////////////////////
    //public void SetAttackPossible()
    //{
    //    attackSituation = AttackSituation.Possible;
    //}

    ////////////////////////////////
    ///// 初期化
    ////////////////////////////////
    //void Awake()
    //{
    //    chara = transform.root.gameObject.GetComponentInChildren<CharaBase>();
    //    string tag = transform.root.gameObject.tag;
    //    if (tag == "Player")
    //    {
    //        enemyTag = "Enemy";
    //    }
    //    else
    //    {
    //        enemyTag = "Player";
    //    }

    //    attackSituation = AttackSituation.Possible;
    //    attackInfo = new System.Collections.Generic.Dictionary<int, DamageType>();
    //    for (int n = 0; n < OutsideAttackInfo.Length; n++)
    //    {
    //        attackInfo.Add(OutsideAttackInfo[n].stateKey, OutsideAttackInfo[n].type);
    //    }

    //}

    ////////////////////////////////
    ///// 当たり判定
    ////////////////////////////////

    //private void OnTriggerEnter(Collider other)
    //{
    //}
    //private void OnTriggerStay(Collider other)
    //{
    //    if (!CheckAttackPossible()) return;

    //    if (other.tag == enemyTag)
    //    {
    //        Attack(other);
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //}

    ///// <summary>
    ///// 攻撃可能か確認
    ///// </summary>
    ///// <returns>攻撃可能ならtrue,そうでなければfalse</returns>
    //bool CheckAttackPossible()
    //{
    //    if (!CheckAttackState()) return false;

    //    switch (attackInfo[chara.currentState])
    //    {
    //        case DamageType.Continue:
    //            return true;
    //        case DamageType.Once:
    //            if (attackSituation == AttackSituation.Possible) return true;
    //            break;
    //    }
    //    return false;
    //}

    ///// <summary>
    ///// 攻撃するステートか確認
    ///// </summary>
    ///// <returns>攻撃ステートならtrue,そうでなければfalse</returns>
    //bool CheckAttackState()
    //{
    //    if (attackInfo.ContainsKey(chara.currentState))
    //    {
    //        return true;
    //    }
    //    return false;
    //}

    ///// <summary>
    ///// 攻撃処理
    ///// </summary>
    ///// <param name="other">当たったオブジェクト</param>
    //void Attack(Collider other)
    //{
    //    //攻撃力が0なら終了
    //    int attackPower = chara.param.Get<int>((int)CharaBase.ParamKey.AttackPower);
    //    Debug.Log(attackPower);

    //    if (attackPower == 0) return;

    //    //敵にダメージを与える
    //    other.GetComponentInChildren<CharaBase>().Damage(attackPower);
    //    attackSituation = AttackSituation.End;
    //    return;
    //}

}
