using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<EnemyFlog>;

public class StateFlogAttackTongue : State
{
    protected override void OnEnter(State prevState)
    {
        //アニメーションの更新
        BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Attack_Tongue");
        SoundManager.instance.PlaySe(Owner.AttackSE,Owner.transform.position);


        //攻撃力設定
        Owner.SetAttackPower(2);

        Owner.SetSubMemory(MemoryType.Punch);
    }

    protected override void OnUpdate()
    {
        //アニメーションが変更されていなければ変更
        if (!BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Attack_Tongue"))
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
        //攻撃力初期化
        Owner.InitAttackPower();
        Owner.InitSubMemory();

        Owner.animator.ResetTrigger("Attack_Tongue");
        SoundManager.instance.StopSe(Owner.AttackSE);
    }
}
