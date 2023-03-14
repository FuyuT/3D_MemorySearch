using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.PlayerState;

/// <summary>
/// �W�����v
/// </summary>
public class StateJump : State
{
    /*******************************
    * private
    *******************************/
    bool IsAcceleration;
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

    void JumpInput()
    {
        //�L�[���͂���Ă�����A�W�����v���x������������i�򋗗������΂��j
        if (Input.GetKey(Owner.equipmentMemories[Owner.currentEquipmentNo].GetKeyCode())
            && IsAcceleration && Owner.nowJumpSpeed > 0)
        {
        }
        else
        {
            IsAcceleration = false;
        }
    }
    void Jump()
    {
        if (IsAcceleration)
        {
            Owner.nowJumpSpeed += Owner.JumpAcceleration * Time.timeScale;
        }
        //�W�����v�̑��x������������
        Owner.nowJumpSpeed -= Owner.JumpDecreaseValue * Time.timeScale;
        //�W�����v�x�N�g�����i�[
        Actor.Transform.IVelocity().AddVelocityY(Owner.nowJumpSpeed);
    }
    /*******************************
    * protected
    *******************************/
    protected override void OnEnter(MyUtil.ActorState<Player> prevState)
    {
        //�A�j���[�V�����̍X�V
        BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Jump_Up");
        //�W�����vSE
        SoundManager.instance.PlaySe(Owner.JumpSE,Owner.transform.position);
        IsAcceleration = true;

        //y���̑��x��0�ɂ���
        Actor.Transform.IVelocity().SetVelocityY(0);
        //������ݒ�
        Owner.nowJumpSpeed = Owner.JumpStartSpeed;
    }
    protected override void OnUpdate()
    {
        MoveInput();

        JumpInput();

        SelectNextState();
    }
    protected override void OnFiexdUpdate()
    {
        Move();

        Jump();

        SelectNextState();
    }
    protected override void SelectNextState()
    {
        //�󒆃_�b�V��
        if (Owner.nowDushDelayTime < 0 && Input.GetKey(KeyCode.Z))
        {
            stateMachine.Dispatch((int)Player.State.Move_Dush);
            return;
        }

        //�W�����v���łȂ��A�����n���Ă�����ҋ@��Ԃ�
        if (!IsAcceleration && Owner.isGround)
        {
            stateMachine.Dispatch((int)Player.State.Idle);
            return;
        }

        //���������Ԃ�I��
        EquipmentSelectNextState();
    }
    protected override void OnExit(MyUtil.ActorState<Player> nextState)
    {
        Actor.Transform.IVelocity().SetVelocityY(0);
        Owner.nowJumpSpeed = 0;
        Actor.Transform.IVelocity().InitRigidBodyVelocity();
    }
}
