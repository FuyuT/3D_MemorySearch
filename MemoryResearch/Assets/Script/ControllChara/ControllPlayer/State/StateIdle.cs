using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Player>;

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
        //�ړ�
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            stateMachine.Dispatch((int)Player.Event.Move);
        }

        //�W�����v
        if (Owner.isGround) //���V���Ă��Ȃ���
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (Owner.CheckPossesionMemory((int)Player.Event.Jump) || Owner.CheckPossesionMemory((int)Player.Event.Double_Jump)) //�������������Ă��邩�m�F
                {
                    stateMachine.Dispatch((int)Player.Event.Jump);
                }
            }
        }

        //�_�b�V���n
        if (Owner.nowDushDelayTime < 0 && Input.GetKey(KeyCode.Z))
        {
            //�󒆃_�b�V��
            if (Owner.situation == (int)Player.Situation.Floating)
            {
                stateMachine.Dispatch((int)Player.Event.Air_Dush);
            }
            //�^�b�N��
            else
            {
                stateMachine.Dispatch((int)Player.Event.Attack_Tackle);
            }

        }

        //�p���`
        if(Input.GetMouseButtonDown(0) && !Owner.ChapterCamera.activeSelf)
        {
            stateMachine.Dispatch((int)Player.Event.Attack_Punch);
        }
    }

}
