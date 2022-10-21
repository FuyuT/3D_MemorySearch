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
    /// �U������
    /// </summary>
    /// <param name="other">���������I�u�W�F�N�g</param>
    void Attack(Collider other)
    {
        //�U���͂�0�Ȃ�I��
        int attackPower = chara.param.Get<int>((int)CharaBase.ParamKey.AttackPower);
        if (attackPower == 0) return;

        //�G�Ƀ_���[�W��^����
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
