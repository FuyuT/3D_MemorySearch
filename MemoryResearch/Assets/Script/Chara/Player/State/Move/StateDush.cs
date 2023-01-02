using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<Player>;

/// <summary>
/// �_�b�V��
/// </summary>
public class StateDush : State
{
    Vector3 dushVelocity;
    Vector3 accelerateionVec;
    float   nowDushTime;

    protected override void OnEnter(State prevState)
    {
        //�_�b�V���x�N�g�����쐬
        dushVelocity = BehaviorMoveToInput.GetDushVec(Owner.transform.forward);

        //�����l���쐬
        accelerateionVec = dushVelocity * Owner.DushAcceleration;

        //������ݒ�
        dushVelocity *= Owner.DushStartSpeed;

        //���Ԃ�ݒ�
        nowDushTime = Owner.DushTime;

        //�U���͐ݒ�

        //�d�͂��g�p���Ȃ�
        Actor.IVelocity().SetUseGravity(false);
    }

    protected override void OnUpdate()
    {
        //�A�j���[�V�����̍X�V
        BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Move_Dush");

        //�ڕW�n�_�܂Ŗ��t���[���ړ�
        if (nowDushTime > 0)
        {
            nowDushTime -= Time.deltaTime;

            dushVelocity += accelerateionVec;

            //�X�s�[�h�ݒ�
            Actor.IVelocity().SetVelocity(dushVelocity);
        }

        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //�ړ����I�����Ă�����ҋ@��
        if (nowDushTime < 0)
        {
            stateMachine.Dispatch((int)Player.State.Idle);
        }
    }
    protected override void OnExit(State nextState)
    {
        Owner.nowDushDelayTime = Owner.DushDelayTime;

        //�d�͂��g�p����
        Actor.IVelocity().SetUseGravity(true);
    }
}
