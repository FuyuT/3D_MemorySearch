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
    * protected
    *******************************/
    protected override void OnEnter(MyUtil.ActorState<Player> prevState)
    {
    }

    protected override void OnUpdate()
    {
        //�A�j���[�V�����̍X�V
        BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Move_Walk");

        Move();

        SelectNextState();
    }

    void Move()
    {       
        //�����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        Vector3 moveAdd = BehaviorMoveToInput.GetInputVec();
        BehaviorMoveToInput.ParseToCameraVec(ref moveAdd);

        //�ړ��X�s�[�h���|����
        moveAdd *= Owner.MoveSpeed;

        //����SE�𗬂�
        SoundManager.instance.PlaySe(Owner.WalkSE);

        Actor.Transform.IVelocity().AddVelocity(moveAdd);
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
        if (Actor.Transform.IVelocity().GetVelocity() == Vector3.zero)
        {
            stateMachine.Dispatch((int)Player.State.Idle);
            return;
        }

        //���������Ԃ�I��
        EquipmentSelectNextState();
    }

    protected override void OnExit(MyUtil.ActorState<Player> nextState)
    {
        Owner.animator.ResetTrigger("Idle");
    }
}
