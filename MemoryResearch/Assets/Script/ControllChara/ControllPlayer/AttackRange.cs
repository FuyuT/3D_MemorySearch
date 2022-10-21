using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    string enemyTag;
    CharaBase chara;
    void Awake()
    {
        chara = transform.parent.gameObject.GetComponentInChildren<CharaBase>();

        string tag = transform.parent.gameObject.tag;
        if (tag == "Player")
        {
            enemyTag = "Enemy";
        }
        else
        {
            enemyTag = "Player";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == enemyTag)
        {
            Attack(other);
        }
    }

    /// <summary>
    /// 攻撃処理
    /// </summary>
    /// <param name="other">当たったオブジェクト</param>
    void Attack(Collider other)
    {
        //攻撃力が0なら終了
        int attackPower = chara.param.Get<int>((int)CharaBase.ParamKey.AttackPower);
        if (attackPower == 0) return;

        //敵にダメージを与える
        other.GetComponentInChildren<CharaBase>().Damage(attackPower);
        return;
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnCollisionExit(Collision collision)
    {
    }
}
