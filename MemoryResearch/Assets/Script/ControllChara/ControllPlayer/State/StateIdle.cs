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
        if (Input.GetKey("up") || Input.GetKey("down") || Input.GetKey("right") || Input.GetKey("left"))
        {
            stateMachine.Dispatch((int)Player.Event.Move);
        }

        //�W�����v
        if (Owner.CheckPossesionMemory((int)Player.Event.Jump) || Owner.CheckPossesionMemory((int)Player.Event.Double_Jump))
        {
            if (Owner.situation != (int)Player.Situation.Floating) //���V���Ă��Ȃ���
            {
                if (Input.GetKeyDown(KeyCode.C))
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
    }

}
