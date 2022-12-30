using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<Player>;

/// <summary>
/// 移動
/// </summary>
public class StateAttack_Punch : State
{
    //攻撃モーションに入っているかの確認
    private bool isAttack;
    protected override void OnEnter(State prevState)
    {
        //todo:攻撃力設定

        Actor.Transform.IVelocity().InitVelocity();

        isAttack = false;
    }

    protected override void OnUpdate()
    {
        SelectNextState();
    }

    protected override void SelectNextState()
    {
        if (Owner.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_1"))
        {
            isAttack = true;
        }

        if (!isAttack) return;
        //アニメーションが待機になっていたら待機状態へ
        if (Owner.animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            stateMachine.Dispatch((int)Player.Event.Idle);
        }
    }

    protected override void OnExit(State nextState)
    {
    }
}
