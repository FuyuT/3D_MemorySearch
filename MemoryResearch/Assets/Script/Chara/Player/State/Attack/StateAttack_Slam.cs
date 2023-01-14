﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.PlayerState;

/// <summary>
/// 叩きつけ
/// </summary>
public class StateAttack_Slam : State
{
    protected override void OnEnter(MyUtil.ActorState<Player> prevState)
    {
        //その場で停止
        Actor.Transform.IVelocity().InitVelocity();

        //攻撃力設定
        Owner.SetAttackPower(15);
    }

    protected override void OnUpdate()
    {
        //アニメーションを更新
        if(!BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Attack_Slam"))
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
            return;
        }

        //装備から状態を選択
        EquipmentSelectNextState();
    }

    protected override void OnExit(MyUtil.ActorState<Player> nextState)
    {
        Owner.SetAttackPower(0);

        Owner.animator.ResetTrigger("Attack_Slam");
    }
}
