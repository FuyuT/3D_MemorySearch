using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.PlayerState;

/// <summary>
/// �ړ�
/// </summary>
public class StateMoveWalk : State
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
    }

    protected override void OnUpdate()
    {
        //�A�j���[�V�����̍X�V
        BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Move_Walk");

        MoveInput();

        SelectNextState();
    }

    protected override void OnFiexdUpdate()
    {
        Move();
    }

    protected override void SelectNextState()
    {
        //�������[�V�����łȂ���ΏI��
        if (!BehaviorAnimation.IsName(ref Owner.animator, "Move_Walk")) return;

        //����
        if (Input.GetKey(KeyCode.LeftShift))
        {
            stateMachine.Dispatch((int)Player.State.Move_Run);
            return;
        }

        //�ҋ@���
        if (moveAdd == Vector3.zero)
        {
            stateMachine.Dispatch((int)Player.State.Idle);
            return;
        }

        //���������Ԃ�I��
        EquipmentSelectNextState();
    }

    protected override void OnExit(MyUtil.ActorState<Player> nextState)
    {
        Owner.animator.ResetTrigger("Move_Walk");
    }
}
