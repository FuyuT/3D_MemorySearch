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
        Owner.situation = (int)Player.Situation.Dush;

        //�����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        Owner.dushVec = Vector3.zero;
        if (Input.GetKey("up"))
        {
            Owner.dushVec += new Vector3(0, 0, 1);
        }
        if (Input.GetKey("down"))
        {
            Owner.dushVec += new Vector3(0, 0, -1);
        }
        if (Input.GetKey("right"))
        {
            Owner.dushVec += new Vector3(1, 0, 0);
        }
        if (Input.GetKey("left"))
        {
            Owner.dushVec += new Vector3(-1, 0, 0);
        }

        //�_�b�V���x�N�g����0�Ȃ�
        if (Owner.dushVec == Vector3.zero)
        {
            //�L�����̑O���x�N�g���P�ʃx�N�g�����擾
            Owner.dushVec = Owner.transform.forward;
        }
        else
        {
            //�P�ʃx�N�g�����쐬
            Owner.dushVec = Camera.main.transform.forward * Owner.dushVec.z + Camera.main.transform.right * Owner.dushVec.x;
            Owner.dushVec.y = 0;
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


    protected override void OnExit(State nextState)
    {
        Owner.situation = (int)Player.Situation.None;
        Owner.nowDushDelayTime = Owner.DushDelayTime;
    }

}
