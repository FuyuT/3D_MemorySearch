using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = State<Player>;

/// <summary>
/// 空中ダッシュ
/// </summary>
public class StateAirDush : State
{
    Vector3 accelerateionVec;
    protected override void OnEnter(State prevState)
    {
        Debug.Log("空中ダッシュ状態へ移行");

        Owner.isGravity = false;

        Owner.dushVec = Vector3.zero;

        if (Input.GetKey("up"))
        {
            Owner.dushVec += Owner.transform.forward;
        }
        else if (Input.GetKey("down"))
        {
            Owner.dushVec -= Owner.transform.forward;
        }
        else if (Input.GetKey("right"))
        {
            Owner.dushVec += Owner.transform.right;
        }
        else if (Input.GetKey("left"))
        {
            Owner.dushVec -= Owner.transform.right;
        }

        //ダッシュベクトルが0なら
        if (Owner.dushVec == Vector3.zero)
        {
            //キャラの前方ベクトルを取得
            Owner.dushVec = Owner.transform.forward;
        }

        //加速値を作成
        accelerateionVec = Owner.dushVec * Owner.DushAcceleration;

        //初速を設定
        Owner.dushVec *= Owner.DushStartSpeed;

        //時間を設定
        Owner.nowDushTime = Owner.DushTime;
    }
    protected override void OnUpdate()
    {
        //目標地点まで毎フレーム移動
        if (Owner.nowDushTime > 0)
        {
            Owner.nowDushTime -= Time.deltaTime;

            Owner.dushVec += accelerateionVec;
            Owner.moveVec += Owner.dushVec;
        }

        NextStateUpdate();
    }

    protected override void NextStateUpdate()
    {
        //移動が終了していたら待機へ
        if (Owner.nowDushTime < 0)
        {
            stateMachine.Dispatch((int)Player.Event.Idle);
        }
    }


    protected override void OnExit(State nextState)
    {
        Owner.isGravity = true;
    }

}
