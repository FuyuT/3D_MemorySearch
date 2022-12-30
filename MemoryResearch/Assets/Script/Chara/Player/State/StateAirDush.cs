using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<Player>;

/// <summary>
/// �󒆃_�b�V��
/// </summary>
public class StateAirDush : State
{
    Vector3 accelerateionSpeed;
    protected override void OnEnter(State prevState)
    {
        //�_�b�V���x�N�g�����쐬
        Owner.dushSpeed = BehaviorMoveToInput.GetDushVec(Owner.transform.forward);

        //�����l���쐬
        accelerateionSpeed = Owner.dushSpeed * Owner.DushAcceleration;

        //������ݒ�
        Owner.dushSpeed *= Owner.DushStartSpeed;

        //���Ԃ�ݒ�
        Owner.nowDushTime = Owner.DushTime;
    }
    protected override void OnUpdate()
    {
        if (Owner.nowDushTime > 0)
        {
            Owner.nowDushTime -= Time.deltaTime;
            //����
            Owner.dushSpeed += accelerateionSpeed;
            //�X�s�[�h�ݒ�
            Actor.Transform.IVelocity().SetVelocity(Owner.dushSpeed);
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
    }

}
