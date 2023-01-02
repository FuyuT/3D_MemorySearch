using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<Player>;

/// <summary>
/// 移動
/// </summary>
public class StateAttack_Punch : State
{
    protected override void OnEnter(State prevState)
    {
        //todo:攻撃力設定
        Owner.SetAttackPower(5);
        //その場で停止
        Actor.Transform.IVelocity().InitVelocity();
    }

    protected override void OnUpdate()
    {
        //アニメーションを更新
        if(!BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Attack_Punch"))
        {
            //アニメーションが変わっていなければ終了
            return;
        }

        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //アニメーションが終了していたら待機へ
        if (Owner.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            stateMachine.Dispatch((int)Player.State.Idle);
        }
    }

    protected override void OnExit(State nextState)
    {
        Owner.SetAttackPower(0);
    }
}
