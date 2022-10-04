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
        Debug.Log("�ړ���Ԃֈڍs");
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

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Owner.moveVec *= Owner.RunSpeed;
        }
        else
        {
            Owner.moveVec *= Owner.MoveSpeed;
        }

        if (Owner.moveVec != Vector3.zero)
        {
            stateMachine.Dispatch((int)Player.Event.Idle);
        }

        NextStateUpdate();
    }

    protected override void NextStateUpdate()
    {
        //�_�b�V���n
        if (Input.GetKey(KeyCode.Z))
        {
            //�󒆃_�b�V��
            if (Owner.isFloating)
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
