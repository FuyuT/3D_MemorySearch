using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Player>;

/// <summary>
/// �_�u���W�����v
/// </summary>
public class StateDoubleJump : State
{
    protected override void OnEnter(State prevState)
    {
        Debug.Log("�_�u���W�����v��Ԃֈڍs");

        Owner.isJump = true;

        Owner.isFloating = true;
        //y���̑��x��0�ɂ���
        Owner.moveVec = new Vector3(Owner.moveVec.x, 0, Owner.moveVec.z);
        //������ݒ�
        Owner.nowJumpSpeed = Owner.JumpStartSpeed;
    }
    protected override void OnUpdate()
    {
        //�L�[���͂ł̈ړ�
        if (Input.GetKey("up"))
        {
            Owner.moveVec += Owner.transform.forward;
        }
        if (Input.GetKey("down"))
        {
            Owner.moveVec -= Owner.transform.forward;
        }
        if (Input.GetKey("right"))
        {
            Owner.moveVec += Owner.transform.right;

        }
        if (Input.GetKey("left"))
        {
            Owner.moveVec -= Owner.transform.right;
        }

        //�W�����v����
        Vector3 moveAdd = Owner.moveVec;

        Owner.nowJumpSpeed -= Owner.JumpDecreaseValue;
        moveAdd.y += Owner.nowJumpSpeed + Owner.JumpAcceleration;

        Owner.moveVec += moveAdd;

        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //�󒆃_�b�V��
        if (Input.GetKey(KeyCode.Z))
        {
            stateMachine.Dispatch((int)Player.Event.Air_Dush);
        }

        //���n������ҋ@��Ԃ�
        if (!Owner.isJump)
        {
            stateMachine.Dispatch((int)Player.Event.Idle);
        }
    }

    protected override void OnExit(State nextState)
    {
        Owner.isJump = true;
    }
}
