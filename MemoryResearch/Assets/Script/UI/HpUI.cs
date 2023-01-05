using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpUI : MonoBehaviour
{
    [SerializeField] Animator[] hpAnimator;

    int beforeHp;

    void Awake()
    {
        beforeHp = 5;
    }

    void Update()
    {
        if (beforeHp <= 0) return;
        Damage();
        Heal();
    }

    void Damage()
    {
        //�v���C���[��HP�ɕύX������΁AUI�̃A�j���[�V�������Đ�����
        if (Player.readPlayer.GetHP() < beforeHp)
        {
            //UI������������A�j���[�V�������Đ�

            for (int n = beforeHp; n > Player.readPlayer.GetHP(); n--)
            {
                if (n <= 0) break;
                BehaviorAnimation.UpdateTrigger(ref hpAnimator[n - 1], "Damage"); //HP�̗v�f���͂O����n�܂�̂�-1
            }
            beforeHp = Player.readPlayer.GetHP();
        }
    }

    void Heal()
    {
        //�v���C���[��HP�ɕύX������΁AUI�̃A�j���[�V�������Đ�����
        if (Player.readPlayer.GetHP() > beforeHp)
        {
            //UI���㏸������A�j���[�V�������Đ�

            for (int n = beforeHp; n < Player.readPlayer.GetHP(); n++)
            {
                BehaviorAnimation.UpdateTrigger(ref hpAnimator[n - 1], "Heal"); //HP�̗v�f���͂O����n�܂�̂�-1
            }
            beforeHp = Player.readPlayer.GetHP();
        }
    }
}
