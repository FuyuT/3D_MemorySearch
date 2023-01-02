using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Player>;

/// <summary>
/// �W�����v
/// </summary>
public class StateJump : State
{
    bool IsAcceleration;

    protected override void OnEnter(State prevState)
    {
        Owner.situation = (int)Player.Situation.Jump;

        IsAcceleration = true;

        //y���̑��x��0�ɂ���
        Owner.moveVec = new Vector3(Owner.moveVec.x, 0, Owner.moveVec.z);
        //������ݒ�
        Owner.nowJumpSpeed = Owner.JumpStartSpeed;
    }

    protected override void OnUpdate()
    {
        Move();
        Jump();
    }

    void Move()
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
        //�J�����̌������猩���i�s�����̒P�ʃx�N�g�����쐬
        Vector3 moveForward = Camera.main.transform.forward * inputVector.z + Camera.main.transform.right * inputVector.x;
        moveForward.y = 0;

        //�ړ��x�N�g�����i�[
        Owner.moveVec += moveForward * Owner.MoveSpeed;
    }

    //�W�����v����
    void Jump()
    {
        //�L�[���͂���Ă�����A�W�����v���x������������i�򋗗������΂��j
        if (Input.GetKey(KeyCode.C) && IsAcceleration)
        {
            Owner.nowJumpSpeed += Owner.JumpAcceleration;
        }
        else
        {
            IsAcceleration = false;
        }

        //�W�����v�̑��x������������
        Owner.nowJumpSpeed -= Owner.JumpDecreaseValue;

        //�W�����v�x�N�g�����i�[
        Owner.moveVec.y += Owner.nowJumpSpeed;

        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //�_�u���W�����v
        if(Owner.CheckPossesionMemory((int)Player.Event.Double_Jump)) //�������������Ă��邩�m�F
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                stateMachine.Dispatch((int)Player.Event.Double_Jump);
            }
        }

        //�󒆃_�b�V��
        if (Owner.nowDushDelayTime < 0 && Input.GetKey(KeyCode.Z))
        {
            stateMachine.Dispatch((int)Player.Event.Air_Dush);
        }

        //���n���Ă�����ҋ@��Ԃ�
        if (Owner.situation == (int)Player.Situation.Jump_End)
        {
            stateMachine.Dispatch((int)Player.Event.Idle);
        }

    }

    protected override void OnExit(State nextState)
    {
        Owner.moveVec.y = 0;
    }
}
