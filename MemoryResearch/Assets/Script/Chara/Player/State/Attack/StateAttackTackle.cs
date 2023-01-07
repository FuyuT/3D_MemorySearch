using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.PlayerState;

/// <summary>
/// �^�b�N��
/// </summary>
public class StateAttackTackle : State
{
    Vector3 tackleVelocity;
    Vector3 accelerateionVec;
    float   nowTackleTime;

    protected override void OnEnter(MyUtil.ActorState<Player> prevState)
    {
        //�A�j���[�V�����̍X�V
        BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Attack_Tackle");

        //�_�b�V���x�N�g�����쐬
        tackleVelocity = BehaviorMoveToInput.GetDushVec(Owner.transform.forward);

        //�����l���쐬
        accelerateionVec = tackleVelocity * Owner.TackleAcceleration;

        //������ݒ�
        tackleVelocity *= Owner.TackleStartSpeed;

        //nowTackleTime
        nowTackleTime = Owner.TackleTime;

        //�U���͐ݒ�
        Owner.SetAttackPower(8);
    }

    protected override void OnUpdate()
    {

        //�^�b�N���̈ړ����Ŗ�����ΏI��
        if (!Owner.animator.GetCurrentAnimatorStateInfo(0).IsName("Tackle_Move_Start")
            && !Owner.animator.GetCurrentAnimatorStateInfo(0).IsName("Tackle_Move"))
        {
            return;
        }

        //�ڕW�n�_�܂Ŗ��t���[���ړ�
        if (nowTackleTime > 0)
        {
            nowTackleTime -= Time.deltaTime;

            tackleVelocity += accelerateionVec;

            //�X�s�[�h�ݒ�
            Actor.IVelocity().SetVelocity(tackleVelocity);
        }

        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //�ړ����I�����Ă�����ҋ@��
        if (nowTackleTime < 0)
        {
            stateMachine.Dispatch((int)Player.State.Idle);
            return;
        }

        //���������Ԃ�I��
        EquipmentSelectNextState();
    }

    protected override void OnExit(MyUtil.ActorState<Player> nextState)
    {
        Owner.nowTackleDelayTime = Owner.TackleDelayTime;

        Owner.SetAttackPower(0);

        Owner.animator.ResetTrigger("Tackle_Move");
    }
}
