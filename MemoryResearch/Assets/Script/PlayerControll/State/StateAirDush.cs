using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Player>;

/// <summary>
/// �󒆃_�b�V��
/// </summary>
public class StateAirDush : State
{
    Vector3 accelerateionVec;
    protected override void OnEnter(State prevState)
    {
        Debug.Log("�󒆃_�b�V����Ԃֈڍs");

        Owner.isGravity = false;

        Owner.dushVec = Vector3.zero;

        if (Input.GetKey("up"))
        {
            Owner.dushVec += Owner.transform.forward;
        }
        else if (Input.GetKey("down"))
        {
            Owner.dushVec -= Owner.transform.forward;
        }
        else if (Input.GetKey("right"))
        {
            Owner.dushVec += Owner.transform.right;
        }
        else if (Input.GetKey("left"))
        {
            Owner.dushVec -= Owner.transform.right;
        }

        //�_�b�V���x�N�g����0�Ȃ�
        if (Owner.dushVec == Vector3.zero)
        {
            //�L�����̑O���x�N�g�����擾
            Owner.dushVec = Owner.transform.forward;
        }

        //�����l���쐬
        accelerateionVec = Owner.dushVec * Owner.DushAcceleration;

        //������ݒ�
        Owner.dushVec *= Owner.DushStartSpeed;

        //���Ԃ�ݒ�
        Owner.nowDushTime = Owner.DushTime;
    }
    protected override void OnUpdate()
    {
        //�ڕW�n�_�܂Ŗ��t���[���ړ�
        if (Owner.nowDushTime > 0)
        {
            Owner.nowDushTime -= Time.deltaTime;

            Owner.dushVec += accelerateionVec;
            Owner.moveVec += Owner.dushVec;
        }

        NextStateUpdate();
    }

    protected override void NextStateUpdate()
    {
        //�ړ����I�����Ă�����ҋ@��
        if (Owner.nowDushTime < 0)
        {
            stateMachine.Dispatch((int)Player.Event.Idle);
        }
    }


    protected override void OnExit(State nextState)
    {
        Owner.isGravity = true;
    }

}
