using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<Player>;

/// <summary>
/// �ҋ@
/// </summary>
public class StateIdle : State
{
    protected override void OnEnter(State prevState)
    {
    }

    protected override void OnUpdate()
    {
        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //�A�j���[�V�������ύX����Ă��Ȃ���Ώ����I��
        BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Idle");

        //�ړ�
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            stateMachine.Dispatch((int)Player.State.Move_Walk);
        }

        //�W�����v
        if (Owner.isGround) //���V���Ă��Ȃ���
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (Owner.CheckPossesionMemory((int)Player.State.Jump) || Owner.CheckPossesionMemory((int)Player.State.Double_Jump)) //�������������Ă��邩�m�F
                {
                    stateMachine.Dispatch((int)Player.State.Jump);
                }
            }
        }

        //�_�b�V���n
        if (Owner.nowDushDelayTime < 0 && Input.GetKey(KeyCode.Z))
        {
            if(Owner.isGround)
            {
                stateMachine.Dispatch((int)Player.State.Move_Dush);
            }
            else
            {
                stateMachine.Dispatch((int)Player.State.Move_Dush);
            }
        }

        //�p���`
        if (Input.GetMouseButtonDown(0) && !Owner.ChapterCamera.activeSelf)
        {
            stateMachine.Dispatch((int)Player.State.Attack_Punch);
        }

        //�K�[�h
        if (Input.GetKey(KeyCode.H))
        {
            stateMachine.Dispatch((int)Player.State.Guard);
            return;
        }

        //�@����
        if (Input.GetKey(KeyCode.J))
        {
            stateMachine.Dispatch((int)Player.State.Attack_Slam);
            return;
        }

        //�^�b�N��
        if (Input.GetKey(KeyCode.K))
        {
            stateMachine.Dispatch((int)Player.State.Attack_Tackle);
            return;
        } 
    }

    protected override void OnExit(State nextState)
    {
        //�A�j���[�V�����̃g���K�[������
        Owner.animator.ResetTrigger("Idle");
    }

}
