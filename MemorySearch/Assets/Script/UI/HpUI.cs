using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpUI : MonoBehaviour
{
    /*******************************
    * private
    *******************************/
    [SerializeField] Animator[] hpAnimator;

    int currentHP;

    void Awake()
    {
       currentHP = hpAnimator.Length;
    }

    /*******************************
    * public
    *******************************/
    public void Damage(int damage)
    {
        //UI������������A�j���[�V�������Đ�
        for (int n = currentHP; n > currentHP - damage; n--)
        {
            if (n <= 0) break;
            BehaviorAnimation.UpdateTrigger(ref hpAnimator[n - 1], "Damage"); //HP�̗v�f���͂O����n�܂�̂�-1
        }
        currentHP -= damage;
    }

    public void Heal(int healValue)
    {
        //UI���㏸������A�j���[�V�������Đ�
        for (int n = currentHP; n <= currentHP + healValue; n++)
        {
            if (n > hpAnimator.Length) break;
            if (n <= 0) continue;
            BehaviorAnimation.UpdateTrigger(ref hpAnimator[n - 1], "Heal"); //HP�̗v�f���͂O����n�܂�̂�-1
        }

        currentHP += healValue;

        if(currentHP > hpAnimator.Length)
        {
            currentHP = hpAnimator.Length;
        }
    }

    public void InitHp(int healValue)
    {
        //UI���㏸������A�j���[�V�������Đ�
        for (int n = currentHP; n <= currentHP + healValue; n++)
        {
            if (n > hpAnimator.Length) break;
            if (n <= 0) continue;
            BehaviorAnimation.UpdateTrigger(ref hpAnimator[n - 1], "Heal"); //HP�̗v�f���͂O����n�܂�̂�-1
        }

        currentHP += healValue;

        if (currentHP > hpAnimator.Length)
        {
            currentHP = hpAnimator.Length;
        }
    }


    public void SetCurrentHP(int hp)
    {
        currentHP = hp;
    }
}
