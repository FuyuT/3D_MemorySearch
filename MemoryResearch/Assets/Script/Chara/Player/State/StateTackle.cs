using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<Player>;

/// <summary>
/// �^�b�N��
/// </summary>
public class StateTackle : State
{
    Vector3 accelerateionVec;
    protected override void OnEnter(State prevState)
    {
        //�_�b�V���x�N�g�����쐬
        Owner.dushSpeed = BehaviorMoveToInput.GetDushVec(Owner.transform.forward);

        //�����l���쐬
        accelerateionVec = Owner.dushSpeed * Owner.DushAcceleration;

        //������ݒ�
        Owner.dushSpeed *= Owner.DushStartSpeed;

        //���Ԃ�ݒ�
        Owner.nowDushTime = Owner.DushTime;

        //�U���͐ݒ�
    }

    protected override void OnUpdate()
    {
        //�ڕW�n�_�܂Ŗ��t���[���ړ�
        if (Owner.nowDushTime > 0)
        {
            Owner.nowDushTime -= Time.deltaTime;

            Owner.dushSpeed += accelerateionVec;

            //�X�s�[�h�ݒ�
            Actor.IVelocity().SetVelocity(Owner.dushSpeed);
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
        Owner.nowDushDelayTime = Owner.DushDelayTime;

        //�U���͐ݒ�
    }
}
