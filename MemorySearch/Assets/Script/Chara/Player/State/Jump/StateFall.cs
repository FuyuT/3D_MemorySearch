using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.PlayerState;

/// <summary>
/// �����i�������̉�����rigidbody�ōs���j
/// </summary>
public class StateFall : State
{
    /*******************************
    * private
    *******************************/
    Vector3 moveAdd;

    void MoveInput()
    {
        //�����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        moveAdd = BehaviorMoveToInput.GetInputVec();
        BehaviorMoveToInput.ParseToCameraVec(ref moveAdd);
    }
    void Move()
    {
        Actor.Transform.IVelocity().AddVelocity(moveAdd * Owner.MoveSpeed);
    }
    /*******************************
    * protected
    *******************************/
    protected override void OnEnter(MyUtil.ActorState<Player> prevState)
    {
        //�A�j���[�V�����̍X�V
        BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Jump_Fall");
    }

    protected override void OnUpdate()
    {
        MoveInput();
        SelectNextState();
    }

    protected override void OnFiexdUpdate()
    {
        Move();
    }

    protected override void SelectNextState()
    {
        if (Owner.isGround || Actor.IVelocity().GetState() != MyUtil.VelocityState.isDown)
        {
            stateMachine.Dispatch((int)Player.State.Idle);
            return;
        }

        //���������Ԃ�I��
        EquipmentSelectNextState();
    }

    protected override void OnExit(MyUtil.ActorState<Player> nextState)
    {
        Owner.animator.ResetTrigger("Jump_Fall");

        Actor.Transform.IVelocity().SetVelocityY(0);
        Actor.Transform.IVelocity().InitRigidBodyVelocity();
    }
}
