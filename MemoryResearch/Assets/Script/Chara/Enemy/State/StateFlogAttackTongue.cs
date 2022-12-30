using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<EnemyFlog>;

public class StateFlogAttackTongue : State
{
    protected override void OnEnter(State prevState)
    {
        Owner.animator.SetTrigger("Attack_Tongue");
    }

    protected override void OnUpdate()
    {
        //�A�j���[�V�������ύX����Ă��Ȃ���Ώ����I��
        if (!BehaviorAnimation.CheckChangedAnimation(ref Owner.animator, "Attack_Tongue"))
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
    }
}
