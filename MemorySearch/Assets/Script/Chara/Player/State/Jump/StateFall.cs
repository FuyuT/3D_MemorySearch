using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.PlayerState;

/// <summary>
/// �����i�������̉�����rigidbody�ōs���j
/// </summary>
public class StateFall : State
{
    protected override void OnEnter(MyUtil.ActorState<Player> prevState)
    {
        //�A�j���[�V�����̍X�V   todo
        BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Jump_Fall");
        Debug.Log("����");
    }

    protected override void OnUpdate()
    {
        Move();
        SelectNextState();
    }

    void Move()
    {
        //�����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        Vector3 moveAdd = BehaviorMoveToInput.GetInputVec();
        BehaviorMoveToInput.ParseToCameraVec(ref moveAdd);

        Actor.Transform.IVelocity().AddVelocity(moveAdd * Owner.MoveSpeed);
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
