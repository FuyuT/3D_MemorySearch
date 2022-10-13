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
        //�����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        Vector3 inputVector = Vector3.zero;
        if (Input.GetKey("up"))
        {
            inputVector += new Vector3(0, 0, 1);
        }
        if (Input.GetKey("down"))
        {
            inputVector += new Vector3(0, 0, -1);
        }
        if (Input.GetKey("right"))
        {
            inputVector += new Vector3(1, 0, 0);
        }
        if (Input.GetKey("left"))
        {
            inputVector += new Vector3(-1, 0, 0);
        }

        //���͂��Ȃ��ꍇ
        if (inputVector == Vector3.zero)
        {
            //�L�����̑O���x�N�g����i�s�����ɐݒ�
            inputVector = Owner.transform.forward;
        }

        Vector3 moveForward = Camera.main.transform.forward * inputVector.z + Camera.main.transform.right * inputVector.x;
        moveForward.y = 0;
        Owner.dushVec = moveForward;

        //�����l���쐬
        accelerateionVec = moveForward * Owner.DushAcceleration;

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
