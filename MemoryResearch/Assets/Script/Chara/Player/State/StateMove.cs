using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Player>;


/// <summary>
/// �ړ�
/// </summary>
public class StateMove : State
{
    protected override void OnEnter(State prevState)
    {
    }

    protected override void OnUpdate()
    {

        //�����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        Vector3 inputVector = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            inputVector += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector += new Vector3(1, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector += new Vector3(-1, 0, 0);
        }

        //�P�ʃx�N�g�����쐬
        Vector3 moveForward = Camera.main.transform.forward * inputVector.z + Camera.main.transform.right * inputVector.x;
        moveForward.y = 0;

        //�ړ��X�s�[�h���|����
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Owner.moveVec += moveForward * Owner.RunSpeed;
        }
        else
        {
            Owner.moveVec += moveForward * Owner.MoveSpeed;
        }

        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //�_�b�V���n
        if (Owner.nowDushDelayTime < 0 && Input.GetKey(KeyCode.Z))
        {
            switch (Owner.situation)
            {
                case (int)Player.Situation.Jump:
                case (int)Player.Situation.Floating:
                    //�󒆃_�b�V��
                    stateMachine.Dispatch((int)Player.Event.Air_Dush);
                    return;
                default:
                    //�^�b�N��
                    stateMachine.Dispatch((int)Player.Event.Attack_Tackle);
                    return;
            }
        }

        //�W�����v
        if (Owner.isGround) //���V���Ă��Ȃ���
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (Owner.CheckPossesionMemory((int)Player.Event.Jump) || Owner.CheckPossesionMemory((int)Player.Event.Double_Jump)) //�������������Ă��邩�m�F
                {
                    stateMachine.Dispatch((int)Player.Event.Jump);
                    return;
                }
            }
        }

        //�p���`
        if (Input.GetMouseButtonDown(0) && !Owner.ChapterCamera.activeSelf)
        {
            stateMachine.Dispatch((int)Player.Event.Attack_Punch);
            return;
        }

        //�ҋ@���
        if (Owner.moveVec == Vector3.zero)
        {
            stateMachine.Dispatch((int)Player.Event.Idle);
            return;
        }
    }
}
