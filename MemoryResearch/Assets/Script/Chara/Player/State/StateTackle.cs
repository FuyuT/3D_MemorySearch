using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<Player>;

/// <summary>
/// タックル
/// </summary>
public class StateTackle : State
{
    Vector3 accelerateionVec;
    protected override void OnEnter(State prevState)
    {
        //ダッシュベクトルを作成
        Owner.dushSpeed = BehaviorMoveToInput.GetDushVec(Owner.transform.forward);

        //加速値を作成
        accelerateionVec = Owner.dushSpeed * Owner.DushAcceleration;

        //初速を設定
        Owner.dushSpeed *= Owner.DushStartSpeed;

        //時間を設定
        Owner.nowDushTime = Owner.DushTime;

        //攻撃力設定
    }

    protected override void OnUpdate()
    {
        //目標地点まで毎フレーム移動
        if (Owner.nowDushTime > 0)
        {
            Owner.nowDushTime -= Time.deltaTime;

            Owner.dushSpeed += accelerateionVec;

            //スピード設定
            Actor.IVelocity().SetVelocity(Owner.dushSpeed);
        }

        SelectNextState();
    }

    protected override void SelectNextState()
    {
        //移動が終了していたら待機へ
        if (Owner.nowDushTime < 0)
        {
            stateMachine.Dispatch((int)Player.Event.Idle);
        }
    }
    protected override void OnExit(State nextState)
    {
        Owner.nowDushDelayTime = Owner.DushDelayTime;

        //攻撃力設定
    }
}
