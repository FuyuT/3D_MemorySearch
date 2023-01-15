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

    //�������Ă����G�̃_���[�W��񂩂�A���������O
    void RemoveDamageInfo()
    {
        foreach (KeyValuePair<int, CharaBase> kvp in hitEnemies)
        {
            kvp.Value.RemoveDamageInfo(chara.GetAttackInfo().id);
        }

        //�U����Ԃ�������
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
    * �Փ˔���
    *******************************/

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == enemyTag)
        {
            Attack(other);
        }
    }

    /// <summary>
    /// �G�̃_���[�W���Ɏ�����ǉ�����
    /// </summary>
    /// <param name="other">���������I�u�W�F�N�g</param>
    void Attack(Collider other)
    {
        //�U���͂�0�Ȃ�I��
        int attackPower = chara.GetAttackInfo().power;

        if (attackPower == 0) return;
        //�G�̃_���[�W���ɒǉ�����
        CharaBase enemy = other.GetComponent<CharaBase>();
        enemy.AddDamageInfo(chara.GetAttackInfo().id, chara.GetAttackInfo());
        //���������G���L�^���Ă���
        if(!hitEnemies.ContainsKey(enemy.GetID()))
        {
            hitEnemies.Add(enemy.GetID(), other.GetComponentInChildren<CharaBase>());
        }
    }

}
