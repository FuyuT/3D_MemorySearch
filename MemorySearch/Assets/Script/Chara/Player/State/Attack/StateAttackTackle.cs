using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.PlayerState;

/// <summary>
/// �^�b�N��
/// </summary>
public class StateAttackTackle : State
{
    /*******************************
    * private
    *******************************/
    Vector3 tackleVelocity;
    Vector3 accelerateionVec;
    float   nowTackleTime;
    bool    isMove;

    /*******************************
    * protected
    *******************************/
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
        Owner.SetAttackPower(2);

        isMove = false;

        //SE
        SoundManager.instance.PlaySe(Owner.TackleSE, Owner.transform.position);
    }
    protected override void OnUpdate()
    {
        //�^�b�N���̈ړ����Ŗ�����ΏI��
        if (!Owner.animator.GetCurrentAnimatorStateInfo(0).IsName("Tackle_Move_Start")
            && !Owner.animator.GetCurrentAnimatorStateInfo(0).IsName("Tackle_Move"))
        {
            return;
        }
        else if(BehaviorAnimation.IsName(ref Owner.animator, "Tackle_Move_Start"))
        {
            if (!isMove)
            {
                Owner.effectWind.Play();
                isMove = true;
            }
        }

        //�ڕW�n�_�܂Ŗ��t���[���ړ�
        if (nowTackleTime > 0)
        {
            nowTackleTime -= Time.deltaTime;

            Owner.effectWind.transform.position = Owner.transform.position;
        }

        SelectNextState();
    }
    protected override void OnFiexdUpdate()
    {
        if (!isMove) return;

        if (nowTackleTime > 0)
        {
            tackleVelocity += accelerateionVec;

            //�X�s�[�h�ݒ�
            Actor.IVelocity().SetVelocity(tackleVelocity);
        }
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

        Owner.animator.ResetTrigger("Attack_Tackle");

        Owner.effectWind.Stop();

        SoundManager.instance.StopSe(Owner.TackleSE);
    }
}
