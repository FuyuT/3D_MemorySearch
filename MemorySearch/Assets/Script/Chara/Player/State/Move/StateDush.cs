using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.PlayerState;

/// <summary>
/// �_�b�V��
/// </summary>
public class StateDush : State
{
    /*******************************
    * private
    *******************************/
    Vector3 dushVelocity;
    Vector3 accelerateionVec;
    float   nowDushTime;


    /*******************************
    * protected
    *******************************/
    protected override void OnEnter(MyUtil.ActorState<Player> nextState)
    {
        if(!Owner.isPossibleDush)
        {
            stateMachine.Dispatch((int)Player.State.Idle);
            return;
        }
        else
        {
            Owner.isPossibleDush = false;
        }

        //�_�b�V���x�N�g�����쐬
        dushVelocity = BehaviorMoveToInput.GetDushVec(Owner.transform.forward);

        //�����l���쐬
        accelerateionVec = dushVelocity * Owner.DushAcceleration;

        //������ݒ�
        dushVelocity *= Owner.DushStartSpeed;

        //���Ԃ�ݒ�
        nowDushTime = Owner.DushTime;

        //�d�͂��g�p���Ȃ�
        Actor.IVelocity().SetUseGravity(false);

    }

    protected override void OnUpdate()
    {
        if(!BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Move_Dush"))
        {
            return;
        }

        if (nowDushTime > 0)
        {
            nowDushTime -= Time.deltaTime;
        }

        SelectNextState();
    }

    protected override void OnFiexdUpdate()
    {
        if (!BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Move_Dush"))
        {
            return;
        }

        if (nowDushTime > 0)
        {
            dushVelocity += accelerateionVec;

            //�X�s�[�h�ݒ�
            Actor.IVelocity().SetVelocity(dushVelocity);
        }
    }

    protected override void SelectNextState()
    {
        //�ړ����I�����Ă�����ҋ@��
        if (nowDushTime < 0)
        {
            stateMachine.Dispatch((int)Player.State.Idle);
            return;
        }

        //���������Ԃ�I��
        EquipmentSelectNextState();
    }
    protected override void OnExit(MyUtil.ActorState<Player> nextState)
    {
        Owner.nowDushDelayTime = Owner.DushDelayTime;

        //�d�͂��g�p����
        Actor.IVelocity().SetUseGravity(true);

        //�A�j���[�V�����̃g���K�[������
        Owner.animator.ResetTrigger("Move_Dush");
    }
}
