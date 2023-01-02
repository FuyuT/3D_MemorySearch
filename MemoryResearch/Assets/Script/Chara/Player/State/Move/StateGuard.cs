using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<Player>;


/// <summary>
/// �K�[�h
/// </summary>
public class StateGuard : State
{
    bool isReady;

    protected override void OnEnter(State prevState)
    {
        isReady = true;
    }

    protected override void OnUpdate()
    {
        AnimUpdate();

        SelectNextState();
    }

    //�A�j���[�V�����̍X�V
    void AnimUpdate()
    {
        if(isReady)
        {
            //�Đ��ł��Ă��Ȃ���΍Đ�����
            BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Guard_Guarding");
        }
        else
        {
            //�Đ��ł��Ă��Ȃ���΍Đ�����
            BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Guard_Ready");

            //�Đ����I�����Ă����珀������
            if (BehaviorAnimation.IsPlayEndCheck(ref Owner.animator, "Guard_Ready"))
            {
                isReady = true;
            }
        }
    }
    
    protected override void SelectNextState()
    {
        //�K�[�h���[�V�����ɓ����Ă��Ȃ���ΏI��
        if (!BehaviorAnimation.IsNameCheck(ref Owner.animator, "Guard_Guarding")) return;


        //�ҋ@
        if (!Input.GetKey(KeyCode.H))
        {
            stateMachine.Dispatch((int)Player.State.Idle);
            return;
        }
    }
}
