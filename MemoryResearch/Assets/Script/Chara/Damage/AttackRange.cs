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
    ///// ������
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
    ///// �����蔻��
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
    ///// �U���\���m�F
    ///// </summary>
    ///// <returns>�U���\�Ȃ�true,�����łȂ����false</returns>
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
    ///// �U������X�e�[�g���m�F
    ///// </summary>
    ///// <returns>�U���X�e�[�g�Ȃ�true,�����łȂ����false</returns>
    //bool CheckAttackState()
    //{
    //    if (attackInfo.ContainsKey(chara.currentState))
    //    {
    //        return true;
    //    }
    //    return false;
    //}

    ///// <summary>
    ///// �U������
    ///// </summary>
    ///// <param name="other">���������I�u�W�F�N�g</param>
    //void Attack(Collider other)
    //{
    //    //�U���͂�0�Ȃ�I��
    //    int attackPower = chara.param.Get<int>((int)CharaBase.ParamKey.AttackPower);
    //    Debug.Log(attackPower);

    //    if (attackPower == 0) return;

    //    //�G�Ƀ_���[�W��^����
    //    other.GetComponentInChildren<CharaBase>().Damage(attackPower);
    //    attackSituation = AttackSituation.End;
    //    return;
    //}

}
