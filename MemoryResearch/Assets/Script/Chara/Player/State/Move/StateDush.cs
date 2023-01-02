using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<Player>;

/// <summary>
/// ダッシュ
/// </summary>
public class StateDush : State
{
    Vector3 dushVelocity;
    Vector3 accelerateionVec;
    float   nowDushTime;

    protected override void OnEnter(State prevState)
    {
        //ダッシュベクトルを作成
        dushVelocity = BehaviorMoveToInput.GetDushVec(Owner.transform.forward);

        //加速値を作成
        accelerateionVec = dushVelocity * Owner.DushAcceleration;

        //初速を設定
        dushVelocity *= Owner.DushStartSpeed;

        //時間を設定
        nowDushTime = Owner.DushTime;

        //攻撃力設定

        //重力を使用しない
        Actor.IVelocity().SetUseGravity(false);
    }

    protected override void OnUpdate()
    {
        //アニメーションの更新
        BehaviorAnimation.UpdateTrigger(ref Owner.animator, "Move_Dush");

        //目標地点まで毎フレーム移動
        if (nowDushTime > 0)
        {
            nowDushTime -= Time.deltaTime;

            dushVelocity += accelerateionVec;

            //スピード設定
            Actor.IVelocity().SetVelocity(dushVelocity);
        }

        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //移動が終了していたら待機へ
        if (nowDushTime < 0)
        {
            stateMachine.Dispatch((int)Player.State.Idle);
        }
    }
    protected override void OnExit(State nextState)
    {
        Owner.nowDushDelayTime = Owner.DushDelayTime;

        //重力を使用する
        Actor.IVelocity().SetUseGravity(true);
    }
}
