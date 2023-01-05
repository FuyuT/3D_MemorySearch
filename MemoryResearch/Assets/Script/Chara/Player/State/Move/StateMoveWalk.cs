using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<Player>;


/// <summary>
/// �ړ�
/// </summary>
public class StateMoveWalk : State
{
    /*******************************
    * protected
    *******************************/
    protected override void OnEnter(State prevState)
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

        Actor.Transform.IVelocity().AddVelocity(moveAdd);
    }

    protected override void SelectNextState()
    {
        //�������[�V�����łȂ���ΏI��
        if (!BehaviorAnimation.IsName(ref Owner.animator, "Move_Walk")) return;

        //�_�b�V���n
        if (Owner.nowDushDelayTime < 0 && Input.GetKey(KeyCode.Z))
        {
            if(Owner.isGround)
            {
                //�^�b�N��
                stateMachine.Dispatch((int)Player.State.Attack_Tackle);
            }
            else
            {
                //�󒆃_�b�V��
                stateMachine.Dispatch((int)Player.State.Move_Dush);
            }
        }

        //�W�����v
        if (Owner.isGround) //���V���Ă��Ȃ���
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (Owner.CheckPossesionMemory((int)Player.State.Jump) || Owner.CheckPossesionMemory((int)Player.State.Double_Jump)) //�������������Ă��邩�m�F
                {
                    stateMachine.Dispatch((int)Player.State.Jump);
                    return;
                }
            }
        }

        //�p���`
        if (Input.GetMouseButtonDown(0) && !Owner.ChapterCamera.activeSelf)
        {
            stateMachine.Dispatch((int)Player.State.Attack_Punch);
            return;
        }

        //�ҋ@���
        if (Actor.Transform.IVelocity().GetVelocity() == Vector3.zero)
        {
            stateMachine.Dispatch((int)Player.State.Idle);
            return;
        }

        //����
        if(Input.GetKey(KeyCode.LeftShift))
        {
            stateMachine.Dispatch((int)Player.State.Move_Run);
            return;
        }

        //�^�b�N��
        if (Input.GetKey(KeyCode.K))
        {
            stateMachine.Dispatch((int)Player.State.Attack_Tackle);
            return;
        }

    }
}
