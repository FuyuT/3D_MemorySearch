using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<Player>;

/// <summary>
/// �_�u���W�����v
/// </summary>
public class StateDoubleJump : State
{
    bool IsAcceleration;

    protected override void OnEnter(State prevState)
    {
        //�A�j���[�V�����̍X�V
        BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Jump_DowbleJump");

        Owner.isGround = false;

        IsAcceleration = true;

        //y���̑��x��0�ɂ���
        Actor.IVelocity().SetVelocityY(0);
        //������ݒ�
        Owner.nowJumpSpeed = Owner.JumpStartSpeed;
    }
    protected override void OnUpdate()
    {
        Move();

        Jump();

        SelectNextState();
    }

    //�ړ�
    void Move()
    {
        //�����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        Vector3 moveAdd = BehaviorMoveToInput.GetInputVec();
        BehaviorMoveToInput.ParseToCameraVec(ref moveAdd);

        Actor.IVelocity().AddVelocity(moveAdd * Owner.MoveSpeed);
    }

    //�W�����v
    void Jump()
    {
        //�L�[���͂���Ă�����A�W�����v���x������������i�򋗗������΂��j
        if (Input.GetKey(KeyCode.C) && IsAcceleration)
        {
            Owner.nowJumpSpeed += Owner.JumpAcceleration;
        }
        else
        {
            IsAcceleration = false;
        }

        //�W�����v�̑��x������������
        Owner.nowJumpSpeed -= Owner.JumpDecreaseValue;

        //�W�����v�x�N�g�����i�[
        Actor.IVelocity().AddVelocityY(Owner.nowJumpSpeed);
    }

    protected override void SelectNextState()
    {
        //�󒆃_�b�V��
        if (Owner.nowDushDelayTime < 0 && Input.GetKey(KeyCode.Z))
        {
            stateMachine.Dispatch((int)Player.State.Move_Dush);
        }

        //�W�����v���łȂ��A�����n���Ă�����ҋ@��Ԃ�
        if (!IsAcceleration && Owner.isGround)
        {
            stateMachine.Dispatch((int)Player.State.Idle);
        }
    }

    protected override void OnExit(State nextState)
    {
        Actor.IVelocity().SetVelocityY(0);
        Owner.nowJumpSpeed = 0;
        Actor.IVelocity().InitRigidBodyVelocity();
    }
}
