using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.PlayerState;

/// <summary>
/// 移動
/// </summary>
public class StateAttack_Punch : State
{
    protected override void OnEnter(MyUtil.ActorState<Player> prevState)
    {
        //攻撃力設定
        Owner.SetAttackPower(5);
        //その場で停止
        Actor.Transform.IVelocity().InitVelocity();
    }

    protected override void OnUpdate()
    {
        //アニメーションを更新
        if(!BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Attack_Punch"))
        {
            SoundManager.instance.PlaySe(Owner.PunchSE,Owner.transform.position);
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

        Owner.animator.ResetTrigger("Attack_Punch");
    }
}
