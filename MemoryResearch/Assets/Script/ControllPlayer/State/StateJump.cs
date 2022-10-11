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
        IsAcceleration = true;

        Owner.isJump = true;
        Debug.Log("�W�����v��Ԃֈڍs");

        Owner.isFloating = true;
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
        Owner.moveVec += Owner.transform.forward;

        //�L�[���͂���Ă�����A�W�����v���x������������i�򋗗������΂��j
        if (Input.GetKey("up") && IsAcceleration)
        {
            Owner.nowJumpSpeed += Owner.JumpAcceleration;
        }
        else
        {
            IsAcceleration = false;
        }

        moveAdd.y += Owner.nowJumpSpeed;

        Owner.moveVec += moveAdd;

        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //�_�u���W�����v
        if (Input.GetKeyDown(KeyCode.C))
        {
            stateMachine.Dispatch((int)Player.Event.Double_Jump);
        }
        
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
        Owner.moveVec.y = 0;
    }
}
