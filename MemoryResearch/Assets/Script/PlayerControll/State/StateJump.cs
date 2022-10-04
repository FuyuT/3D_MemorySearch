using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Player>;

/// <summary>
/// �W�����v
/// </summary>
public class StateJump : State
{
    protected override void OnEnter(State prevState)
    {
        Owner.isGravity = false;
        Debug.Log("�W�����v��Ԃֈڍs");

        Owner.isFloating = true;
        //������ݒ�
        Owner.nowJumpSpeed = Owner.JumpStartSpeed;
    }
    protected override void OnUpdate()
    {
        //���n������ҋ@��Ԃ�
        if (!Owner.isFloating)
        {
            stateMachine.Dispatch((int)Player.Event.Idle);
            return;
        }

        

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

        Owner.nowJumpSpeed -= Owner.Gravity;
        moveAdd.y += Owner.nowJumpSpeed + Owner.JumpAcceleration;

        Owner.moveVec += moveAdd;


        NextStateUpdate();
    }

    protected override void NextStateUpdate()
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
    }

    protected override void OnExit(State nextState)
    {
        Owner.isGravity = true;
    }
}
