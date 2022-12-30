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
        //アニメーションが変更されていなければ処理終了
        if (!BehaviorAnimation.CheckChangedAnimation(ref Owner.animator, "Attack_Tongue"))
        {
            return;
        }

        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //攻撃モーションが終了したら待機へ
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
