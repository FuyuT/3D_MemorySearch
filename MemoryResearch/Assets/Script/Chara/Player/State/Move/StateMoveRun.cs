using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.PlayerState;


/// <summary>
/// �ړ�
/// </summary>
public class StateMoveRun : State
{
    bool isReady;

    protected override void OnEnter(MyUtil.ActorState<Player> prevState)
    {
        isReady = false;
    }

    protected override void OnUpdate()
    {
        AnimUpdate();

        Run();

        SelectNextState();
    }

    //�A�j���[�V�����̍X�V
    void AnimUpdate()
    {
        if(isReady)
        {
            //�Đ��ł��Ă��Ȃ���΍Đ�����
            BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Move_Run");
        }
        else
        {
            //�Đ��ł��Ă��Ȃ���΍Đ�����
            BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Move_Run_Ready");

            //�Đ����I�����Ă����珀������
            if (BehaviorAnimation.IsPlayEnd(ref Owner.animator, "Move_Run_Ready"))
            {
                isReady = true;
            }
        }
    }
    
    void Run()
    {

        //�����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        Vector3 moveAdd = BehaviorMoveToInput.GetInputVec();
        BehaviorMoveToInput.ParseToCameraVec(ref moveAdd);

        //�ړ��X�s�[�h���|����
        moveAdd *= Owner.RunSpeed;

        Actor.Transform.IVelocity().AddVelocity(moveAdd);
    }

    protected override void SelectNextState()
    {
        //���郂�[�V�����ɓ����Ă��Ȃ���ΏI��
        if (!BehaviorAnimation.IsName(ref Owner.animator, "Move_Run")) return;

        //�ҋ@���
        if (!Input.GetKey(KeyCode.LeftShift) 
            || Actor.Transform.IVelocity().GetVelocity() == Vector3.zero)
        {
            stateMachine.Dispatch((int)Player.State.Idle);
            return;
        }

        //���������Ԃ�I��
        EquipmentSelectNextState();
    }
    protected override void OnExit(MyUtil.ActorState<Player> nextState)
    {
        //�A�j���[�V�����̃g���K�[������
        Owner.animator.ResetTrigger("Move_Run");
    }

}
