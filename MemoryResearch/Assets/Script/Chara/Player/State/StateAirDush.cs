using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = MyUtil.ActorState<Player>;

/// <summary>
/// 空中ダッシュ
/// </summary>
public class StateAirDush : State
{
    Vector3 accelerateionSpeed;
    protected override void OnEnter(State prevState)
    {
        //ダッシュベクトルを作成
        Owner.dushSpeed = BehaviorMoveToInput.GetDushVec(Owner.transform.forward);

        //加速値を作成
        accelerateionSpeed = Owner.dushSpeed * Owner.DushAcceleration;

        //初速を設定
        Owner.dushSpeed *= Owner.DushStartSpeed;

        //時間を設定
        Owner.nowDushTime = Owner.DushTime;
    }
    protected override void OnUpdate()
    {
        if (Owner.nowDushTime > 0)
        {
            Owner.nowDushTime -= Time.deltaTime;
            //加速
            Owner.dushSpeed += accelerateionSpeed;
            //スピード設定
            Actor.Transform.IVelocity().SetVelocity(Owner.dushSpeed);
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
    }

}
