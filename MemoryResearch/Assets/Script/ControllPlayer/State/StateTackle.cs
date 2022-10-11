using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Player>;

/// <summary>
/// �^�b�N��
/// </summary>
public class StateTackle : State
{
    Vector3 accelerateionVec;
    protected override void OnEnter(State prevState)
    {
        Debug.Log("�^�b�N����Ԃֈڍs");

        Owner.dushVec = Vector3.zero;

        //���͂ɂ���Đi�s������ύX
        if (Input.GetKey("up"))
        {
            Owner.dushVec += Owner.transform.forward;
        }
        if (Input.GetKey("down"))
        {
            Owner.dushVec -= Owner.transform.forward;
        }
        if (Input.GetKey("right"))
        {
            Owner.dushVec += Owner.transform.right;
        }
        if (Input.GetKey("left"))
        {
            Owner.dushVec -= Owner.transform.right;
        }

        //���͂��Ȃ��ꍇ
        if (Owner.dushVec == Vector3.zero)
        {
            //�L�����̑O���x�N�g����i�s�����ɐݒ�
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

        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //�ړ����I�����Ă�����ҋ@��
        if (Owner.nowDushTime < 0)
        {
            stateMachine.Dispatch((int)Player.Event.Idle);
        }
    }

}
