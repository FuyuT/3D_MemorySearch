using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<Player>;


/// <summary>
/// �ړ�
/// </summary>
public class StateMoveRun : State
{
    bool isReady;

    protected override void OnEnter(State prevState)
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

        //�_�b�V���n
        if (Owner.nowDushDelayTime < 0 && Input.GetKey(KeyCode.Z))
        {
            if(Owner.isGround)
            {
                //�^�b�N��
                stateMachine.Dispatch((int)Player.State.Attack_Tackle);
            }
            else
            {
                //�󒆃_�b�V��
                stateMachine.Dispatch((int)Player.State.Move_Dush);
            }
        }

        //�W�����v
        if (Owner.isGround) //���V���Ă��Ȃ���
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (Owner.CheckPossesionMemory((int)Player.State.Jump) || Owner.CheckPossesionMemory((int)Player.State.Double_Jump)) //�������������Ă��邩�m�F
                {
                    stateMachine.Dispatch((int)Player.State.Jump);
                    return;
                }
            }
        }

        //�p���`
        if (Input.GetMouseButtonDown(0) && !Owner.ChapterCamera.activeSelf)
        {
            stateMachine.Dispatch((int)Player.State.Attack_Punch);
            return;
        }

        //�ҋ@���
        if (!Input.GetKey(KeyCode.LeftShift) 
            || Actor.Transform.IVelocity().GetVelocity() == Vector3.zero)
        {
            stateMachine.Dispatch((int)Player.State.Idle);
            return;
        }
    }
}
