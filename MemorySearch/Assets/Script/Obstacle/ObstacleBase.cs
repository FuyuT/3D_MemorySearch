using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBase : MonoBehaviour
{
    /*******************************
    * Update
    *******************************/

    /*******************************
    * protected
    *******************************/
    protected AttackInfo attackInfo;

    [SerializeField]
    protected int attackPower;

    [SerializeField]
    protected int collisionExclusionID;

    Dictionary<int, CharaBase> hitEnemies;


    protected void UpdateBase()
    {
        //todo:攻撃を与えていた対象のダメージリストから除外するか判断
    }

    //当たっていた敵のダメージ情報から、自分を除外
    void RemoveDamageInfo()
    {
        foreach (KeyValuePair<int, CharaBase> kvp in hitEnemies)
        {
            kvp.Value.RemoveDamageInfo(attackInfo.id);
        }
    }

    /*******************************
    * public
    *******************************/
    public ObstacleBase()
    {
        attackInfo = new AttackInfo();
        attackInfo.power = attackPower;
        collisionExclusionID = -1;
    }

    public void SetDamage(int damage)
    {
        this.attackInfo.power = damage;
    }
    public void SetCollisionExclusionID(int id)
    {
        this.collisionExclusionID = id;
    }

    /*******************************
    * colision
    *******************************/

    private void OnTriggerStay(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
            case "Enemy":
                Attack(other);
                break;
            case "Untagged":
                return;
        }

        //親IDが設定されていたら
        if (collisionExclusionID != -1)
        {
            //オブジェクトの親に当たっていたら終了
            try
            {
                if (other.gameObject.GetComponent<CharaBase>().GetID() == collisionExclusionID) return;
            }
            catch
            {
            }
        }
    }

    /// <summary>
    /// 敵のダメージ情報に自分を追加する
    /// </summary>
    /// <param name="other">当たったオブジェクト</param>
    private void Attack(Collider other)
    {
        //攻撃力が0なら終了
        if (attackInfo.power == 0) return;
        //敵のダメージ情報に追加する
        CharaBase enemy = other.GetComponent<CharaBase>();

        AttackInfo temp = attackInfo;
        attackInfo.attacker = gameObject;
        attackInfo.attackPos = gameObject.transform.position;
        enemy.AddDamageInfo(attackInfo.id, temp);
        //当たった敵を記録しておく
        if (!hitEnemies.ContainsKey(enemy.GetID()))
        {
            hitEnemies.Add(enemy.GetID(), other.GetComponentInChildren<CharaBase>());
        }
    }
}