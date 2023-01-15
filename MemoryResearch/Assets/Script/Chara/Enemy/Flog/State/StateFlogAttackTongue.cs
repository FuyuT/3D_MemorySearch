using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<EnemyFlog>;

public class StateFlogAttackTongue : State
{
    protected override void OnEnter(State prevState)
    {
        //�A�j���[�V�����̍X�V
        BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Attack_Tongue");
        SoundManager.instance.PlaySe(Owner.AttackSE,Owner.transform.position);


        //�U���͐ݒ�
        Owner.SetAttackPower(2);

        Owner.SetSubMemory(MemoryType.Punch);
    }

    protected override void OnUpdate()
    {
        //�A�j���[�V�������ύX����Ă��Ȃ���ΕύX
        if (!BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Attack_Tongue"))
        {
            return;
        }

        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //�U�����[�V�������I��������ҋ@��
        if (Owner.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            stateMachine.Dispatch((int)EnemyFlog.State.Idle);
            return;
        }
    }

    protected override void OnExit(State nextState)
    {
        //�U���͏�����
        Owner.InitAttackPower();
        Owner.InitSubMemory();

        Owner.animator.ResetTrigger("Attack_Tongue");
        SoundManager.instance.StopSe(Owner.AttackSE);
    }
}
