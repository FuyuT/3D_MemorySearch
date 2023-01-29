using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<EnemyFlog>;

public class StateFlogIdle : State
{ 
    protected override void OnEnter(State prevState)
    {
        //�A�j���[�V�����̍X�V������O�ɁA�J�ڂł���X�e�[�g�����邩�m���߂�
        SelectNextState();

        //�A�j���[�V�����̍X�V
        BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Idle_1");
    }

    protected override void OnUpdate()
    {
        Actor.IVelocity().InitVelocity();

        //�T�m�͈͓��Ƀ^�[�Q�b�g������΁A������̕���������
        if (Owner.searchRange.InTarget)
        {
            RotateToTarget();
        }

        SelectNextState();
    }

    void RotateToTarget()
    {
        //�^�[�Q�b�g�ւ̃x�N�g�����v�Z
        Vector3 vec = Vector3.Normalize(Player.readPlayer.GetPos() - Actor.IPosition().GetPosition()) * Owner.moveSpeed;

        Actor.Transform.RotateUpdateToVec(vec, Owner.rotateSpeed * 3);
    }

    protected override void SelectNextState()
    {
        if (SelectAttack()) return;

        if (SelectJump()) return;
    }

    bool SelectAttack()
    {
        //�T�m�͈͓��Ƀ^�[�Q�b�g�����Ȃ��Ȃ�I��
        if (!Owner.searchRange.InTarget) return false;

        //�ˌ��̃f�B���C���I����Ă���Ύˌ���
        if (Owner.projectileDelay > Owner.projectileDelayMax)
        {
            stateMachine.Dispatch((int)EnemyFlog.State.Attack_Shot);
            return true;
        }

        //�U���͈͂ɓG������ΐ�U����
        if (Owner.attackRange.InTarget)
        {
            stateMachine.Dispatch((int)EnemyFlog.State.Attack_Tongue);
            return true;
        }

        return false;
    }

    bool SelectJump()
    {
        //�T�m�͈͓��Ƀ^�[�Q�b�g�����Ȃ��Ȃ�I��
        if (!Owner.searchRange.InTarget) return false;

        //�W�����v�̃f�B���C���I����Ă���΃W�����v��
        if (Owner.delayJumpTime >= Owner.delayJumpTimeMax)
        {
            stateMachine.Dispatch((int)EnemyFlog.State.Jump);
            return true;
        }

        return false;
    }

    protected override void OnExit(State prevState)
    {
        Owner.animator.ResetTrigger("Idle_1");
    }
}