using MyUtil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.PlayerState;

/// <summary>
/// �K�[�h
/// </summary>
public class StateGuard : State
{
    /*******************************
    * private
    *******************************/
    bool isReady;

    void AnimUpdate()
    {
        if (isReady)
        {
            //�Đ��ł��Ă��Ȃ���΍Đ�����
            BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Guard_Guarding");
            SoundManager.instance.PlaySe(Owner.GuardSE, Owner.transform.position);
        }
        else
        {
            //�Đ��ł��Ă��Ȃ���΍Đ�����
            BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Guard_Ready");

            //�Đ����I�����Ă����珀������
            if (BehaviorAnimation.IsPlayEnd(ref Owner.animator, "Guard_Ready"))
            {
                isReady = true;
            }
        }
    }
    /*******************************
    * protected
    *******************************/
    protected override void OnEnter(MyUtil.ActorState<Player> prevState)
    {
        isReady = true;
        Owner.SetDefencePower(1);
    }
    protected override void OnUpdate()
    {
        AnimUpdate();

        SelectNextState();
    }
    protected override void SelectNextState()
    {
        //�K�[�h���[�V�����ɓ����Ă��Ȃ���ΏI��
        if (!BehaviorAnimation.IsName(ref Owner.animator, "Guard_Guarding")) return;

        //�ҋ@
        if (!Input.GetKey(Owner.equipmentMemories[Owner.currentEquipmentNo].GetKeyCode()))
        {
            stateMachine.Dispatch((int)Player.State.Idle);
            return;
        }

        //���������Ԃ�I��
        EquipmentSelectNextState();
    }
    protected override void OnExit(MyUtil.ActorState<Player> nextState)
    {
        Owner.InitDefencePower();

        Owner.animator.ResetTrigger("Guard_Guarding");
        SoundManager.instance.StopSe(Owner.GuardSE);
    }
}
