using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.PlayerState;

/// <summary>
/// ダッシュ
/// </summary>
public class StateDush : State
{
    /*******************************
    * private
    *******************************/
    Vector3 dushVelocity;
    Vector3 accelerateionVec;
    float   nowDushTime;


    /*******************************
    * protected
    *******************************/
    protected override void OnEnter(MyUtil.ActorState<Player> nextState)
    {
        if(!Owner.isPossibleDush)
        {
            stateMachine.Dispatch((int)Player.State.Idle);
            return;
        }
        else
        {
            Owner.isPossibleDush = false;
        }

        //ダッシュベクトルを作成
        dushVelocity = BehaviorMoveToInput.GetDushVec(Owner.transform.forward);

        //加速値を作成
        accelerateionVec = dushVelocity * Owner.DushAcceleration;

        //初速を設定
        dushVelocity *= Owner.DushStartSpeed;

        //時間を設定
        nowDushTime = Owner.DushTime;

        //重力を使用しない
        Actor.IVelocity().SetUseGravity(false);

    }

    protected override void OnUpdate()
    {
        if(!BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Move_Dush"))
        {
            return;
        }

        if (nowDushTime > 0)
        {
            nowDushTime -= Time.deltaTime;
        }

        SelectNextState();
    }

    protected override void OnFiexdUpdate()
    {
        if (!BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Move_Dush"))
        {
            return;
        }

        if (nowDushTime > 0)
        {
            dushVelocity += accelerateionVec;

            //スピード設定
            Actor.IVelocity().SetVelocity(dushVelocity);
        }
    }

    protected override void SelectNextState()
    {
        //移動が終了していたら待機へ
        if (nowDushTime < 0)
        {
            stateMachine.Dispatch((int)Player.State.Idle);
            return;
        }

        //装備から状態を選択
        EquipmentSelectNextState();
    }
    protected override void OnExit(MyUtil.ActorState<Player> nextState)
    {
        Owner.nowDushDelayTime = Owner.DushDelayTime;

        //重力を使用する
        Actor.IVelocity().SetUseGravity(true);

        //アニメーションのトリガーを解除
        Owner.animator.ResetTrigger("Move_Dush");
    }
}
