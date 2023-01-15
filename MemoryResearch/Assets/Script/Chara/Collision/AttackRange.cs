using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    /*******************************
    * private
    *******************************/
    [SerializeField] CharaBase chara;
    Dictionary<int, CharaBase> hitEnemies;

    string enemyTag;

    private void Awake()
    {
        SetEnemyTag();
    }

    void SetEnemyTag()
    {
        if (chara.gameObject.tag == "Player")
        {
            enemyTag = "Enemy";
        }
        else
        {
            enemyTag = "Player";
        }
    }

    private void OnDisable()
    {
        RemoveDamageInfo();
    }

    //当たっていた敵のダメージ情報から、自分を除外
    void RemoveDamageInfo()
    {
        foreach (KeyValuePair<int, CharaBase> kvp in hitEnemies)
        {
            kvp.Value.RemoveDamageInfo(chara.GetAttackInfo().id);
        }

        //攻撃状態を初期化
        chara.GetAttackInfo().situation = AttackSituation.Possible;
    }

    /*******************************
    * public
    *******************************/
    public AttackRange()
    {
        hitEnemies = new Dictionary<int, CharaBase>();
    }

    /*******************************
    * 衝突判定
    *******************************/

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == enemyTag)
        {
            Attack(other);
        }
    }

    /// <summary>
    /// 敵のダメージ情報に自分を追加する
    /// </summary>
    /// <param name="other">当たったオブジェクト</param>
    void Attack(Collider other)
    {
        //攻撃力が0なら終了
        int attackPower = chara.GetAttackInfo().power;

        if (attackPower == 0) return;
        //敵のダメージ情報に追加する
        CharaBase enemy = other.GetComponent<CharaBase>();
        enemy.AddDamageInfo(chara.GetAttackInfo().id, chara.GetAttackInfo());
        //当たった敵を記録しておく
        if(!hitEnemies.ContainsKey(enemy.GetID()))
        {
            hitEnemies.Add(enemy.GetID(), other.GetComponentInChildren<CharaBase>());
        }
    }

}
