using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<Player>;

/// <summary>
/// �W�����v
/// </summary>
public class StateJump : State
{
    bool IsAcceleration;

    protected override void OnEnter(State prevState)
    {
        Actor.Transform.IVelocity().SetUseGravity(false);
        Owner.isGround = false;

        IsAcceleration = true;

        //y���̑��x��0�ɂ���
        Actor.Transform.IVelocity().SetVelocityY(0);
        //������ݒ�
        Owner.nowJumpSpeed = Owner.JumpStartSpeed;
    }

    protected override void OnUpdate()
    {
        Move();

        Jump();

        SelectNextState();
    }

    void Move()
    {
        //�����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        Vector3 moveAdd = BehaviorMoveToInput.GetInputVec();
        BehaviorMoveToInput.ParseToCameraVec(ref moveAdd);

        Actor.Transform.IVelocity().AddVelocity(moveAdd * Owner.MoveSpeed);
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
        Actor.Transform.IVelocity().AddVelocityY(Owner.nowJumpSpeed);
    }

    protected override void SelectNextState()
    {
        //�_�u���W�����v
        //todo:�������������Ă��邩�m�F
        if (Owner.CheckPossesionMemory((int)Player.Event.Double_Jump))
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                stateMachine.Dispatch((int)Player.Event.Double_Jump);
            }
        }

        //�󒆃_�b�V��
        if (Owner.nowDushDelayTime < 0 && Input.GetKey(KeyCode.Z))
        {
            stateMachine.Dispatch((int)Player.Event.Air_Dush);
        }

        //�W�����v���łȂ��A�����n���Ă�����ҋ@��Ԃ�
        if (!IsAcceleration && Owner.isGround)
        {
            stateMachine.Dispatch((int)Player.Event.Idle);
        }

    }

    protected override void OnExit(State nextState)
    {
        Actor.Transform.IVelocity().SetVelocityY(0);
        Actor.Transform.IVelocity().SetUseGravity(true);
        Owner.nowJumpSpeed = 0;
        Actor.Transform.IVelocity().InitRigidBodyVelocity();
    }
}
